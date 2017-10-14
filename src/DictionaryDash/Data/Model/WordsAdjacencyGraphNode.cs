using System.Collections.Generic;

namespace DictionaryDash.Data.Model
{
    public class WordsAdjacencyGraphNode : IWordsAdjacencyGraphNode
    {
        public string Word { get; set; }
        public bool WasVisited { get; set; }
        public string PreviousNode { get; set; }
        public HashSet<string> AdjacentNodes { get; set; }
    }
}
