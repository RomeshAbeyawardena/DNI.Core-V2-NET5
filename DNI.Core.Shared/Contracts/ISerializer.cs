using System.Collections;
using System.Collections.Generic;
using DNI.Core.Shared.Enumerations;

namespace DNI.Core.Shared.Contracts
{
    /// <summary>
    /// Represents a serializer used to serialize and deserialize objects.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Returns the <see cref="SerializerType" /> of the current instance
        /// </summary>
        SerializerType Type { get; }

        /// <summary>
        /// Serialises the <paramref name="value"/> into a byte <see cref="IEnumerable" />
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        IEnumerable<byte> Serialize<T>(T value);

        /// <summary>
        /// Deserializes the serialized <paramref name="data"/> into <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        T Deserialize<T>(IEnumerable<byte> data);
    }
}
