using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using V6Tools.V6Convert;

namespace V6ThuePost.HDDT_GDT_GOV
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class CanonicalizationMethod
    {
        [JsonProperty("@Algorithm")]
        public string Algorithm { get; set; }
    }

    public class CQT : AutoXml
    {
        public CQT(XmlNode node) : base(node)
        {
            foreach (XmlNode item in node.ChildNodes)
            {
                if (item.Name == nameof(Signature))
                {
                    Signature = new Signature(); // !!!
                }
            }
        }

        public Signature Signature { get; set; }
    }

    public class DigestMethod
    {
        [JsonProperty("@Algorithm")]
        public string Algorithm { get; set; }
    }

    
    public class DLHDon
    {
        public DLHDon(XmlNode node)
        {
            Id = node.Attributes["Id"].Value;
            foreach (XmlNode item in node.ChildNodes)
            {
                if (item.Name == "TTChung")
                {
                    TTChung = new TTChung(item);
                }
                else if (item.Name == "NDHDon")
                {
                    NDHDon = new NDHDon(item);
                }
                else if (item.Name == "TTKhac")
                {
                    TTKhac = new TTKhac(item);
                }
            }
        }

        [JsonProperty("@Id")]
        public string Id { get; set; }
        public TTChung TTChung { get; set; }
        public NDHDon NDHDon { get; set; }
        public TTKhac TTKhac { get; set; }
    }

    public class DSCKS : AutoXml
    {
        public DSCKS(XmlNode node) : base(node)
        {
            foreach (XmlNode item in node.ChildNodes)
            {
                if (item.Name == nameof(NBan))
                {
                    NBan = new NBan(item);
                }
                else if (item.Name == nameof(CQT))
                {
                    CQT = new CQT(item);
                }
            }
        }

        public NBan NBan { get; set; }
        public CQT CQT { get; set; }
    }

    public class DSHHDVu
    {
        private XmlNode node;

        public DSHHDVu(XmlNode node)
        {
            this.node = node;
            HHDVu = new List<HHDVu>();
            foreach (XmlNode item in node.ChildNodes)
            {
                if (item.Name == "HHDVu")
                {
                    HHDVu.Add(new HHDVu(item));
                }
            }
        }

        public List<HHDVu> HHDVu { get; set; }
    }

    public class HHDVu : AutoXml
    {
        public HHDVu(XmlNode node) : base(node)
        {
            foreach (XmlNode item in node.ChildNodes)
            {
                if (item.Name == "TTKhac")
                {
                    TTKhac = new TTKhac(item);
                }
            }
        }

        public string TChat { get; set; }
        public int STT { get; set; }
        public string MHHDVu { get; set; }
        public string THHDVu { get; set; }
        public string DVTinh { get; set; }
        public decimal SLuong { get; set; }
        public decimal DGia { get; set; }
        public decimal TLCKhau { get; set; }
        public decimal STCKhau { get; set; }
        public decimal ThTien { get; set; }
        public string TSuat { get; set; }
        public TTKhac TTKhac { get; set; }
    }

    public class KeyInfo
    {
        public X509Data X509Data { get; set; }
    }

    public class LTSuat : AutoXml
    {
        public LTSuat(XmlNode node) : base(node)
        {
        }

        public string TSuat { get; set; }
        public string ThTien { get; set; }
        public string TThue { get; set; }
    }

    public class MCCQT : AutoXml
    {
        public MCCQT(XmlNode node) : base(node)
        {
        }

        [JsonProperty("@Id")]
        public string Id { get; set; }

        [JsonProperty("#text")]
        public string text { get; set; }
    }

    public class NBan : AutoXml
    {
        public NBan(XmlNode node) : base(node)
        {
            foreach (XmlNode item in node.ChildNodes)
            {
                if (item.Name == nameof(TTKhac))
                {
                    TTKhac = new TTKhac(item);
                }
                else if (item.Name == nameof(Signature))
                {
                    Signature = new Signature(); // !!!!!
                }
            }
        }

        public string Ten { get; set; }
        public string MST { get; set; }
        public string DChi { get; set; }
        public string SDThoai { get; set; }
        public string STKNHang { get; set; }
        public string TNHang { get; set; }
        public string Fax { get; set; }
        public TTKhac TTKhac { get; set; }
        public Signature Signature { get; set; }
    }

    public class NDHDon
    {
        private XmlNode node;

        public NDHDon(XmlNode node)
        {
            this.node = node;
            foreach (XmlNode item in node.ChildNodes)
            {
                if (item.Name == "NBan")
                {
                    NBan = new NBan(item);
                }
                else if (item.Name == "NMua")
                {
                    NMua = new NMua(item);
                }
                else if (item.Name == "DSHHDVu")
                {
                    DSHHDVu = new DSHHDVu(item);
                }
                else if (item.Name == "TToan")
                {

                }
            }
        }

        public NBan NBan { get; set; }
        public NMua NMua { get; set; }
        public DSHHDVu DSHHDVu { get; set; }
        public TToan TToan { get; set; }
    }

    public class NMua : AutoXml
    {
        public NMua(XmlNode node) : base(node)
        {
            foreach (XmlNode item in node.ChildNodes)
            {
                if (item.Name == nameof(TTKhac))
                {
                    TTKhac = new TTKhac(item);
                }
            }
        }

        public string Ten { get; set; }
        public string MST { get; set; }
        public string DChi { get; set; }
        public string MKHang { get; set; }
        public TTKhac TTKhac { get; set; }
    }

    public class Object_
    {
        [JsonProperty("@Id")]
        public string Id { get; set; }
        public SignatureProperties SignatureProperties { get; set; }
    }

    public class Reference
    {
        [JsonProperty("@URI")]
        public string URI { get; set; }
        public Transforms Transforms { get; set; }
        public DigestMethod DigestMethod { get; set; }
        public string DigestValue { get; set; }
    }

    public class HDon
    {
        public HDon(XmlDocument doc)
        {
            if (doc.ChildNodes != null && doc.ChildNodes.Count > 0)
            foreach (XmlNode item in doc.ChildNodes[0].ChildNodes)
            {
                if (item.Name == nameof(DLHDon))
                {
                    DLHDon = new DLHDon(item);
                }
                if (item.Name == nameof(MCCQT))
                {
                    MCCQT = new MCCQT(item);
                }
                if (item.Name == nameof(DSCKS))
                {
                    DSCKS = new DSCKS(item);
                }
            }
        }

        public DLHDon DLHDon { get; set; }
        public MCCQT MCCQT { get; set; }
        public string DLQRCode { get; set; }
        public DSCKS DSCKS { get; set; }
    }

    public class Signature
    {
        [JsonProperty("@xmlns")]
        public string xmlns { get; set; }

        [JsonProperty("@Id")]
        public string Id { get; set; }
        public SignedInfo SignedInfo { get; set; }
        public string SignatureValue { get; set; }
        public KeyInfo KeyInfo { get; set; }
        public Object_ Object { get; set; }
    }

    public class SignatureMethod
    {
        [JsonProperty("@Algorithm")]
        public string Algorithm { get; set; }
    }

    public class SignatureProperties
    {
        public SignatureProperty SignatureProperty { get; set; }
    }

    public class SignatureProperty
    {
        [JsonProperty("@Target")]
        public string Target { get; set; }
        public DateTime SigningTime { get; set; }
    }

    public class SignedInfo
    {
        public CanonicalizationMethod CanonicalizationMethod { get; set; }
        public SignatureMethod SignatureMethod { get; set; }
        public List<Reference> Reference { get; set; }
    }

    public class THTTLTSuat : AutoXml
    {
        public THTTLTSuat(XmlNode node) : base(node)
        {
            foreach (XmlNode item in node.ChildNodes)
            {
                if (item.Name == "LTSuat")
                {
                    LTSuat = new LTSuat(item);
                }
            }
        }

        public LTSuat LTSuat { get; set; }
    }

    public class Transform
    {
        [JsonProperty("@Algorithm")]
        public string Algorithm { get; set; }
    }

    public class Transforms
    {
        public Transform Transform { get; set; }
    }

    public class TTChung : AutoXml
    {
        public TTChung(XmlNode node) : base(node)
        {
            foreach (XmlNode item in node.ChildNodes)
            {
                if (item.Name == nameof(TTKhac))
                {
                    TTKhac = new TTKhac(item);
                }
            }
        }
        /// <summary>
        /// Phiên bản
        /// </summary>
        public string PBan { get; set; }
        /// <summary>
        /// Tên hóa đơn
        /// </summary>
        public string THDon { get; set; }
        /// <summary>
        /// Ký hiệu mẫu số HD
        /// </summary>
        public string KHMSHDon { get; set; }
        /// <summary>
        /// Ký hiệu
        /// </summary>
        public string KHHDon { get; set; }
        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string SHDon { get; set; }
        /// <summary>
        /// Ngày lập
        /// </summary>
        public DateTime NLap { get; set; }
        /// <summary>
        /// Đơn vị tiền tệ
        /// </summary>
        public string DVTTe { get; set; }
        /// <summary>
        /// Tỷ giá
        /// </summary>
        public decimal TGia { get; set; }
        public string HTTToan { get; set; }
        public string MSTTCGP { get; set; }
        public TTKhac TTKhac { get; set; }
    }

    public class TTin : AutoXml
    {
        public TTin(XmlNode node) : base(node)
        {
        }

        public string TTruong { get; set; }
        public string KDLieu { get; set; }
        public string DLieu { get; set; }
    }

    public class TTKhac
    {
        private XmlNode node;

        public TTKhac(XmlNode node)
        {
            this.node = node;
            TTin = new List<TTin>();
            foreach (XmlNode item in node.ChildNodes)
            {
                TTin.Add(new TTin(item));
            }
        }

        public List<TTin> TTin { get; set; }
    }

    public class TToan : AutoXml
    {
        public TToan(XmlNode node) : base(node)
        {
            foreach (XmlNode item in node.ChildNodes)
            {
                if (item.Name == "THTTLTSuat")
                {
                    THTTLTSuat = new THTTLTSuat(item);
                }
                else if (item.Name == "TTKhac")
                {
                    TTKhac = new TTKhac(item);
                }
            }
        }

        public THTTLTSuat THTTLTSuat { get; set; }
        public string TgTCThue { get; set; }
        public string TgTThue { get; set; }
        public string TTCKTMai { get; set; }
        public string TgTTTBSo { get; set; }
        public string TgTTTBChu { get; set; }
        public TTKhac TTKhac { get; set; }
    }

    public class X509Data : AutoXml
    {
        public X509Data(XmlNode node) : base(node)
        {
        }

        public string X509SubjectName { get; set; }
        public string X509Certificate { get; set; }
    }





}
