using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Extensions.Logging;

namespace MyPluggableApplication.Core.Readers
{
    public class ReaderB : IReader
    {
        private readonly ILogger mLogger;

        private string[] mItems = new[]
        {
            "B.Item A",
            "B.Item B",
            "B.Item C",
            "B.Item D"
        };

        public ReaderB()
        {
            
        }

        public ReaderB(ILogger logger)
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
