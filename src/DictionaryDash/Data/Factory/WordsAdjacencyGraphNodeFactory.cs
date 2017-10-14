using System.Collections.Generic;
using DictionaryDash.Data.Model;

namespace DictionaryDash.Data.Factory
{
    public class WordsAdjacencyGraphNodeFactory : IWordsAdjacencyGraphNodeFactory
    {
        public IWordsAdjacencyGraphNode Create(string word = null)
        {
            return new WordsAdjacencyGraphNode()
            {
                Word = word ?? string.Empty,
                AdjacentNodes = new HashSet<string>(),
                PreviousNode = string.Empty,
                WasVisited = false
            };
        }
    }
}
