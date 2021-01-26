using DNI.Core.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Defaults
{
    public class DefaultConditionalActionSwitchCase : IConditionalActionSwitchCase
    {
        public DefaultConditionalActionSwitchCase(Func<bool> condition, Action action)
        {
            Condition = condition;
            Action = action;
        }

        public Func<bool> Condition { get; }

        public Action Action { get; }
    }

    public class DefaultConditionalActionSwitchCase<TResult>
        : DefaultConditionalActionSwitchCase,
        IConditionalActionSwitchCase<TResult>
    {
        public DefaultConditionalActionSwitchCase(Func<bool> condition, Func<TResult> action)
            : base(condition, () => action())
        {
            Action = action;
        }

        public new Func<TResult> Action { get; }
    }

    public class DefaultConditionalActionSwitchCase<TParameter, TResult>
        : DefaultConditionalActionSwitchCase<TResult>,
        IConditionalActionSwitchCase<TParameter, TResult>
    {
        public DefaultConditionalActionSwitchCase(
             Func<TParameter, bool> condition,
             Func<TParameter, TResult> action,
             TParameter parameter = default)
            : base(() => condition(parameter), 
                  () => action(parameter))
        {
            Condition = condition;
            Action = action;
        }

        public new Func<TParameter, bool> Condition { get; }

        public new Func<TParameter, TResult> Action { get; }
    }
}
