using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPluggableApplication.Core.Factories
{
    public interface ITesterFactory
    {
        ITester CreateTester(string testerType);
    }
}
