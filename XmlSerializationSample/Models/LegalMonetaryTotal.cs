using System.Xml.Serialization;

namespace XmlSerializationSample.Models
{
    public class LegalMonetaryTotal
    {
        [XmlElement(Namespace = NameSpaces.CBC)]
        public PayableAmount PayableAmount { get; set; }
    }

    public class PayableAmount
    {
        [XmlAttribute("currencyID")]
        public string CurrencyID { get; set; }

        [XmlText]
        public double Text { get; set; }
    }
}
