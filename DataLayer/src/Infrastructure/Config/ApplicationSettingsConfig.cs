using System.Reflection;
using DataLayer.Application.Interface;

namespace DataLayer.Infrastructure.Config;

public class ApplicationSettingsConfig : IApplicationSettingsConfig
{
    private readonly string _configParentKey = "ApplicationSettings";

    private readonly IConfig _config;

    public ApplicationSettingsConfig(IConfig config)
    {
        _config = config;
    }
    public string DbConnectionString()
    {
        return _config.GetConfigValue<string>($"{_configParentKey}:{MethodBase.GetCurrentMethod()!.Name}");

    }

    public bool EnableSensitiveDataLogging()
    {
        return _config.GetConfigValue<bool>($"{_configParentKey}:{MethodBase.GetCurrentMethod()!.Name}", false);
    }
    
    public string Location()
    {
        return _config.GetConfigValue<string>($"{_configParentKey}:{MethodBase.GetCurrentMethod()!.Name}");
    }
}