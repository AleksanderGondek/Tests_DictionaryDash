using System.Collections.Generic;
using DictionaryDash.Data.Model;

namespace DictionaryDashTests.Data.Model
{
    public class WordsAdjacencyGraphNodeForTests : IWordsAdjacencyGraphNode
    {
        public string Word { get; set; }
        public bool WasVisited { get; set; }
        public string PreviousNode { get; set; }
        public HashSet<string> AdjacentNodes { get; set; }
    }
}
