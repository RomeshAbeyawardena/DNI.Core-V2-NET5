using DNI.Core.Tests.Assets;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Tests
{
    public class ModelSanitizerTests
    {
        [Test]public void SanitizeModel()
        {
            testModelSanitizer = new TestModelSanitizer();
            var student = new Student{ Name = " Johnny", Type = new StudentType { Name = " Senior" } };
            testModelSanitizer.SanitizeModel(student);
            Assert.AreEqual("Johnny", student.Name);
            Assert.AreEqual("Senior", student.Type.Name);
        }

        private TestModelSanitizer testModelSanitizer;
    }
}
