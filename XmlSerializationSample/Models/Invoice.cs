using System.Collections.Generic;
using System.Xml.Serialization;
using XmlSerializationSample.Models.UBLExtensions;

namespace XmlSerializationSample.Models
{
    public class Invoice
    {
        [XmlArray("UBLExtensions", Namespace = NameSpaces.EXT)]
        [XmlArrayItem("UBLExtension", Type = typeof(UBLExtension), Namespace = NameSpaces.EXT)]
        public List<UBLExtension> UBLExtensions { get; set; }

        [XmlElement(Namespace = NameSpaces.CBC)]
        public string UBLVersionID { get; set; }
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string CustomizationID { get; set; }
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string ID { get; set; }
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string IssueDate { get; set; }
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string InvoiceTypeCode { get; set; }
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string DocumentCurrencyCode { get; set; }

        [XmlElement(Namespace = NameSpaces.CAC)]
        public Signature Signature { get; set; }

        [XmlElement(Namespace = NameSpaces.CAC)]
        public AccountingSupplierParty AccountingSupplierParty { get; set; }

        [XmlElement(Namespace = NameSpaces.CAC)]
        public AccountingCustomerParty AccountingCustomerParty { get; set; }

        [XmlElement(Namespace = NameSpaces.CAC)]
        public TaxTotal TaxTotal { get; set; }

        [XmlElement(Namespace = NameSpaces.CAC)]
        public LegalMonetaryTotal LegalMonetaryTotal { get; set; }

        [XmlElement(ElementName = "InvoiceLine", Namespace = NameSpaces.CAC)]
        public List<InvoiceLine.InvoiceLine> InvoiceLines { get; set; }
    }
}
