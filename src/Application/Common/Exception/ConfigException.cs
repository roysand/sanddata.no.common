namespace Application.Common.Exception;

public class ConfigException : System.Exception
{
    public ConfigException(string message)
        : base(message)
    {
    }  
}