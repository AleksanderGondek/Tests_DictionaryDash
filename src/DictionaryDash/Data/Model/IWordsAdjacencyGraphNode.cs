using System.Collections.Generic;

namespace DictionaryDash.Data.Model
{
    public interface IWordsAdjacencyGraphNode
    {
        string Word { get; set; }
        bool WasVisited { get; set; }
        string PreviousNode { get; set; }
        HashSet<string> AdjacentNodes { get; set; }
    }
}
