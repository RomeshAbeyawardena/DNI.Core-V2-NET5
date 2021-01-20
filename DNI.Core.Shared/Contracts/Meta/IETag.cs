using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Meta
{
    public interface IETag<T>
    {
        T ETag { get; set; }
    }

    public interface IETag : IETag<string>
    {
        
    }
}
