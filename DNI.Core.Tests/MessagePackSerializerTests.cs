using NUnit.Framework;
using DNI.Core.Abstractions.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNI.Core.Tests.Assets;

namespace DNI.Core.Tests
{
    public class MessagePackSerializerTests
    {
        [Test] public void Serialize_Deserialize()
        {
            var expected = new Student { Id = 1, Name = "Sam", Created = new DateTime(1987,07, 1) };
            var s = sut.Serialize(expected);

            Assert.IsNotEmpty(s);

            var s1 = sut.Deserialize<Student>(s);

            Assert.AreEqual(expected, s1);
        } 

        private MessagePackSerializer sut = new MessagePackSerializer();
    }
}
