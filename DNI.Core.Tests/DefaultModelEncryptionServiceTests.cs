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

namespace DNI.Core.Tests
{
    public class DefaultModelEncryptionServiceTests
    {
        [SetUp]
        public void Setup()
        {
            services = new ServiceCollection();
            services
                .AddSingleton(typeof(IModelEncryptionService<>), typeof(DefaultModelEncryptionService<>))
                .AddSingleton<IEncryptionFactory, EncryptionFactory>()
                .AddSingleton<IHashServiceFactory, HashServiceFactory>()
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
            Assert.AreNotEqual("Harrison", person.FirstName);
            Assert.AreNotEqual("Doe", person.FirstName);
        }

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
