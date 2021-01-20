using DNI.Core.Abstractions.Services;
using DNI.Core.Shared.Contracts.Factories;
using DNI.Core.Shared.Contracts.Services;
using DNI.Core.Tests.Assets;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Tests
{
    public class ETagServiceTests
    {
        [Test] public void ETagService()
        {
            
            hashServiceMock = new Mock<IHashService>();
            hashServiceMock.Setup(h => h.HashString(It.IsAny<string>(), Encoding.ASCII))
                .Returns<string, Encoding>((v, v1) => Convert.ToBase64String(v1.GetBytes(v)))
                .Verifiable();

            hashServiceFactoryMock = new Mock<IHashServiceFactory>();
            hashServiceFactoryMock.Setup(f => f.GetHashService(HashAlgorithmName.SHA512))
                .Returns(hashServiceMock.Object)
                .Verifiable();

            eTagService = new ETagService(hashServiceFactoryMock.Object);

            const string separator = "::";

            var student = new Student
            {
                Created = new DateTime(2021, 01, 20),
                Gender = new Gender { Name = "Female" },
                Id = 1,
                Name = "Chloe",
                Type = new StudentType { Name = "Senior" }
            };

            var student2 = new Student
            {
                Created = new DateTime(2020, 01, 20),
                Gender = new Gender { Name = "Female" },
                Id = 1,
                Name = "Chloe",
                Type = new StudentType { Name = "Senior" }
            };

            var student3 = new Student
            {
                Created = new DateTime(2020, 01, 20),
                Gender = new Gender { Name = "Female" },
                Id = default,
                Name = "Chloe",
                Type = new StudentType { Name = "Senior" }
            };

            var eTag = eTagService.Generate(student, separator, Encoding.ASCII);

            var eTag2 = eTagService.Generate(student2, separator, Encoding.ASCII);

            var eTag3 = eTagService.Generate(student3, separator, Encoding.ASCII);

            hashServiceFactoryMock.Verify();
            hashServiceMock.Verify();
            Assert.AreNotEqual(eTag, eTag2);

            Assert.AreNotEqual(eTag3, eTag2);
        }
        private Mock<IHashServiceFactory> hashServiceFactoryMock;
        private Mock<IHashService> hashServiceMock;
        private ETagService eTagService;
    }
}
