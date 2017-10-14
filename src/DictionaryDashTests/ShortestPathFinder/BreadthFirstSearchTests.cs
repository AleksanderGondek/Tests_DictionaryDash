using System.Collections.Generic;
using System.Linq;
using DictionaryDash.Data.Model;
using DictionaryDash.ShortestPathFinder;
using DictionaryDashTests.Data.Model;
using DictionaryDashTests.TestUtils;
using Xunit;

namespace DictionaryDashTests.ShortestPathFinder
{
    public class BreadthFirstSearchTests
    {
        private readonly BreadthFirstSearch _breadthFirstSearch;

        public BreadthFirstSearchTests()
        {
            _breadthFirstSearch = new BreadthFirstSearch(CommonMocks.GetILogManagerMock().Object)
            {
                StartWord = "aaa",
                TargetWord = "abc",
                WordsAdjacencyGraph = new Dictionary<string, IWordsAdjacencyGraphNode>()
                {
                    {
                        "aaa", new WordsAdjacencyGraphNodeForTests
                        {
                            Word = "aaa",
                            WasVisited = false,
                            PreviousNode = string.Empty,
                            AdjacentNodes = new HashSet<string>() {"aba", "baa"}
                        }
                    },
                    {
                        "aba", new WordsAdjacencyGraphNodeForTests
                        {
                            Word = "aba",
                            WasVisited = false,
                            PreviousNode = string.Empty,
                            AdjacentNodes = new HashSet<string>() {"aaa", "bba", "abc"}
                        }
                    },
                    {
                        "baa", new WordsAdjacencyGraphNodeForTests
                        {
                            Word = "baa",
                            WasVisited = false,
                            PreviousNode = string.Empty,
                            AdjacentNodes = new HashSet<string>() {"aaa"}
                        }
                    },
                    {
                        "bba", new WordsAdjacencyGraphNodeForTests
                        {
                            Word = "bba",
                            WasVisited = false,
                            PreviousNode = string.Empty,
                            AdjacentNodes = new HashSet<string>() {"baa", "aba"}
                        }
                    },
                    {
                        "abc", new WordsAdjacencyGraphNodeForTests
                        {
                            Word = "abc",
                            WasVisited = false,
                            PreviousNode = string.Empty,
                            AdjacentNodes = new HashSet<string>() {"aba"}
                        }
                    },
                    {
                        "yyy", new WordsAdjacencyGraphNodeForTests
                        {
                            Word = "yyy",
                            WasVisited = false,
                            PreviousNode = string.Empty,
                            AdjacentNodes = new HashSet<string>()
                        }
                    },
                }
            };
        }

        [Fact]
        public void GetShortestPathShouldVisitEachConnectedNodeOfGraph()
        {
            _breadthFirstSearch.GetShortestPath();
            foreach (var graphNode in _breadthFirstSearch.WordsAdjacencyGraph)
            {
                if(!graphNode.Value.AdjacentNodes.Any()) { continue; }
                Assert.True(graphNode.Value.WasVisited);
            }
        }

        [Fact]
        public void GetShortestPathShouldReturnValidShortestPath()
        {
            Assert.Equal(new List<string>() {"aaa", "aba", "abc"}, _breadthFirstSearch.GetShortestPath());
        }

        [Fact]
        public void GetShortestPathShouldReturnEmptyListIfNoPathExists()
        {
            _breadthFirstSearch.TargetWord = "yyy";
            Assert.Equal(new List<string>(), _breadthFirstSearch.GetShortestPath());
        }

        [Fact]
        public void GetShortestPathShouldHandleDisconnectedGraph()
        {
            _breadthFirstSearch.StartWord = "aaa";
            _breadthFirstSearch.TargetWord = "ccc";
            _breadthFirstSearch.WordsAdjacencyGraph = new Dictionary<string, IWordsAdjacencyGraphNode>()
            {
                {
                    "aaa", new WordsAdjacencyGraphNodeForTests
                    {
                        Word = "aaa",
                        WasVisited = false,
                        PreviousNode = string.Empty,
                        AdjacentNodes = new HashSet<string>() {"aba"}
                    }
                },
                {
                    "aba", new WordsAdjacencyGraphNodeForTests
                    {
                        Word = "aba",
                        WasVisited = false,
                        PreviousNode = string.Empty,
                        AdjacentNodes = new HashSet<string>() {"aaa"}
                    }
                },
                {
                    "ccc", new WordsAdjacencyGraphNodeForTests
                    {
                        Word = "ccc",
                        WasVisited = false,
                        PreviousNode = string.Empty,
                        AdjacentNodes = new HashSet<string>() {"ccb"}
                    }
                },
                {
                    "ccb", new WordsAdjacencyGraphNodeForTests
                    {
                        Word = "ccb",
                        WasVisited = false,
                        PreviousNode = string.Empty,
                        AdjacentNodes = new HashSet<string>() {"ccc"}
                    }
                }
            };

            Assert.Empty(_breadthFirstSearch.GetShortestPath());
        }
    }
}
