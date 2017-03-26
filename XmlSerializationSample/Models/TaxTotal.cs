using System.Xml.Serialization;

namespace XmlSerializationSample.Models
{
    public class TaxTotal
    {
        [XmlElement(Namespace = NameSpaces.CBC)]
        public TaxAmount TaxAmount { get; set; }
        [XmlElement(Namespace = NameSpaces.CAC)]
        public TaxSubTotal TaxSubTotal { get; set; }
    }

    public class TaxSubTotal
    {
        [XmlElement(Namespace = NameSpaces.CBC)]
        public TaxAmount TaxAmount { get; set; }
        [XmlElement(Namespace = NameSpaces.CAC)]
        public TaxCategory TaxCategory { get; set; }
    }

    public class TaxAmount
    {
        [XmlAttribute]
        public string currencyID { get; set; }
        [XmlText]
        public double Value { get; set; }
    }

    public class TaxCategory
    {
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string TaxExemptionReasonCode { get; set; }
        [XmlElement(Namespace = NameSpaces.CAC)]
        public TaxScheme TaxScheme { get; set; }
    }

    public class TaxScheme
    {
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string ID { get; set; }
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string Name { get; set; }
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string TaxTypeCode { get; set; }
    }
}
