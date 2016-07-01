using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Example.Infrastructure
{
    public class ApiException : Exception
    {
        private readonly HttpStatusCode statusCode;

        public ApiException(HttpStatusCode statusCode, string message, Exception ex)
            : base(message, ex)
        {
            this.statusCode = statusCode;
        }

        public ApiException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            this.statusCode = statusCode;
        }

        public ApiException(HttpStatusCode statusCode)
        {
            this.statusCode = statusCode;
        }

        public HttpStatusCode StatusCode
        {
            get { return this.statusCode; }
        }
    }
}