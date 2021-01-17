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
            testModelSanitizer = new TestModelSanitizer(false);
            var student = new Student{ Name = " Johnny", Type = new StudentType { Name = " Senior" } };
            testModelSanitizer.SanitizeModel(student);
            Assert.AreEqual("Johnny", student.Name);
            Assert.AreEqual("Senior", student.Type.Name);
        }

        [Test]public void SanitizeModel_with_html_stripping()
        {
            testModelSanitizer = new TestModelSanitizer(true);
            var student = new Student{ Name = "<p>Joh<br>nny</p>", 
                Gender = new Gender { Name = "<p>Male<br/>Female</p>" }, 
                Type = new StudentType { Name = "<div><b>S</b>enior</div>" } };
            testModelSanitizer.SanitizeModel(student);
            Assert.AreEqual("Johnny", student.Name);
            Assert.AreEqual("Senior", student.Type.Name);
            Assert.AreEqual("MaleFemale", student.Gender.Name);
        }

        private TestModelSanitizer testModelSanitizer;
    }
}
