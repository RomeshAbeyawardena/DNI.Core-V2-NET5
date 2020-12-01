using DNI.Core.Shared.Enumerations;

namespace DNI.Core.Shared.Contracts.Factories
{
    public interface ISerializerFactory
    {
        ISerializer GetSerializer(SerializerType serializerType);
    }
}
