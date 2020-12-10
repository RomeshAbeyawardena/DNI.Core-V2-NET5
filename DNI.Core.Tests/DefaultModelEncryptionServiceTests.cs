using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using DNI.Core.Abstractions.Extensions;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNI.Core.Abstractions.Services;
using DNI.Core.Shared.Contracts.Services;
using DNI.Core.Shared.Contracts.Factories;
using DNI.Core.Abstractions.Factories;
using Moq;
using DNI.Core.Shared.Enumerations;
using DNI.Core.Shared.Options;
using System.Security.Cryptography;
using DNI.Core.Shared;
using System.Net.Security;

namespace DNI.Core.Tests
{
    public class DefaultModelEncryptionServiceTests
    {
        [SetUp]
        public void Setup()
        {
            encryptionClassificationFactoryMock = new Mock<IEncryptionClassificationFactory>();

            encryptionClassificationFactoryMock
                .Setup(s => s.GetEncryptionOptions(It.Is<EncryptionClassification>(a => a == EncryptionClassification.CommonData || a == EncryptionClassification.PersonalData)))
                .Returns(new EncryptionOptions{ 
                    AlgorithmName = AsymmetricAlgorithmName.Rijndael,
                    Salt = "27e311da-81be-450c-8fef-711aa2cdfb1a",
                    Key = "a539a0d4-1a28-4254-8721-4e321400b460",
                    IVSalt = "d51d3076-1a4c-40c5-84a9-bb357de831b7",
                    IVKey = "958a82ae-6efc-4f78-b12e-c8e78a9d401d"
                }).Verifiable();

            encryptionFactoryMock = new Mock<IEncryptionFactory>();
            encryptionServiceMock = new Mock<IEncryptionService>();

            encryptionServiceMock.Setup(s => s.Encrypt(It.IsAny<string>(), It.IsAny<EncryptionOptions>())).Verifiable();
            encryptionFactoryMock.Setup(s => s.GetEncryptionService(AsymmetricAlgorithmName.Rijndael))
                .Returns(encryptionServiceMock.Object).Verifiable();

            hashServiceMock =  new Mock<IHashServiceFactory>();
            services = new ServiceCollection();
            services
                .AddSingleton(typeof(IModelEncryptionService<>), typeof(DefaultModelEncryptionService<>))
                .AddSingleton(encryptionClassificationFactoryMock.Object)
                .AddSingleton(encryptionFactoryMock.Object)
                .AddSingleton(hashServiceMock.Object)
                .RegisterModelForFluentEncryption<Person>(ct => ct
                    .Configure(person => person.Reference, EncryptionClassification.None, EncryptionPolicy.NoEncryption)
                    .Configure(person => person.EmailAddress, EncryptionClassification.PersonalData)
                    .Configure(person => person.FirstName, EncryptionClassification.CommonData)  
                    .Configure(person => person.MiddleName, EncryptionClassification.CommonData)
                    .Configure(person => person.LastName, EncryptionClassification.CommonData));
        }

        [Test]
        public void EncryptAndDecrypt()
        {
            var modelEncryptionService = ServiceProvider
                .GetRequiredService<IModelEncryptionService<Person>>();

            var person = new Person { 
                EmailAddress = "john.doe@website.net",
                FirstName = "John", 
                MiddleName = "Harrison", 
                LastName = "Doe" 
            };

            modelEncryptionService.Encrypt(person);

            encryptionClassificationFactoryMock.Verify();
            encryptionFactoryMock.Verify();
            encryptionServiceMock.Verify();
        }

        private Mock<IEncryptionService> encryptionServiceMock;
        private Mock<IEncryptionClassificationFactory> encryptionClassificationFactoryMock;
        private Mock<IEncryptionFactory> encryptionFactoryMock; 
        private Mock<IHashServiceFactory> hashServiceMock;
        private IServiceCollection services;
        private IServiceProvider ServiceProvider => services.BuildServiceProvider();
    }

    internal class Person
    {
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Reference { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
