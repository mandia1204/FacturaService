using System;
using System.IO;
using System.Xml;
using XmlSerializationSample.Models;
using XmlSerializationSample.Models.Envelope;
using XmlSerializationSample.ServiceRequest;
using XmlSerializationSample.Util;
using XmlSerializationSample.Builders;

namespace XmlSerializationSample.Senders
{
    public class InvoiceSender
    {
        private string _uri { get; set; }
        private RequestManager _requestManager { get; set; }
        private SoapBuilder _builder { get; set; }

        public InvoiceSender(string uri, RequestManager requestManager, SoapBuilder builder)
        {
            _uri = uri;
            _requestManager = requestManager;
            _builder = builder;
        }

        public string Send(Envelope envelope , string fileContent, string action)
        {
            var serializer = new Serializer();
            var soap = serializer.Serialize(envelope, null, NameSpaces.GetEnvelopeNamespaces(), true);

            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(soap);

            var boundaryInit = _builder.GetBoundary("init");
            var request = _requestManager.CreateWebRequest(_uri, action, boundaryInit);

            //using (Stream stream = request.GetRequestStream())
            //{
            //    soapEnvelopeXml.Save(stream);
            //}
            //
            //var fileBytes = File.ReadAllBytes(@"C:\Users\marvin\Documents\attachmentSample.txt");
            var fileBytes = File.ReadAllBytes(@"C:\Users\marvin\Google Drive\Facturacion Electronica\xml\20100066603-01-F001-1.zip");

            var soapRequest = new SoapRequest
            {
                Request = request,
                //SoapXml = soapEnvelopeXml,
                SoapStr = soap,
                FileBytes = fileBytes,
                FileContent = fileContent,
                FileName = "20100066603-01-F001-1.zip"
            };

            _builder.Build(soapRequest);

            return _requestManager.GetResponse(request);
        }
    }
}
