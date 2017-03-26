using System.Xml.Serialization;

namespace XmlSerializationSample.Models.InvoiceLine
{
    public class InvoiceLine
    {
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string ID { get; set; }

        [XmlElement(Namespace = NameSpaces.CBC)]
        public InvoicedQuantity InvoicedQuantity { get; set; }

        [XmlElement(Namespace = NameSpaces.CBC)]
        public LineExtensionAmount LineExtensionAmount { get; set; }

        [XmlElement(Namespace = NameSpaces.CAC)]
        public PricingReference PricingReference { get; set; }

        [XmlElement(Namespace = NameSpaces.CAC)]
        public TaxTotal TaxTotal { get; set; }

        [XmlElement(Namespace = NameSpaces.CAC)]
        public Item Item { get; set; }

        [XmlElement(Namespace = NameSpaces.CAC)]
        public Price Price { get; set; }
    }

    public class InvoicedQuantity
    {
        [XmlAttribute()]
        public string unitCode { get; set; }
        [XmlText()]
        public int Value { get; set; }
    }

    public class LineExtensionAmount
    {
        [XmlAttribute()]
        public string currencyID { get; set; }
        [XmlText()]
        public double Value { get; set; }
    }

    public class PricingReference
    {
        [XmlElement(Namespace = NameSpaces.CAC)]
        public AlternativeConditionPrice AlternativeConditionPrice { get; set; }
    }

    public class AlternativeConditionPrice
    {
        [XmlElement(Namespace = NameSpaces.CBC)]
        public PriceAmount PriceAmount { get; set; }
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string PriceTypeCode { get; set; }
    }

    public class Item
    {
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string Description { get; set; }
        [XmlElement(Namespace = NameSpaces.CAC)]
        public SellersItemIdentification SellersItemIdentification { get; set; }
    }

    public class SellersItemIdentification
    {
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string ID { get; set; }
    }

    public class Price
    {
        [XmlElement(Namespace = NameSpaces.CBC)]
        public PriceAmount PriceAmount { get; set; }
    }

    public class PriceAmount
    {
        [XmlAttribute()]
        public string currencyID { get; set; }
        [XmlText()]
        public double Value { get; set; }
    }
}
