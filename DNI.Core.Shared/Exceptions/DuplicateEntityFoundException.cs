using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Exceptions
{
    [Serializable]
    public class DuplicateEntityFoundException : Exception
    {
        public DuplicateEntityFoundException() { }
        public DuplicateEntityFoundException(string message) : base(message) { }
        public DuplicateEntityFoundException(string message, Exception inner) : base(message, inner) { }
        protected DuplicateEntityFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
