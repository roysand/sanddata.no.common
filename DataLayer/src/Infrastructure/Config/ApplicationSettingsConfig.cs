using System.Reflection;
using DataLayer.Application.Interface;

namespace DataLayer.Infrastructure.Config;

public class ApplicationSettingsConfig : IApplicationSettingsConfig
{
    private readonly string ConfigParentKey = "ApplicationSettings";

    private readonly IConfig _config;

    public ApplicationSettingsConfig(IConfig config)
    {
        _config = config;
    }
    public string DbConnectionString()
    {
        return _config.GetConfigValue<string>($"{ConfigParentKey}:{MethodBase.GetCurrentMethod()!.Name}");

    }

    public bool EnableSensitiveDataLogging()
    {
        return _config.GetConfigValue<bool>($"{ConfigParentKey}:{MethodBase.GetCurrentMethod()!.Name}", false);
    }
}