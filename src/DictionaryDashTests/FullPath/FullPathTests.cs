using System;
using Autofac;
using DictionaryDash;
using Xunit;

namespace DictionaryDashTests.FullPath
{
    public class FullPathTests : IDisposable
    {
        private readonly IContainer _appplicationContainer;
        private readonly ILifetimeScope _applicationScope;

        public FullPathTests()
        {
            _appplicationContainer = TestApplicationSetup.GetDefaultApplicationContainer();
            _applicationScope = _appplicationContainer.BeginLifetimeScope();
        }
        public void Dispose()
        {
            _appplicationContainer.Dispose();
        }

        [Theory]
        [InlineData("-p=./FullPath/Tests/test01.txt")]
        public void SimpleShortestChainLargeDictionary(string pathToTestFile)
        {
            var application = _applicationScope.Resolve<IApplication>();
            Assert.Equal(5, application.Start(new [] { pathToTestFile }));
        }

        [Theory]
        [InlineData("-p=./FullPath/Tests/test02.txt")]
        public void ShortestChainExampleGiven(string pathToTestFile)
        {
            var application = _applicationScope.Resolve<IApplication>();
            Assert.Equal(4, application.Start(new[] { pathToTestFile }));
        }

        [Theory]
        [InlineData("-p=./FullPath/Tests/test03.txt")]
        public void ShortestChainSmallTestOne(string pathToTestFile)
        {
            var application = _applicationScope.Resolve<IApplication>();
            Assert.Equal(3, application.Start(new[] { pathToTestFile }));
        }

        [Theory]
        [InlineData("-p=./FullPath/Tests/test04.txt")]
        public void ShortestChainSmallTestTwo(string pathToTestFile)
        {
            var application = _applicationScope.Resolve<IApplication>();
            Assert.Equal(4, application.Start(new[] { pathToTestFile }));
        }

        [Theory]
        [InlineData("-p=./FullPath/Tests/test05.txt")]
        public void NoShortestChain(string pathToTestFile)
        {
            var application = _applicationScope.Resolve<IApplication>();
            Assert.Equal(-1, application.Start(new[] { pathToTestFile }));
        }

        [Theory]
        [InlineData("-p=./FullPath/Tests/test06.txt")]
        public void ShortestChainTwoPathsOfSameLength(string pathToTestFile)
        {
            var application = _applicationScope.Resolve<IApplication>();
            Assert.Equal(5, application.Start(new[] { pathToTestFile }));
        }

        [Theory]
        [InlineData("-p=./FullPath/Tests/test07.txt")]
        public void ShortestChainWithTwoSimmilarWordsSwitch(string pathToTestFile)
        {
            var application = _applicationScope.Resolve<IApplication>();
            Assert.Equal(4, application.Start(new[] { pathToTestFile }));
        }
    }
}
