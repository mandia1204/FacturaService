using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using XmlSerializationSample.Builders;
using XmlSerializationSample.Clients;
using XmlSerializationSample.Models;
using XmlSerializationSample.Models.Envelope;
using XmlSerializationSample.Repositories;
using XmlSerializationSample.ServiceRequest;
using XmlSerializationSample.Util;

namespace XmlSerializationSample.Services
{
    public class InvoiceService
    {
        private InvoiceRepository _repository;
        private InvoiceClient _client;
        private RequestManager _requestManager { get; set; }
        private SoapBuilder _soapBuilder { get; set; }
        private Serializer _serializer { get; set; }
        private Zipper _zipper { get; set; }

        public InvoiceService(InvoiceRepository repository, InvoiceClient client,
                RequestManager requestManager, SoapBuilder soapBuilder, Serializer serializer,
                Zipper zipper
        )
        {
            _repository = repository;
            _client = client;
            _requestManager = requestManager;
            _soapBuilder = soapBuilder;
            _serializer = serializer;
            _zipper = zipper;
        }

        public string SendBill(string invoiceId)
        {
            var invoice = _repository.GetInvoice(invoiceId);
            var fileName = "20100066603-01-F001-1";
            var envelope = _repository.GetEnvelope(GetEnvelopeOptions(fileName));
            var request = _requestManager.CreateWebRequest(GetRequestOptions());

            string soapStr = _serializer.Serialize(envelope, null, NameSpaces.GetEnvelopeNamespaces(), true);
            string invoiceStr = _serializer.Serialize(invoice, NameSpaces.DEFAULT, NameSpaces.GetInvoiceNamespaces());

            var zippedBytes = _zipper.Zip(fileName, invoiceStr);

            var soapBuilderRequest = GetSoapBuilderRequest(request, soapStr, zippedBytes, fileName);

            //updates the web request with encoding, soap header, file content.
            _soapBuilder.Build(soapBuilderRequest);

            return _client.SendBill(request);
        }

        #region private methods

        private SoapRequest GetSoapBuilderRequest(HttpWebRequest request, string soapStr, byte[] invoice, string fileName)
        {
            //Replace with parameter fileContent
            //var fileBytes = File.ReadAllBytes(@"C:\Users\marvin\Google Drive\Facturacion Electronica\xml\20100066603-01-F001-1.zip");

            return new SoapRequest
            {
                Request = request,
                SoapStr = soapStr,
                FileContent = invoice,
                FileName = fileName + ".zip"
            };
        }

        private EnvelopeOptions GetEnvelopeOptions(string fileName)
        {
            return new EnvelopeOptions
            {
                Username = "20100066603MODDATOS",
                Password = "moddatos",
                FileName = fileName + ".zip"
            };
        }

        private RequestOptions GetRequestOptions()
        {
            var headers = new NameValueCollection { { "MIME-Version", "1.0" }, { "Accept-Encoding", "gzip, deflate" } };
            return new RequestOptions
            {
                Method = "POST",
                ContentType = "multipart/related",
                Type = "text/xml",
                Start = "<rootpart@soapui.org>",
                Uri = "http://localhost.fiddler:8088/mockBillServicePortBinding",
                Action = "\"urn:sendBill\"",
                HasAttachment = true,
                Headers = headers,
                Boundary = _soapBuilder.GetBoundary("init")
            };
        }

        #endregion
    }
}
