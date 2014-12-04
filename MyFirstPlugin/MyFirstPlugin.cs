using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPluggableApplication.Core.Plugin;
using MyPluggableApplication.Core.Readers;

namespace MyFirstPlugin
{
    public class MyFirstPlugin : IPlugin
    {
        public string Name
        {
            get { return "MyFirstPlugin"; }
        }

        public string Description
        {
            get { return "Description"; }
        }

        public void Load()
        {
            Console.WriteLine("MyFirstPlugin.Load()");
        }

        public void Unload()
        {
            Console.WriteLine("MyFirstPlugin.Unload()");
        }

        public void OnRead(IReader reader, string message)
        {
            var reads = reader.Read();

            foreach (string line in reads)
                Console.WriteLine("MyFirstPlugin.OnRead({0}) - {1}", message, line);
        }
    }
}
