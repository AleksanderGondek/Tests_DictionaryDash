using System.Collections.Generic;
using DictionaryDash.Data.Model;

namespace DictionaryDash.Data.Preparation
{
    public class AdjacentWordsAppender : IAdjacentWordsAppender
    {
        private static readonly string[] LatinAlphabet =
        {
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m",
            "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
        };

        public void AddAdjacentWords(IDictionary<string, IWordsAdjacencyGraphNode> wordsGraph)
        {
            foreach (var graphNode in wordsGraph)
            {
                for (var index = 0; index < graphNode.Key.Length; index++)
                {
                    foreach (var letter in LatinAlphabet)
                    {
                        var newWord = graphNode.Key.Substring(0, index) + letter + graphNode.Key.Substring(index + 1);
                        if (!graphNode.Key.Equals(newWord) && wordsGraph.ContainsKey(newWord))
                        {
                            wordsGraph[graphNode.Key].AdjacentNodes.Add(newWord);
                        }
                    }
                }
            }
        }
    }
}
