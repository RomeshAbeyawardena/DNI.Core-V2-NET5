using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared
{
    public sealed class DateRange : DateTimeRange
    {
        public DateRange(DateTime minimum, DateTime maximum) 
            : base(minimum, maximum)
        {

        }
    }

    public class DateTimeRange : Range<DateTimeOffset>
    {
        public DateTimeRange(DateTimeOffset minimum, DateTimeOffset maximum) 
            : base(minimum, maximum)
        {

        }

        public override bool IsInRange(DateTimeOffset value)
        {
            return value >= Minimum && value <= Maximum;
        }
    }
}
