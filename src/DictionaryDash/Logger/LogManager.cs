using NLog;

namespace DictionaryDash.Logger
{
    public class LogManager : ILogManager
    {
        public ILogger GetLogger(string loggerName)
        {
            return NLog.LogManager.GetLogger(loggerName);
        }
    }
}
