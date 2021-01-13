using Newtonsoft.Json;
using System.Collections.Generic;
using DNI.Core.Shared.Enumerations;

namespace DNI.Core.Abstractions.Serializers
{
    internal class JsonSerializer : SerializerBase
    {
        public override T Deserialize<T>(IEnumerable<byte> data)
        {
            return ConsumeStreamReader(data, (memoryStream, textReader) => { 
                using (var jsonReader = new JsonTextReader(textReader))
                    return jsonSerializer.Deserialize<T>(jsonReader);
            });
        }

        public override IEnumerable<byte> Serialize<T>(T value)
        {
            return ConsumeStreamWriter((memoryStream, streamWriter) => { 
                var jsonWriter = new JsonTextWriter(streamWriter); 
                jsonSerializer.Serialize(jsonWriter, value);
            });
        }

        public JsonSerializer(Newtonsoft.Json.JsonSerializer jsonSerializer)
            : base(SerializerType.Json)
        {
            this.jsonSerializer = jsonSerializer;
        }

        private readonly Newtonsoft.Json.JsonSerializer jsonSerializer;
    }
}
