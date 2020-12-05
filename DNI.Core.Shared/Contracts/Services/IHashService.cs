﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Services
{
    public interface IHashService
    {
        string HashString(string value, Encoding encoding);
        string AlgorithmName { get; }
    }
}
