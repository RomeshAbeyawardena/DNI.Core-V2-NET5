using System;
using System.Collections.Generic;

namespace DNI.Core.Shared.Contracts
{
    public interface IRandomStringGenerator
    {
        IEnumerable<byte> GetRandomBytes(int length, params Range[] ranges);
        string GenerateString(int length);
    }
}
