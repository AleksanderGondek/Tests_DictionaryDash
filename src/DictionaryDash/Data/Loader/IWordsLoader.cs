using System;
using System.Collections.Generic;
using DictionaryDash.Data.Model;

namespace DictionaryDash.Data.Loader
{
    public interface IWordsLoader
    {
        Tuple<string, string, IDictionary<string, IWordsAdjacencyGraphNode>> LoadWordsChainAndDictionary(string pathToFile);
    }
}
