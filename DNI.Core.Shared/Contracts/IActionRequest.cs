using MediatR;
using DNI.Core.Shared.Enumerations;

namespace DNI.Core.Shared.Contracts
{
    public interface IActionRequest<TResponse> : IRequest<TResponse>
    {
        RequestAction Action { get; }
        RequestQueryType QueryType { get; }
    }
}
