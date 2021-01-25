using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts
{
    public interface ITaskSet<TParameter, TResult>
    {
        Action<TParameter> Prepare { get; }
        Func<TParameter, TResult> Retrieve { get; }
        TResult Invoke(TParameter parameter);
    }
}
