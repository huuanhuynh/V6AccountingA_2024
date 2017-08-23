using System;

namespace V6Structs
{
    // Cần đặt tên dạng hoa chữ đầu tiên. còn lại chữ thường.
    public enum V6TableName
    {
        None,
        Albp,
        Albpcc,
        Albpht,
        Albpts,
        Alcc,
        Alck,
        Alckm,
        Alckmct,
        Alcltg,
        Alct, 
        Alctct,
        Alcthd,
        Aldmpbct,
        Aldmpbph,
        Aldmvt,
        Aldmvtct,
        Aldvcs,
        Aldvt,
        Algia,
        Algia2,
        Algia200,
        Algiavon,
        Algiavon3,
        Algiavv,
        Alhd,
        Alhttt,
        Alhtvc,
        Alkc,
        Alkh,
        Alkhct,
        Alkho,
        Alkhtg,
        Alkmb,
        Akhungck,
        Alkmbct,
        Alkmm,
        Alkmmct,
        Alku,
        Allnx,
        Allo,
        Alloaicc,
        Alloaick,
        Alloaivc,
        Alloaivt,
        Almagd,
        Almagia,
        Almauhd,
        Alnhcc,
        Alnhdvcs,
        Alnhhd,
        Alnhkh,
        Alnhkh2,
        Alnhku,
        Alnhphi,
        Alnhtk,
        Alnhtk0,
        Alnhts,
        Alnhvt,
        Alnhvv,
        Alnhytcp,
        Acosxlt_alnhytcp,
        Acosxlsx_alnhytcp,
        Alnk,
        Alnt,
        Alnv,
        Alnvien,
        Alpb,
        Alpb1,
        Alphi,
        Alphuong,
        Alplcc,
        Alplts,
        Alqddvt,
        Alqg,
        Alql,
        Alquan,
        Alstt,
        Altd,
        Altd2,
        Altd3,
        Altgcc,
        Altgnt,
        Altgts,
        Althau,
        Althauct,
        Althue,
        Altinh,
        Altk0,
        Altk1,
        Altk2,
        Altklkku,
        Altklkvv,
        Altknh,
        Alts,
        Altt,
        Alttvt,
        Alvc,
        Alvitri,
        Alvt,
        Alvttg,
        Alvv,
        Alytcp,
        Acosxlsx_alytcp,
        Acosxlt_alytcp,
        Acosxlt_aldmvt,
        Acosxlsx_aldmvt,
        Notable,
        V6option,
        V6soft,
        Abvt,
    
     
        Abkh,
        Abtk,
        Ablo,
        V6user,
        Alct1,
        V6menu,
        Althue30,
        Alsonb,
        Am81,
        Albc,
        V_alts,
        CorpLan, CorpLan1, CorpLan2,
        V_alcc,
        V_alts01,
        V_alcc01,
        Aldm,
        Hlns,
        Abvv,
        Abhd,
        Abtd,
        Abtd2,
        Abtd3,
        Abspdd,
        ACOSXLT_ABSPDD,
        Acosxlsx_abspdd,
        Abspytcp,
        Acosxlt_abspytcp,
        Acosxlsx_abspytcp,
        Abvitri,
        Abku,
        Abphi,
        Acvv,
        Acku,
        Am71,
        Am72,
        Alloaiyt,
        Acosxlt_alloaiyt,
        Acosxlsx_alloaiyt,
        Alct2,
        Almaubc,
        Almaubcct,
        Abvvkh,
        Abhdkh,
        Abbpkh,
        Alinit,
        Alinit1,
        Alct3,
        Abntxt,
        V6help_qa,
        V6valid,
        V6lookup,
        Abnghi,
        Hrpersonal,
        Hrappfamily,
        Hrlstrelation,
        Hrimages,
        Hrlstschool,
        Prhlcong,
        Prhlnhcong,
        Hrxhlnhca,
        Hrxhlca,
        PRLICHLE,
        Hrxky,
        Prhlthuetn,
        prhlphucap,
        Prhlttbh,
        Prhltp,
        Prloailuong,
        HRLSTCONTRACTTYPE,
        Prhlloaitn,
        Hrxhltg,
        Hrlstreligion,
        Hrlstnational,
        Hrlstnationality,
        Hrlstpcs,
        Hrlstdegree,
        Hrlstcourse,
        hrlstlanguage,
        Hrlstlang_level,
        HRLSTLIVINGARR,
        Hrlstethnic,
        Alvtct1,
        Alkhct1,
        Acosxlt_alpbph,
        Acosxlt_alpbct,
        Acosxlsx_alpbph,
        Acosxlsx_alpbct,
        Alreport1,
        Alreport,
        Alnhvitri,
        Alkuct

    }

    public static class V6TableHelper
    {
        /// <summary>
        /// Đổi kiểu thành V6TableName
        /// </summary>
        /// <param name="tableName">Không phân biệt hoa thường</param>
        /// <returns></returns>
        public static V6TableName ToV6TableName(string tableName)
        {
            //Đã có hàm convert Tự động. Tên enum cần ở dạng Firstupper.
            tableName = tableName.ToLower();
            switch (tableName)
            {
                case "albp":
                    return V6TableName.Albp;
                case "Acosxlsx_aldmvt":
                    return V6TableName.Acosxlsx_aldmvt;
                case "albpcc":
                    return V6TableName.Albpcc;
                case "albpht":
                    return V6TableName.Albpht;
                case "albpts":
                    return V6TableName.Albpts;
                case "alcc":
                    return V6TableName.Alcc;
                case "alck":
                    return V6TableName.Alck;
                case "alckm":
                    return V6TableName.Alckm;
                case "alckmct":
                    return V6TableName.Alckmct;
                case "alcltg":
                    return V6TableName.Alcltg;
                case "alct":
                    return V6TableName.Alct;
                case "alctct":
                    return V6TableName.Alctct;
                case "alcthd":
                    return V6TableName.Alcthd;
                case "aldmpbct":
                    return V6TableName.Aldmpbct;
                case "aldmpbph":
                    return V6TableName.Aldmpbph;
                case "aldmvt":
                    return V6TableName.Aldmvt;
                case "Acosxlt_aldmvt":
                    return V6TableName.Acosxlt_aldmvt;
                case "aldmvtct":
                    return V6TableName.Aldmvtct;
                case "aldvcs":
                    return V6TableName.Aldvcs;
                case "aldvt":
                    return V6TableName.Aldvt;
                case "algia":
                    return V6TableName.Algia;
                case "algia2":
                    return V6TableName.Algia2;
                case "algia200":
                    return V6TableName.Algia200;
                case "algiavon":
                    return V6TableName.Algiavon;
                case "algiavon3":
                    return V6TableName.Algiavon3;
                case "algiavv":
                    return V6TableName.Algiavv;
                case "alhd":
                    return V6TableName.Alhd;
                case "alhttt":
                    return V6TableName.Alhttt;
                case "alhtvc":
                    return V6TableName.Alhtvc;
                case "alkc":
                    return V6TableName.Alkc;
                case "alkh":
                    return V6TableName.Alkh;
                case "alkho":
                    return V6TableName.Alkho;
                case "alkhtg":
                    return V6TableName.Alkhtg;
                case "alkmb":
                    return V6TableName.Alkmb;
                case "Akhungck":
                    return V6TableName.Akhungck;
                case "alkmbct":
                    return V6TableName.Alkmbct;
                case "alkmm":
                    return V6TableName.Alkmm;
                case "alkmmct":
                    return V6TableName.Alkmmct;
                case "alku":
                    return V6TableName.Alku;
                case "alkuct":
                    return V6TableName.Alkuct;
                case "allnx":
                    return V6TableName.Allnx;
                case "allo":
                    return V6TableName.Allo;
                case "alloaicc":
                    return V6TableName.Alloaicc;
                case "alloaick":
                    return V6TableName.Alloaick;
                case "alloaivc":
                    return V6TableName.Alloaivc;
                case "alloaivt":
                    return V6TableName.Alloaivt;
                case "almagd":
                    return V6TableName.Almagd;
                case "almagia":
                    return V6TableName.Almagia;
                case "almauhd":
                    return V6TableName.Almauhd;
                case "alnhcc":
                    return V6TableName.Alnhcc;
                case "alnhdvcs":
                    return V6TableName.Alnhdvcs;
                case "alnhhd":
                    return V6TableName.Alnhhd;
                case "alnhkh":
                    return V6TableName.Alnhkh;
                case "alnhkh2":
                    return V6TableName.Alnhkh2;
                case "alnhku":
                    return V6TableName.Alnhku;
                case "alnhphi":
                    return V6TableName.Alnhphi;
                case "alnhtk":
                    return V6TableName.Alnhtk;
                case "alnhtk0":
                    return V6TableName.Alnhtk0;
                case "alnhts":
                    return V6TableName.Alnhts;
                case "alnhvt":
                    return V6TableName.Alnhvt;
                case "alnhvv":
                    return V6TableName.Alnhvv;
                case "alnhytcp":
                    return V6TableName.Alnhytcp;
                case "Acosxlt_alnhytcp":
                    return V6TableName.Acosxlt_alnhytcp;
                case "Acosxlsx_alnhytcp":
                    return V6TableName.Acosxlsx_alnhytcp;
                case "alnk":
                    return V6TableName.Alnk;
                case "alnt":
                    return V6TableName.Alnt;
                case "alnv":
                    return V6TableName.Alnv;
                case "alnvien":
                    return V6TableName.Alnvien;
                case "alpb":
                    return V6TableName.Alpb;
                case "alpb1":
                    return V6TableName.Alpb1;
                case "alphi":
                    return V6TableName.Alphi;
                case "alphuong":
                    return V6TableName.Alphuong;
                case "alplcc":
                    return V6TableName.Alplcc;
                case "alplts":
                    return V6TableName.Alplts;
                case "alqddvt":
                    return V6TableName.Alqddvt;
                case "alqg":
                    return V6TableName.Alqg;
                case "alql":
                    return V6TableName.Alql;
                case "alquan":
                    return V6TableName.Alquan;
                case "alstt":
                    return V6TableName.Alstt;
                case "altd":
                    return V6TableName.Altd;
                case "altd2":
                    return V6TableName.Altd2;
                case "altd3":
                    return V6TableName.Altd3;
                case "altgcc":
                    return V6TableName.Altgcc;
                case "altgnt":
                    return V6TableName.Altgnt;
                case "altgts":
                    return V6TableName.Altgts;
                case "althau":
                    return V6TableName.Althau;
                case "althauct":
                    return V6TableName.Althauct;
                case "althue":
                    return V6TableName.Althue;
                case "altinh":
                    return V6TableName.Altinh;
                case "altk0":
                    return V6TableName.Altk0;
                case "altk1":
                    return V6TableName.Altk1;
                case "altk2":
                    return V6TableName.Altk2;
                case "altklkku":
                    return V6TableName.Altklkku;
                case "altklkvv":
                    return V6TableName.Altklkvv;
                case "altknh":
                    return V6TableName.Altknh;
                case "alts":
                    return V6TableName.Alts;
                case "altt":
                    return V6TableName.Altt;
                case "alttvt":
                    return V6TableName.Alttvt;
                case "alvc":
                    return V6TableName.Alvc;
                case "alvitri":
                    return V6TableName.Alvitri;
                case "alvt":
                    return V6TableName.Alvt;
                case "alvttg":
                    return V6TableName.Alvttg;
                case "alvv":
                    return V6TableName.Alvv;
                case "alytcp":
                    return V6TableName.Alytcp;
                case "Acosxlt_alytcp":
                    return V6TableName.Acosxlt_alytcp;
                case "Acosxlsx_alytcp":
                    return V6TableName.Acosxlsx_alytcp;
                case "v6option":
                    return V6TableName.V6option;
                case "v6soft":
                    return V6TableName.V6soft;

                case "abvt":
                    return V6TableName.Abvt;
                case "abkh":
                    return V6TableName.Abkh;
                case "abtk":
                    return V6TableName.Abtk;
                case "ablo":
                    return V6TableName.Ablo;
                case "v6user":
                    return V6TableName.V6user;
                case "v6menu":
                    return V6TableName.V6menu;
                case "alct1":
                    return V6TableName.Alct1;
                case "althue30":
                    return V6TableName.Althue30;
                case "alsonb":
                    return V6TableName.Alsonb;
                case "v_alts":
                    return V6TableName.V_alts;
                case "v_alcc":
                    return V6TableName.V_alcc;
                case "v_alts01":
                    return V6TableName.V_alts01;
                case "v_alcc01":
                    return V6TableName.V_alcc01;

                case "aldm":
                    return V6TableName.Aldm;

                case "corplan":
                    return V6TableName.CorpLan;
                case "corplan1":
                    return V6TableName.CorpLan1;
                case "corplan2":
                    return V6TableName.CorpLan2;
                case "hlns":
                    return V6TableName.Hlns;
                case "Abvv":
                    return V6TableName.Abvv;
                case "Abhd":
                    return V6TableName.Abhd;
                case "Abtd":
                    return V6TableName.Abtd;
                case "Abtd2":
                    return V6TableName.Abtd2;
                case "Abtd3":
                    return V6TableName.Abtd3;
                case "Abspdd":
                    return V6TableName.Abspdd;
                case "ACOSXLT_ABSPDD":
                    return V6TableName.ACOSXLT_ABSPDD;
                case "Acosxlsx_abspdd":
                    return V6TableName.Acosxlsx_abspdd;
                case "Abspytcp":
                    return V6TableName.Abspytcp;
                case "Acosxlt_abspytcp":
                    return V6TableName.Acosxlt_abspytcp;
                case "Acosxlsx_abspytcp":
                    return V6TableName.Acosxlsx_abspytcp;
                case "Abvitri":
                    return V6TableName.Abvitri;
                case "Abku":
                    return V6TableName.Abku;
                case "Abphi":
                    return V6TableName.Abphi;
                case "Acvv":
                    return V6TableName.Acvv;
                case "Acku":
                    return V6TableName.Acku;
                case "Abhdkh":
                    return V6TableName.Abhdkh;
                case "Prhlcong":
                    return V6TableName.Prhlcong;
                case "Prhlnhcong":
                    return V6TableName.Prhlnhcong;
                case "Hrxhlnhca":
                    return V6TableName.Hrxhlnhca;
                case "Hrxhlca":
                    return V6TableName.Hrxhlca;
                case "PRLICHLE":
                    return V6TableName.PRLICHLE;
                case "Hrxky":
                    return V6TableName.Hrxky;
                case "Prhlthuetn":
                    return V6TableName.Prhlthuetn;
                case "prhlphucap":
                    return V6TableName.prhlphucap;
                case "Prhlttbh":
                    return V6TableName.Prhlttbh;
                case "Prhltp":
                    return V6TableName.Prhltp;
                case "Prloailuong":
                    return V6TableName.Prloailuong;
                case "HRLSTCONTRACTTYPE":
                    return V6TableName.HRLSTCONTRACTTYPE;
                case "Prhlloaitn":
                    return V6TableName.Prhlloaitn;
                case "Hrxhltg":
                    return V6TableName.Hrxhltg;
                case "alreport1":
                    return V6TableName.Alreport1;
                case "alreport":
                    return V6TableName.Alreport;
                case "alnhvitri":
                    return V6TableName.Alnhvitri;
  
                default:
                    try
                    {
                        string Tablename = "" + tableName[0].ToString().ToUpper() + tableName.Substring(1);
                        return EnumConvert.FromString<V6TableName>(Tablename);
                    }
                    catch (Exception)
                    {
                        return V6TableName.Notable;
                    }
            }
        }

        #region ==== GetDefaultSortField ====

        public static string GetDefaultSortField(string tableName)
        {
            var tb = ToV6TableName(tableName);
            return GetDefaultSortField(tb);
        }

        public static string GetDefaultSortField(V6TableName name)
        {
            string result;
            switch (name)
            {
          
                case V6TableName.Albc: result = "ma_file"; break;
                case V6TableName.Albp: result = "ma_bp"; break;
                case V6TableName.Albpcc: result = "ma_bp"; break;
                case V6TableName.Albpht: result = "ma_bpht"; break;
                case V6TableName.Albpts: result = "ma_bp"; break;
                case V6TableName.Alcc: result = "so_the_cc"; break;
                case V6TableName.Alck: result = "ma_ck"; break;
                case V6TableName.Alckm: result = "ma_ck"; break;
                case V6TableName.Alckmct: result = "ma_ck"; break;
                case V6TableName.Alcltg: result = "stt"; break;
                case V6TableName.Alct: result = "ma_ct"; break;
                case V6TableName.Alctct: result = "ma_ct"; break;
                case V6TableName.Alcthd: result = "ma_hd"; break;
                case V6TableName.Aldmpbct: result = "stt"; break;
                case V6TableName.Aldmpbph: result = "ma_bpht"; break;
                case V6TableName.Aldmvt: result = "ma_bpht"; break;
                case V6TableName.Aldmvtct: result = "ma_bpht"; break;
                case V6TableName.Aldvcs: result = "ma_dvcs"; break;
                case V6TableName.Aldvt: result = "dvt"; break;
                case V6TableName.Algia: result = "ma_gia"; break;
                case V6TableName.Algia2: result = "ma_gia"; break;
                case V6TableName.Algia200: result = "ma_gia"; break;
                case V6TableName.Algiavon: result = "ma_gia"; break;
                case V6TableName.Algiavon3: result = "ma_kho"; break;
                case V6TableName.Algiavv: result = "ma_vv"; break;
                case V6TableName.Alhd: result = "ma_hd"; break;
                case V6TableName.Alhttt: result = "ma_httt"; break;
                case V6TableName.Alhtvc: result = "ma_htvc"; break;
                case V6TableName.Alkc: result = "STT"; break;
                case V6TableName.Alkh: result = "ma_kh"; break;
                case V6TableName.Alkhct: result = "dia_chi"; break;
                case V6TableName.Alkho: result = "ma_kho"; break;
                case V6TableName.Alkhtg: result = "ma_khtg"; break;
                case V6TableName.Alkmb: result = "ma_km"; break;
                case V6TableName.Akhungck: result = "ma_dvcs"; break;
                case V6TableName.Alkmbct: result = "ma_km"; break;
                case V6TableName.Alkmm: result = "ma_km"; break;
                case V6TableName.Alkmmct: result = "ma_dvcs"; break;
                case V6TableName.Alku: result = "ma_ku"; break;
                case V6TableName.Alkuct: result = "ma_ku"; break;
                case V6TableName.Allnx: result = "ma_lnx"; break;
                case V6TableName.Allo: result = "ma_vt"; break;
                case V6TableName.Alloaicc: result = "loai_cc0"; break;
                case V6TableName.Alloaick: result = "ma_loai"; break;
                case V6TableName.Alloaivc: result = "ma_loai"; break;
                case V6TableName.Alloaivt: result = "loai_vt"; break;
                case V6TableName.Almagd: result = "ma_ct_me"; break;
                case V6TableName.Almagia: result = "ma_gia"; break;
                case V6TableName.Almauhd: result = "ma_mauhd"; break;
                case V6TableName.Alnhcc: result = "ma_nh"; break;
                case V6TableName.Alnhdvcs: result = "ma_nh"; break;
                case V6TableName.Alnhhd: result = "ma_nh"; break;
                case V6TableName.Alnhkh: result = "ma_nh"; break;
                case V6TableName.Alnhkh2: result = "ma_nh"; break;
                case V6TableName.Alnhku: result = "ma_nh"; break;
                case V6TableName.Alnhphi: result = "ma_nh"; break;
                case V6TableName.Alnhtk: result = "ma_nh"; break;
                case V6TableName.Alnhtk0: result = "ma_nh"; break;
                case V6TableName.Alnhts: result = "ma_nh"; break;
                case V6TableName.Alnhvt: result = "ma_nh"; break;
                case V6TableName.Alnhvv: result = "ma_nh"; break;
                case V6TableName.Alnhytcp: result = "nhom"; break;
                case V6TableName.Acosxlt_alnhytcp: result = "nhom"; break;
                case V6TableName.Acosxlsx_alnhytcp: result = "nhom"; break;
                case V6TableName.Alnk: result = "ma_nk"; break;
                case V6TableName.Alnt: result = "ma_nt"; break;
                case V6TableName.Alnv: result = "ma_nv"; break;
                case V6TableName.Alnvien: result = "ma_nvien"; break;
                case V6TableName.Alpb: result = "stt_rec"; break;
                case V6TableName.Alpb1: result = "stt_rec"; break;
                case V6TableName.Alphi: result = "ma_phi"; break;
                case V6TableName.Alphuong: result = "ma_phuong"; break;
                case V6TableName.Alplcc: result = "ma_loai"; break;
                case V6TableName.Alplts: result = "ma_loai"; break;
                case V6TableName.Alqddvt: result = "ma_vt"; break;
                case V6TableName.Alqg: result = "ma_qg"; break;
                case V6TableName.Alql: result = "nam"; break;
                case V6TableName.Alquan: result = "ma_quan"; break;
                case V6TableName.Alstt: result = "stt_rec"; break;
                case V6TableName.Altd: result = "ma_td"; break;
                case V6TableName.Altd2: result = "ma_td2"; break;
                case V6TableName.Altd3: result = "ma_td3"; break;
                case V6TableName.Altgcc: result = "ma_tg_cc"; break;
                case V6TableName.Altgnt: result = "ma_nt"; break;
                case V6TableName.Altgts: result = "ma_tg_ts"; break;
                case V6TableName.Althau: result = "ma_thau"; break;
                case V6TableName.Althauct: result = "ma_dvcs"; break;
                case V6TableName.Althue: result = "ma_thue"; break;
                case V6TableName.Altinh: result = "ma_tinh"; break;
                case V6TableName.Altk0: result = "tk"; break;
                case V6TableName.Altk1: result = "tk"; break;
                case V6TableName.Altk2: result = "tk2"; break;
                case V6TableName.Altklkku: result = "tk_lkku"; break;
                case V6TableName.Altklkvv: result = "tk_lkvv"; break;
                case V6TableName.Altknh: result = "tknh"; break;
                case V6TableName.Alts: result = "so_the_ts"; break;
                case V6TableName.Altt: result = "ma_dm"; break;
                case V6TableName.Alttvt: result = "tt_vt"; break;
                case V6TableName.Alvc: result = "ma_vc"; break;
                case V6TableName.Alvitri: result = "ma_vitri"; break;
                case V6TableName.Alvt: result = "ma_vt"; break;
                case V6TableName.Alvttg: result = "ma_vttg"; break;
                case V6TableName.Alvv: result = "ma_vv"; break;
                case V6TableName.Alytcp: result = "ma_ytcp"; break;
                case V6TableName.Acosxlsx_alytcp: result = "ma_ytcp"; break;
                case V6TableName.Acosxlt_alytcp: result = "ma_ytcp"; break;
                case V6TableName.Acosxlt_aldmvt: result = "ma_bpht"; break;
                case V6TableName.Acosxlsx_aldmvt: result = "ma_bpht"; break;
                case V6TableName.V6option: result = "name"; break;
                case V6TableName.V6soft: result = "name"; break;
                case V6TableName.V6user: result = "user_id"; break;
                case V6TableName.Alct1: result = "ma_ct"; break;
                case V6TableName.V6menu: result = "v2id,jobid,itemid"; break;

                case V6TableName.Abkh: result = "nam"; break;
                case V6TableName.Ablo: result = "nam"; break;
                case V6TableName.Abtk: result = "nam"; break;
                case V6TableName.Abvt: result = "nam"; break;
                case V6TableName.Abntxt: result = "nam"; break;

                case V6TableName.Abspdd: result = "nam"; break;
                case V6TableName.ACOSXLT_ABSPDD:result = "nam"; break;
                case V6TableName.Acosxlsx_abspdd: result = "nam"; break;
                    
                case V6TableName.Abspytcp: result = "nam"; break;
                case V6TableName.Acosxlt_abspytcp: result = "nam"; break;
                case V6TableName.Acosxlsx_abspytcp: result = "nam"; break;
                case V6TableName.Abvitri: result = "nam"; break;
                case V6TableName.Abhd: result = "nam"; break;
                case V6TableName.Abtd: result = "nam"; break;
                case V6TableName.Abtd2: result = "nam"; break;
                case V6TableName.Abtd3: result = "nam"; break;
                case V6TableName.Abvv: result = "nam"; break;
                case V6TableName.Abphi: result = "nam"; break;
                case V6TableName.Abku: result = "nam"; break;
                case V6TableName.Acku: result = "nam"; break;
                case V6TableName.Acvv: result = "nam"; break;



                case V6TableName.Althue30: result = "ma_thue"; break;
                case V6TableName.Alsonb: result = "ma_sonb"; break;

                case V6TableName.V_alts: result = "so_the_ts"; break;
                case V6TableName.V_alcc: result = "so_the_cc"; break;
                case V6TableName.V_alts01: result = "so_the_ts"; break;
                case V6TableName.V_alcc01: result = "so_the_cc"; break;
                case V6TableName.Aldm: result = "stt"; break;

                case V6TableName.CorpLan: result = "id"; break;
                case V6TableName.CorpLan1: result = "id"; break;
                case V6TableName.CorpLan2: result = "id"; break;
                case V6TableName.Hlns: result = "ma_dvcs,ma_bpns,ma_ns"; break;
                case V6TableName.Alloaiyt: result = "loai_yt"; break;
                case V6TableName.Acosxlt_alloaiyt: result = "loai_yt"; break;
                case V6TableName.Acosxlsx_alloaiyt: result = "loai_yt"; break;
                    
                case V6TableName.Alct2: result = "ma_ct"; break;

                case V6TableName.Almaubc: result = "ma_maubc"; break;
                case V6TableName.Almaubcct: result = "mau_bc"; break;
                case V6TableName.Abvvkh: result = "ma_vv"; break;
                case V6TableName.Abbpkh: result = "ma_bp"; break;
                case V6TableName.Abhdkh: result = "ma_hd"; break;

                case V6TableName.Alinit: result = "ma_ct_me"; break;
                case V6TableName.Alct3: result = "ma_ct"; break;
                case V6TableName.V6help_qa: result = "stt"; break;
                case V6TableName.V6valid: result = "ma"; break;
                case V6TableName.V6lookup: result = "vvar"; break;
                case V6TableName.Abnghi: result = "nam"; break;
                case V6TableName.Hrpersonal: result = "order_no"; break;
                case V6TableName.Hrappfamily: result = "stt_rec"; break;
                case V6TableName.Hrlstrelation: result = "name"; break;
                case V6TableName.Hrlstschool: result = "name"; break;

                case V6TableName.Prhlcong: result = "ma_cong"; break;
                case V6TableName.Prhlnhcong: result = "ma_nhom"; break;
                case V6TableName.Hrxhlnhca: result = "ma_nhca"; break;
                case V6TableName.Hrxhlca: result = "ma_ca"; break;
                case V6TableName.PRLICHLE: result = "ma_cong"; break;
                case V6TableName.Hrxky: result = "ma_tg"; break;
                case V6TableName.Prhlthuetn: result = "thue"; break;
                case V6TableName.prhlphucap: result = "ma_pc"; break;
                case V6TableName.Prhlttbh: result = "ma_ttbh"; break;
                case V6TableName.Prhltp: result = "ma_loai"; break;
                case V6TableName.Prloailuong: result = "ma_loai_lg"; break;
                case V6TableName.HRLSTCONTRACTTYPE: result = "name"; break;
                case V6TableName.Prhlloaitn: result = "ma_loai_tn"; break;
                case V6TableName.Hrxhltg: result = "ma_tg"; break;
                case V6TableName.Hrlstreligion: result = "name"; break;
                case V6TableName.Hrlstnational: result = "name"; break;
                case V6TableName.Hrlstnationality: result = "name"; break;
                case V6TableName.Hrlstpcs: result = "name"; break;
                case V6TableName.Hrlstdegree: result = "name"; break;
                case V6TableName.Hrlstcourse: result = "name"; break;
                case V6TableName.hrlstlanguage: result = "name"; break;
                case V6TableName.Hrlstlang_level: result = "name"; break;
                case V6TableName.HRLSTLIVINGARR: result = "name"; break;
                case V6TableName.Hrlstethnic: result = "name"; break;
                case V6TableName.Alkhct1: result = "ma_kh"; break;
                case V6TableName.Alvtct1: result = "ma_vt"; break;
                case V6TableName.Acosxlt_alpbph: result = "ma_bpht"; break;
                case V6TableName.Acosxlsx_alpbph: result = "ma_bpht"; break;
                case V6TableName.Alreport1: result = "ma_bc"; break;
                case V6TableName.Alreport: result = "ma_bc"; break;
                case V6TableName.Alnhvitri: result = "ma_nh"; break;
                default:
                    result = "";
                    break;
            }
            return result;
        }

        #endregion GetDefaultSortField

        public static string V6TableCaption(string name, string lang)
        {
            return V6TableCaption(ToV6TableName(name), lang);
        }
        public static string V6TableCaption(V6TableName name, string lang)
        {
            switch (name)
            {
                case V6TableName.None:
                    return "None";

                case V6TableName.Albp:
                    return lang == "V" ? "Danh mục bộ phận" : " Department list";

                case V6TableName.Albpcc:
                    return lang == "V" ? "Danh mục bộ phận sử dụng công cụ" : " Department tools list";

                case V6TableName.Albpht:
                    return lang == "V" ? "Danh mục bộ phận hạch toán" : " Cost center list";

                case V6TableName.Albpts:
                    return lang == "V" ? "Danh mục bộ phận sử dụng tài sản cố định" : " Department FA list";

                case V6TableName.Alcc:
                    return lang == "V" ? "Danh mục công cụ" : " Tools list";

                case V6TableName.Alck:
                    return lang == "V" ? "Danh mục chiết khấu" : " Disccount list";

                case V6TableName.Alckm:
                    return lang == "V" ? "Danh mục " : "  list";

                case V6TableName.Alckmct:
                    return lang == "V" ? "Danh mục " : "  list";

                case V6TableName.Alcltg:
                    return lang == "V" ? "Danh mục " : "  list";

                case V6TableName.Alct:
                    return lang == "V" ? "Danh mục chứng từ" : "  Voucher list";

                case V6TableName.Alct1:
                    return lang == "V" ? "Danh mục chứng từ mở rộng " : " Advance list";

                case V6TableName.Alctct:
                    return lang == "V" ? "Danh mục " : "  list";

                case V6TableName.Alcthd:
                    return lang == "V" ? "Danh mục " : "  list";

                case V6TableName.Aldmpbct:
                    return lang == "V" ? "Danh mục " : "  list";

                case V6TableName.Aldmpbph:
                    return lang == "V" ? "Danh mục " : "  list";

                case V6TableName.Aldmvt:
                    return lang == "V" ? "Danh mục định mức vật tư" : " list";

                case V6TableName.Aldmvtct:
                    return lang == "V" ? "Danh mục định mức vật tư ct" : " list";
                case V6TableName.Acosxlt_aldmvt:
                    return lang == "V" ? "Danh mục định mức vật tư ct" : " list";
                case V6TableName.Acosxlsx_aldmvt:
                    return lang == "V" ? "Danh mục định mức vật tư ct SPDH" : " list";
                case V6TableName.Aldvcs:
                    return lang == "V" ? "Danh mục đơn vị cơ sở " : " Agent  list";

                case V6TableName.Aldvt:
                    return lang == "V" ? "Danh mục đơn vị tính" : "  UOM list";

                case V6TableName.Algia:
                    return lang == "V" ? "Danh mục giá vốn" : "Cost price  list";

                case V6TableName.Algia2:
                    return lang == "V" ? "Danh mục giá bán" : "  Price list";

                case V6TableName.Algia200:
                    return lang == "V" ? "Danh mục " : "  list";

                case V6TableName.Algiavon:
                    return lang == "V" ? "Danh mục " : "  list";

                case V6TableName.Algiavon3:
                    return lang == "V" ? "Danh mục " : "  list";

                case V6TableName.Algiavv:
                    return lang == "V" ? "Danh mục " : "  list";

                case V6TableName.Alhd:
                    return lang == "V" ? "Danh mục hợp đồng" : " Contract list";

                case V6TableName.Alhttt:
                    return lang == "V" ? "Danh mục hình thức thanh toán" : "Payment term  list";

                case V6TableName.Alhtvc:
                    return lang == "V" ? "Danh mục hình thức vận chuyển " : "Transport type  list";

                case V6TableName.Alkc:
                    return lang == "V" ? "Danh mục kết chuyển tự động" : "Make auto voucher  list";

                case V6TableName.Alkh:
                    return lang == "V" ? "Danh mục khách hàng/ nhà cung cấp/ nhân viên" : "Customer/ Supplier/ Employee list";
                case V6TableName.Alkhct:
                    return lang == "V" ? "Danh mục khách hàng/ nhà cung cấp/ nhân viên chi tiết" : "Customer/ Supplier/ Employee list details";
                
                case V6TableName.Alkho:
                    return lang == "V" ? "Danh mục kho hàng " : " Warehouse list";

                case V6TableName.Alkhtg:
                    return lang == "V" ? "Danh mục " : "  list";

                case V6TableName.Alkmb:
                    return lang == "V" ? "Khai báo khung khuyến mãi" : "Promotion list";
                case V6TableName.Akhungck:
                    return lang == "V" ? "Khai báo khung chiếc khấu" : "";
                case V6TableName.Alkmbct:
                    return lang == "V" ? "Khai báo khung khuyến mãi chi tiết" : "Promotion list details";

                case V6TableName.Alkmm:
                    return lang == "V" ? "Danh mục " : "  list";

                case V6TableName.Alkmmct:
                    return lang == "V" ? "Danh mục " : "  list";

                case V6TableName.Alku:
                    return lang == "V" ? "Danh mục khế ước " : "  Agreement list";
                case V6TableName.Alkuct:
                    return lang == "V" ? "Danh mục khế ước chi tiết " : "  Agreement details list";
                case V6TableName.Allnx:
                    return lang == "V" ? "Danh mục nhập xuất " : "  Type in-ount list";

                case V6TableName.Allo:
                    return lang == "V" ? "Danh mục lô hàng" : " Lot list";

                case V6TableName.Alloaicc:
                    return lang == "V" ? "Danh mục loại công cụ" : "  Type tools list";

                case V6TableName.Alloaick:
                    return lang == "V" ? "Danh mục " : "  list";

                case V6TableName.Alloaivc:
                    return lang == "V" ? "Danh mục loại dịch vụ " : "  Services list";

                case V6TableName.Alloaivt:
                    return lang == "V" ? "Danh mục loại vật tư " : "  list";

                case V6TableName.Almagd:
                    return lang == "V" ? "Danh mục mã giao dịch " : "  list";

                case V6TableName.Almagia:
                    return lang == "V" ? "Danh mục mã giá" : "  list";

                case V6TableName.Almauhd:
                    return lang == "V" ? "Danh mục mẫu hóa đơn " : "  list";

                case V6TableName.Alnhcc:
                    return lang == "V" ? "Danh mục nhóm công cụ " : "  list";

                case V6TableName.Alnhdvcs:
                    return lang == "V" ? "Danh mục đơn vị cơ sở " : "  list";
                case V6TableName.Alnhhd:
                    return lang == "V" ? "Danh mục nhóm hợp đồng" : "  list";

                case V6TableName.Alnhkh:
                    return lang == "V" ? "Danh mục nhóm khách hàng" : "  list";

                case V6TableName.Alnhkh2:
                    return lang == "V" ? "Danh mục " : "  list";

                case V6TableName.Alnhku:
                    return lang == "V" ? "Danh mục khế ước" : "  list";

                case V6TableName.Alnhphi:
                    return lang == "V" ? "Danh mục nhóm phí" : "  list";

                case V6TableName.Alnhtk:
                    return lang == "V" ? "Danh mục nhóm tài khoản" : "  list";

                case V6TableName.Alnhtk0:
                    return lang == "V" ? "Danh mục " : "  list";

                case V6TableName.Alnhts:
                    return lang == "V" ? "Danh mục nhóm tài sản " : "  list";

                case V6TableName.Alnhvt:
                    return lang == "V" ? "Danh mục nhóm vật tư " : "  list";

                case V6TableName.Alnhvv:
                    return lang == "V" ? "Danh mục nhóm vụ việc" : "  list";

                case V6TableName.Alnhytcp:
                    return lang == "V" ? "Danh mục nhóm yếu tố chi phí " : "  list";

                case V6TableName.Acosxlt_alnhytcp:
                    return lang == "V" ? "Danh mục nhóm yếu tố chi phí SXLT" : "  list";

                case V6TableName.Acosxlsx_alnhytcp:
                    return lang == "V" ? "Danh mục nhóm yếu tố chi phí SXDH" : "  list";

                case V6TableName.Alnk:
                    return lang == "V" ? "Danh mục nhật ký" : "  list";

                case V6TableName.Alnt:
                    return lang == "V" ? "Danh mục ngoại tệ" : " FC list";

                case V6TableName.Alnv:
                    return lang == "V" ? "Danh mục nguồn vốn " : "  list";

                case V6TableName.Alnvien:
                    return lang == "V" ? "Danh mục nhân viên " : " Employee list";

                case V6TableName.Alpb:
                    return lang == "V" ? "Danh mục phân bổ " : "  list";

                case V6TableName.Alpb1:
                    return lang == "V" ? "Danh mục phân bổ 1" : "  list";

                case V6TableName.Alphi:
                    return lang == "V" ? "Danh mục phí" : " Fee list";

                case V6TableName.Alphuong:
                    return lang == "V" ? "Danh mục " : "  list";

                case V6TableName.Alplcc:
                    return lang == "V" ? "Danh mục phân loại công cụ" : "  Type tool list";

                case V6TableName.Alplts:
                    return lang == "V" ? "Danh mục phân loại tài sản" : "  Type FA list";

                case V6TableName.Alqddvt:
                    return lang == "V" ? "Danh mục quy đổi đơn vị tính " : " UOM list";

                case V6TableName.Alqg:
                    return lang == "V" ? "Danh mục quốc gia " : "  Nation list";

                case V6TableName.Alql:
                    return lang == "V" ? "Danh mục " : "  list";

                case V6TableName.Alquan:
                    return lang == "V" ? "Danh mục " : "  list";

                case V6TableName.Alstt:
                    return lang == "V" ? "Danh mục khai báo hệ thống nhập liệu V6" : " System list";

                case V6TableName.Altd:
                    return lang == "V" ? "Danh mục tự định nghĩa" : "  User define name list";

                case V6TableName.Altd2:
                    return lang == "V" ? "Danh mục tự định nghĩa 2" : " User define name2  list";

                case V6TableName.Altd3:
                    return lang == "V" ? "Danh mục tự định nghĩa 3" : " User define name3 list";

                case V6TableName.Altgcc:
                    return lang == "V" ? "Danh mục tăng giảm công cụ" : "  list";


                case V6TableName.Altgnt:
                    return lang == "V" ? "Danh mục tỷ giá ngoại tệ" : "  list";

                case V6TableName.Altgts:
                    return lang == "V" ? "Danh mục tăng giảm tài sản " : "  list";

                case V6TableName.Althau:
                    return lang == "V" ? "Danh mục thầu" : "  list";

                case V6TableName.Althauct:
                    return lang == "V" ? "Danh mục thầu chi tiết" : "  list";

                case V6TableName.Althue:
                    return lang == "V" ? "Danh mục thuế suất" : " Tax list";

                case V6TableName.Altinh:
                    return lang == "V" ? "Danh mục " : "  list";

                case V6TableName.Altk0:
                    return lang == "V" ? "Danh mục tài khoản " : " Account list";

                case V6TableName.Altk1:
                    return lang == "V" ? "Danh mục " : "  list";

                case V6TableName.Altk2:
                    return lang == "V" ? "Danh mục " : "  list";

                case V6TableName.Altklkku:
                    return lang == "V" ? "Danh mục " : "  list";

                case V6TableName.Altklkvv:
                    return lang == "V" ? "Danh mục " : "  list";

                case V6TableName.Altknh:
                    return lang == "V" ? "Danh mục tài khoản ngân hàng " : " Bank of Account list";

                case V6TableName.Alts:
                    return lang == "V" ? "Danh mục tài sản cố định " : " FA list";

                case V6TableName.Altt:
                    return lang == "V" ? "Danh mục thông tin" : "  list";

                case V6TableName.Alttvt:
                    return lang == "V" ? "Danh mục tình trạng vật tư" : " Status list";

                case V6TableName.Alvc:
                    return lang == "V" ? "Danh mục vận chuyển " : "  list";

                case V6TableName.Alvitri:
                    return lang == "V" ? "Danh mục vị trí " : " Location list";

                case V6TableName.Alvt:
                    return lang == "V" ? "Danh mục vật tư/ hàng hóa" : " Items/product list";

                case V6TableName.Alvttg:
                    return lang == "V" ? "Danh mục " : "  list";

                case V6TableName.Alvv:
                    return lang == "V" ? "Danh mục vụ việc/ công trình" : " Job list";

                case V6TableName.Alytcp:
                    return lang == "V" ? "Danh mục yếu tố chi phí " : "  list";
                case V6TableName.Acosxlt_alytcp:
                    return lang == "V" ? "Danh mục yếu tố chi phí " : "  list";
                case V6TableName.Acosxlsx_alytcp:
                    return lang == "V" ? "Danh mục yếu tố chi phí SPDH" : "  list";
                case V6TableName.Notable:
                    return lang == "V" ? ".. " : " ..";

                case V6TableName.V6option:
                    return lang == "V" ? "Danh mục tham số hệ thống V6 " : " V6 Option list";

                case V6TableName.V6soft:
                    return lang == "V" ? "Danh mục cài đặt bản quyền phần mềm V6 " : "  V6 License list";

                case V6TableName.Abvt:
                    return lang == "V" ? "Tồn kho vật tư đầu kỳ " : " Beginning of Items";
                case V6TableName.Abntxt:
                    return lang == "V" ? "Vào chi tiết tồn kho nhập trước xuất trước " : " Beginning of Items FIFO";

                case V6TableName.Abkh:
                    return lang == "V" ? "Số dư công nợ đầu kỳ " : "  Beginning of Customer/Suplier";

                case V6TableName.Abtk:
                    return lang == "V" ? "Số dư tài khoản đầu kỳ " : " Beginning of Account";

                case V6TableName.Ablo:
                    return lang == "V" ? "Số dư theo lô/ hạn sử dụng " : "  Beginning of Lot";

                case V6TableName.Abvitri:
                    return lang == "V" ? "Số dư theo vị trí " : "  Beginning of Location";
                case V6TableName.V6user:
                    return lang == "V" ? "Khai báo và phân quyền người sử dụng" : " User list";

                case V6TableName.V6menu:
                    return lang == "V" ? "Khai báo tham số menu" : " Menu list";

                case V6TableName.Althue30:
                    return lang == "V" ? "Danh mục thuế suất đầu vào" : " Tax list";

                case V6TableName.Alsonb:
                    return lang == "V" ? "Danh mục số nội bộ" : " Batch No list";

                case V6TableName.Am81:
                    return lang == "V" ? "Danh mục hóa đơn" : "Invoices";

                case V6TableName.Albc:
                    return lang == "V" ? "Danh mục mẫu báo cáo" : "Report form";

                case V6TableName.V_alts:
                    return lang == "V" ? "Khai báo, xóa giảm tài sản" : "Fixed asset";
                case V6TableName.V_alcc:
                    return lang == "V" ? "Khai báo, xóa giảm công cụ" : "Tools";

                case V6TableName.V_alts01:
                    return lang == "V" ? "Khai báo thôi, xóa khấu hao tài sản" : "Fixed asset";
                case V6TableName.V_alcc01:
                    return lang == "V" ? "Khai báo thôi, xóa phân bổ công cụ" : "Tools";

                case V6TableName.Hlns:
                    return lang == "V" ? "Nhân sự" : "Hlns";

                case V6TableName.Aldm:
                    return lang == "V" ? "Quản lý Danh mục" : "List options";

                case V6TableName.Abku:
                    return lang == "V" ? "Số dư đầu kỳ theo khế ước" : ".ABKU.";
                case V6TableName.Acku:
                    return lang == "V" ? "Số phát sinh đầu kỳ theo khế ước" : ".ACKU.";
                case V6TableName.Abspdd:
                    return lang == "V" ? "Cập nhật sản phẩm dở dang" : "Product in processing";
                case V6TableName.ACOSXLT_ABSPDD:
                    return lang == "V" ? "Cập nhật sản phẩm dở dang SXLT" : "Product in processing";
                case V6TableName.Acosxlsx_abspdd:
                    return lang == "V" ? "Cập nhật sản phẩm dở dang SXDH" : "Product in processing";
                case V6TableName.Abspytcp:
                    return lang == "V" ? "Cập nhật dở dang đầu kỳ theo yếu tố" : "Beginning of Product in processing";
                case V6TableName.Acosxlt_abspytcp:
                    return lang == "V" ? "Cập nhật dở dang đầu kỳ theo yếu tố SXLT" : "Beginning of Product in processing";
                case V6TableName.Acosxlsx_abspytcp:
                    return lang == "V" ? "Cập nhật dở dang đầu kỳ theo yếu tố SXDH" : "Beginning of Product in processing";
                case V6TableName.Abtd:
                    return lang == "V" ? "Số dư đầu kỳ tự định nghĩa" : ".ABTD.";
                case V6TableName.Abtd2:
                    return lang == "V" ? "Số dư đầu kỳ tự định nghĩa 2" : ".ABTD2.";
                case V6TableName.Abtd3:
                    return lang == "V" ? "Số dư đầu kỳ tự định nghĩa 3" : ".ABTD3.";

                case V6TableName.CorpLan: return "Language";
                case V6TableName.CorpLan1: return "Language1";
                case V6TableName.CorpLan2: return "Language2";
                case V6TableName.Alloaiyt:
                    return lang == "V" ? "Danh mục loại yếu tố chi phí " : "  Type list";
                case V6TableName.Acosxlt_alloaiyt:
                    return lang == "V" ? "Danh mục loại yếu tố chi phí SXLT" : "  Type list";
                case V6TableName.Acosxlsx_alloaiyt:
                    return lang == "V" ? "Danh mục loại yếu tố chi phí SXDH" : "  Type list";
                case V6TableName.Almaubc:
                    return lang == "V" ? "Danh mục mẫu báo cáo" : "Almaubc";
                case V6TableName.Almaubcct:
                    return lang == "V" ? "Danh mục mẫu báo cáo chi tiết " : "Almaubcct";
                case V6TableName.Alinit:
                    return lang == "V" ? "Khai báo phím chức năng, ngầm định" : "Set hotkey and default";
                case V6TableName.V6help_qa:
                    return lang == "V" ? "Thư viện hỏi đáp" : "Questions-Answers";
                case V6TableName.V6valid:
                    return lang == "V" ? "Thông tin bắt buộc khi nhập liệu" : "V6valid fields";
                case V6TableName.V6lookup:
                    return lang == "V" ? "Cài đặt danh mục V6lookup" : "V6lookup setting";
                case V6TableName.Abnghi:
                    return lang == "V" ? "Cập nhật chỉ số ghi (điện,nước...)" : "Input infor";
                case V6TableName.Hrpersonal:
                    return lang == "V" ? "Danh mục nhân viên" : "Personal ";
                case V6TableName.Hrappfamily:
                    return lang == "V" ? "Thông tin gia đìng" : "Family";
                case V6TableName.Hrlstrelation:
                    return lang == "V" ? "Quan hệ" : "Relation";
                case V6TableName.Prhlcong:
                    return lang == "V" ? "Danh mục ký  hiệu mã công" : "Time card code";
                case V6TableName.Prhlnhcong:
                    return lang == "V" ? "Danh mục nhóm công" : "Time card code group";
                case V6TableName.Hrxhlnhca:
                    return lang == "V" ? "Danh mục ca làm việc" : "";
                case V6TableName.Hrxhlca:
                    return lang == "V" ? "Danh mục chi tiết ca làm việc" : "";
                case V6TableName.PRLICHLE:
                    return lang == "V" ? "Khai báo ngày nghỉ , lễ..." : "";
                case V6TableName.Hrxky:
                    return lang == "V" ? "Khai báo kì tính lương" : "";
                case V6TableName.Prhlthuetn:
                    return lang == "V" ? "Danh mục thuế thu nhập cá nhân" : "";
                case V6TableName.prhlphucap:
                    return lang == "V" ? "Danh mục các khoản phụ cấp" : "";
                case V6TableName.Prhlttbh:
                    return lang == "V" ? "Danh mục các khoản trả thay BHXH" : "";
                case V6TableName.Prhltp:
                    return lang == "V" ? "Danh mục thưởng, phạt" : "";
                case V6TableName.Prloailuong:
                    return lang == "V" ? "Danh mục các loại lương" : "";
                case V6TableName.HRLSTCONTRACTTYPE:
                    return lang == "V" ? "Danh mục hợp đồng" : "";
                case V6TableName.Prhlloaitn:
                    return lang == "V" ? "Danh mục loại thu nhập tính thuế" : "";
                case V6TableName.Hrxhltg:
                    return lang == "V" ? "Danh mục thời gian" : "";
                case V6TableName.Hrlstreligion:
                    return lang == "V" ? "Danh mục tôn giáo" : "";
                case V6TableName.Hrlstnational:
                    return lang == "V" ? "Danh mục quốc gia" : "";
                case V6TableName.Hrlstnationality:
                    return lang == "V" ? "Danh mục quốc tịch" : "";
                case V6TableName.Hrlstpcs:
                    return lang == "V" ? "Danh mục tỉnh/thành" : "";
                case V6TableName.Hrlstdegree:
                    return lang == "V" ? "Danh mục bằng cấp" : "";
                case V6TableName.Hrlstcourse:
                    return lang == "V" ? "Danh mục chuyên nghành" : "";
                case V6TableName.hrlstlanguage:
                    return lang == "V" ? "Danh mục ngôn ngữ" : "";
                case V6TableName.Hrlstlang_level:
                    return lang == "V" ? "Danh mục ngôn ngữ cao cấp" : "";
                case V6TableName.HRLSTLIVINGARR:
                    return lang == "V" ? "Danh mục điều kiện sống" : "";
                case V6TableName.Hrlstethnic:
                    return lang == "V" ? "Danh mục dân tộc" : "";
                case V6TableName.Alkhct1:
                    return lang == "V" ? "Danh mục khách hàng/ nhà cung cấp/ nhân viên chi tiết 1" : "Customer/ Supplier/ Employee list details 1";
                case V6TableName.Alvtct1:
                    return lang == "V" ? "Danh mục vật tư chi tiết 1" : "Item list details 1";
                case V6TableName.Alreport1:
                    return lang == "V" ? "Danh mục thêm mới báo cáo CT( V6 Advance)" : " Add new reports list( V6 Advance)";
                case V6TableName.Alreport:
                    return lang == "V" ? "Danh mục thêm mới báo cáo ( V6 Advance)" : " Add new reports list( V6 Advance)";
                case V6TableName.Alnhvitri:
                    return lang == "V" ? "Danh mục nhóm vị trí" : " Location list";
                default:
                    
                    return "...";
            }
        }
    }

    public static class EnumConvert
    {
        public static T FromString<T>(string value)
        {
            return (T) Enum.Parse(typeof (T), value, true);
        }
    }
}