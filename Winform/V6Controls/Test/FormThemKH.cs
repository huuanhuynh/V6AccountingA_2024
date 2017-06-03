using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using V6Forms;

namespace Test
{
    public partial class FormThemKH : V6FormInput
    {
        /// <summary>
        /// Khởi tạo Form Thêm khách hàng
        /// </summary>
        /// <param name="ConString"></param>
        public FormThemKH(string user_id, string ConString, string TableName) : base(user_id, ConString,TableName,"ma_kh")
        {
            InitializeComponent();
            dateTimePickerDate0.Value = DateTime.Today;
            txtTime0.Text = DateTime.Now.ToString("HH:mm:ss");
            txtUser_id0.Text = _UserID;
        }
        
        /// <summary>
        /// Khởi tạo Form thêm khách hàng
        /// </summary>
        /// <param name="ConString"></param>
        /// <param name="TableName"></param>
        /// <param name="PrimaryKeyField"></param>
        public FormThemKH(string user_id, string ConString, string TableName, string PrimaryKeyField) : base(user_id, ConString,TableName, PrimaryKeyField)
        {
            InitializeComponent();
            dateTimePickerDate0.Value = DateTime.Today;
            txtTime0.Text = DateTime.Now.ToString("HH:mm:ss");
            txtUser_id0.Text = _UserID;
        }

        /// <summary>
        /// Khởi tạo Form sửa thông tin khách hàng
        /// </summary>
        /// <param name="ConString"></param>
        /// <param name="PrimaryKeyValue"></param>
        public FormThemKH(string user_id, string ConString, string TableName, string PrimaryKeyField, object ma_kh)
            : base(user_id, ConString, TableName, PrimaryKeyField, ma_kh)
        { InitializeComponent(); }

        private void FormThemKH_Load(object sender, EventArgs e)
        {
            dateTimePickerDate2.Value = DateTime.Today;
            txtTime2.Text = DateTime.Now.ToString("HH:mm:ss");
            txtUser_id2.Text = _UserID;
        }
    }
}
