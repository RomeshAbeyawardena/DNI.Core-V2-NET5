﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts
{
    public interface IRandomStringGenerator
    {
        IEnumerable<byte> GetRandomBytes(int length, params Range[] ranges);
        string GenerateString(int length);
    }
}