using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions
{
    public abstract class ActionRequestBase<TResponse> : IActionRequest<TResponse>
    {
        protected ActionRequestBase(RequestAction requestAction, RequestQueryType requestQueryType = RequestQueryType.None)
        {
            Action = requestAction;
            QueryType = requestQueryType;
        }

        public RequestQueryType QueryType { get; }
        public RequestAction Action { get; }
    }
}
