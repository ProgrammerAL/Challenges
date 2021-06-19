using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FileAndDirectoryBrowserWebApi.Exceptions.HttpExceptions;

using Microsoft.AspNetCore.Http;

namespace FileAndDirectoryBrowserWebApi.Middleware
{
    public class HttpExceptionResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpExceptionResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (HttpExceptionBase httpExceptionBase)
            {
                var response = httpContext.Response;
                response.StatusCode = (int)httpExceptionBase.StatusCode;
                await response.WriteAsync(httpExceptionBase.Message);
            }
        }
    }
}
