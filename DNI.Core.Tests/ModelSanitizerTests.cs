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
            var student = new Student{ Name = " Johnny" };
            testModelSanitizer.SanitizeModel(student);
            Assert.AreEqual("Johnny", student.Name);
        }

        private TestModelSanitizer testModelSanitizer;
    }
}
