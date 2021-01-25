using DNI.Core.Shared.Contracts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Abstractions
{
    public abstract class TaskSetBase<TParameter, TResult> : ITaskSet<TParameter, TResult>
    {
        
        protected TaskSetBase (
            Func<TParameter, TResult> retrieve,
            IEnumerable<Action<TParameter>> prepare,
            IEnumerable<Action<TResult>> configure)
        {
            retrieveAction = retrieve;
            preparationActions = new ConcurrentBag<Action<TParameter>>(prepare);
            configurationActions = new ConcurrentBag<Action<TResult>>(configure);
        }

        public Action<TParameter> Prepare => (args) => Iterate(ref preparationActions, args);

        public Action<TResult> Configure => (result) => Iterate(ref configurationActions, result);

        public Func<TParameter, TResult> Retrieve => retrieveAction;

        public TResult Invoke(TParameter args)
        {
            Prepare(args);
            var result = Retrieve(args);
            Configure(result);
            return result;
        }

        private void Iterate<TParam>(ref ConcurrentBag<Action<TParam>> bag, TParam parameter)
        {
            var newBag = new ConcurrentBag<Action<TParam>>();
            while(bag.TryTake(out var item))
            { 
                item(parameter); 
                newBag.Add(item);
            }

            bag = newBag;
        }


        private ConcurrentBag<Action<TParameter>> preparationActions;
        private ConcurrentBag<Action<TResult>> configurationActions;
        private readonly Func<TParameter, TResult> retrieveAction;
    }

    public abstract class TaskSetBase<TResult> : TaskSetBase<IEnumerable<object>, TResult>
    {
        protected TaskSetBase(
            Func<IEnumerable<object>, TResult> retrieve, 
            IEnumerable<Action<IEnumerable<object>>> prepare, 
            IEnumerable<Action<TResult>> configure) : base(retrieve, prepare, configure)
        {

        }
    }
}
