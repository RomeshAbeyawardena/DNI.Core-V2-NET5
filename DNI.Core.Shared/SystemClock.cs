using DNI.Core.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared
{
    internal class SystemClock : ISystemClock
    {
        public static ISystemClock CreateDefault()
        {
            return new SystemClock();
        }

        public static ISystemClock CreateDefault(DateTimeOffset? dateTimeOffset)
        {
            return new SystemClock(dateTimeOffset);
        }

        public DateTimeOffset Now => dateTimeNow ?? DateTimeOffset.UtcNow;

        private SystemClock(DateTimeOffset? dateTimeOffset = null)
        {
            dateTimeNow = dateTimeOffset;
        }

        private readonly DateTimeOffset? dateTimeNow;
    }
}
