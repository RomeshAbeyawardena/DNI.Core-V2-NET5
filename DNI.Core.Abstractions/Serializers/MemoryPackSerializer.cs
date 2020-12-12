using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNI.Core.Shared.Abstractions;
using DNI.Core.Shared.Contracts;
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
