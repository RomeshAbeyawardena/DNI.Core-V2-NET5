using DNI.Core.Shared.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared
{
    public abstract class HashServiceBase : IHashService
    {
        public abstract string HashString(string value, Encoding encoding);
        public abstract string HashString(string value, string salt, int iterations,  int totalNumberOfBytes, Encoding encoding);

        public string AlgorithmName { get; }

        protected HashServiceBase(string algorithmName)
        {
            AlgorithmName = algorithmName;
        }
    }
}
