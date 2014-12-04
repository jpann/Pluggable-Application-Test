using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using log4net.Config;
using MyPluggableApplication.Core.Factories;
using MyPluggableApplication.Core.Plugin;
using MyPluggableApplication.Core.Readers;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;
using Ninject.Extensions.Logging.Log4net;

namespace PluggableApplication
{
    class Program
    {
        private static IKernel kernel;
        private static IReaderFactory readerFactory;
        private static string pluginsDirectory = "plugins";
        private static PluginManager pluginManager;

        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            // Create the IoC kernel
            kernel = CreateKernel();

            // Get IReaderFactory
            readerFactory = kernel.Get<IReaderFactory>();

            // Get ReaderA
            IReader readerA = readerFactory.CreateReader("ReaderA");

            // Get the full plugins directory
            pluginsDirectory = Path.Combine(Environment.CurrentDirectory, pluginsDirectory);

            pluginManager = kernel.Get<PluginManager>();
            pluginManager.PluginDirectory = pluginsDirectory;
            pluginManager.LoadPlugins();

            BindPlugins(pluginsDirectory);

            var loadedPlugins = kernel.GetAll<IPlugin>();

            foreach (IPlugin plugin in loadedPlugins)
            {
                Console.WriteLine("Plugin Resolved: '{0}'", plugin.Name);
            }

            pluginManager.SendRead(readerA, "test!!!");

            Console.ReadKey();
        }

        private static IKernel CreateKernel()
        {
            var settings = new NinjectSettings()
            {
                LoadExtensions = false
            };

            var kernel = new StandardKernel(
                settings,
                new Log4NetModule(), 
                new DependencyModule());

            kernel.Load<FuncModule>();

            return kernel;
        }

        private static void BindPlugins(string pluginDirectory)
        {
            kernel.Bind(
               x => x.FromAssembliesInPath(pluginDirectory)
                     .SelectAllClasses().InheritedFrom<IPlugin>()
                     .BindAllInterfaces());
        }
    }
}
