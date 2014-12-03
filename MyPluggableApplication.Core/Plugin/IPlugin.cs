using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPluggableApplication.Core.Readers;

namespace MyPluggableApplication.Core.Plugin
{
    public interface IPlugin
    {
        string Name { get; }
        string Description { get; }

        void Load();
        void Unload();
        void OnRead(IReader reader, string message);
    }
}
