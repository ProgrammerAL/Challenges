using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace FileAndDirectoryBrowserWebApi.Exceptions.HttpExceptions
{
    [Serializable]
    public class NotFoundException : HttpExceptionBase
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public NotFoundException(string message)
            : base(message)
        { 
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
        }
    }
}
