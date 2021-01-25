using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts
{
    public interface ITaskSet<TResult>
    {
        /// <summary>
        /// Prepares the data used by <see cref="Retrieve"/>
        /// </summary>
        /// <returns></returns>
        Action<IEnumerable<object>> Prepare { get; }
        /// <summary>
        /// Retrieves the data using parameters supplied in <see cref="Prepare"/>
        /// </summary>
        /// <returns></returns>
        Func<IEnumerable<object>, TResult> Retrieve { get; }
        /// <summary>
        /// Configures the data returned by <see cref="Configure"/>
        /// </summary>
        /// <returns></returns>
        Action<TResult> Configure { get; }

        TResult Invoke(IEnumerable<object> args);
    }

    public interface ITaskSet<TParameter, TResult> : ITaskSet<TResult>
    {
        new Action<TParameter> Prepare { get; }
        new Func<TParameter, TResult> Retrieve { get; }

        TResult Invoke(TParameter parameter);
    }
}
