using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Options
{
    public class ETagServiceOptions
    {
        public string Separator { get; set; } 
        public HashAlgorithmName HashAlgorithmName { get; set; } 
        public Encoding Encoding { get; set; }
    }
}
