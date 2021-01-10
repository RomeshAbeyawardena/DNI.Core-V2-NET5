using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Dto
{
    public class Exception
    {
        public Exception(System.Exception exception)
        {
            Message = exception.Message;
            Code = exception.HResult;
            HelpLink = exception.HelpLink;
            InnerException = new Exception(exception.InnerException);
        }

        public string Message { get; }
        public int Code { get; }
        public string HelpLink { get; }
        public Exception InnerException { get; }
    }
}
