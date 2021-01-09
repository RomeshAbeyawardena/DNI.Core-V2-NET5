using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts
{
    public interface IPagingCriteria
    {
        int TotalItemsPerPage { get; set; }
        int PageIndex { get; set; }
    }
}
