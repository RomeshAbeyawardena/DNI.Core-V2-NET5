using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonSerializer = DNI.Core.Abstractions.Serializers.JsonSerializer;
namespace DNI.Core.Tests
{
    public class JsonSerializerTests
    {
        [Test] public void Serialize()
        {
            var expected = new Student { Id = 1, Name = "Sam", Created = new DateTime(1987,07, 1) };
            var s = sut.Serialize(expected);

            Assert.IsNotEmpty(s);

            var s1 = sut.Deserialize<Student>(s);

            Assert.AreEqual(expected, s1);
        }

        private class Student
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime Created { get; set; }

            public override bool Equals(object obj)
            {
                if(obj is Student student)
                    return Equals(student);

                return false;
            }

            public bool Equals(Student student)
            {
                return student.Id == Id
                    && student.Name == Name
                    && student.Created == Created;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Id, Name, Created);
            }
        }

        JsonSerializer sut = new JsonSerializer(Newtonsoft.Json.JsonSerializer.CreateDefault());
    }
}
