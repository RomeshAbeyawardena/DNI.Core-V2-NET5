﻿using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Extensions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace DNI.Core.Shared
{
    public static class EntityResult
    {
        public static IEntityResult<TEntity> Create<TEntity>(TEntity entity, int affectedRows, Action<ISwitch<string, string>> propertiesAction)
        {
            return new EntityResult<TEntity>(entity, affectedRows, propertiesAction);
        }

        public static IEntityResult<TEntity> Create<TEntity>(TEntity entity)
        {
            return new EntityResult<TEntity>(entity);
        }

        public static IEntityResult<TEntity> Create<TEntity>(TEntity entity, int affectedRows,  params KeyValuePair<string, string>[] keyValuePairs)
        {
            return new EntityResult<TEntity>(entity, affectedRows, keyValuePairs);
        }

        public static IEntityResult<TEntity> Create<TEntity>(Exception exception, params ValidationFailure[] validationFailures)
        {
            return new EntityResult<TEntity>(exception, validationFailures);
        }
    }


    internal class EntityResult<TEntity> : Attempt<TEntity>, IEntityResult<TEntity>
    {
        internal EntityResult(TEntity result)
            : base(result)
        {
        }

        internal EntityResult(Exception exception, params ValidationFailure[] validationFailures)
            : base(exception, validationFailures)
        {

        }

        internal EntityResult(TEntity result, int affectedRows)
            : this(result)
        {
            AffectedRows = affectedRows;
        }

        internal EntityResult(TEntity result, int affectedRows,  params KeyValuePair<string, string>[] keyValuePairs)
            : this(result, affectedRows, s => keyValuePairs.ForEach(k => s.Add(k)))
        {

        }

        internal EntityResult(TEntity result, int affectedRows, Action<ISwitch<string, string>> propertiesAction)
            : this(result, affectedRows)
        {
            var propertiesSwitch = Switch.Create<string, string>();
            propertiesAction(propertiesSwitch);
            Properties = propertiesSwitch;
        }

        public int AffectedRows { get;  }

        public IDictionary<string, string> Properties { get;  }
    }
}