using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Options
{
    public class EncryptionOptions
    {
        public static EncryptionOptions Default => new EncryptionOptions { Encoding = Encoding.ASCII };
        public Encoding Encoding { get; set; }
        public IEnumerable<byte> Key { get; set; }
        public IEnumerable<byte> InitialVector { get; set; }
    }
}
