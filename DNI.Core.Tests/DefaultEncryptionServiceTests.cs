using DNI.Core.Abstractions;
using DNI.Core.Abstractions.Services;
using DNI.Core.Shared;
using DNI.Core.Shared.Contracts.Factories;
using DNI.Core.Shared.Contracts.Services;
using DNI.Core.Shared.Options;
using Moq;
using NUnit.Framework;
using System.Security.Cryptography;
using System.Text;

namespace DNI.Core.Tests
{
    public class DefaultEncryptionServiceTests
    {
        [SetUp]
        public void Setup()
        {
            hashServiceMock = new Mock<IHashService>();
            hashServiceFactoryMock = new Mock<IHashServiceFactory>();

            hashServiceMock
                .Setup(hashService => hashService.Hash("a7932d0c68994618841006807ea1a6ba", "3a36a67a468c4b51", 10000, 32, Encoding.ASCII))
                .Returns(Encoding.ASCII.GetBytes("a7932d0c68994618841006807ea1a6ba"));

            hashServiceMock
                .Setup(hashService => hashService.Hash("c440a7f9bd2f459cb091a3e6bdbdbb03", "8c45e461a6a90e99", 10000, 16, Encoding.ASCII))
                .Returns(Encoding.ASCII.GetBytes("8c45e461a6a90e99"));

            hashServiceFactoryMock
                .Setup(serviceFactory => serviceFactory.GetHashService(HashAlgorithmName.SHA512))
                .Returns(hashServiceMock.Object);
            defaultEncryptionService = new DefaultEncryptionService(hashServiceFactoryMock.Object, AsymmetricAlgorithmName.Rijndael, 
                new EncryptionOptions { 
                    KeySize = 32,
                    IVSize = 16,
                    HashAlgorithName = HashAlgorithmName.SHA512,
                    Encoding = Encoding.ASCII,
                    Key = "a7932d0c68994618841006807ea1a6ba",
                    Salt = "3a36a67a468c4b51",
                    IVKey = "c440a7f9bd2f459cb091a3e6bdbdbb03",
                    IVSalt = "8c45e461a6a90e99"
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

        private Mock<IHashService> hashServiceMock;
        private Mock<IHashServiceFactory> hashServiceFactoryMock;
        private IEncryptionService defaultEncryptionService;
    }
}