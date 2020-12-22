using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Web.Abstractions
{
    public abstract class MiddlewareBase
    {
        public async Task Invoke(HttpContext context)
        {
            if(await InvokeMiddleware(context))
            {
                await next(context);
            }
        }

        /// <summary>
        /// A method that will be fired on
        /// </summary>
        /// <param name="context"></param>
        /// <returns>Returns a <see cref="bool" to determine whether the next middleware should execute/></returns>
        protected abstract Task<bool> InvokeMiddleware(HttpContext context);

        protected MiddlewareBase(RequestDelegate next)
        {
            this.next = next;
        }

        private readonly RequestDelegate next;

    }
}
