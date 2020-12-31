using DNI.Core.Shared.Contracts.Handlers;

namespace DNI.Core.Shared.Handlers
{
    public static class Handler
    {
        /// <summary>
        /// Gets the default handler
        /// </summary>
        public static IHandler Default => GetHandler();

        /// <summary>
        /// Gets or creates an instance of <see cref="IHandler"/>
        /// </summary>
        /// <returns></returns>
        public static IHandler GetHandler()
        {
            return handler ??= new DefaultHandler();
        }

        internal static IHandler GetHandler(ICatchHandler catchHandler, IFinallyHandler finallyHandler)
        {
            return new DefaultHandler(catchHandler, finallyHandler);
        }

        private static IHandler handler;
    }
}
