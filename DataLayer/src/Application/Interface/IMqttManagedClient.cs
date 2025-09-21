using DataLayer.Domain.Models.AmsLeser;

namespace DataLayer.Infrastructure.Interface;

public interface IMqttManagedClient
{
    Task SubscribeAsync(string topic, int qos = 1);
    Task UnSubscribeAsync(string topic);
    Task Disconnect();
    Task Save(AMSReaderData data);
}