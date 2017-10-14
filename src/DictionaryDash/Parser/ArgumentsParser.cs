using System;
using DictionaryDash.Logger;
using Mono.Options;
using NLog;

namespace DictionaryDash.Parser
{
    public class ArgumentsParser : IArgumentsParser
    {
        public string PathToInputFile { get; set; }

        private static ILogger _logger;
        private bool _showHelp;

        public ArgumentsParser(ILogManager logManager)
        {
            if (_logger == null)
            {
                _logger = logManager.GetLogger("ArgumentsParser");
            }
        }

        public void HandleArguments(string[] args)
        {
            var inputParser = new OptionSet()
            {
                {"p|path=", "path to file containing problem data", path => PathToInputFile = path},
                {"h|help", "show help message and exit application", help => _showHelp = help != null}
            };

            try
            {
                inputParser.Parse(args);
            }
            catch (OptionException exception)
            {
                _logger.Error(exception, "DictionaryDash invalid input. Use `dictionarydash --help` for more information.");
                return;
            }

            if (_showHelp)
            {
                _logger.Info("DictionaryDash was run with --help parameter. Printing out options and exiting.");
                inputParser.WriteOptionDescriptions(Console.Out);
                return;
            }

            if (string.IsNullOrEmpty(PathToInputFile))
            {
                _logger.Warn("Path to file containing data was not specified, application will exit.");
            }
        }
    }
}
