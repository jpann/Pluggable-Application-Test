using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Extensions.Logging;

namespace MyPluggableApplication.Core.Readers
{
    public class Reader : IReader
    {
        private readonly ILogger mLogger;

        public Reader(ILogger logger)
        {
            this.mLogger = logger;
        }

        public string Read()
        {
            throw new NotImplementedException();
        }

        public string Read(int id)
        {
            throw new NotImplementedException();
        }
    }
}
