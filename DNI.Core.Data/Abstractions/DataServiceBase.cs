﻿using DNI.Core.Data.Extensions;
using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Services;
using DNI.Core.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Data.Abstractions
{
    public abstract class DataServiceBase<TEntity> : Core.Abstractions.DataServiceBase<TEntity>
       where TEntity : class
    {
        public override Task<int> SaveAsync(TEntity entity, CancellationToken cancellationToken)
        {
            return Repository.AddOrUpdate(identityKey, entity, cancellationToken);
        }

        protected DataServiceBase(IAsyncRepository<TEntity> repository,
            IModelEncryptionService<TEntity> modelEncryptionService,
            Func<TEntity, object> identityKey)
            : base(repository, modelEncryptionService)
        {
            this.identityKey = identityKey;
        }

        protected DataServiceBase(IAsyncRepository<TEntity> repository,
            IModelEncryptionService<TEntity> modelEncryptionService)
            : base(repository, modelEncryptionService)
        {
            this.identityKey = GetKeyMemberExpression();
        }

        private Func<TEntity, object> GetKeyMemberExpression()
        {
            var entityType = typeof(TEntity);

            var propertiesWithKeyAttribute = entityType
                .GetPropertiesWithAttribute<KeyAttribute>();

            if (!propertiesWithKeyAttribute.Any())
            {
                throw new NullReferenceException("No members or properties decorated with a KeyAttribute could be found");
            }

            var propertyInfo = propertiesWithKeyAttribute.First();

            var parameterExpression = Expression.Parameter(entityType, entityType.Name.ToLower());
            var propertyOrFieldExpression = Expression.PropertyOrField(parameterExpression, propertyInfo.Name);
            return Expression.Lambda<Func<TEntity, object>>(propertyOrFieldExpression, parameterExpression).Compile();
        }

        private readonly Func<TEntity, object> identityKey;
    }
}
