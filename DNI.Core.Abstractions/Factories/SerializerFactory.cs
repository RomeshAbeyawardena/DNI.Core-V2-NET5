using System;
using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Factories;
using DNI.Core.Shared.Enumerations;

namespace DNI.Core.Abstractions.Factories
{
    internal class SerializerFactory : ImplementationFactoryBase<ISerializer, SerializerType>, ISerializerFactory
    {
        public ISerializer GetSerializer(SerializerType serializerType)
        {
            return GetServiceType(serializerType);
        }

        public SerializerFactory(IServiceProvider serviceProvider)
            : base(serviceProvider, service => service.Type)
        {

        }
    }
}
