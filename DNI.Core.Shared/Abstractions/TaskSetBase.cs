using DNI.Core.Shared.Contracts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Abstractions
{
    public class TaskSetBase<TResult> : ITaskSet<TResult>
    {
        protected TaskSetBase (
            Func<IEnumerable<object>, TResult> retrieve,
            IEnumerable<Action<IEnumerable<object>>> prepare,
            IEnumerable<Action<TResult>> configure)
        {
            retrieveAction = retrieve;
            preparationActions = new ConcurrentBag<Action<IEnumerable<object>>>(prepare);
            configurationActions = new ConcurrentBag<Action<TResult>>(configure);
        }

        public Action<IEnumerable<object>> Prepare => (args) => Iterate(ref preparationActions, args);

        public Action<TResult> Configure => (result) => Iterate(ref configurationActions, result);

        public Func<IEnumerable<object>, TResult> Retrieve => retrieveAction;

        public TResult Invoke(IEnumerable<object> args)
        {
            Prepare(args);
            var result = Retrieve(args);
            Configure(result);
            return result;
        }

        private void Iterate<TParameter>(ref ConcurrentBag<Action<TParameter>> bag, TParameter parameter)
        {
            var newBag = new ConcurrentBag<Action<TParameter>>();
            while(bag.TryTake(out var item))
            { 
                item(parameter); 
                newBag.Add(item);
            }

            bag = newBag;
        }


        private ConcurrentBag<Action<IEnumerable<object>>> preparationActions;
        private ConcurrentBag<Action<TResult>> configurationActions;
        private readonly Func<IEnumerable<object>, TResult> retrieveAction;
    }
}
