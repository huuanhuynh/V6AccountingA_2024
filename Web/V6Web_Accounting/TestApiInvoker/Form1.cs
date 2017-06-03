using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ApiConnect;
using ApiConnect.ApiInvoker;
using DTO = V6Soft.Models.Accounting.DTO;

namespace TestApiInvoker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = GetCustomers();
        }

        private List<DTO.Customer> GetCustomers()
        {
            var apiInvoker = new WebApiInvoker();
            var result = apiInvoker.GetCustomers();
            return result.Data.Value;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                var key = dataGridView1.Rows[e.RowIndex].Cells["uid"].Value.ToString();
                dataGridView2.DataSource = GetCustomerById(new Guid(key));
            }
        }

        private List<DTO.Customer> GetCustomerById(Guid uid)
        {
            var apiInvoker = new WebApiInvoker();
            var result = apiInvoker.GetCustomer(uid);
            var singleResult = result.Data;
            return new List<DTO.Customer> { singleResult };
        }
    }
}
