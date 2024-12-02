using System.Security.Authentication;
using DataLayer.Application.Interface;
using DataLayer.Domain.Common.Enum;
using DataLayer.Domain.Entities;
using DataLayer.Domain.Models.AmsLeser;
using DataLayer.Infrastructure.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using MQTTnet.Extensions.ManagedClient;
using Newtonsoft.Json;

namespace DataLayer.Infrastructure.Clients
{
    public abstract class MqttManagedClient : IMqttManagedClient
    {
        private readonly IConfig _config;
        private readonly ILogger<MqttManagedClient> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _clientId = System.Net.Dns.GetHostName() + "-" + Guid.NewGuid().ToString().Substring(0,8);
        private IManagedMqttClient? _client;
        private readonly MqttFactory _factory;
        public MqttManagedClient(IConfig config, ILogger<MqttManagedClient> logger, IConfiguration configuration)
        {
            _config = config;
            _logger = logger;
            _configuration = configuration;
            _factory = new MqttFactory();
        }
        private async Task ConnectAsync()
        {
            var messageBuilder = new MqttClientOptionsBuilder()
                .WithClientId(_clientId)
                .WithCredentials(_config.MqttConfig.MQTTUserName(), _config.MqttConfig.MQTTUserPassword())
                .WithTcpServer(_config.MqttConfig.MQTTServerURI(), _config.MqttConfig.MQTTServerPortNr())
                .WithCleanSession();
            
            // Workaround - original code does not work!! _config.MqttConfig.MQTTUseTLS(); 
            var options = _configuration.GetValue<bool>("MQTT:MQTTUseTLS")
                ? messageBuilder
                    .WithTlsOptions(o => o.WithSslProtocols(SslProtocols.Tls13))
                    .Build()
                : messageBuilder
                    .Build();

            var managedOptions = new ManagedMqttClientOptionsBuilder()
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                .WithClientOptions(options)
                .Build();

            _client = _factory.CreateManagedMqttClient();
            if (_client == null)
            {
                _logger.LogError("Failed to connect to MQTTnet client");
                throw new InvalidOperationException("Failed to connect to MQTTnet client");
            }

            _client.ConnectedAsync += e =>
            {
                _logger.LogInformation("Application is connected to broker");
                return Task.CompletedTask;
            };

            _client.DisconnectedAsync += e =>
            {
                _logger.LogInformation("Appliction is disconnected from broker");
                return Task.CompletedTask;
            };

            _client.ApplicationMessageReceivedAsync += async e =>
            {
                try
                {
                    var amsReaderData =
                        JsonConvert.DeserializeObject<AMSReaderData>(System.Text.Encoding.Default.GetString(e.ApplicationMessage.PayloadSegment));

                    if (amsReaderData != null)
                    {
                        await Save(amsReaderData);
                    }
                    else
                    {
                        _logger.LogWarning("Error parsing data from AMS Reader, not save data to db!");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    //throw;
                }
            };
            
            await _client.StartAsync(managedOptions);
        }

        public async Task SubscribeAsync(string topic, int qos = 1)
        {
            if (_client == null || !_client.IsConnected)
            {
                await ConnectAsync();
            }
            
            var mqttSubscribeOptions = _factory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(f => { f.WithTopic(topic); })
                .Build();

            await _client.SubscribeAsync(topic, (MqttQualityOfServiceLevel)qos);
        }

        public async Task UnSubscribeAsync(string topic)
        {
            await _client.UnsubscribeAsync(topic);
        }

        public async Task Disconnect()
        {
            await _client?.StopAsync()!;
        }

        public virtual async Task Save(AMSReaderData data)
        {
            Console.WriteLine(
                $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.zzz}{JsonConvert.SerializeObject(data)}");
                
            var detail = new Detail()
            {
                MeasurementId = Guid.NewGuid(),
                TimeStamp = data.TimeStamp,
                ObisCode = "1-0:1.7.0.255",
                Name = "Active power",
                ValueStr = "Active power",
                ObisCodeId = ObisCodeId.PowerUsed,
                Unit = "kW",
                Location = _config.ApplicationSettingsConfig.Location(),
                ValueNum = (decimal)data.Data.P / 1000
            };
        
            await Task.CompletedTask;
        }
    }
}