using Microsoft.Extensions.DependencyInjection;

namespace DNI.Core.Shared.Contracts
{
    public interface IServiceRegistration
    {
        IServiceCollection RegisterServices(IServiceCollection services);
    }
}
