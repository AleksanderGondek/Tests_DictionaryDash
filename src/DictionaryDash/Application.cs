using System;
using DictionaryDash.Data.Loader;
using DictionaryDash.Data.Preparation;
using DictionaryDash.Logger;
using DictionaryDash.Parser;
using DictionaryDash.ShortestPathFinder;
using NLog;

namespace DictionaryDash
{
    public class Application : IApplication
    {
        private static ILogger _logger;

        private readonly IArgumentsParser _argumentsParser;
        private readonly IWordsLoader _wordsLoader;
        private readonly IAdjacentWordsAppender _adjacentWordsAppender;
        private readonly IBreadthFirstSearch _breadthFirstSearch;

        public Application(ILogManager logManager, IArgumentsParser argumentsParser, IWordsLoader wordsLoader,
            IAdjacentWordsAppender adjacentWordsAppender, IBreadthFirstSearch breadthFirstSearch)
        {
            if (_logger == null)
            {
                _logger = logManager.GetLogger("Application");
            }

            _argumentsParser = argumentsParser;
            _wordsLoader = wordsLoader;
            _adjacentWordsAppender = adjacentWordsAppender;
            _breadthFirstSearch = breadthFirstSearch;
        }

        public int Start(string[] args)
        {
            _logger.Info("Application was started.");
            var shortestPathLength = -1;

            try
            {
                _argumentsParser.HandleArguments(args);
                if (string.IsNullOrEmpty(_argumentsParser.PathToInputFile)) { return shortestPathLength; }

                var data = _wordsLoader.LoadWordsChainAndDictionary(_argumentsParser.PathToInputFile);
                if(data == null) {  return shortestPathLength; }

                var startWord = data.Item1;
                var targetWord = data.Item2;
                var graph = data.Item3;

                _adjacentWordsAppender.AddAdjacentWords(graph);

                _breadthFirstSearch.WordsAdjacencyGraph = graph;
                _breadthFirstSearch.StartWord = startWord;
                _breadthFirstSearch.TargetWord = targetWord;

                var response = _breadthFirstSearch.GetShortestPath();
                shortestPathLength = response.Count - 1;

                _logger.Info($"Shortest transformation length is: {shortestPathLength}");
                _logger.Info($"Shortest transformation is: {string.Join(" -> ", response)}");
            }
            catch (Exception fatalException)
            {
                _logger.Fatal(fatalException, "Encountered fatal exception, aplication will exit.");
            }

            return shortestPathLength;
        }
    }
}
