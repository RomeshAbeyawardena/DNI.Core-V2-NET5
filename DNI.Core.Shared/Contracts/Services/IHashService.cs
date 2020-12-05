using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Services
{
    public interface IHashService
    {
        IEnumerable<byte> Hash(string value, string salt, int iterations, int totalNumberOfBytes, Encoding encoding);
        string HashString(string value, Encoding encoding);
        string HashString(string value, string salt, int iterations, int totalNumberOfBytes, Encoding encoding);
        string AlgorithmName { get; }
    }
}
