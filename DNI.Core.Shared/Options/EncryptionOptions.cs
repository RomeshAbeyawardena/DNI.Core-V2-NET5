using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Options
{
    public class EncryptionOptions
    {
        public static EncryptionOptions Default => new EncryptionOptions { Encoding = Encoding.ASCII };
        public Encoding Encoding { get; set; }
        public string Key { get; set; }
        public string Salt { get; set; }
        public int KeySize { get; set; }
        public int IVSize { get; set; }
        public HashAlgorithmName HashAlgorithName { get; set; }
    }
}
