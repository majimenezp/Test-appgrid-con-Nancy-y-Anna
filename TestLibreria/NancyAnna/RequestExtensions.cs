using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reactive.Linq;
using Nancy.IO;

namespace TestLibreria.NancyAnna
{
    public static class RequestExtensions
    {
        public static Nancy.Request ToNancyRequest(this Anna.Request.Request request)
        {
            Nancy.Request rq = new Nancy.Request(
                request.HttpMethod,
                request.Url.AbsolutePath,
                request.Headers,
                new RequestStream(request.InputStream, 0, true),
                "http");
            return rq;
        }
    }
}
