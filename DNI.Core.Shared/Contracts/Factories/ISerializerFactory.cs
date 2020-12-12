using DNI.Core.Shared.Enumerations;

namespace DNI.Core.Shared.Contracts.Factories
{
    /// <summary>
    /// Represents a factory used for returning a specific <see cref="ISerializer"/> from a dependency container
    /// </summary>
    public interface ISerializerFactory
    {
        /// <summary>
        /// Retrieves a <see cref="ISerializer"/> from a dependency container
        /// </summary>
        /// <param name="serializerType">The type of <see cref="ISerializer"/> to be returned</param>
        /// <returns>An instance of <see cref="ISerializer"/></returns>
        ISerializer GetSerializer(SerializerType serializerType);
    }
}
