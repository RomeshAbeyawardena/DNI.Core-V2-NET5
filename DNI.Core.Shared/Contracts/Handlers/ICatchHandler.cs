using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Handlers
{
    public interface ICatchHandler
    {
        Exception Exception { get; }
        ICatchHandler When<TException>(Action<Exception> catchAction) where TException : Exception;
        ICatchHandler Default(Action<Exception> catchAllAction);
        Action<Exception> GetCatchAction();
    }
}
