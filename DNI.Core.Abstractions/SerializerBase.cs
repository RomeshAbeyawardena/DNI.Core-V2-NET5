using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Enumerations;

namespace DNI.Core.Abstractions
{
    public abstract class SerializerBase : ISerializer
    {
        public SerializerType Type { get; }

        public abstract T Deserialize<T>(IEnumerable<byte> data);
        public abstract IEnumerable<byte> Serialize<T>(T value);

        protected SerializerBase(SerializerType serializerType)
        {
            Type = serializerType;
        }

        protected T ConsumeStreamReader<T>(IEnumerable<byte> data, Func<MemoryStream, StreamReader, T> action)
        {
            using var memoryStream = new MemoryStream(data.ToArray());
            using var textReader = new StreamReader(memoryStream);

            return action(memoryStream, textReader);
        }

        protected IEnumerable<byte> ConsumeStreamWriter(Action<MemoryStream, StreamWriter> action)
        {
            using var memoryStream = new MemoryStream();
            using var streamWriter = new StreamWriter(memoryStream);
            action(memoryStream, streamWriter);
            return memoryStream.ToArray();
        }
    }
}
