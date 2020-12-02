using DNI.Core.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions
{
    public abstract class ActionRequestBase
    {
        protected ActionRequestBase(RequestAction requestAction)
        {
            Action = requestAction;
        }

        public RequestAction Action { get; set; }
    }
}
