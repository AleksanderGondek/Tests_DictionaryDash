using System.Collections.Generic;
using DictionaryDash.Data.Loader;
using DictionaryDash.Data.Model;
using DictionaryDashTests.TestUtils;
using Xunit;

namespace DictionaryDashTests.Data.Loader
{
    public class WordsLoaderTests
    {
        private WordsLoader _wordsLoader;

        [Fact]
        public void LoadWordsChainAndDictionaryShouldReturnNullIfNoFileIsFound()
        {
            _wordsLoader = new WordsLoader(CommonMocks.GetILogManagerMock().Object, 
                CommonMocks.GetIWordsAdjacencyGraphNodeFactoryMock().Object,
                CommonMocks.GetIFileWrapperMock(false, null).Object);

            Assert.Null(_wordsLoader.LoadWordsChainAndDictionary("nonExistingPath"));
        }

        [Fact]
        public void LoadWordsChainAndDictionaryShouldReturnNullIfTooLittleDataIsRead()
        {
            _wordsLoader = new WordsLoader(CommonMocks.GetILogManagerMock().Object,
                CommonMocks.GetIWordsAdjacencyGraphNodeFactoryMock().Object,
                CommonMocks.GetIFileWrapperMock(true, new List<string>() { "word" }).Object);

            Assert.Null(_wordsLoader.LoadWordsChainAndDictionary("nonExistingPath"));
        }


        [Fact]
        public void LoadWordsChainAndDictionaryShouldReturnValidData()
        {
            _wordsLoader = new WordsLoader(CommonMocks.GetILogManagerMock().Object,
                CommonMocks.GetIWordsAdjacencyGraphNodeFactoryMock().Object,
                CommonMocks.GetIFileWrapperMock(true, new List<string>() { "git", "cog", "fye", "dog", "ceg" }).Object);

            var returnedTouple = _wordsLoader.LoadWordsChainAndDictionary("nonExistingPath");
            Assert.Equal(returnedTouple.Item1, "git");
            Assert.Equal(returnedTouple.Item2, "cog");
            Assert.IsType<Dictionary<string, IWordsAdjacencyGraphNode>>(returnedTouple.Item3);
            Assert.Equal(5, returnedTouple.Item3.Count);
        }

        [Fact]
        public void LoadWordsChainAndDictionaryShouldLowercaseWords()
        {
            _wordsLoader = new WordsLoader(CommonMocks.GetILogManagerMock().Object,
                CommonMocks.GetIWordsAdjacencyGraphNodeFactoryMock().Object,
               CommonMocks.GetIFileWrapperMock(true, new List<string>() { "gIt", "Cog", "fyE", "DOG", "CeG" }).Object);

            var returnedTouple = _wordsLoader.LoadWordsChainAndDictionary("nonExistingPath");
            Assert.Equal(returnedTouple.Item1, "git");
            Assert.Equal(returnedTouple.Item2, "cog");
            Assert.IsType<Dictionary<string, IWordsAdjacencyGraphNode>>(returnedTouple.Item3);

            Assert.Equal(5, returnedTouple.Item3.Count);
            foreach (var graphNodeKey in returnedTouple.Item3.Keys)
            {
                Assert.Equal(graphNodeKey.ToLowerInvariant(), graphNodeKey);
            }
        }
    }
}
