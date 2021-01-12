namespace DNI.Core.Shared.Dto
{
    public class Exception
    {
        public Exception(System.Exception exception)
        {
            Message = exception.Message;
            Code = exception.HResult;
            HelpLink = exception.HelpLink;

            if(exception.InnerException != null)
            { 
                InnerException = new Exception(exception.InnerException);
            }
        }

        public string Message { get; }
        public int Code { get; }
        public string HelpLink { get; }
        public Exception InnerException { get; }
    }
}
