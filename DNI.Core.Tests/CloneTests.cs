using DNI.Core.Shared.Extensions;
using DNI.Core.Tests.Assets;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Tests
{
    public class CloneTests
    {
        [Test]public void Clone()
        {
            var student = new Student
            {
                Id = 1,
                Name = "Julia Morris",
                Created = new DateTime(2020, 11, 23),
                Gender = new Gender { Name = "Hello" },
                Type = new StudentType { Name = "Senior" }
            };


            var student2 = student;
            var clonedStudent = student.Clone();

            Assert.AreSame(student, student2);
            Assert.AreNotSame(student, clonedStudent);
        }
    }
}
