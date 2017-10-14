using System.Collections.Generic;
using DictionaryDash.Data.Model;

namespace DictionaryDash.ShortestPathFinder
{
    public interface IBreadthFirstSearch
    {
        IDictionary<string, IWordsAdjacencyGraphNode> WordsAdjacencyGraph { get; set; }
        string StartWord { get; set; }
        string TargetWord { get; set; }
        IList<string> GetShortestPath();
    }
}
