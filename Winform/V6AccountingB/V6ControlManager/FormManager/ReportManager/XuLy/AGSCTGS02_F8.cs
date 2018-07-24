using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class AGSCTGS02_F8 : V6FormControl
    {
        #region Biến toàn cục

        protected DataRow _am;
        protected string _numlist, _text, _program;
        protected int _year, _month1, _month2;

        //protected string _reportFileF5, _reportTitleF5, _reportTitle2F5;
        public delegate void HandleF4Success();

        public event HandleF4Success UpdateSuccessEvent;

        protected DataSet _ds;
        protected DataTable _tbl, _tbl2;
        //private V6TableStruct _tStruct;
        /// <summary>
        /// Dùng cho procedure chính (program?)
        /// </summary>
        protected List<SqlParameter> _pList;

        public bool ViewDetail { get; set; }
        
        
        #endregion 

        #region ==== Properties ====

       
        #endregion properties
        public AGSCTGS02_F8()
        {
            InitializeComponent();
        }

        public AGSCTGS02_F8(string numlist, int year, int month1, int month2, string program)
        {
            _numlist = numlist;
            _year = year;
            _month1 = month1;
            _month2 = month2;
            _program = program;

            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                txtNam.Value = _year;
                txtKy1.Value = _month1;
                txtKy2.Value = _month2;
               


            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        
        
        private void FormBaoCaoHangTonTheoKho_Load(object sender, EventArgs e)
        {
            //SetStatus2Text();
        }

        
        public void btnNhan_Click(object sender, EventArgs e)
        {
            
            try
            {
               
                //@Type AS VARCHAR(8),
                //@Year AS INT,
                //@Period1 AS INT,
                //@Period2 AS INT,
                //@NumList VARCHAR(MAX),
                //@User_id AS INT,
                //@Ma_dvcs VARCHAR(50) = ''

                if (this.ShowConfirmMessage("Có chắc chắn xóa không ?") == DialogResult.Yes)
                {

                    SqlParameter[] plist =
                    {
                        new SqlParameter("@Type", "DEL"),
                        new SqlParameter("@Year", txtNam.Value),
                        new SqlParameter("@Period1", txtKy1.Value),
                        new SqlParameter("@Period2", txtKy2.Value),
                        new SqlParameter("@NumList", _numlist),
                        new SqlParameter("@User_id", V6Login.UserId),
                        new SqlParameter("@Ma_dvcs", "")

                    };

                    V6BusinessHelper.ExecuteProcedureNoneQuery(_program, plist);


                    OnUpdateSuccessEvent();
                    Dispose();
                    V6ControlFormHelper.ShowMainMessage("Thực hiện xong.");
                }
                else
                {
                    Dispose();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Delete error:\n" + ex.Message);
            }
        }
        
        private void btnHuy_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                btnHuy.PerformClick();
            }
            else if (keyData == (Keys.Control | Keys.Enter))
            {
                btnNhan.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected int _oldIndex = -1;

        protected virtual void OnUpdateSuccessEvent()
        {
            var handler = UpdateSuccessEvent;
            if (handler != null) handler();
        }
    }
}
