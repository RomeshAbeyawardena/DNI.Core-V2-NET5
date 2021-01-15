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
            return GetException(s => CreateException<TException>(
                PrependOrAppend(args, typeof(TException), s).ToArray()), isMultiple);
        }

        public TException GetException<TException>(Func<string, TException> buildAction, bool isMultiple) where TException : Exception
        {
            return buildAction(GetResourceText<TException, object>(null, isMultiple));
        }

        public TException GetException<TEntity, TException>(bool isMultiple, params object[] args)
            where TException : Exception
        {
            return GetException<TEntity, TException>(s => CreateException<TException>(
                PrependOrAppend(args, typeof(TException), s).ToArray()), isMultiple);
        }

        public TException GetException<TEntity, TException>(Func<string, TException> buildAction, bool isMultiple)
            where TException : Exception
        {
            return buildAction(GetResourceText<TException, TEntity>(null, isMultiple));
        }

        public Exception GetException(Type type, bool isMultiple, params object[] args)
        {
            return GetException<object>(type, s => CreateException(type, PrependOrAppend(args, type, s).ToArray()), isMultiple);
        }

        public Exception GetException<TEntity>(Type type, bool isMultiple, params object[] args)
        {
            return GetException<TEntity>(type, s => CreateException(type, PrependOrAppend(args, type, s).ToArray()), isMultiple);
        }

        public Exception GetException<TEntity>(Type type, Func<string, Exception> buildAction, bool isMultiple)
        {
            return buildAction(GetResourceText<TEntity>(type, null, isMultiple));
        }

        private IEnumerable<object> PrependOrAppend(IEnumerable<object> items, Type exceptionType, object parameter)
        {
            if(exceptionType == typeof(ArgumentNullException))
            {
                return items.Append(parameter);
            }

            return items.Prepend(parameter);
        }

        private TException CreateException<TException>(object[] args)
            where TException : Exception
        {
            return (TException)CreateException(typeof(TException), args);
        }

        private Exception CreateException(Type type, object[] args)
        {
            return (Exception)Activator.CreateInstance(type, args);
        }

        private string GetResourceText<TException, T>(IDictionary<string, string> placeHolders, bool isMultiple)
            where TException : Exception
        {
            placeHolders ??= new Dictionary<string, string>();

            placeHolders.Add("type", GetEntityName<T>(isMultiple));

            return resourceManager.Get<TException>(placeHolders);
        }

        private string GetResourceText<T>(Type type, IDictionary<string, string> placeHolders, bool isMultiple)
        {
            placeHolders ??= new Dictionary<string, string>();

            placeHolders.Add("type", GetEntityName<T>(isMultiple));

            return resourceManager.Get(type, placeHolders);
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
