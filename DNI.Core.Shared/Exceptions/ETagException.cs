using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Exceptions
{
    [Serializable]
    public class ETagException : Exception
    {
        public ETagException() { }
        public ETagException(string message) : base(message) { }
        public ETagException(string message, Exception inner) : base(message, inner) { }
        protected ETagException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
