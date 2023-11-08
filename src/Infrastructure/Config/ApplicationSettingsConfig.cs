using System.Reflection;
using Application.Common.Interface;

namespace Infrastructure.Config;

public class ApplicationSettingsConfig :  IApplicationSettingsConfig
{
    private readonly IConfig _config;
    private readonly string ConfigParentKey = "ApplicationSettings";

    public ApplicationSettingsConfig(IConfig config)
    {
        _config = config;
    }
    public string DbConnectionString()
    {
        return _config.GetConfigValue<string>($"{ConfigParentKey}:{MethodBase.GetCurrentMethod()!.Name}");
    } 
}