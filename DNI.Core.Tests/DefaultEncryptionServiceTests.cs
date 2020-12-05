using DNI.Core.Abstractions;
using DNI.Core.Abstractions.Services;
using DNI.Core.Shared.Contracts.Services;
using DNI.Core.Shared.Options;
using NUnit.Framework;
using System.Text;

namespace DNI.Core.Tests
{
    public class DefaultEncryptionServiceTests
    {
        [SetUp]
        public void Setup()
        {
            defaultEncryptionService = new DefaultEncryptionService("AES", new EncryptionOptions { 
                Encoding = Encoding.ASCII,
                Key = EncryptionServiceBase.GenerateKey("a7932d0c68994618841006807ea1a6ba", //3a36a67a468c4b51
                     EncryptionOptions.Default),
                InitialVector = EncryptionServiceBase.GenerateKey("3a36a67a468c4b51", 
                     EncryptionOptions.Default)
            });
        }
        
        [TestCase("My-Secret-Test-String")]
        [TestCase("My-Secret-Test-String-2")]
        [TestCase("My-Secret-Test-String-AB1")]
        [TestCase("my-email-address@somewebsite.com")]
        [TestCase("A34983823948DGUHA")]
        public void EncryptionDecryptionTest(string expectedResult)
        {
            var encryptedData = defaultEncryptionService.Encrypt(expectedResult);

            var decryptedData= defaultEncryptionService.Decrypt(encryptedData);
            
            Assert.AreEqual(expectedResult, decryptedData);
        }

        private IEncryptionService defaultEncryptionService;
    }
}