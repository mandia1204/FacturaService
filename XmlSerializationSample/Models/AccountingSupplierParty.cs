using System.Xml.Serialization;

namespace XmlSerializationSample.Models
{
    public class AccountingSupplierParty
    {
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string CustomerAssignedAccountID { get; set; }
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string AdditionalAccountID { get; set; }

        [XmlElement(Namespace = NameSpaces.CAC)]
        public Party Party { get; set; }
    }

    public class Party
    {
        [XmlElement(Namespace = NameSpaces.CAC)]
        public PostalAddress PostalAddress { get; set; }

        [XmlElement(Namespace = NameSpaces.CAC)]
        public PartyLegalEntity PartyLegalEntity { get; set; }
    }

    public class PostalAddress
    {
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string ID { get; set; }
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string StreetName { get; set; }
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string CitySubdivisionName { get; set; }
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string CityName { get; set; }
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string CountrySubentity { get; set; }
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string District { get; set; }
        [XmlElement(Namespace = NameSpaces.CAC)]
        public Country Country { get; set; }
    }

    public class Country
    {
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string IdentificationCode { get; set; }
    }

    public class PartyLegalEntity
    {
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string RegistrationName { get; set; }
    }
}
