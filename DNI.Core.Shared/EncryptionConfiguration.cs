using System.Collections.Generic;

namespace DNI.Core.Shared
{
    public class EncryptionConfiguration
    {
        public IEnumerable<byte> Key { get; set; }
        public IEnumerable<byte> InitialVector { get; set; }
    }
}
