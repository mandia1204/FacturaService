using System.Net;
using XmlSerializationSample.ServiceRequest;

namespace XmlSerializationSample.Clients
{
    public class InvoiceClient
    {
        private RequestManager _requestManager { get; set; }

        public InvoiceClient(RequestManager requestManager)
        {
            _requestManager = requestManager;
        }

        public string SendBill(HttpWebRequest request)
        {
            return _requestManager.GetResponse(request); ;
        }
    }
}
