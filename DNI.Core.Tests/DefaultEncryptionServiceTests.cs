using DNI.Core.Abstractions;
using DNI.Core.Abstractions.Services;
using DNI.Core.Shared.Contracts.Factories;
using DNI.Core.Shared.Contracts.Services;
using DNI.Core.Shared.Options;
using Moq;
using NUnit.Framework;
using System.Text;

namespace DNI.Core.Tests
{
    public class DefaultEncryptionServiceTests
    {
        [SetUp]
        public void Setup()
        {
            hashServiceFactoryMock = new Mock<IHashServiceFactory>();
            defaultEncryptionService = new DefaultEncryptionService(hashServiceFactoryMock.Object, "AES", new EncryptionOptions { 
                Encoding = Encoding.ASCII,
                Key = "a7932d0c68994618841006807ea1a6ba",
                Salt = "3a36a67a468c4b51"
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

        private IMock<IHashServiceFactory> hashServiceFactoryMock;
        private IEncryptionService defaultEncryptionService;
    }
}