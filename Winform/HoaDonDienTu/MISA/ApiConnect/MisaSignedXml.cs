using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Security.Cryptography.Xml;

namespace V6ThuePost_MISA_Api
{
    public class MisaSignedXml : SignedXml
    {
        private DataObject _dataObjects = null;

        public MisaSignedXml(XmlDocument xml)
            : base(xml)
        {
        }

        public MisaSignedXml(XmlElement xmlElement)
            : base(xmlElement)
        {
        }

        public override XmlElement GetIdElement(XmlDocument doc, string id)
        {
            // check to see if it's a standard ID reference
            XmlElement idElem = base.GetIdElement(doc, id);

            if (idElem == null)
            {
                XmlNamespaceManager nsManager = new XmlNamespaceManager(doc.NameTable);
                nsManager.AddNamespace("wsu", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");

                idElem = doc.SelectSingleNode("//*[@Id=\"" + id + "\"]", nsManager) as XmlElement;
            }

            if (idElem == null)
            {

                if (_dataObjects != null)
                {
                    var xmlElement = _dataObjects.GetXml();
                    XmlNamespaceManager nsManager = new XmlNamespaceManager(doc.NameTable);
                    nsManager.AddNamespace("wsu", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
                    idElem = xmlElement.SelectSingleNode("//*[@Id=\"" + id + "\"]", nsManager) as XmlElement;

                }
            }
            return idElem;
        }
        public new void AddObject(DataObject dataObject)
        {
            base.AddObject(dataObject);
            _dataObjects = dataObject;
        }
    }

}