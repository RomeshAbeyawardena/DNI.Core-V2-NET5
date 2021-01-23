using DNI.Core.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Tests.Assets
{
    public class TestDataService<T> : DataServiceBase<T>
        where T : class
    {
        public TestDataService(
            Shared.Contracts.IAsyncRepository<T> repository, 
            Shared.Contracts.Services.IModelEncryptionService<T> modelEncryptionService)
            : base(repository, modelEncryptionService)
        {

        }

        public new Func<T, object> IdentityKey => base.IdentityKey;

        public override Task<bool> ExistsAsync(T entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
