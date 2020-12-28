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
        public static IHandler GetHandler()
        {
            return handler ??= new DefaultHandler();
        }

        private static IHandler handler;
    }
}
