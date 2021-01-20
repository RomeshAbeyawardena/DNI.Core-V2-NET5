using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Services
{
    public interface IETagService
    {
        string Generate(object model, string separator, Encoding encoding);
        string Generate<T>(T model, string separator, Encoding encoding); 
    }
}
