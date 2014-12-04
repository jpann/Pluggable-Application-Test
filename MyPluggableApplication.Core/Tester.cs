using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPluggableApplication.Core.Readers;
using Ninject.Extensions.Logging;

namespace MyPluggableApplication.Core
{
    public class Tester : ITester
    {
        private readonly IReader mReader;
        private readonly ILogger mLogger;

        public Tester(IReader reader, ILogger logger)
        {
            this.mReader = reader;
            this.mLogger = logger;
        }

        public bool Test(int id)
        {
            try
            {
                this.mReader.Read(id);

                return true;
            }
            catch (Exception er)
            {
                this.mLogger.Error(er, "Failed");
                return false;
            }
        }
    }
}
