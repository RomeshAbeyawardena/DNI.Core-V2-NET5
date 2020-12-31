using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DNI.Core.Web.Abstractions
{
    /// <summary>
    /// Represents an abstract middleware pipeline for ASP.NET
    /// </summary>
    public abstract class MiddlewareBase
    {
        /// <summary>
        /// The pipeline task to be invoked against ASP.NET
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            if(await InvokeMiddleware(context))
            {
                await next(context);
            }
        }

        /// <summary>
        /// A method that will be fired on <see cref="Invoke(HttpContext)"/>
        /// </summary>
        /// <param name="context"></param>
        /// <returns>Returns a <see cref="bool" /> to determine whether the next middleware should execute/></returns>
        protected abstract Task<bool> InvokeMiddleware(HttpContext context);

        /// <summary>
        /// Implementes a middleware pipeline
        /// </summary>
        /// <param name="next"></param>
        protected MiddlewareBase(RequestDelegate next)
        {
            this.next = next;
        }

        private readonly RequestDelegate next;

    }
}
