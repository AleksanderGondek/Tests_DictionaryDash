using NLog;

namespace DictionaryDash.Logger
{
    public interface ILogManager
    {
        ILogger GetLogger(string loggerName);
    }
}
    