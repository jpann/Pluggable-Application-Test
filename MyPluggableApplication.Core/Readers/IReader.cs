﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPluggableApplication.Core.Readers
{
    public interface IReader
    {
        IEnumerable<string> Read();
        string Read(int id);
    }
}
