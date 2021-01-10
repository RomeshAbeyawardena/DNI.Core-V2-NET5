using DNI.Core.Shared.Contracts.Services;
using System.Collections.Generic;
using System.Text;

namespace DNI.Core.Abstractions
{
    public abstract class HashServiceBase : IHashService
    {
        public abstract IEnumerable<byte> Hash(string value, string salt, int iterations,  int totalNumberOfBytes, Encoding encoding);
        public abstract string HashString(string value, Encoding encoding);
        public abstract string HashString(string value, string salt, int iterations,  int totalNumberOfBytes, Encoding encoding);

        public string AlgorithmName { get; }

        protected HashServiceBase(string algorithmName)
        {
            AlgorithmName = algorithmName;
        }
    }
}
