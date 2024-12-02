namespace DataLayer.Application.Interface;

public interface IConfig
{
    T GetConfigValue<T>(string configKey, bool mustExist = true);
    IApplicationSettingsConfig ApplicationSettingsConfig { get;  }   
    IMqttConfig MqttConfig { get;  }
}