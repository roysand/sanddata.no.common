using System.Reflection;
using DataLayer.Application.Interface;

namespace DataLayer.Infrastructure.Config;

public class MqttConfig : IMqttConfig
{
    private readonly IConfig _config;
    private readonly string _configParentKey = "MQTT";

    public MqttConfig(IConfig config)
    {
        _config = config;
    }
    
    public string MQTTTopic()
    {
        return _config.GetConfigValue<string>($"{_configParentKey}:{MethodBase.GetCurrentMethod()!.Name}", true);
    }

    public string MQTTServerURI()
    {
        return _config.GetConfigValue<string>($"{_configParentKey}:{MethodBase.GetCurrentMethod()!.Name}", true);
    }

    public int MQTTServerPortNr()
    {
        return _config.GetConfigValue<int>($"{_configParentKey}:{MethodBase.GetCurrentMethod()!.Name}", true);
    }

    public string MQTTUserName()
    {
        return _config.GetConfigValue<string>($"{_configParentKey}:{MethodBase.GetCurrentMethod()!.Name}", true);
    }

    public string MQTTUserPassword()
    {
        return _config.GetConfigValue<string>($"{_configParentKey}:{MethodBase.GetCurrentMethod()!.Name}", true);
    }

    public bool MQTTUseTLS()
    {
        return _config.GetConfigValue<bool>($"{_configParentKey}:{MethodBase.GetCurrentMethod()!.Name}");
    }

    public int MQTTDelayCountBeforeSaveToDb()
    {
        return _config.GetConfigValue<int>($"{_configParentKey}:{MethodBase.GetCurrentMethod()!.Name}", true);
    }
}