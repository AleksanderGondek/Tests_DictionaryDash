using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using DictionaryDash.Data.Model;
using DictionaryDash.Logger;
using NLog;

namespace DictionaryDash.ShortestPathFinder
{
    public class BreadthFirstSearch : IBreadthFirstSearch
    {
        public IDictionary<string, IWordsAdjacencyGraphNode> WordsAdjacencyGraph { get; set; }
        public string StartWord { get; set; }
        public string TargetWord { get; set; }
        private static ILogger _logger;

        public BreadthFirstSearch(ILogManager logManager)
        {
            if (_logger == null)
            {
                _logger = logManager.GetLogger("BreadthFirstSearch");
            }
        }

        public IList<string> GetShortestPath()
        {
            TraverseAllGraph();
            return RetrieveShorestPath();
        }

        private void TraverseAllGraph()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            _logger.Info($"BFS on graph of {WordsAdjacencyGraph.Count} nodes started.");

            var queue = new Queue<IWordsAdjacencyGraphNode>();
            queue.Enqueue(WordsAdjacencyGraph[StartWord]);

            while (queue.Any())
            {
                var currentNode = queue.Dequeue();
                currentNode.WasVisited = true;

                foreach (var neighbour in currentNode.AdjacentNodes)
                {
                    var neighBourNode = WordsAdjacencyGraph[neighbour];
                    if (neighBourNode.WasVisited) continue;

                    neighBourNode.WasVisited = true;
                    neighBourNode.PreviousNode = currentNode.Word;
                    queue.Enqueue(neighBourNode);
                }
            }

            stopWatch.Stop();
            _logger.Info($"BFS on graph of {WordsAdjacencyGraph.Count} nodes finished - " +
                         $"took {stopWatch.Elapsed.TotalSeconds} seconds.");
        }

        private IList<string> RetrieveShorestPath()
        {
            var shortestChain = new List<string>();
            var node = WordsAdjacencyGraph[TargetWord];
            while (!string.IsNullOrEmpty(node.PreviousNode) && !node.Word.Equals(StartWord))
            {
                shortestChain.Add(node.Word);
                node = WordsAdjacencyGraph[node.PreviousNode];
            }
            shortestChain.Add(StartWord);

            if (!ShortestPathExists(shortestChain))
            {
                shortestChain.Clear();
            }

            shortestChain.Reverse();
            return shortestChain;
        }

        private bool ShortestPathExists(ICollection<string> wordsChain)
        {
            return wordsChain.Contains(StartWord) && wordsChain.Contains(TargetWord);
        }
    }
}
