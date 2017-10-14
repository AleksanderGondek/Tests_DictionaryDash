using System.Collections.Generic;
using System.Linq;
using DictionaryDash.Data.Model;
using DictionaryDash.Data.Preparation;
using DictionaryDashTests.Data.Model;
using Xunit;

namespace DictionaryDashTests.Data.Preparation
{
    public class AdjacentWordsAppenderTests
    {
        private readonly AdjacentWordsAppender _adjacentWordsAppender;

        public AdjacentWordsAppenderTests()
        {
            _adjacentWordsAppender = new AdjacentWordsAppender();
        }

        [Fact]
        public void AddAdjacentWordsShouldProperlyFillInGraph()
        {
            var simpleGraph = new Dictionary<string, IWordsAdjacencyGraphNode>()
            {
                {"aaa", new WordsAdjacencyGraphNodeForTests {Word = "aaa", AdjacentNodes = new HashSet<string>()}},
                {"aab", new WordsAdjacencyGraphNodeForTests {Word = "aab", AdjacentNodes = new HashSet<string>()}},
                {"abb", new WordsAdjacencyGraphNodeForTests {Word = "abb", AdjacentNodes = new HashSet<string>()}},
                {"bbb", new WordsAdjacencyGraphNodeForTests {Word = "bbb", AdjacentNodes = new HashSet<string>()}},
                {"abc", new WordsAdjacencyGraphNodeForTests {Word = "abc", AdjacentNodes = new HashSet<string>()}},
                {"zzz", new WordsAdjacencyGraphNodeForTests {Word = "zzz", AdjacentNodes = new HashSet<string>()}}
            };

            var expectedWordsNeighbours = new Dictionary<string, List<string>>()
            {
                {"aaa", new List<string>() {"aab"}},
                {"aab", new List<string>() {"aaa", "abb"}},
                {"abb", new List<string>() {"aab", "bbb", "abc"}},
                {"bbb", new List<string>() {"abb"}},
                {"abc", new List<string>() {"abb"}},
                {"zzz", new List<string>() },
            };

            _adjacentWordsAppender.AddAdjacentWords(simpleGraph);
 
            foreach (var graphNode in simpleGraph.Values)
            {
                var graphNodeAdjacentNodes = graphNode.AdjacentNodes.ToList();
                graphNodeAdjacentNodes.Sort();
                expectedWordsNeighbours[graphNode.Word].Sort();
                Assert.Equal(graphNodeAdjacentNodes, expectedWordsNeighbours[graphNode.Word]);
            }
        }
    }
}
