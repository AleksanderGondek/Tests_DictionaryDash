using Autofac;
using DictionaryDash.Configuration;

namespace DictionaryDash
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var container = ApplicationSetup.GetDefaultApplicationContainer();
            using (var scope = container.BeginLifetimeScope())
            {
                var application = scope.Resolve<IApplication>();
                return application.Start(args);
            }
        }
    }
}
