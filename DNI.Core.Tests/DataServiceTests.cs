using DNI.Core.Shared.Contracts;
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
    public class DataServiceTests
    {
        [Test] public void Resolves_KeyAttribute()
        {
            studentRepositoryMock = new Mock<IAsyncRepository<Student>>();
            modelEncryptionServiceMock = new Mock<IModelEncryptionService<Student>>();
            sut = new TestDataService<Student>(
                studentRepositoryMock.Object, 
                modelEncryptionServiceMock.Object);

            var student = new Student { Id = 1 };

            Assert.AreEqual(1, sut.IdentityKey(student));
        }

        private Mock<IModelEncryptionService<Student>> modelEncryptionServiceMock;
        private Mock<IAsyncRepository<Student>> studentRepositoryMock;
        private TestDataService<Student> sut;
    }
}
