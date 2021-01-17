using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Tests.Assets
{
    [MessagePack.MessagePackObject(true)]
    public class Gender
    {
        public string Name { get; set; }
    }
}
