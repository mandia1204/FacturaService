using System;
using System.IO;
using System.Net;

namespace XmlSerializationSample.ServiceRequest
{
    public class RequestManager
    {
        public HttpWebRequest CreateWebRequest(RequestOptions opt)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(opt.Uri);
            request.Headers.Add("SOAPAction", opt.Action);
            request.Headers.Add(opt.Headers);
            request.Method = opt.Method;
            var contenType = string.Format("{0}; type=\"{1}\"; start=\"{2}\"", opt.ContentType, opt.Type, opt.Start);
            if (opt.HasAttachment)
            {
                contenType += string.Format("; boundary=\"{0}\"",opt.Boundary);
            }
            request.ContentType = contenType;

            return request;
        }

        public string GetResponse(HttpWebRequest request)
        {
            // begin async call to web request.
            IAsyncResult asyncResult = request.BeginGetResponse(null, null);
            asyncResult.AsyncWaitHandle.WaitOne();

            string soapResult ="";
            using (WebResponse webResponse = request.EndGetResponse(asyncResult))
            {
                using (var reader = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = reader.ReadToEnd();
                }
            }
            return soapResult;
        }
    }
}