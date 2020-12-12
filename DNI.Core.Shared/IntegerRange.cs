using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared
{
    public class IntegerRange : Range<int>
    {
        public IntegerRange(int minimum, int maximum) : base(minimum, maximum)
        {

        }

        public override bool IsInRange(int value)
        {
            return value >= Minimum && value <= Maximum;
        }
    }
}
