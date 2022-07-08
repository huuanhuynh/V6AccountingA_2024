using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;

namespace V6ThuePost_MISA_Api
{
    public class SignXmlUtil
    {
        private const string mscDateTimeFormatString = "yyyy-MM-ddTHH:mm:ss";
        /// <summary>
        /// Hàm hiển thị danh sách các chứng thư số trong X509Store cho phép người dùng chọn chứng thư số
        /// </summary>
        /// <returns></returns>
        public static X509Certificate2 GetCertificateFromStore()
        {
            X509Store oStore = new X509Store(StoreLocation.CurrentUser);
            oStore.Open(OpenFlags.OpenExistingOnly);
            X509Certificate2Collection oFilterCertificateCollection = new X509Certificate2Collection();

            //Add thêm các chứng thư khác k chứa MST
            foreach (X509Certificate2 oFilterCert in oStore.Certificates)
            {

                if (!string.IsNullOrWhiteSpace(oFilterCert.Subject) && oFilterCert.Subject.Contains("MST:") && !oFilterCertificateCollection.Contains(oFilterCert))
                {
                    oFilterCertificateCollection.Add(oFilterCert);
                }
            }

            X509Store oStoreLocalMachine = new X509Store(StoreLocation.LocalMachine);
            oStoreLocalMachine.Open(OpenFlags.OpenExistingOnly);

            foreach (X509Certificate2 oFilterCert in oStoreLocalMachine.Certificates)
            {
                //Nếu đã add chứng thư số này ở CurrentUser thì không add nữa
                if (!string.IsNullOrWhiteSpace(oFilterCert.Subject) && oFilterCert.Subject.Contains("MST:") && !oFilterCertificateCollection.Contains(oFilterCert))
                {
                    oFilterCertificateCollection.Add(oFilterCert);
                }
            }

            X509Certificate2Collection oSelectedCert = null;

            oSelectedCert = X509Certificate2UI.SelectFromCollection(oFilterCertificateCollection, "MeInvoice.vn", "Chọn chứng thư số", X509SelectionFlag.SingleSelection);

            if (oSelectedCert.Count > 0)
            {
                return oSelectedCert[0];
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Ký XML
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="cert"></param>
        /// <param name="uri"></param>
        public static void SignXml(XmlDocument xmlDoc, X509Certificate2 cert, string uri)
        {
            if (xmlDoc == null)
            {
                throw new ArgumentException("xmlDoc");
            }
            if (cert == null)
            {
                throw new ArgumentException("cert");
            }
            MisaSignedXml signedXml = new MisaSignedXml(xmlDoc);
            signedXml.Signature.Id = "seller";  //Thiết lập id cho chữ ký của người bán
            //Add the key to the SignedXml document.
            if (cert.HasPrivateKey)
            {
                signedXml.SigningKey = cert.PrivateKey;
            }
            signedXml.KeyInfo = GetCertificateKeyInfo(cert);
            //Create a reference to be signed.
            Reference reference = new Reference();
            reference.Uri = String.Format("#{0}", uri);
            //Add an enveloped transformation to the reference.
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);

            //Add the reference to the SignedXml object.
            signedXml.AddReference(reference);

            //Add thêm signingTime
            DataObject signingTimeObject = CreateSigningTimeObject();
            signedXml.AddObject(signingTimeObject);
            //add reference cho SigingTime
            Reference signingReference = new Reference();
            signingReference.Uri = "#SigningTime";
            //end of thêm signingTime

            signedXml.AddReference(signingReference);
            signedXml.ComputeSignature();

            XmlElement xmlDigitalSignature = signedXml.GetXml();
            //Append the element to the XML document.
            XmlNode signedNode = xmlDoc.SelectSingleNode("CKSNNT");

            if (signedNode == null)
            {
                signedNode = xmlDoc.SelectSingleNode("//DSCKS/NNT");
            }
            if (signedNode == null)
            {
                signedNode = xmlDoc.SelectSingleNode("//DSCKS/NBan");
            }
            if (signedNode == null)
            {
                throw new Exception("Không tìm thấy node chứa chữ ký số người bán.");
            }
            signedNode.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));
        }

        private static KeyInfo GetCertificateKeyInfo(X509Certificate2 cert)
        {
            KeyInfo certificateKeyInfo = new KeyInfo();
            KeyInfoX509Data x509Data = new KeyInfoX509Data();
            x509Data.AddCertificate(cert);


            x509Data.AddSubjectName(cert.Subject);
            certificateKeyInfo.AddClause(x509Data);
            return certificateKeyInfo;
        }

        private static DataObject CreateSigningTimeObject()
        {
            //Add thêm signingTime        
            XmlDocument document = new XmlDocument();
            var signaturePropertiesNode = document.CreateElement("SignatureProperties", SignedXml.XmlDsigNamespaceUrl);
            var signaturePropertyNode = document.CreateElement("SignatureProperty", SignedXml.XmlDsigNamespaceUrl);
            signaturePropertyNode.SetAttribute("Target", "#seller");

            var signingTimeNode = document.CreateElement("SigningTime", SignedXml.XmlDsigNamespaceUrl);
            signingTimeNode.InnerText = DateTime.Now.ToString(mscDateTimeFormatString);

            document.AppendChild(signaturePropertiesNode).AppendChild(signaturePropertyNode).AppendChild(signingTimeNode);

            DataObject signingTimeObject = new DataObject
            {
                Id = "SigningTime",
                Data = document.ChildNodes
            };
            return signingTimeObject;

        }
    }
}
