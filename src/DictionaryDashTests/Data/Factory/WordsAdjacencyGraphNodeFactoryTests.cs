using System.Collections.Generic;
using DictionaryDash.Data.Factory;
using Xunit;

namespace DictionaryDashTests.Data.Factory
{
    public class WordsAdjacencyGraphNodeFactoryTests
    {
        private readonly WordsAdjacencyGraphNodeFactory _wordsAdjacencyGraphNodeFactory;
   
        public WordsAdjacencyGraphNodeFactoryTests()
        {
            _wordsAdjacencyGraphNodeFactory = new WordsAdjacencyGraphNodeFactory();    
        }

        [Fact]
        public void CreateReturnsInitializedGraphNodeObject()
        {
            var returnedNode = _wordsAdjacencyGraphNodeFactory.Create();
            Assert.NotNull(returnedNode);
            Assert.True(string.IsNullOrEmpty(returnedNode.Word));
            Assert.True(string.IsNullOrEmpty(returnedNode.PreviousNode));
            Assert.NotNull(returnedNode.AdjacentNodes);
            Assert.IsType<HashSet<string>>(returnedNode.AdjacentNodes);
            Assert.Empty(returnedNode.AdjacentNodes);
            Assert.False(returnedNode.WasVisited);
        }

        [Theory]
        [InlineData("exquisite")]
        public void CreateReturnsInitializedGraphNodeObjectWithPassedWord(string word)
        {
            var returnedNode = _wordsAdjacencyGraphNodeFactory.Create(word);
            Assert.NotNull(returnedNode);
            Assert.Equal(word, returnedNode.Word);
            Assert.True(string.IsNullOrEmpty(returnedNode.PreviousNode));
            Assert.NotNull(returnedNode.AdjacentNodes);
            Assert.IsType<HashSet<string>>(returnedNode.AdjacentNodes);
            Assert.Empty(returnedNode.AdjacentNodes);
            Assert.False(returnedNode.WasVisited);
        }
    }
}
