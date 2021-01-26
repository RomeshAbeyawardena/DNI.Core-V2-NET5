using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts
{
    public interface IConditionalActionSwitch
    {
        IEnumerable<IConditionalActionSwitchCase> GetAll<TParameter, TResult>(TParameter parameter = default);
        IConditionalActionSwitchCase GetFirstOrDefault<TParameter, TResult>(TParameter parameter = default);
        IEnumerable<IConditionalActionSwitchCase> ConditionalActions { get; }
        IConditionalActionSwitch CaseWhen(Func<bool> condition, Action action);
        IConditionalActionSwitch CaseWhen<TResult>(Func<bool> condition, Func<TResult> action);
        IConditionalActionSwitch CaseWhen<TParameter, TResult>(Func<TParameter, bool> condition, Func<TParameter, TResult> action);
    }
}
