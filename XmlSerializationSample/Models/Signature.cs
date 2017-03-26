using System.Xml.Serialization;

namespace XmlSerializationSample.Models
{
    public class Signature
    {
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string ID { get; set; }

        [XmlElement(Namespace = NameSpaces.CAC)]
        public SignatoryParty SignatoryParty { get; set; }

        [XmlElement(Namespace = NameSpaces.CAC)]
        public DigitalSignatureAttachment DigitalSignatureAttachment { get; set; }
    }

    public class SignatoryParty
    {
        [XmlElement(Namespace = NameSpaces.CAC)]
        public PartyIdentification PartyIdentification { get; set; }

        [XmlElement(Namespace = NameSpaces.CAC)]
        public PartyName PartyName { get; set; }
    }

    public class PartyIdentification
    {
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string ID { get; set; }
    }

    public class PartyName
    {
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string Name { get; set; }
    }

    public class DigitalSignatureAttachment
    {
        [XmlElement(Namespace = NameSpaces.CAC)]
        public ExternalReference ExternalReference { get; set; }
    }

    public class ExternalReference
    {
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string URI { get; set; }
    }
}
