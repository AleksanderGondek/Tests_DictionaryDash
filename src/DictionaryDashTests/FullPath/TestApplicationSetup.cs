using Autofac;
using DictionaryDash;
using DictionaryDash.Data.Factory;
using DictionaryDash.Data.Loader;
using DictionaryDash.Data.Model;
using DictionaryDash.Data.Preparation;
using DictionaryDash.Data.Utils;
using DictionaryDash.Logger;
using DictionaryDash.Parser;
using DictionaryDash.ShortestPathFinder;

namespace DictionaryDashTests.FullPath
{
    public static class TestApplicationSetup
    {
        public static IContainer GetDefaultApplicationContainer()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<Application>().As<IApplication>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<LogManager>().As<ILogManager>();
            containerBuilder.RegisterType<FileWrapper>().As<IFileWrapper>();
            containerBuilder.RegisterType<ArgumentsParser>().As<IArgumentsParser>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<AdjacentWordsAppender>().As<IAdjacentWordsAppender>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<WordsAdjacencyGraphNode>().As<IWordsAdjacencyGraphNode>();
            containerBuilder.RegisterType<WordsAdjacencyGraphNodeFactory>()
                .As<IWordsAdjacencyGraphNodeFactory>()
                .InstancePerLifetimeScope();
            containerBuilder.RegisterType<WordsLoader>().As<IWordsLoader>();
            containerBuilder.RegisterType<BreadthFirstSearch>().As<IBreadthFirstSearch>();

            return containerBuilder.Build();
        }
    }
}
