﻿using System;
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
            //request.Headers.Add(HttpRequestHeader.ContentEncoding, "gzip");
            request.Method = "POST";
            request.Headers.Add("MIME-Version", "1.0");
            //"multipart/form-data; type=\"application/xop+xml;\" boundary=\"" + GetBoundary() + "\"";
            var contenType = string.Format("multipart/related; type=\"text/xml\"; start=\"<rootpart@soapui.org>\"; boundary=\"{0}\"", boundary);
            request.ContentType = contenType;

            return request;
        }

        public HttpWebRequest CreateWebRequest(RequestOptions opt)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(opt.Uri);
            request.Headers.Add("SOAPAction", opt.Action);
            request.Headers.Add(opt.Headers);
            request.Method = opt.Method;
            var contenType = string.Format("{0}; type=\"{1}\"; start=\"{3}\"", opt.ContentType, opt.Type, opt.Start);
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