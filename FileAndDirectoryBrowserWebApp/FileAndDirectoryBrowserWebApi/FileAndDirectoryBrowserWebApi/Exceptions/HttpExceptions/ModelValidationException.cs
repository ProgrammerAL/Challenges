using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace FileAndDirectoryBrowserWebApi.Exceptions.HttpExceptions
{
    public class ModelValidationException : HttpExceptionBase
    {
        public ModelValidationException(string message)
            : base(message)
        {
        }

        protected ModelValidationException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
        }
        
        public override HttpStatusCode StatusCode => HttpStatusCode.UnprocessableEntity;
    }
}
