using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNI.Core.Shared.Abstractions;
using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Enumerations;

namespace DNI.Core.Abstractions.Serializers
{
    public class JsonSerializer : SerializerBase
    {
        public override T Deserialize<T>(IEnumerable<byte> data)
        {
            using var jsonReader = ConsumeStreamReader(data, (memoryStream, textReader) => new JsonTextReader(textReader));
            return jsonSerializer.Deserialize<T>(jsonReader);
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
