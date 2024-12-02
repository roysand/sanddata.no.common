namespace DataLayer.Application.Interface;

public interface IMqttConfig
{
    string MQTTTopic();
    string MQTTServerURI();
    int MQTTServerPortNr();
    string MQTTUserName();
    string MQTTUserPassword();
    bool MQTTUseTLS();
    int MQTTDelayCountBeforeSaveToDb();
}