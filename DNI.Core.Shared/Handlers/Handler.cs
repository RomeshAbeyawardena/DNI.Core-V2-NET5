using DNI.Core.Shared.Contracts.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private static IHandler handler;
    }
}
