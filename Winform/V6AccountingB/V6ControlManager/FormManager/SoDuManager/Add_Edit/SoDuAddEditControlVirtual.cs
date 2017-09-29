using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;

namespace V6ControlManager.FormManager.SoDuManager.Add_Edit
{
    public partial class SoDuAddEditControlVirtual : V6FormControl
    {
        protected V6Categories Categories;
        protected string _maCt;
        public V6TableName TableName { get; set; }
        protected string _table2Name, _table3Name, _table4Name;
        protected V6TableStruct _TableStruct;
        public V6Mode Mode = V6Mode.Add;
        protected DataTable data3, data4;
        /// <summary>
        /// Data đưa vào để edit.
        /// </summary>
        protected SortedDictionary<string, object> DataOld { get; set; }
        public DataTable AD{get; set; }
        
        /// <summary>
        /// Dùng khi gọi form update, chứa giá trị cũ trước khi update.
        /// </summary>
        private SortedDictionary<string, object> _keys= new SortedDictionary<string, object>();
        /// <summary>
        /// Chứa data dùng để insert hoặc edit.
        /// </summary>
        public SortedDictionary<string, object> DataDic { get; set; }
        public SortedDictionary<string, object> DataDic2 { get; set; }

        protected DataGridViewRow _gv1EditingRow;
        protected DataGridViewRow _gv2EditingRow;
        protected DataGridViewRow _gv3EditingRow;
        protected DataGridViewRow _gv4EditingRow;

        #region ==== For API Mode ====

        public string AddLink = "";
        public string EditLink = "";
        #endregion for api mode

        /// <summary>
        /// Dùng tự do, gán các propertie, field xong sẽ gọi loadAll
        /// </summary>
        public SoDuAddEditControlVirtual()
        {
            InitializeComponent();
            Categories = new V6Categories();
        }

        protected bool _call_LoadDetails_in_base = true;
        private void AddEditControlVirtual_Load(object sender, EventArgs e)
        {
            //virtual
            if (_call_LoadDetails_in_base)
            LoadDetails();
            //load truoc lop ke thua
            if (Mode == V6Mode.Add)
            {
                DoBeforeAdd();
                if (DataOld != null) DoBeforeCopy();
            }
            else if (Mode == V6Mode.Edit)
            {
                DoBeforeEdit();
            }
            else if (Mode == V6Mode.View)
            {
                DoBeforeView();
            }
            _ready0 = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName">Bảng đang xử lý</param>
        /// <param name="mode">Add/Edit/View</param>
        /// <param name="keys">Nếu data null thì load bằng keys</param>
        /// <param name="data">Gán dữ liệu này lên form</param>
        public void MyInit(V6TableName tableName, V6Mode mode,
            SortedDictionary<string, object> keys, SortedDictionary<string, object> data)
        {
            TableName = tableName;
            Mode = mode;
            _keys = keys;
            DataOld = data;
            if(Mode == V6Mode.View)  V6ControlFormHelper.SetFormControlsReadOnly(this, true);
            LoadAll();
            
        }
        
        private void LoadAll()
        {
            LoadStruct();//MaxLength...
            V6ControlFormHelper.LoadAndSetFormInfoDefine(TableName.ToString(), this, Parent);

            if (Mode==V6Mode.Edit)
            {
                if(DataOld!=null) SetData(DataOld); else LoadData();
            }
            else if(Mode == V6Mode.Add)
            {
                if (DataOld != null) SetData(DataOld);
                else
                {
                    if(_keys!=null) LoadData();
                }
            }
            else if (Mode == V6Mode.View)
            {
                if (DataOld != null)
                {
                    SetData(DataOld);
                }
                else
                {
                    if (_keys != null) LoadData();
                }
            }
        }

        /// <summary>
        /// Hàm tải dữ liệu chi tiết, gọi ở Virtual_Load
        /// Khi cần dùng sẽ viết override, không cần gọi lại!
        /// </summary>
        public virtual void LoadDetails()
        {
            //try
            //{
            //    if (DataOld != null) // All Mode
            //    {
            //        string sttRec = DataOld["STT_REC"].ToString();
            //        {
            //            string sql = "SELECT a.*,b.ten_vt as ten_vt FROM " + _table2Name +
            //                         " as a left join alvt b on a.ma_vt=b.ma_vt  Where stt_rec = @rec";
            //            SqlParameter[] plist = {new SqlParameter("@rec", sttRec)};
            //            AD = SqlConnect.ExecuteDataset(CommandType.Text, sql, plist).Tables[0];

            //            dataGridView1.DataSource = AD;
            //            dataGridView1.HideColumnsAldm(_table2Name);
            //            dataGridView1.SetCorplan2();
            //        }
            //    }
            //    else
            //    {
            //        string sttRec = "";
            //        string sql = "SELECT a.*,b.ten_vt as ten_vt FROM " + _table2Name +
            //                    " as a left join alvt b on a.ma_vt=b.ma_vt  Where stt_rec = @rec";
            //        SqlParameter[] plist = { new SqlParameter("@rec", sttRec) };
            //        AD = SqlConnect.ExecuteDataset(CommandType.Text, sql, plist).Tables[0];

            //        SetDataToGrid(dataGridView1, AD, txtMaCt.Text);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    this.ShowErrorMessage(GetType() + ".LoadDetails: " + ex.Message, "Aldmvt");
            //}
        }

        /// <summary>
        /// Gán dữ liệu và format.
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="data"></param>
        /// <param name="mact"></param>
        /// <param name="tableName">Dùng để load format HideColumns. Nếu để null sẽ tự lấy _table2Name</param>
        public void SetDataToGrid(V6ColorDataGridView dgv, object data, string mact, string tableName = null)
        {
            if (AD != null)
            {
                if (tableName == null) tableName = _table2Name;
                dgv.DataSource = data;

                var invoice = new V6InvoiceBase(mact);
                dgv.SetCorplan2();
                V6ControlFormHelper.FormatGridViewAndHeader(dgv, invoice.GRDS_AD, invoice.GRDF_AD,
                        V6Setting.IsVietnamese ? invoice.GRDHV_AD : invoice.GRDHE_AD);
                dgv.HideColumnsAldm(tableName);
            }
        }

        protected virtual void SetDefaultDetail() { }

        private void LoadStruct()
        {
            try
            {   
                _TableStruct = V6BusinessHelper.GetTableStruct(TableName.ToString());
                V6ControlFormHelper.SetFormStruct(this, _TableStruct);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Load struct eror!\n" + ex.Message);
            }

        }

        ///// <summary>
        ///// Tải thông tin tự định nghĩa lên form
        ///// </summary>
        //internal void LoadUserDefineInfo()
        //{
        //    try
        //    {
        //        var key = new SortedDictionary<string, object> {{"ma_dm", TableName.ToString()}};
        //        var selectResult = Categories.Select(V6TableName.Altt, key);
        //        V6ControlFormHelper.SetFormInfoDefine(this, selectResult.Data, V6Setting.Language);
                
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ShowErrorMessage(GetType() + ".Load info error!\n" + ex.Message);
        //    }
        //}

        private void LoadData()
        {
            try
            {
                if (_keys != null && _keys.Count > 0)
                {
                    var selectResult = Categories.Select(TableName, _keys);
                    if (selectResult.Data.Rows.Count == 1)
                    {
                        DataOld = selectResult.Data.Rows[0].ToDataDictionary();
                        SetData(DataOld);
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
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Load data error!\n" + ex.Message);
            }
        }
        
        public bool DoInsertOrUpdate(bool showMessage = true)
        {
            if (Mode==V6Mode.Edit)
            {
                try
                {
                    DataDic = GetData();
                    int b = UpdateData();
                    if (b > 0)
                    {
                        AfterUpdate();
                        return true;
                    }
                    
                    if (showMessage) ShowTopMessage(V6Text.UpdateFail);
                }
                catch (Exception e1)
                {
                    if (showMessage) this.ShowErrorException(GetType() + ".UpdateFail", e1);
                }
            }
            else if(Mode == V6Mode.Add)
            {
                try
                {
                    DataDic = GetData();
                    bool b = InsertNew();
                    if (b)
                    {
                        AfterInsert();
                    }
                    else
                    {
                        if (showMessage) ShowTopMessage(V6Text.AddFail);
                    }
                    return b;
                }
                catch (Exception ex)
                {
                    if (showMessage) this.ShowErrorException(GetType() + ".AddFail", ex);
                }
            }
            return false;
        }

        public virtual bool InsertNew()
        {
            try
            {
                FixFormData();
                DataDic = GetData();
                ValidateData();
                var result = Categories.Insert(TableName, DataDic);
                return result;
            }
            catch (Exception ex)
            {
                this.ShowInfoMessage(ex.Message);
                this.WriteExLog(GetType() + ".InsertNew", ex);
                return false;
            }
        }

        public virtual int UpdateData()
        {
            try
            {
                FixFormData();
                DataDic = GetData();
                ValidateData();
                //Lấy thêm UID từ DataEditNếu có.
                if (DataOld.ContainsKey("UID"))
                {
                    _keys["UID"] = DataOld["UID"];
                }
                var result = Categories.Update(TableName, DataDic, _keys);
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
            base.SetData(d);
            AfterSetData();
        }

        /// <summary>
        /// Chuẩn hóa lại dữ liệu trước khi xử lý.
        /// </summary>
        public virtual void FixFormData()
        {

        }

        /// <summary>
        /// Các form kế thừa cần override hàm này.
        /// Throw new [V6Categories]Exception nếu có dữ liệu sai
        /// </summary>
        /// <returns></returns>
        public virtual void ValidateData()
        {
          
        }

        /// <summary>
        /// Được gọi sau khi thêm thành công.
        /// </summary>
        public virtual void AfterInsert()
        {

        }

        /// <summary>
        /// Được gọi sau khi sửa thành công.
        /// </summary>
        public virtual void AfterUpdate()
        {

        }

        /// <summary>
        /// Hàm được gọi sau khi gán data chính(AM) lên form.
        /// </summary>
        public virtual void AfterSetData()
        {
            this.SetAllVvarBrotherFields();
        }

        /// <summary>
        /// Xử lý khi load form trường hợp sửa
        /// Dùng hàm check IsExistOneCode proc
        /// </summary>
        public virtual void DoBeforeEdit()
        {
            
        }
        public virtual void DoBeforeCopy()
        {

        }
        public virtual void DoBeforeAdd()
        {

        }
        public virtual void DoBeforeView()
        {

        }
        
    }

}
