using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPluggableApplication.Core.Plugin;
using MyPluggableApplication.Core.Readers;

namespace MySecondPlugin
{
    public class MySecondPlugin : IPlugin
    {
        public string Name
        {
            get { return "MySecondPlugin"; }
        }

        public string Description
        {
            get { return "Description"; }
        }

        public void Load()
        {
            Console.WriteLine("MySecondPlugin.Load()");
        }

        public void Unload()
        {
            Console.WriteLine("MySecondPlugin.Unload()");
        }

        public void OnRead(IReader reader, string message)
        {
            var reads = reader.Read();

            foreach (string line in reads)
                Console.WriteLine("MySecondPlugin.OnRead({0}) - {1}", message, line);
        }
    }
}
