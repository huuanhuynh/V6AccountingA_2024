using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace V6MDBtoSQLthread
{
    public partial class FormXmlTable : Form
    {
        public FormXmlTable(string file)
        {
            InitializeComponent();
            xmlFileName = file;
        }
        DataTable tbl;
        string xmlFileName;
        
        private void FormXmlTable_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(xmlFileName))
            {
                LoadXmlToGrid(xmlFileName);
            }
        }
        
        private void btnAddColumn_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Columns.Contains(txtColumnName.Text.Trim()))
                    throw new Exception("Đã tồn tại cột [" + txtColumnName.Text.Trim() + "]");

                if (chkAddAtIndex.Checked && dataGridView1.SelectedCells.Count>0)
                {
                    DataGridViewCell cell = dataGridView1.SelectedCells[0];

                    DataGridViewColumn dgvc = new DataGridViewColumn(cell);

                    dgvc.Name = txtColumnName.Text.Trim();
                    dgvc.HeaderText = txtColumnName.Text.Trim();
                    dataGridView1.Columns.Insert(cell.ColumnIndex, dgvc);
                }
                else
                {
                    
                    dataGridView1.Columns.Add(txtColumnName.Text.Trim(), txtColumnName.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void btnLoadXml_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "Xml file|*.xtb|All files|*.*";
            o.DefaultExt = "xml";
            if (o.ShowDialog() == DialogResult.OK)
            {   
                xmlFileName = o.FileName;
                LoadXmlToGrid(xmlFileName);
            }
            
        }

        private void btnSaveXml_Click(object sender, EventArgs e)
        {
            if (xmlFileName != null)
            {   
                try
                {
                    tbl.WriteXml(xmlFileName,true);
                    MessageBox.Show("Đã lưu!\n" +
                        "Lưu ý: Chỉ lưu với cấu trúc cột cũ!\n"+
                        "Muốn lưu với cấu trúc mới hãy bấm\n"+
                        "[Save New Xml Struct]", FormMain.__DialogTitle);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lưu lỗi!\n" + ex.Message, FormMain.__DialogTitle);
                }
            }
            else
            {
                btnSaveNew_Click(null, null);
            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Xml Table|*.xtb";
            sf.DefaultExt = "xtb";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                xmlFileName = sf.FileName;
                CreateNewXmlStruct(xmlFileName);

            }
        }
        void change(DataTable tb)
        {
            tb.Rows[0][0] = "huuan";
        }
        private void LoadXmlToGrid(string fileName)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(fileName);
                txtDataSetName.Text = ds.DataSetName;

                if (ds.Tables.Count > 0)
                {
                    tbl = ds.Tables[0];
                    txtTableName.Text = tbl.TableName;
                }
                else
                {
                    tbl = new DataTable();
                    MessageBox.Show("Không đọc được bảng dữ liệu nào!");
                }
                //change(tbl);

                BindingSource bSource = new BindingSource();
                bSource.DataSource = tbl;

                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = bSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show("LoadXml: " + ex.Message, FormMain.__DialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CreateNewXmlStruct(string fileName)
        {
            try
            {
                #region ==== start ====
                DataSet ds = new DataSet();
                DataTable table= new DataTable(txtTableName.Text.Trim());
                ds.Tables.Add(table);
                if (txtTableName.Text.Trim() != "")
                    table.TableName = txtTableName.Text.Trim();
                
                if (txtDataSetName.Text.Trim() != "")
                    ds.DataSetName = txtDataSetName.Text.Trim();

                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    table.Columns.Add(dataGridView1.Columns[i].Name);
                }
                #endregion ==== start ====
                DataRow row;
                for(int i = 0; i< (dataGridView1.Rows.Count-1); i++)
                //foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    DataGridViewRow item = dataGridView1.Rows[i];
                    row = table.NewRow();
                    for (int j = 0; j < item.Cells.Count; j++)
                    {
                        if (item.Cells[j].Value == null)
                            row[j] = " ";
                        else
                            row[j] = item.Cells[j].Value;
                    }
                    table.Rows.Add(row);
                }
                
                table.WriteXml(fileName,true);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error! - " + FormMain.__DialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemoveColumn_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    DataGridViewCell cell = dataGridView1.SelectedCells[0];
                    if (chkDeleteColumn.Checked || MessageBox.Show("Có chắc bạn muốn xóa cột [" +
                        dataGridView1.Columns[cell.ColumnIndex].Name +"]?", "Xóa cột.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        dataGridView1.Columns.Remove(dataGridView1.Columns[cell.ColumnIndex]);
                    }
                }
                else
                {
                    MessageBox.Show("Chưa có cột nào được chọn!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error! - " + FormMain.__DialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemoveRow_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    DataGridViewCell cell = dataGridView1.SelectedCells[0];
                    if (chkDeleteRow.Checked || MessageBox.Show("Có chắc bạn muốn xóa dòng [" +
                        (cell.RowIndex+1) + "]: "+
                        dataGridView1.Rows[cell.RowIndex].Cells[0].Value 
                        +" ?", "Xóa dòng.", 
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question) 
                        == DialogResult.OK)
                    {
                        dataGridView1.Rows.Remove(dataGridView1.Rows[cell.RowIndex]);
                    }
                }
                else
                {
                    MessageBox.Show("Chưa có dòng nào được chọn!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error! - " + FormMain.__DialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void removeRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnRemoveRow_Click(null, null);
        }

        private void removeColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnRemoveColumn_Click(null, null);
        }

        void Insert(DataTable tb1, DataTable tb2)
        {
            string TableName = "???";
            string insertsql = "";
            string fields = "", paramlist="";
            for (int i = 0; i < tb2.Columns.Count; i++)
            {
                fields += "," + tb2.Columns[i].ColumnName;
                paramlist += ",@" + tb2.Columns[i].ColumnName;
            }
            if (fields.Length > 0)
            {
                fields = fields.Substring(1);//bỏ đi cái dấu [,]
            }
            if (paramlist.Length > 0)
            {
                paramlist = paramlist.Substring(1);
            }
            insertsql = "Insert into [" + TableName + "]\n(" + fields + ")\nValues(" + paramlist + ")";

            List<SqlParameter> insertParams;
            foreach (DataRow item in tb1.Rows)
            {                
                insertParams = new List<SqlParameter>();
                for (int i = 0; i < tb2.Columns.Count; i++)
                {
                    string field = tb2.Columns[i].ColumnName;
                    if(tb1.Columns.Contains(field))
                        insertParams.Add(new SqlParameter(field, item[field]));
                    else
                    {
                        if (tb2.Columns[i].AllowDBNull)
                            insertParams.Add(new SqlParameter(field, null));
                        else
                        {
                            switch (tb2.Columns[i].DataType.ToString())
                            {
                                case "System.Char":
                                case "System.String":
                                    insertParams.Add(new SqlParameter(field, ""));
                                    break;
                                case "System.DateTime":
                                    insertParams.Add(new SqlParameter(field, DateTime.Now));
                                    break;
                                case "System.Boolean": 
                                    insertParams.Add(new SqlParameter(field, false));
                                    break;
                                case "System.Byte":
                                case "System.Int16":
                                case "System.Int32":
                                case "System.Int64":
                                case "System.Decimal":
                                case "System.Double":
                                    insertParams.Add(new SqlParameter(field, 0));
                                    break;
                                case "System.DBNull":
                                    insertParams.Add(new SqlParameter(field, ""));
                                    break;
                                default:
                                    insertParams.Add(new SqlParameter(field, ""));
                                    break;
                            }
                        }
                    }
                }
                //Thực thi sql
                V6Library.SqlHelper.ExecuteNonQuery("connectionstring", CommandType.Text, insertsql, insertParams.ToArray());
            }
        }

        private void btnRemoveAllRows_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Xóa hết dòng?", "Xóa dòng", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    while (dataGridView1.Rows.Count > 1)
                    {
                        dataGridView1.Rows.Remove(dataGridView1.Rows[0]);
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRemoveAllColumns_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Xóa hết cột?","Xóa cột",MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
                while (dataGridView1.Columns.Count > 0)
                {
                    dataGridView1.Columns.Remove(dataGridView1.Columns[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void Temp(DataTable tbl, SortedList<string,object> lists)
        {
            lists.Add("Ma_vt", "abcg");
            lists.Add("???", 123);
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                for (int j = 0; j < lists.Count; j++)
                {
                    tbl.Rows[i][lists.Keys[j]] = lists.Values[j];
                    //tbl.Rows[i][lists.ElementAt(j).Key] = lists.ElementAt(j).Value;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

    }
}
