using System.Xml.Serialization;

namespace XmlSerializationSample.Models.Envelope
{
    [XmlRoot(Namespace = NameSpaces.SOAPENV)]
    public class Envelope
    {
        [XmlElement(Namespace = NameSpaces.SOAPENV)]
        public Header Header { get; set; }

        [XmlElement(Namespace = NameSpaces.SOAPENV)]
        public Body Body { get; set; }
    }

    public class Header
    {
        [XmlElement(Namespace = NameSpaces.WSSE)]
        public Security Security { get; set; }
    }

    public class Security
    {
        [XmlElement(Namespace = NameSpaces.WSSE)]
        public UsernameToken UsernameToken { get; set; }
    }

    public class UsernameToken
    {
        [XmlElement(Namespace = NameSpaces.WSSE)]
        public string Username { get; set; }
        [XmlElement(Namespace = NameSpaces.WSSE)]
        public string Password { get; set; }
    }

    public class Body
    {
        [XmlElement(ElementName = "sendBill", Namespace = NameSpaces.SER)]
        public SendBill SendBill { get; set; }
    }

    public class SendBill
    {
        [XmlElement(ElementName = "fileName", Namespace ="")]
        public string FileName { get; set; }
        [XmlElement(ElementName = "contentFile", Namespace = "")]
        public string ContentFile { get; set; }
    }
}
