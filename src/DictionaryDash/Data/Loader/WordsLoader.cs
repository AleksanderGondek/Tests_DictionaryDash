using System;
using System.Collections.Generic;
using DictionaryDash.Data.Factory;
using DictionaryDash.Data.Model;
using DictionaryDash.Data.Utils;
using DictionaryDash.Logger;
using NLog;

namespace DictionaryDash.Data.Loader
{
    public class WordsLoader : IWordsLoader
    {
        private readonly IWordsAdjacencyGraphNodeFactory _wordsAdjacencyGraphNodeFactory;
        private readonly IFileWrapper _fileWrapper;
        private static ILogger _logger;

        public WordsLoader(ILogManager logManager, 
            IWordsAdjacencyGraphNodeFactory wordsAdjacencyGraphNodeFactory, IFileWrapper fileWrapper)
        {
            if (_logger == null)
            {
                _logger = logManager.GetLogger("WordsLoader");
            }

            _wordsAdjacencyGraphNodeFactory = wordsAdjacencyGraphNodeFactory;
            _fileWrapper = fileWrapper;
        }

        public Tuple<string, string, IDictionary<string, IWordsAdjacencyGraphNode>> LoadWordsChainAndDictionary(string pathToFile)
        {
            if (!_fileWrapper.Exists(pathToFile))
            {
                _logger.Error($"Following file does not exist: {pathToFile}");
                return null;
            }

            var dictionaryOfWords = new Dictionary<string, IWordsAdjacencyGraphNode>();
            var sourceWord = string.Empty;
            var targetWord = string.Empty;
            var linesReadCounter = 0;
            foreach (var line in _fileWrapper.ReadLines(pathToFile))
            {
                var lowerInvariant = line.ToLowerInvariant();

                linesReadCounter++;
                if (linesReadCounter == 1)
                {
                    sourceWord = lowerInvariant;
                }
                else if (linesReadCounter == 2)
                {
                    targetWord = lowerInvariant;
                }

                if (dictionaryOfWords.ContainsKey(lowerInvariant))
                {
                    _logger.Warn($"Passed file contained duplicated word - {lowerInvariant}");
                    continue;
                }

                dictionaryOfWords.Add(lowerInvariant, _wordsAdjacencyGraphNodeFactory.Create(lowerInvariant));
            }

            if (linesReadCounter < 2)
            {
                _logger.Error("Passed file did not contain enough data to create word chain.");
                return null;
            }

            return new Tuple<string, string, IDictionary<string, IWordsAdjacencyGraphNode>>(sourceWord, targetWord, dictionaryOfWords);
        }
    }
}
