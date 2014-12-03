using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;

namespace PluggableApplication
{
    public class DependencyModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<PluginManager>()
                .ToSelf();
        }
    }
}
