using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Factories
{
    public interface IExceptionResourceFactory
    {
        TException GetException<TException>(bool isMultiple, params object[] args)
            where TException : Exception;
        TException GetException<TException>(Func<string, TException> buildAction, bool isMultiple)
            where TException : Exception;

        TException GetException<TEntity, TException>(bool isMultiple, params object[] args)
            where TException : Exception;

        TException GetException<TEntity, TException>(Func<string, TException> buildAction, bool isMultiple)
            where TException : Exception;
    }
}
