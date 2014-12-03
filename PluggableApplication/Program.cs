using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MyPluggableApplication.Core.Plugin;
using Ninject;
using Ninject.Extensions.Conventions;

namespace PluggableApplication
{
    class Program
    {
        private static IKernel kernel;
        private static string pluginsDirectory = "plugins";
        private static PluginManager pluginManager;

        static void Main(string[] args)
        {
            // Create the IoC kernel
            kernel = CreateKernel();

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

            Console.ReadKey();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel(new DependencyModule());

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
