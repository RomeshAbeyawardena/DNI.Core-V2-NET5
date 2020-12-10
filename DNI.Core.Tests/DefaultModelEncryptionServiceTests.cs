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

namespace DNI.Core.Tests
{
    public class DefaultModelEncryptionServiceTests
    {
        [SetUp]
        public void Setup()
        {
            encryptionClassificationFactoryMock = new Mock<IEncryptionClassificationFactory>();
            encryptionFactoryMock = new Mock<IEncryptionFactory>();
            hashServiceMock =  new Mock<IHashServiceFactory>();
            services = new ServiceCollection();
            services
                .AddSingleton(typeof(IModelEncryptionService<>), typeof(DefaultModelEncryptionService<>))
                .AddSingleton(encryptionClassificationFactoryMock.Object)
                .AddSingleton(encryptionFactoryMock.Object)
                .AddSingleton(hashServiceMock.Object)
                .RegisterModelForFluentEncryption<Person>(ct => ct
                    .Configure(person => person.FirstName)  
                    .Configure(person => person.MiddleName)
                    .Configure(person => person.LastName));
        }

        [Test]
        public void EncryptAndDecrypt()
        {
            var modelEncryptionService = ServiceProvider
                .GetRequiredService<IModelEncryptionService<Person>>();

            var person = new Person { 
                FirstName = "John", 
                MiddleName = "Harrison", 
                LastName = "Doe" 
            };

            modelEncryptionService.Encrypt(person);

            Assert.AreNotEqual("John", person.FirstName);
            Assert.AreNotEqual("Harrison", person.MiddleName);
            Assert.AreNotEqual("Doe", person.LastName);
        }

        private Mock<IEncryptionClassificationFactory> encryptionClassificationFactoryMock;
        private Mock<IEncryptionFactory> encryptionFactoryMock; 
        private Mock<IHashServiceFactory> hashServiceMock;
        private IServiceCollection services;
        private IServiceProvider ServiceProvider => services.BuildServiceProvider();
    }

    internal class Person
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
