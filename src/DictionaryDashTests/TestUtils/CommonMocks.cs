using System.Collections.Generic;
using DictionaryDash.Data.Factory;
using DictionaryDash.Data.Utils;
using DictionaryDash.Logger;
using DictionaryDashTests.Data.Model;
using Moq;
using NLog;

namespace DictionaryDashTests.TestUtils
{
    public static class CommonMocks
    {
        public static Mock<ILogManager> GetILogManagerMock()
        {
            var loggerManagerMock = new Mock<ILogManager>();
            var loggerMock = new Mock<ILogger>();
            loggerManagerMock.Setup(loggerManager => 
                loggerManager.GetLogger(It.IsAny<string>())).Returns(loggerMock.Object);

            return loggerManagerMock;
        }

        public static Mock<IFileWrapper> GetIFileWrapperMock(bool fileExistsReturnValue,
            IEnumerable<string> readLinesReturnValue)
        {
            var fileWrapperMock = new Mock<IFileWrapper>();
            fileWrapperMock.Setup(fileWrapper => fileWrapper.Exists(It.IsAny<string>())).Returns(fileExistsReturnValue);
            fileWrapperMock.Setup(fileWrapper => fileWrapper.ReadLines(It.IsAny<string>())).Returns(readLinesReturnValue);

            return fileWrapperMock;
        }

        public static Mock<IWordsAdjacencyGraphNodeFactory> GetIWordsAdjacencyGraphNodeFactoryMock()
        {
            var wordsAdjacencyGraphNodeFactoryMock = new Mock<IWordsAdjacencyGraphNodeFactory>();
            wordsAdjacencyGraphNodeFactoryMock.Setup(factoryMock => factoryMock.Create(It.IsAny<string>()))
                .Returns((string word) => new WordsAdjacencyGraphNodeForTests() {Word = word});

            return wordsAdjacencyGraphNodeFactoryMock;
        }
    }
}
