namespace Application.Common.Interface;

public partial interface IConfig
{
    IApplicationSettingsConfig ApplicationSettingsConfig { get;  }
    T GetConfigValue<T>(string configKey, bool mustExist = true);
}