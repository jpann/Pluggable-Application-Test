using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Extensions.Logging;

namespace MyPluggableApplication.Core.Readers
{
    public class ReaderA : IReader
    {
        private readonly ILogger mLogger;

        private string[] mItems = new[]
        {
            "A.Item A",
            "A.Item B",
            "A.Item C",
            "A.Item D"
        };

        public ReaderA()
        {
            
        }

        public ReaderA(ILogger logger)
        {
            this.mLogger = logger;
        }

        public IEnumerable<string> Read()
        {
            for (int i = 0; i < this.mItems.Length; i++)
                yield return this.mItems[i];
        }

        public string Read(int id)
        {
            if (id > mItems.Length)
                throw new IndexOutOfRangeException();

            return this.mItems[id];
        }
    }
}
