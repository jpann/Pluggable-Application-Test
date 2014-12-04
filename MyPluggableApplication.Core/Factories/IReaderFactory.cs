using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPluggableApplication.Core.Readers;

namespace MyPluggableApplication.Core.Factories
{
    public interface IReaderFactory
    {
        IReader CreateReader(string readerType);
    }
}
