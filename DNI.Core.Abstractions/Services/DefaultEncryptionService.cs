using DNI.Core.Shared.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace DNI.Core.Abstractions.Services
{
    internal class DefaultEncryptionService : EncryptionServiceBase
    {

        public DefaultEncryptionService(string encryptionMethod) : base(encryptionMethod)
        {
        }

        public DefaultEncryptionService(string encryptionMethod, EncryptionOptions encryptionOptions) : base(encryptionMethod, encryptionOptions)
        {
        }

        public DefaultEncryptionService(string encryptionMethod, IOptions<EncryptionOptions> options) : base(encryptionMethod, options)
        {
        }

        public override string Decrypt(string value, EncryptionOptions encryptionOptions)
        {
            if (encryptionOptions == default)
            {
                encryptionOptions = EncryptionOptions;
            }

            var encryptedBytes = Convert.FromBase64String(value);// encryptionOptions.Encoding.GetBytes(value);

            return Decrypt(encryptedBytes, encryptionOptions.Key, encryptionOptions.InitialVector);
        }

        public override string Encrypt(string value, EncryptionOptions encryptionOptions)
        {
            if (encryptionOptions == default)
            {
                encryptionOptions = EncryptionOptions;
            }

            return ConvertToBase64String(
                Encrypt(value, encryptionOptions.Key, encryptionOptions.InitialVector));
        }

        private IEnumerable<byte> Encrypt(string value, IEnumerable<byte> key, IEnumerable<byte> initialVector)
        {
            using (var encryptor = SymmetricAlgorithm.CreateEncryptor(key.ToArray(), initialVector.ToArray()))
            using (var stream = new MemoryStream())
            {
                using (var cryptoStreamWriter = new CryptoStream(stream, encryptor, CryptoStreamMode.Write))
                using (var streamWriter = new StreamWriter(cryptoStreamWriter))
                    streamWriter.Write(value);

                return stream.ToArray();
            }
        }

        private string Decrypt(IEnumerable<byte> rawStringBytes, IEnumerable<byte> key, IEnumerable<byte> initialVector)
        {
            using (var encryptor = SymmetricAlgorithm.CreateDecryptor(key.ToArray(), initialVector.ToArray()))
            using (var stream = new MemoryStream(rawStringBytes.ToArray()))
            using (var cryptoStreamReader = new CryptoStream(stream, encryptor, CryptoStreamMode.Read))
            using (var streamReader = new StreamReader(cryptoStreamReader))
                return streamReader.ReadToEnd();

        }

        private SymmetricAlgorithm SymmetricAlgorithm => SymmetricAlgorithm.Create(EncryptionMethod);
    }
}
