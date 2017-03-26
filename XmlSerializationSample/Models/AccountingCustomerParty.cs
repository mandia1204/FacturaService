using System.Xml.Serialization;

namespace XmlSerializationSample.Models
{
    public class AccountingCustomerParty
    {
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string CustomerAssignedAccountID { get; set; }
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string AdditionalAccountID { get; set; }

        [XmlElement(Namespace = NameSpaces.CAC)]
        public Party Party { get; set; }
    }
}
