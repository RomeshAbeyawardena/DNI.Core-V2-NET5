using DNI.Core.Shared.Contracts.Factories;
using DNI.Core.Shared.Managers;
using FluentValidation.Internal;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions.Factories
{
    internal class ExceptionResourceFactory : IExceptionResourceFactory
    {
        public ExceptionResourceFactory(IResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
        }

        public TException GetException<TException>(bool isMultiple, params object[] args) where TException : Exception
        {
            return GetException(s => CreateException<TException>(args.Prepend(s).ToArray()), isMultiple);
        }

        public TException GetException<TException>(Func<string, TException> buildAction, bool isMultiple) where TException : Exception
        {
            return buildAction(GetResourceText<TException, object>(null, isMultiple));
        }

        public TException GetException<TEntity, TException>(bool isMultiple, params object[] args)
            where TException : Exception
        {
            return GetException<TEntity, TException>(s => CreateException<TException>(args.Prepend(s).ToArray()), isMultiple);
        }

        public TException GetException<TEntity, TException>(Func<string, TException> buildAction, bool isMultiple)
            where TException : Exception
        {
            return buildAction(GetResourceText<TException, TEntity>(null, isMultiple));
        }

        private TException CreateException<TException>(object[] args)
        {
            return (TException)Activator.CreateInstance(typeof(TException), args);
        }

        private string GetResourceText<TException, T>(IDictionary<string, string> placeHolders, bool isMultiple)
            where TException : Exception
        {
            placeHolders ??= new Dictionary<string, string>();

            placeHolders.Add("type", GetEntityName<T>(isMultiple));

            return resourceManager.Get<TException>(placeHolders);
        }

        private string GetEntityName<T>(bool isMultiple)
        {
            var entityTypeName = typeof(T).Name.SplitPascalCase();

            return isMultiple 
                ? entityTypeName.Pluralize() 
                : entityTypeName.Singularize();

        }

        private readonly IResourceManager resourceManager;
    }
}
