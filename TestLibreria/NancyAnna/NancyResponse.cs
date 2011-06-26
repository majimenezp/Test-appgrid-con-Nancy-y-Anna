using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;
using System.IO;
using System.Reactive.Linq;

namespace TestLibreria.NancyAnna
{
    public class NancyResponse:Anna.Responses.Response
    {
        private readonly byte[] body;

        public NancyResponse(Response response)
        {
            this.Headers = response.Headers;
            this.StatusCode = (int)response.StatusCode;

            //if (!this.Headers.ContainsKey("Content-Type"))
            //{
            //    this.Headers["Content-Type"] = "text/html";
            //}
            this.Headers["Content-Type"] = response.ContentType;

            // Not sure how to map the delegate to what Anna expects,
            // so grab the body contents and stick it in a string.
            // Probably not the most efficient way of doing it :-)
            var bodyStream = new MemoryStream();
            response.Contents.Invoke(bodyStream);
            this.body = bodyStream.ToArray();

            this.WriteStream = Write;
        }

        public IObservable<Stream> Write(Stream stream)
        {
            //var bytes = Encoding.UTF8.GetBytes(this.body);
            var bytes = this.body;
            return Observable.FromAsyncPattern<byte[], int, int>(stream.BeginWrite, stream.EndWrite)(bytes, 0, bytes.Length)
                .Select(u => stream);
        }
    }
}
