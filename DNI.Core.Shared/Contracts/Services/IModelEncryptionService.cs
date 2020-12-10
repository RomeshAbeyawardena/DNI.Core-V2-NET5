using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Services
{
    public interface IModelEncryptionService<T> 
    {
        void Encrypt(T model);
        void Decrypt(T model);
    }
}
