using DataLayer.Application.Interface;
using DataLayer.Domain.Common.Exceptions;
using Microsoft.Extensions.Configuration;

namespace DataLayer.Infrastructure.Config;

public class Config : IConfig
{
    private readonly IConfiguration _configuration;
    public IApplicationSettingsConfig ApplicationSettingsConfig { get; }

    public Config(IConfiguration configuration)
    {
        _configuration = configuration;
        
        ApplicationSettingsConfig = new ApplicationSettingsConfig(this);
    }
    
    public T GetConfigValue<T>(string configKey, bool mustExist = true)
    {
        try
        {
            var configValue = _configuration.GetValue<T>(configKey);
            if (EqualityComparer<T>.Default.Equals(configValue, default(T)) && mustExist)
            {
                throw new ConfigException($"Config value '{configKey}' is missing");    
            }
            
            return configValue!;
        }
        catch (Exception)
        {
            throw new ConfigException($"Config value '{configKey}' is missing");
        }
    }
}