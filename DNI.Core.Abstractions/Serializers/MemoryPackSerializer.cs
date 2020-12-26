using System.Collections.Generic;
using System.Linq;
using DNI.Core.Shared.Enumerations;
using MessagePack;

namespace DNI.Core.Abstractions.Serializers
{
    internal class MemoryPackSerializer : SerializerBase
    {
        public override T Deserialize<T>(IEnumerable<byte> data)
        {
            return MessagePackSerializer.Deserialize<T>(data.ToArray());
        }

        public override IEnumerable<byte> Serialize<T>(T value)
        {
            return MessagePackSerializer.Serialize(value);
        }

        public MemoryPackSerializer()
            : base(SerializerType.MemoryPack)
        {
            
        }

    }
}
