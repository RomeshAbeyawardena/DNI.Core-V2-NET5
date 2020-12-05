﻿using DNI.Core.Shared.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using DNI.Core.Shared.Contracts.Factories;
using DNI.Core.Shared.Contracts.Services;
using DNI.Core.Shared;

namespace DNI.Core.Abstractions.Services
{
    internal class DefaultEncryptionService : EncryptionServiceBase
    {
        public DefaultEncryptionService(IHashServiceFactory hashServiceFactory, string encryptionMethod) 
            : base(encryptionMethod)
        {
            this.hashServiceFactory = hashServiceFactory;
        }

        public DefaultEncryptionService(IHashServiceFactory hashServiceFactory, string encryptionMethod, EncryptionOptions encryptionOptions) 
            : this(hashServiceFactory, encryptionMethod)
        {
        }

        public DefaultEncryptionService(IHashServiceFactory hashServiceFactory, string encryptionMethod, IOptions<EncryptionOptions> options) 
            : this(hashServiceFactory, encryptionMethod, options.Value)
        {

        }

        public override string Decrypt(string value, EncryptionOptions encryptionOptions)
        {
            if (encryptionOptions == default)
            {
                encryptionOptions = EncryptionOptions;
            }

            var encryptedBytes = Convert.FromBase64String(value);// encryptionOptions.Encoding.GetBytes(value);

            var encryptionConfiguration = GetEncryptionConfiguration(encryptionOptions);

            return Decrypt(encryptedBytes, encryptionConfiguration.Key, encryptionConfiguration.InitialVector);
        }

        public override string Encrypt(string value, EncryptionOptions encryptionOptions)
        {
            if (encryptionOptions == default)
            {
                encryptionOptions = EncryptionOptions;
            }

            var encryptionConfiguration = GetEncryptionConfiguration(encryptionOptions);

            return ConvertToBase64String(
                Encrypt(value, encryptionConfiguration.Key, encryptionConfiguration.InitialVector));
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

        private EncryptionConfiguration GetEncryptionConfiguration(EncryptionOptions encryptionOptions)
        {
            var hashService = GetHashService(encryptionOptions.HashAlgorithName);

            return new EncryptionConfiguration {
                Key = hashService.Hash(encryptionOptions.Key, encryptionOptions.Salt, 10000, encryptionOptions.KeySize, encryptionOptions.Encoding),
                InitialVector = hashService.Hash(encryptionOptions.Key, encryptionOptions.Salt, 10000, encryptionOptions.KeySize, encryptionOptions.Encoding),
            };
        }

        private IHashService GetHashService(HashAlgorithmName hashAlgorithmName)
        {
            return hashServiceFactory.GetHashService(hashAlgorithmName);
        }

        private readonly IHashServiceFactory hashServiceFactory;
        private SymmetricAlgorithm SymmetricAlgorithm => SymmetricAlgorithm.Create(EncryptionMethod);
    }
}
