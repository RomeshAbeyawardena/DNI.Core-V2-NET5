﻿using DNI.Core.Tests.Assets;
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
        [Test] public void Serialize_Deserialize()
        {
            var expected = new Student { Id = 1, Name = "Sam", Created = new DateTime(1987,07, 1) };
            var s = sut.Serialize(expected);

            Assert.IsNotEmpty(s);

            var s1 = sut.Deserialize<Student>(s);

            Assert.AreEqual(expected, s1);
        }

        JsonSerializer sut = new JsonSerializer(Newtonsoft.Json.JsonSerializer.CreateDefault());
    }
}
