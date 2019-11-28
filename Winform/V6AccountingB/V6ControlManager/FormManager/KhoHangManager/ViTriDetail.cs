using System;
using System.Data;

namespace V6ControlManager.FormManager.KhoHangManager
{
    public class ViTriDetail
    {
        public DataRow _rowData;
        /// <summary>
        /// Dữ liệu vật tư theo vị trí?
        /// </summary>
        public DataRow _rowDataVitriVattu { get; set; }
        //K5D509
        public string MA_VT = null;   //K5
        public string MA_KHO = null;   //K5
        public string CODE = null;  //K501
        public string DAY = null;   //A
        public string KE = null;    //A1
        public string HANG = null;  //09
        public string TYPE = null;  //
        //public DateTime _cuoiNgay;

        public ViTriDetail(DataRow row)
        {
            _rowData = row;
            string ma_vitri = row["MA_VITRI"].ToString().Trim().ToUpper();
            string maVitriShort = KhoHangHelper.GetMaVitriShort(ma_vitri);
            string maKho = row["MA_KHO"].ToString().Trim();
            string code = row["CODE"].ToString().Trim();

            MA_VITRI = ma_vitri;
            MA_VITRI_SHORT = maVitriShort;
            MA_KHO = maKho;
            CODE = code;
            DAY = KhoHangHelper.GetCodeDay_FromCode(code);
            KE = KhoHangHelper.GetCodeKe_FromCode(code);
            HANG = KhoHangHelper.GetCodeVitri_FromCode(code);
            TYPE = row["TYPE"].ToString().Trim();
        }

        public string MA_VITRI { get; set; }
        public string MA_VITRI_SHORT { get; set; }
    }
}
