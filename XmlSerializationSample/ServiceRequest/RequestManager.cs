using System;
using System.IO;
using System.Net;

namespace XmlSerializationSample.ServiceRequest
{
    public class RequestManager
    {
        private string _encodingName;
        public RequestManager(string encodingName)
        {
            _encodingName = encodingName;
        }

        public HttpWebRequest CreateWebRequest(string url, string action, string boundary)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", action);
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            //User-Agent: Apache-HttpClient/4.1.1 (java 1.5)

            //request.Headers.Add(HttpRequestHeader.ContentEncoding, "gzip");
            //request.ContentType = "text/xml;charset=\"utf-16\"";
            //request.Accept = "text/xml";
            request.Method = "POST";
            request.Headers.Add("MIME-Version", "1.0");
            //multipart / related; type = "text/xml"; start = "<rootpart@soapui.org>"; boundary = "----=_Part_12_1770977271.1490462803037"
            //"multipart/form-data; type=\"application/xop+xml;\" boundary=\"" + GetBoundary() + "\"";
            //var contenType = string.Format("multipart/related; type=\"text/xml\"; charset=\"utf-16\"; start=\"<rootpart@soapui.org>\"; boundary=\"{0}\"", boundary);
            var contenType = string.Format("multipart/related; type=\"text/xml\"; start=\"<rootpart@soapui.org>\"; boundary=\"{0}\"", boundary);
            //var contentType = String.Format(contenTypeFormat, "");
            //var contenType = string.Format("multipart/related; type=\"text/xml\"; charset=\"{0}\"; start=\"<rootpart@soapui.org>\"; boundary=\"{1}\"", _encodingName,  boundary);
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