using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Meta
{
    public interface IModified<T>
        where T : struct
    {
        T Modified { get; set; }
    }

    public interface IModified : IModified<DateTimeOffset>
    {

    }
}
