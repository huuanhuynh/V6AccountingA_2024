using System;
using System.Collections.Generic;
using V6AccountingBusiness;
using System.Windows.Forms;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    public partial class ThongTinBaoHiem : V6FormControl
    {
        public SortedDictionary<string, object> DataOld;
        public SortedDictionary<string, object> DataDic { get; set; }
        public SortedDictionary<string, object> _keys = new SortedDictionary<string, object>();
        public Control button = null;
        public ThongTinBaoHiem()
        {
            InitializeComponent();
            MyInit();
            V6ControlFormHelper.SetFormControlsReadOnly(this,true);
            
        }

        public void MyInit()
        {
            V6ControlFormHelper.SetFormControlsReadOnly(this, true);
            buttonSua.Enabled = false;
            buttonNhan.Enabled = false;
            buttonHuy.Enabled = false;
        }
       
        public  void LoadData(string stt_rec)
        {
            try
            {
                var _keys = new SortedDictionary<string, object> { { "STT_REC",stt_rec } };
                if (_keys != null && _keys.Count > 0)
                {
                    var selectResult = V6BusinessHelper.Select("vprdmnsluong",_keys,"*","","");
                    if (selectResult.Data.Rows.Count == 1)
                    {
                        DataOld = selectResult.Data.Rows[0].ToDataDictionary();
                        V6ControlFormHelper.SetFormDataDictionary(this, DataOld);
                    }
                    else if (selectResult.Data.Rows.Count > 1)
                    {
                        throw new Exception("Lấy dữ liệu sai >1");
                    }
                    else
                    {
                        throw new Exception("Không lấy được dữ liệu!");
                    }
                }
                V6ControlFormHelper.SetFormControlsReadOnly(this, true);
                buttonSua.Enabled = true;
                buttonNhan.Enabled = false;
                buttonHuy.Enabled = false;
              
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".LoadData", ex);
            }
           
        }
        public  int UpdateData()
        {
            try
            {

               // FixFormData();
                DataDic = GetData();
                //ValidateData();
                //Lấy thêm UID từ DataEditNếu có.
                if (DataOld.ContainsKey("UID"))
                {
                    _keys["UID"] = DataOld["UID"];
                }
                var result = V6BusinessHelper.Update("hrgeneral2", DataDic, _keys);
                return result;
            }
            catch (Exception ex)
            {
                this.ShowInfoMessage(ex.Message);
                this.WriteExLog(GetType() + ".UpdateData", ex);
                return 0;
            }
        }
        public override void SetData(IDictionary<string, object> d)
        {

            LoadData(d["STT_REC"].ToString().Trim());

        }

        private void buttonSua_Click_1(object sender, EventArgs e)
        {
            var paren = Parent.Parent;
            button = V6ControlFormHelper.GetControlByName(paren, "btnNhan");
            button.Enabled = false;
            buttonSua.Enabled = false;
            buttonNhan.Enabled = true;
            buttonHuy.Enabled = true;
            V6ControlFormHelper.SetFormControlsReadOnly(this, false);
           
        }

        private void buttonNhan_Click_1(object sender, EventArgs e)
        {
            buttonNhan.Enabled = false;
            buttonHuy.Enabled = false;
            buttonSua.Enabled = true;
            V6ControlFormHelper.SetFormControlsReadOnly(this, true);
            UpdateData();
            button.Enabled = true;
        }

        private void buttonHuy_Click_1(object sender, EventArgs e)
        {
            buttonSua.Enabled = true;
            buttonNhan.Enabled = false;
            buttonHuy.Enabled = false;
            V6ControlFormHelper.SetFormDataDictionary(this, DataOld);
            V6ControlFormHelper.SetFormControlsReadOnly(this, true);
            button.Enabled = true;
        }

        private void txtworkdate2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
