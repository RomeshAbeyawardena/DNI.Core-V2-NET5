using DNI.Core.Shared.Contracts.Handlers;

namespace DNI.Core.Shared.Handlers
{
    internal class DefaultFinallyHandler : DefaultHandler, IFinallyHandler
    {
        internal static IFinallyHandler Create()
        {
            return new DefaultFinallyHandler();
        }

        private DefaultFinallyHandler()
        {

        }
    }
}
