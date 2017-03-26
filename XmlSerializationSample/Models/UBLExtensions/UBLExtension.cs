using System.Collections.Generic;
using System.Xml.Serialization;

namespace XmlSerializationSample.Models.UBLExtensions
{
    public class UBLExtension
    {
        [XmlElement(Namespace = NameSpaces.EXT)]
        public ExtensionContent ExtensionContent { get; set; }
    }

    public class ExtensionContent
    {
        [XmlElement("AdditionalInformation", Type = typeof(AdditionalInformation), Namespace = NameSpaces.SAC)]
        [XmlElement("Signature", Type = typeof(Signature), Namespace = NameSpaces.DS)]
        public object Value { get; set; }
    }

    public class AdditionalInformation
    {
        [XmlElement("AdditionalMonetaryTotal", Namespace = NameSpaces.SAC)]
        public List<AdditionalMonetaryTotal> AdditionalMonetaryTotalList { get; set; }
        [XmlElement("AdditionalProperty", Namespace = NameSpaces.SAC)]
        public List<AdditionalProperty> AdditionalPropertyList { get; set; }
    }

    public class AdditionalMonetaryTotal
    {
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string ID { get; set; }

        [XmlElement(Namespace = NameSpaces.CBC)]
        public PayableAmount PayableAmount { get; set; }
    }

    public class AdditionalProperty
    {
        [XmlElement(Namespace = NameSpaces.CBC)]
        public string ID { get; set; }

        [XmlElement(Namespace = NameSpaces.CBC)]
        public string Value { get; set; }
    }

    public class Signature
    {
        [XmlAttribute("Id")]
        public string Id { get; set; }

        [XmlElement(Namespace = NameSpaces.DS)]
        public SignedInfo SignedInfo { get; set; }

        [XmlElement(Namespace = NameSpaces.DS)]
        public string SignatureValue { get; set; }

        [XmlElement(Namespace = NameSpaces.DS)]
        public KeyInfo KeyInfo { get; set; }
    }

    public class SignedInfo
    {
        [XmlElement(Namespace = NameSpaces.DS)]
        public CanonicalizationMethod CanonicalizationMethod { get; set; }

        [XmlElement(Namespace = NameSpaces.DS)]
        public SignatureMethod SignatureMethod { get; set; }

        [XmlElement(Namespace = NameSpaces.DS)]
        public Reference Reference { get; set; }
    }

    public class CanonicalizationMethod
    {
        [XmlAttribute("Algorithm")]
        public string Algorithm { get; set; }
    }

    public class SignatureMethod
    {
        [XmlAttribute("Algorithm")]
        public string Algorithm { get; set; }
    }

    public class Transform
    {
        [XmlAttribute("Algorithm")]
        public string Algorithm { get; set; }
    }

    public class DigestMethod
    {
        [XmlAttribute("Algorithm")]
        public string Algorithm { get; set; }
    }

    public class Reference
    {
        [XmlAttribute("URI")]
        public string URI { get; set; }

        [XmlArray("Transforms", Namespace = NameSpaces.DS)]
        [XmlArrayItem("Transform", Type = typeof(Transform), Namespace = NameSpaces.DS)]
        public List<Transform> Transforms { get; set; }

        [XmlElement(Namespace = NameSpaces.DS)]
        public DigestMethod DigestMethod { get; set; }
        [XmlElement(Namespace = NameSpaces.DS)]
        public string DigestValue { get; set; }
    }

    public class KeyInfo
    {
        [XmlElement(Namespace = NameSpaces.DS)]
        public X509Data X509Data { get; set; }
    }

    public class X509Data
    {
        [XmlElement(Namespace = NameSpaces.DS)]
        public string X509SubjectName { get; set; }
        [XmlElement(Namespace = NameSpaces.DS)]
        public string X509Certificate { get; set; }
    }
}
