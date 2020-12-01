using System.Collections.Generic;
using DNI.Core.Shared.Enumerations;

namespace DNI.Core.Shared.Contracts
{
    public interface ISerializer
    {
        SerializerType Type { get; }
        IEnumerable<byte> Serialize<T>(T value);
        T Deserialize<T>(IEnumerable<byte> data);
    }
}
