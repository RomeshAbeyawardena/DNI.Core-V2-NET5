using DNI.Core.Shared;
using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions
{
    public class EntityResult<TEntity> : IEntityResult<TEntity>
    {
        public EntityResult(TEntity result)
        {
            Result = result;
        }

        public EntityResult(int affectedRows, TEntity result)
            : this(result)
        {
            AffectedRows = affectedRows;
        }

        public EntityResult(int affectedRows, TEntity result, params KeyValuePair<string, string>[] keyValuePairs)
            : this(affectedRows, result, s => keyValuePairs.ForEach(k => s.Add(k)))
        {

        }

        public EntityResult(int affectedRows, TEntity result, Action<ISwitch<string, string>> propertiesAction)
            : this(affectedRows, result)
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
