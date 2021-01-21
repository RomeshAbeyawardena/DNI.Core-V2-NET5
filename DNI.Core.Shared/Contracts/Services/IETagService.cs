using DNI.Core.Shared.Contracts.Meta;
using DNI.Core.Shared.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Services
{
    public interface IETagService
    {
        string Generate(object model, ETagServiceOptions options);
        string Generate(object model, string separator, Encoding encoding);
        string Generate<T>(T model, ETagServiceOptions options); 
        string Generate<T>(T model, string separator, Encoding encoding); 
        bool Validate<T>(T sourcemodel, T model, ETagServiceOptions options)
            where T : IETag;
    }
}
