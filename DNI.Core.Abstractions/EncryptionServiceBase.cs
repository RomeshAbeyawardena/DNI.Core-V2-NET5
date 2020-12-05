using DNI.Core.Shared.Contracts.Services;
using DNI.Core.Shared.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions
{
    public abstract class EncryptionServiceBase : IEncryptionService
    {
        public EncryptionOptions EncryptionOptions { get; }
        public string EncryptionMethod { get; }
        public abstract string Decrypt(string value, EncryptionOptions encryptionOptions = null);
        public abstract string Encrypt(string value, EncryptionOptions encryptionOptions = null);

        public static IEnumerable<byte> GenerateKey(string key, EncryptionOptions encryptionOptions)
        {
            return encryptionOptions.Encoding.GetBytes(key);
        }

        protected EncryptionServiceBase(string encryptionMethod)
        {
            EncryptionMethod = encryptionMethod;
        }

        protected EncryptionServiceBase(string encryptionMethod, EncryptionOptions encryptionOptions)
            : this(encryptionMethod)
        {
            EncryptionMethod = encryptionMethod;
            EncryptionOptions = encryptionOptions;
        }

        protected EncryptionServiceBase(string encryptionMethod, IOptions<EncryptionOptions> options)
            : this(encryptionMethod, options?.Value)
        {

        }

        protected static string ConvertToBase64String(IEnumerable<byte> base64ByteArray)
        {
            return Convert.ToBase64String(base64ByteArray.ToArray());
        }

        protected IEnumerable<byte> ConvertFromBase64String(IEnumerable<byte> base64ByteArray, Encoding encoding = default)
        {
            if(encoding == default)
            {
                encoding = EncryptionOptions.Encoding;
            }

            var encodedString = encoding.GetString(base64ByteArray.ToArray());
            return Convert.FromBase64String(encodedString);
        }
    }
}
