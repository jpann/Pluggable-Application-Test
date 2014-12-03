using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPluggableApplication.Core.Readers
{
    public interface IReader
    {
        string Read();
        string Read(int id);
    }
}
