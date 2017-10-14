using System.Collections.Generic;
using DictionaryDash.Data.Model;

namespace DictionaryDash.Data.Preparation
{
    public interface IAdjacentWordsAppender
    {
        void AddAdjacentWords(IDictionary<string, IWordsAdjacencyGraphNode> wordsGraph);
    }
}
