using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MyPluggableApplication.Core;
using MyPluggableApplication.Core.Factories;
using MyPluggableApplication.Core.Readers;
using Ninject;
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

            this.Bind<ITesterFactory>()
                .ToFactory(() => new UseFirstArgumentAsNameInstanceProvider());

            this.Bind<PluginManager>()
                .ToSelf();

            this.Bind<IReader>()
                .To<ReaderA>()
                .Named("ReaderA");

            this.Bind<IReader>()
                .To<ReaderB>()
                .Named("ReaderB");

            this.Bind<ITester>()
                .To<Tester>()
                .Named("ReaderA")
                .WithConstructorArgument("reader", c => c.Kernel.Get<IReader>("ReaderA"));

            this.Bind<ITester>()
                .To<Tester>()
                .Named("ReaderB")
                .WithConstructorArgument("reader", c => c.Kernel.Get<IReader>("ReaderB"));
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
