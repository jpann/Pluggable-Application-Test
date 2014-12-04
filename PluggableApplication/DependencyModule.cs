using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MyPluggableApplication.Core.Factories;
using MyPluggableApplication.Core.Readers;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using Ninject.Parameters;

namespace PluggableApplication
{
    public class DependencyModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IReaderFactory>()
                .ToFactory(() => new UseFirstArgumentAsNameInstanceProvider()); 

            this.Bind<PluginManager>()
                .ToSelf();

            this.Bind<IReader>()
                .To<ReaderA>()
                .Named("ReaderA");

            this.Bind<IReader>()
                .To<ReaderB>()
                .Named("ReaderB");
        }
    }

    public class UseFirstArgumentAsNameInstanceProvider : StandardInstanceProvider
    {
        protected override string GetName(System.Reflection.MethodInfo methodInfo, object[] arguments)
        {
            return (string)arguments[0];
        }

        protected override IConstructorArgument[] GetConstructorArguments(MethodInfo methodInfo, object[] arguments)
        {
            return base.GetConstructorArguments(methodInfo, arguments).Skip(1).ToArray();
        }
    } 
}
