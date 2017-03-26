using XmlSerializationSample.Builders;
using XmlSerializationSample.ServiceRequest;
using System;
using System.Text;
using XmlSerializationSample.Util;
using XmlSerializationSample.Services;
using XmlSerializationSample.Repositories;
using XmlSerializationSample.Clients;

namespace XmlSerializationSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var encoding = Encoding.UTF8;
            var requestManager = new RequestManager();
            var repo = new InvoiceRepository();
            var builder = new SoapBuilder(encoding);
            var serializer = new Serializer();

            var client = new InvoiceClient(requestManager);
            var service = new InvoiceService(repo, client, requestManager ,  builder, serializer);
            
            var result = service.SendBill("12345abc");
            Console.WriteLine(result);
            //Console.Read();
        }
    }
}
