
using System.Xml.Serialization;

namespace XmlSerializationSample.Models
{
    public class NameSpaces
    {
        public const string DEFAULT = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2";
        public const string CAC = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2";
        public const string CBC = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2";
        public const string CCTS = "urn:un:unece:uncefact:documentation:2";
        public const string DS = "http://www.w3.org/2000/09/xmldsig#";
        public const string EXT = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2";
        public const string QDT = "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2";
        public const string SAC = "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1";
        public const string UDT = "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2";
        public const string XSI = "http://www.w3.org/2001/XMLSchema-instance";

        public const string SOAPENV = "http://schemas.xmlsoap.org/soap/envelope/";
        public const string SER = "http://service.sunat.gob.pe";
        public const string WSSE = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";

        public static XmlSerializerNamespaces GetInvoiceNamespaces()
        {
            var ns = new XmlSerializerNamespaces();
            ns.Add("cac", CAC);
            ns.Add("cbc", CBC);
            ns.Add("ccts", CCTS);
            ns.Add("ds", DS);
            ns.Add("ext", EXT);
            ns.Add("qdt", QDT);
            ns.Add("sac", SAC);
            ns.Add("udt", UDT);
            ns.Add("xsi", XSI);
            return ns;
        }

        public static XmlSerializerNamespaces GetEnvelopeNamespaces()
        {
            var ns = new XmlSerializerNamespaces();
            ns.Add("soapenv", SOAPENV);
            ns.Add("ser", SER);
            ns.Add("wsse", WSSE);
            return ns;
        }
    }
}
