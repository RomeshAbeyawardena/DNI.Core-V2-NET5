using System.Collections.Generic;
using System.Linq;
using DNI.Core.Shared.Enumerations;
using MessagePack;

namespace DNI.Core.Abstractions.Serializers
{
    internal class MessagePackSerializer : SerializerBase
    {
        public override T Deserialize<T>(IEnumerable<byte> data)
        {
            return MessagePack.MessagePackSerializer.Deserialize<T>(data.ToArray());
        }

        public override IEnumerable<byte> Serialize<T>(T value)
        {
            return MessagePack.MessagePackSerializer.Serialize(value);
        }

        public MessagePackSerializer()
            : base(SerializerType.MessagePack)
        {
            
        }

    }
}
