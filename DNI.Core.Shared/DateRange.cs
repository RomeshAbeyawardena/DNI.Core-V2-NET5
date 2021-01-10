using DNI.Core.Shared.Contracts;
using System;

namespace DNI.Core.Shared
{
    public sealed class DateRange : DateTimeRange
    {
        public DateRange(ISystemClock systemClock)
            : base(systemClock)
        {

        }

        public DateRange(DateTime minimum, DateTime maximum) 
            : base(minimum, maximum)
        {

        }

        public new DateTime Minimum => base.Minimum.DateTime;
        public new DateTime? Maximum => base.Maximum?.DateTime;

        public DateTime UtcMinimum => base.Minimum.UtcDateTime;
        public DateTime? UtcMaximum => base.Maximum?.UtcDateTime;
    }

    public class DateTimeRange : Range<DateTimeOffset>
    {
        public DateTimeRange(ISystemClock systemClock)
            : this(systemClock.Now, systemClock.Now)
        {

        }

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
