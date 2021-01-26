using DNI.Core.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Defaults
{
    public class DefaultConditionalActionSwitch : IConditionalActionSwitch
    {
        public IEnumerable<IConditionalActionSwitchCase> ConditionalActions { get; private set; }

        public IConditionalActionSwitch CaseWhen(Func<bool> condition, Action action)
        {
            ConditionalActions = ConditionalActions
                .Append(new DefaultConditionalActionSwitchCase(condition, action));
            
            return this;
        }

        public IConditionalActionSwitch CaseWhen<TResult>(Func<bool> condition, Func<TResult> action)
        {
            ConditionalActions = ConditionalActions
                .Append(new DefaultConditionalActionSwitchCase<TResult>(condition, action));
            return this;
        }

        public IConditionalActionSwitch CaseWhen<TParameter, TResult>(Func<TParameter, bool> condition, Func<TParameter, TResult> action)
        {
            ConditionalActions = ConditionalActions
                .Append(new DefaultConditionalActionSwitchCase<TParameter, TResult>(condition, action));
            return this;
        }

        public IEnumerable<IConditionalActionSwitchCase> GetAll<TParameter, TResult>(TParameter parameter = default)
        {
            var cases = new List<IConditionalActionSwitchCase>();
            foreach (var conditionalAction in ConditionalActions)
            {
                if(conditionalAction is IConditionalActionSwitchCase<TParameter,TResult> conditionalActionWithResultAndParameter)
                {
                    conditionalActionWithResultAndParameter.Condition(parameter);
                }

                if(conditionalAction.Condition())
                    cases.Add(conditionalAction);
            }

            return cases.ToArray();
        }

        public IConditionalActionSwitchCase GetFirstOrDefault<TParameter, TResult>(TParameter parameter = default)
        {
            return GetAll<TParameter, TResult>(parameter).FirstOrDefault();
        }
    }
}
