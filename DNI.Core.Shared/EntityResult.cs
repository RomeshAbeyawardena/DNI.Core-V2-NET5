using DNI.Core.Shared;
using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }


    public class EntityResult<TEntity> : IEntityResult<TEntity>
    {
        internal EntityResult(TEntity result)
        {
            Result = result;
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

        public TEntity Result { get;  }

        public int AffectedRows { get;  }

        public IDictionary<string, string> Properties { get;  }
    }
}
