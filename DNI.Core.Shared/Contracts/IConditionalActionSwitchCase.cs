using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts
{
    public interface IConditionalActionSwitchCase
    {
        Func<bool> Condition { get; }
        Action Action { get; }
    }

    public interface IConditionalActionSwitchCase<TResult> : IConditionalActionSwitchCase
    {
        new Func<TResult> Action { get; }
    }

    public interface IConditionalActionSwitchCase<TParameter, TResult> : IConditionalActionSwitchCase<TResult>
    {
        new Func<TParameter, bool> Condition { get; }
        new Func<TParameter, TResult> Action { get; }
    }
}
