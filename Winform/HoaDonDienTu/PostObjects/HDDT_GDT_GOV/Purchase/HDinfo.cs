using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V6ThuePost.HDDT_GDT_GOV.Purchase
{
    public class HDinfo
    {   
        public string nbmst { get; set; }
        public int khmshdon { get; set; }
        public string khhdon { get; set; }
        public int shdon { get; set; }
        public string cqt { get; set; }
        public List<Cttkhac> cttkhac { get; set; }
        public string dvtte { get; set; }
        public string hdon { get; set; }
        public string hsgcma { get; set; }
        public string hsgoc { get; set; }
        public int hthdon { get; set; }
        public int htttoan { get; set; }
        public string id { get; set; }
        public String idtbao { get; set; }
        public String khdon { get; set; }
        public String khhdgoc { get; set; }
        public String khmshdgoc { get; set; }
        public String lhdgoc { get; set; }
        public string mhdon { get; set; }
        public String mtdiep { get; set; }
        public string mtdtchieu { get; set; }
        public string nbdchi { get; set; }
        public String nbhdktngay { get; set; }
        public String nbhdktso { get; set; }
        public String nbhdso { get; set; }
        public String nblddnbo { get; set; }
        public String nbptvchuyen { get; set; }
        public string nbstkhoan { get; set; }
        public string nbten { get; set; }
        public string nbtnhang { get; set; }
        public String nbtnvchuyen { get; set; }
        public List<Nbttkhac> nbttkhac { get; set; }
        public DateTime ncma { get; set; }
        public DateTime ncnhat { get; set; }
        public string ngcnhat { get; set; }
        public DateTime nky { get; set; }
        public string nmdchi { get; set; }
        public string nmmst { get; set; }
        public String nmstkhoan { get; set; }
        public string nmten { get; set; }
        public String nmtnhang { get; set; }
        public String nmtnmua { get; set; }
        public List<Nmttkhac> nmttkhac { get; set; }
        public DateTime ntao { get; set; }
        public DateTime ntnhan { get; set; }
        public string pban { get; set; }
        public int ptgui { get; set; }
        public String shdgoc { get; set; }
        public int tchat { get; set; }
        public DateTime tdlap { get; set; }
        public double tgia { get; set; }
        public double tgtcthue { get; set; }
        public double tgtthue { get; set; }
        public string tgtttbchu { get; set; }
        public double tgtttbso { get; set; }
        public string thdon { get; set; }
        public int thlap { get; set; }
        public List<Thttlphi> thttlphi { get; set; }
        public List<Thttltsuat> thttltsuat { get; set; }
        public string tlhdon { get; set; }
        public double? ttcktmai { get; set; }
        public int tthai { get; set; }
        public List<Ttkhac> ttkhac { get; set; }
        public int tttbao { get; set; }
        public List<Ttttkhac> ttttkhac { get; set; }
        public int ttxly { get; set; }
        public string tvandnkntt { get; set; }
        public String mhso { get; set; }
        public int ladhddt { get; set; }
        public string mkhang { get; set; }
        public string nbsdthoai { get; set; }
        public string nbdctdtu { get; set; }
        public String nbfax { get; set; }
        public String nbwebsite { get; set; }
        public string nbcks { get; set; }
        public String nmsdthoai { get; set; }
        public string nmdctdtu { get; set; }
        public String nmcmnd { get; set; }
        public String nmcks { get; set; }
        public int bhphap { get; set; }
        public String hddunlap { get; set; }
        public String gchdgoc { get; set; }
        public String tbhgtngay { get; set; }
        public String bhpldo { get; set; }
        public String bhpcbo { get; set; }
        public String bhpngay { get; set; }
        public String tdlhdgoc { get; set; }
        public String tgtphi { get; set; }
        public String unhiem { get; set; }
        public String mstdvnunlhdon { get; set; }
        public String tdvnunlhdon { get; set; }
        public String nbmdvqhnsach { get; set; }
        public String nbsqdinh { get; set; }
        public String nbncqdinh { get; set; }
        public String nbcqcqdinh { get; set; }
        public String nbhtban { get; set; }
        public String nmmdvqhnsach { get; set; }
        public String nmddvchden { get; set; }
        public String nmtgvchdtu { get; set; }
        public String nmtgvchdden { get; set; }
        public String nbtnban { get; set; }
        public String dcdvnunlhdon { get; set; }
        public String dksbke { get; set; }
        public String dknlbke { get; set; }
        public string thtttoan { get; set; }
        public string msttcgp { get; set; }
        public string cqtcks { get; set; }
        public string gchu { get; set; }
        public String kqcht { get; set; }
        public String hdntgia { get; set; }
        public String tgtkcthue { get; set; }
        public String tgtkhac { get; set; }
        public String nmshchieu { get; set; }
        public String nmnchchieu { get; set; }
        public String nmnhhhchieu { get; set; }
        public String nmqtich { get; set; }
        public String ktkhthue { get; set; }
        public String hdhhdvu { get; set; }
        public String qrcode { get; set; }
        public String ttmstten { get; set; }
        public String ladhddtten { get; set; }
        public String hdxkhau { get; set; }
        public String hdxkptquan { get; set; }
        public String hdgktkhthue { get; set; }
        public String hdonLquans { get; set; }
        public bool tthdclquan { get; set; }
        public String pdndungs { get; set; }
        public String hdtbssrses { get; set; }
        public String hdTrung { get; set; }
        public String isHDTrung { get; set; }
    }

    public class Thttlphi
    {
        public string tlphi { get; set; }
        public double? tphi { get; set; }
    }

    public class Cttkhac
    {
        public string ttruong { get; set; }
        public string kdlieu { get; set; }
        public string dlieu { get; set; }
    }

    public class Nbttkhac
    {
        public string ttruong { get; set; }
        public string kdlieu { get; set; }
        public string dlieu { get; set; }
    }

    public class Nmttkhac
    {
        public string ttruong { get; set; }
        public string kdlieu { get; set; }
        public string dlieu { get; set; }
    }

    
    public class Thttltsuat
    {
        public string tsuat { get; set; }
        public double thtien { get; set; }
        public double tthue { get; set; }
        public String gttsuat { get; set; }
    }

    public class Ttkhac
    {
        public string ttruong { get; set; }
        public string kdlieu { get; set; }
        public String dlieu { get; set; }
    }

    public class Ttttkhac
    {
        public string ttruong { get; set; }
        public string kdlieu { get; set; }
        public string dlieu { get; set; }
    }
}
