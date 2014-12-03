using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using log4net.Plugin;
using Ninject.Extensions.Logging;
using PluggableApplication.Extensions;

namespace PluggableApplication
{
    public class PluginManager
    {
        private readonly ILogger mLogger;
        private string mPluginDirectory;
        private List<IPlugin> mPlugins = new List<IPlugin>();

        public string PluginDirectory
        {
            get { return this.mPluginDirectory; }
            set
            {
                if (!Directory.Exists(value))
                    throw new DirectoryNotFoundException(value);

                this.mPluginDirectory = value;
            }
        }

        public List<IPlugin> Plugins
        {
            get { return this.mPlugins; }
        }

        public PluginManager(
            ILogger logger)
        {
            this.mLogger = logger;
        }

        public PluginManager(
            string pluginDirectory,
            ILogger logger)
        {
            if (string.IsNullOrEmpty(pluginDirectory))
                throw new ArgumentException("pluginDirectory");

            if (!Directory.Exists(pluginDirectory))
                throw new DirectoryNotFoundException(pluginDirectory);

            this.mPluginDirectory = pluginDirectory;
            this.mLogger = logger;
        }

        public void LoadPlugins()
        {
            DirectoryInfo info = new DirectoryInfo(this.mPluginDirectory);

            foreach (FileInfo file in info.GetFiles("*.dll"))
            {
                Assembly currentAssembly = Assembly.LoadFile(file.FullName);

                foreach (Type type in currentAssembly.GetTypes())
                {
                    if (!type.ImplementsInterface(typeof(IPlugin)))
                        continue;

                    IPlugin plugin = (IPlugin)Activator.CreateInstance(type);

                    this.mPlugins.Add(plugin);
                }
            }
        }
    }
}
