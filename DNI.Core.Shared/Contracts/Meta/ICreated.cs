using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Meta
{
    public interface ICreated<T> 
        where T : struct
    {
        T Created { get; set; }
    }

    public interface ICreated : ICreated<DateTimeOffset>
    {

    }
}
