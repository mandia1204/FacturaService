using XmlSerializationSample.Models;
using XmlSerializationSample.Models.Envelope;
using System.Collections.Generic;
using XmlSerializationSample.Models.UBLExtensions;
using XmlSerializationSample.Models.InvoiceLine;
using XmlSerializationSample.Builders;
using XmlSerializationSample.Senders;
using XmlSerializationSample.ServiceRequest;
using System;
using System.Text;
using XmlSerializationSample.Util;

namespace XmlSerializationSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var serializer = new Serializer();
            var invoice = GetFirstSample();
            var xml = serializer.Serialize(invoice, NameSpaces.DEFAULT, NameSpaces.GetInvoiceNamespaces());
            var encoding = Encoding.UTF8;

            var requestManager = new RequestManager(encoding.BodyName);
            var requestSubmitter = new SoapBuilder(encoding);
            var uri = "http://localhost.fiddler:8088/mockBillServicePortBinding";
            var action = "\"urn:sendBill\"";
            var invoiceSender = new InvoiceSender(uri, requestManager, requestSubmitter);
            var envelope = GetEnvelope();

            var result = invoiceSender.Send(envelope, xml, action);
            Console.WriteLine(result);
            //Console.Read();
        }

        private static Envelope GetEnvelope()
        {
            var envelope = new Envelope
            {
                Header = new Header
                {
                    Security = new Security
                    {
                        UsernameToken = new UsernameToken
                        {
                            Username= "20100066603MODDATOS",
                            Password= "moddatos"
                        }
                    }
                },
                Body = new Body
                {
                    SendBill = new SendBill
                    {
                        FileName = "20100066603-01-F001-1.zip",
                        ContentFile = "cid:20100066603-01-F001-1.zip"
                    }
                }
            };
            return envelope;
        }

        private static Invoice GetFirstSample()
        {
            var newInvoice = new Invoice
            {
                UBLExtensions = new List<UBLExtension>
                {
                    new UBLExtension { ExtensionContent = new ExtensionContent
                        {
                            Value = new AdditionalInformation
                                   {
                                       AdditionalMonetaryTotalList = new List<AdditionalMonetaryTotal>
                                        {
                                           new AdditionalMonetaryTotal
                                           {
                                                ID ="1001",
                                                PayableAmount = new PayableAmount
                                                {
                                                    CurrencyID="PEN",
                                                    Text= 348199.15
                                                }
                                           },
                                           new AdditionalMonetaryTotal
                                           {
                                                ID ="1003",
                                                PayableAmount = new PayableAmount
                                                {
                                                    CurrencyID="PEN",
                                                    Text= 12350
                                                }
                                           },
                                           new AdditionalMonetaryTotal
                                           {
                                                ID ="1004",
                                                PayableAmount = new PayableAmount
                                                {
                                                    CurrencyID="PEN",
                                                    Text= 30
                                                }
                                           },
                                           new AdditionalMonetaryTotal
                                           {
                                                ID ="2005",
                                                PayableAmount = new PayableAmount
                                                {
                                                    CurrencyID="PEN",
                                                    Text= 59230.51
                                                }
                                           }
                                        },
                                       AdditionalPropertyList = new List<AdditionalProperty>
                                       {
                                           new AdditionalProperty
                                            {
                                                ID="1000",
                                                Value ="CUATROCIENTOS VEINTITRES MIL DOSCIENTOS VEINTICINCO Y 00/100"
                                            }
                                       }
                                   }
                        }
                    },
                    new UBLExtension { ExtensionContent = new ExtensionContent
                        {
                            Value = new Models.UBLExtensions.Signature { Id ="SignatureSP",
                                SignedInfo = new SignedInfo
                                {
                                    CanonicalizationMethod = new CanonicalizationMethod { Algorithm="http://www.w3.org/TR/2001/REC-xml-c14n-20010315" },
                                    SignatureMethod = new SignatureMethod { Algorithm="http://www.w3.org/2000/09/xmldsig#rsa-sha1" },
                                    Reference = new Reference
                                    {
                                        URI ="",
                                        Transforms = new List<Transform> { new Transform { Algorithm = "http://www.w3.org/2000/09/xmldsig#enveloped-signature" } },
                                        DigestMethod = new DigestMethod { Algorithm="http://www.w3.org/2000/09/xmldsig#sha1" },
                                        DigestValue ="ryg5Vl+6zuSrAlgSQUYrWeaSQjk="
                                    }
                                },
                                SignatureValue ="SOiGQpmVz7hBgGjIOQNlcwyHkQLC4S7R2zBuNnOUj4KjZQb3//xNPJMRB67m8x1mpQE6pffiH85v MzYLJ9nt7MLLZXOfP+rPGfkJBmNbYxaGLj9v3qZWyyEzHFGKS+8OfVSgMsHNwZ3IqfuICzc/xo8L 7sFj+aT16IHf5TYffb0=",
                                KeyInfo = new KeyInfo
                                {
                                    X509Data = new X509Data
                                    {
                                        X509SubjectName ="1.2.840.113549.1.9.1=#161a4253554c434140534f55544845524e504552552e434f4d2e5045,CN=Juan Robles,OU=20100454523,O=SOPORTE TECNOLOGICOS EIRL,L=LIMA,ST=LIMA,C=PE",
                                        X509Certificate = "MIIESTCCAzGgAwIBAgIKWOCRzgAAAAAAIjANBgkqhkiG9w0BAQUFADAnMRUwEwYKCZImiZPyLGQB GRYFU1VOQVQxDjAMBgNVBAMTBVNVTkFUMB4XDTEwMTIyODE5NTExMFoXDTExMTIyODIwMDExMFow gZUxCzAJBgNVBAYTAlBFMQ0wCwYDVQQIEwRMSU1BMQ0wCwYDVQQHEwRMSU1BMREwDwYDVQQKEwhT T1VUSEVSTjEUMBIGA1UECxMLMjAxMDAxNDc1MTQxFDASBgNVBAMTC0JvcmlzIFN1bGNhMSkwJwYJ KoZIhvcNAQkBFhpCU1VMQ0FAU09VVEhFUk5QRVJVLkNPTS5QRTCBnzANBgkqhkiG9w0BAQEFAAOB jQAwgYkCgYEAtRtcpfBLzyajuEmYt4mVH8EE02KQiETsdKStUThVYM7g3Lkx5zq3SH5nLH00EKGC tota6RR+V40sgIbnh+Nfs1SOQcAohNwRfWhho7sKNZFR971rFxj4cTKMEvpt8Dr98UYFkJhph6Wn sniGM2tJDq9KJ52UXrlScMfBityx0AsCAwEAAaOCAYowggGGMA4GA1UdDwEB/wQEAwIE8DBEBgkq hkiG9w0BCQ8ENzA1MA4GCCqGSIb3DQMCAgIAgDAOBggqhkiG9w0DBAICAIAwBwYFKw4DAgcwCgYI KoZIhvcNAwcwHQYDVR0OBBYEFG/m6twbiRNzRINavjq+U0j/sZECMBMGA1UdJQQMMAoGCCsGAQUF BwMCMB8GA1UdIwQYMBaAFN9kHQDqWONmozw3xdNSIMFW2t+7MFkGA1UdHwRSMFAwTqBMoEqGImh0 dHA6Ly9wY2IyMjYvQ2VydEVucm9sbC9TVU5BVC5jcmyGJGZpbGU6Ly9cXHBjYjIyNlxDZXJ0RW5y b2xsXFNVTkFULmNybDB+BggrBgEFBQcBAQRyMHAwNQYIKwYBBQUHMAKGKWh0dHA6Ly9wY2IyMjYv Q2VydEVucm9sbC9wY2IyMjZfU1VOQVQuY3J0MDcGCCsGAQUFBzAChitmaWxlOi8vXFxwY2IyMjZc Q2VydEVucm9sbFxwY2IyMjZfU1VOQVQuY3J0MA0GCSqGSIb3DQEBBQUAA4IBAQBI6wJ/QmRpz3C3 rorBflOvA9DOa3GNiiB7rtPIjF4mPmtgfo2pK9gvnxmV2pST3ovfu0nbG2kpjzzaaelRjEodHvkc M3abGsOE53wfxqQF5uf/jkzZA9hbLHtE1aLKBD0Mhzc6cvI072alnE6QU3RZ16ie9CYsHmMrs+sP HMy8DJU5YrdnqHdSn2D3nhKBi4QfT/WURPOuo6DF4iWgrCyMf3eJgmGKSUN3At5fK4HSpfyURT0k boaJKNBgQwy0HhGh5BLM7DsTi/KwfdUYkoFgrY71Pm23+ra+xTow1Vk9gj5NqrlpMY5gAVQXEIo1 ++GxDtaK/5EiVKSqzJ6geIfz"
                                    }
                                }
                            }
                        }
                    }
                },
                UBLVersionID = "2.0",
                CustomizationID = "1.0",
                ID= "F001-4355",
                IssueDate = "2012-03-14",
                InvoiceTypeCode ="01",
                DocumentCurrencyCode = "PEN",
                Signature = new Models.Signature
                {
                      ID = "IDSignSP"
                    , SignatoryParty = new SignatoryParty
                    {
                        PartyIdentification = new PartyIdentification { ID= "20100454523" },
                        PartyName = new PartyName { Name= "SOPORTE TECNOLOGICO EIRL" }
                    },
                    DigitalSignatureAttachment = new DigitalSignatureAttachment
                    {
                        ExternalReference = new ExternalReference
                        {
                            URI = "#SignatureSP"
                        }
                    }
                },
                AccountingSupplierParty = new AccountingSupplierParty
                {
                    CustomerAssignedAccountID = "20100454523",
                    AdditionalAccountID = "6",
                    Party = new Party
                    {
                        PostalAddress = new PostalAddress
                        {
                            ID= "150111",
                            StreetName= "AV. LOS PRECURSORES #1245",
                            CitySubdivisionName="URB. MIGUEL GRAU",
                            CityName = "LIMA",
                            CountrySubentity = "LIMA",
                            District = "EL AGUSTINO",
                            Country = new Country
                            {
                                IdentificationCode ="PE"
                            }
                        },
                        PartyLegalEntity = new PartyLegalEntity
                        {
                            RegistrationName= "SOPORTE TECNOLOGICOS EIRL"
                        }
                    }
                },
                AccountingCustomerParty = new AccountingCustomerParty
                {
                    CustomerAssignedAccountID = "20587896411",
                    AdditionalAccountID ="6",
                    Party = new Party
                    {
                        PartyLegalEntity = new PartyLegalEntity
                        {
                            RegistrationName= "SERVICABINAS S.A."
                        }
                    }
                },
                TaxTotal = new TaxTotal
                {
                    TaxAmount = new TaxAmount
                    {
                        currencyID="PEN",
                         Value= 62675.85
                    },
                    TaxSubTotal = new TaxSubTotal
                    {
                        TaxAmount = new TaxAmount
                        {
                            currencyID = "PEN",
                            Value = 62675.85
                        },
                        TaxCategory = new TaxCategory
                        {
                            TaxScheme = new TaxScheme
                            {
                                ID= "1000",
                                Name= "IGV",
                                TaxTypeCode = "VAT"
                            }
                        }
                    }
                },
                LegalMonetaryTotal = new LegalMonetaryTotal
                {
                    PayableAmount = new PayableAmount { CurrencyID="PEN", Text= 423225.00}
                },
                InvoiceLines = new List<InvoiceLine>
                {
                    new InvoiceLine
                    {
                        ID="1",
                        InvoicedQuantity = new InvoicedQuantity
                        {
                            unitCode ="NIU",
                            Value=2000
                        },
                        LineExtensionAmount = new LineExtensionAmount
                        {
                            currencyID="PEN",
                            Value=149491.53
                        },
                        PricingReference = new PricingReference
                        {
                            AlternativeConditionPrice = new AlternativeConditionPrice
                            {
                                PriceAmount = new PriceAmount
                                {
                                    currencyID="PEN",
                                    Value=98
                                },
                                PriceTypeCode ="01"
                            }
                        },
                        TaxTotal = new TaxTotal
                        {
                            TaxAmount = new TaxAmount
                            {
                                currencyID="PEN",
                                Value= 26908.47
                            },
                            TaxSubTotal = new TaxSubTotal
                            {
                                TaxAmount = new TaxAmount
                                {
                                    currencyID = "PEN",
                                    Value = 26908.47
                                },
                                TaxCategory = new TaxCategory
                                {
                                    TaxExemptionReasonCode ="10",
                                    TaxScheme = new TaxScheme
                                    {
                                        ID= "1000",
                                        Name= "IGV",
                                        TaxTypeCode = "VAT"
                                    }
                                }
                            }
                           
                        }, Item = new Item
                        {
                            Description ="Grabadora LG Externo Modelo: GE20LU10",
                            SellersItemIdentification = new SellersItemIdentification
                            {
                                ID="GLG199"
                            }
                        },Price = new Price
                        {
                            PriceAmount = new PriceAmount
                            {
                                currencyID = "PEN",
                                Value = 83.05
                            }
                        }
                    },
                    new InvoiceLine
                    {
                        ID="2",
                        InvoicedQuantity = new InvoicedQuantity
                        {
                            unitCode ="NIU",
                            Value=300
                        },
                        LineExtensionAmount = new LineExtensionAmount
                        {
                            currencyID="PEN",
                            Value=133983.05
                        },
                        PricingReference = new PricingReference
                        {
                            AlternativeConditionPrice = new AlternativeConditionPrice
                            {
                                PriceAmount = new PriceAmount
                                {
                                    currencyID="PEN",
                                    Value=620.00
                                },
                                PriceTypeCode ="01"
                            }
                        },
                        TaxTotal = new TaxTotal
                        {
                            TaxAmount = new TaxAmount
                            {
                                currencyID="PEN",
                                Value= 24116.95
                            },
                            TaxSubTotal = new TaxSubTotal
                            {
                                TaxAmount = new TaxAmount
                                {
                                    currencyID = "PEN",
                                    Value = 24116.95
                                },
                                TaxCategory = new TaxCategory
                                {
                                    TaxExemptionReasonCode ="10",
                                    TaxScheme = new TaxScheme
                                    {
                                        ID= "1000",
                                        Name= "IGV",
                                        TaxTypeCode = "VAT"
                                    }
                                }
                            }

                        }, Item = new Item
                        {
                            Description ="Monitor LCD ViewSonic VG2028WM 20",
                            SellersItemIdentification = new SellersItemIdentification
                            {
                                ID="MVS546"
                            }
                        },Price = new Price
                        {
                            PriceAmount = new PriceAmount
                            {
                                currencyID = "PEN",
                                Value = 525.42
                            }
                        }
                    },
                }
            };

            return newInvoice;
        }
    }
}
