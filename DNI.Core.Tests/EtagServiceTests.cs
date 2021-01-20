using DNI.Core.Abstractions.Services;
using DNI.Core.Shared.Contracts.Services;
using DNI.Core.Tests.Assets;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
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
                .Returns<string, Encoding>((v, v1) => Convert.ToBase64String(v1.GetBytes(v)));
            eTagService = new ETagService(hashServiceMock.Object);

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

            var eTag = eTagService.Generate(student, "::", Encoding.ASCII);

            var eTag2 = eTagService.Generate(student2, "::", Encoding.ASCII);

            Assert.AreNotEqual(eTag, eTag2);
        }

        private Mock<IHashService> hashServiceMock;
        private ETagService eTagService;
    }
}
