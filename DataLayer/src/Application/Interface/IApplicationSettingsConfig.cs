namespace DataLayer.Application.Interface;

public interface IApplicationSettingsConfig
{
    string DbConnectionString();
    bool EnableSensitiveDataLogging();
    Guid LocationId();
}