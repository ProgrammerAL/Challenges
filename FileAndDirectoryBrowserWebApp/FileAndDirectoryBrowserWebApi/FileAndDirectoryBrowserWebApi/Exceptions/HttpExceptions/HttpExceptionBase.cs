using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace FileAndDirectoryBrowserWebApi.Exceptions.HttpExceptions
{
    public abstract class HttpExceptionBase : Exception
    {
        protected HttpExceptionBase(string message)
            : base(message)
        {
        }

        protected HttpExceptionBase(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public abstract HttpStatusCode StatusCode { get; }
    }
}
