using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using ICSharpCode;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
//using System.Threading.Tasks;
using System.Xml;
using System.Threading;
using System.Data.Common;
using System.Web;
using System.Text.RegularExpressions;
using System.IO.Compression;




namespace Bai_Tap_Thep__Povision
{
    public partial class frmConvertXMLtoExcel : Form
    {
        //private string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        //private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        DataTable dt = new DataTable();

        public frmConvertXMLtoExcel()
        {
            InitializeComponent();
        }

        private void frmConvertXMLtoExcel_Load(object sender, EventArgs e)
        {

        }

        private void btnChonExcel_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;
                dt.Clear();           
                openFileDialog1.ShowDialog();
                txtAddExcel.Text = openFileDialog1.FileName;              
            }
            catch (Exception ex)
            {

            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string filePath = openFileDialog1.FileName;
            string extension = Path.GetExtension(filePath);
            string conStr, sheetName;

            conStr = string.Empty;
            switch (extension)
            {

                case ".xls": //Excel 97-03
                    conStr = string.Format(App_Code.class_Proc.Excel03ConString, filePath, "YES");
                    break;

                case ".xlsx": //Excel 07
                    conStr = string.Format(App_Code.class_Proc.Excel07ConString, filePath, "YES");
                    break;
            }

            //Get the name of the First Sheet.
            using (OleDbConnection con = new OleDbConnection(conStr))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = con;
                    con.Open();
                    DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                    con.Close();
                }
            }

            //Read Data from the First Sheet.
            using (OleDbConnection con = new OleDbConnection(conStr))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    using (OleDbDataAdapter oda = new OleDbDataAdapter())
                    {

                        cmd.CommandText = "SELECT * From [" + sheetName + "]";
                        cmd.Connection = con;
                        con.Open();
                        oda.SelectCommand = cmd;
                        oda.Fill(dt);
                        con.Close();

                        //Populate DataGridView.
                        dataGridView1.DataSource = dt;
                    }
                }
            }
        }

        private string convertSpecialCharacter(string xmlData)
        {
            return "<![CDATA[" + xmlData + "]]>";
        }

        private string[] arrMaKH;
        public string ParseIKhachHang(byte[] data, int comID, string accountName, ref string mesage, ref int failtCount)
        {
            StringBuilder rv = new StringBuilder("<Customers>\n");
            mesage = "";
            failtCount = 0;
            StringBuilder Failed = new StringBuilder("<Customers>");
            try
            {
                //const string Fkey = "Fkey";
                //const string ArasingDate = "NgayHoaDon";
                const string TenKhachHang = "Họ tên";
                const string MaKhachHang = "MÃ KH";
                const string DiaChiKhachHang = "Địa chỉ";
                const string DienThoaiKhachHang = "SDT";
                const string MaSoThue = "MST";
                const string Email = "Email";
                //const string HinhThucTT = "HinhThucTT";
                //const string TinhTrangTT = "TinhTrangTT";
                //const string KyHoaDon = "KyHoaDon";
                //const string SanPham = "SanPham";
                //const string DonViTinh = "DonViTinh";
                //const string SoLuong = "SoLuong";
                //const string DonGia = "DonGia";
                //const string ThanhTien = "ThanhTien";
                //const string TienBan = "TienBan";
                //const string ThueSuat = "ThueSuat";
                //const string TienThue = "TienThue";
                //const string TongCong = "TongCong";
                //const string SoSanPham = "SoSanPham";
                Guid guid = Guid.NewGuid();

                string b_AppPath = Application.ExecutablePath;
                string b_ten = txtAddExcel.Text;

              //  string b_sheet = tbExcelSheet.Text;
              
              //  b_dt_kq = Fdt_Excel(b_ten, b_sheet);

                if (dt == null) { return null; }
                else
                {
                    DataTable objTable = dt;
                    
                    // Khởi tạo mảng lưu trữ mã khách hàng
                    arrMaKH = new string[objTable.Rows.Count];
                    for (int x = 0; x < arrMaKH.Length; x++)
                    {
                        arrMaKH[x] = "";
                    }
                    int i=0;
                    while ( i < objTable.Rows.Count)
                    {                    
                        DataRow dr = objTable.Rows[i];
                        StringBuilder sb = new StringBuilder("<Customer><Name>");//Ho Va Ten
                        sb.Append(convertSpecialCharacter(dr[TenKhachHang].ToString().Trim()));
                        sb.Append("</Name><Code>");//MaKH
                        string cuscode = dr[MaKhachHang].ToString().Trim().Replace(" ", "");
                        sb.Append(convertSpecialCharacter(cuscode.ToUpper()));
                      
                        sb.Append("</Code><TaxCode>");
                        if (dr.Table.Columns.Contains(MaSoThue))
                        {
                            sb.Append(dr[MaSoThue].ToString().Trim());
                        }

                        sb.Append("</TaxCode><Address>");//Dia Chi Khach Hang
                        if (dr.Table.Columns.Contains(DiaChiKhachHang))
                        {
                            sb.Append(convertSpecialCharacter(dr[DiaChiKhachHang].ToString().Trim()));
                        }

                        sb.Append("</Address><BankAccountName>");
                        sb.Append("</BankAccountName><BankName>");
                        sb.Append("</BankName><BankNumber>");
                        sb.Append("</BankNumber><Email>");
                        if (dr.Table.Columns.Contains(Email))
                        {
                            sb.Append(dr[Email].ToString().Trim());
                        }

                        sb.Append("</Email><Fax>");
                        sb.Append("</Fax><Phone>");
                        if (dr.Table.Columns.Contains(DienThoaiKhachHang))
                        {
                            sb.Append(dr[DienThoaiKhachHang].ToString().Trim());
                        }

                        sb.Append("</Phone><ContactPerson>");
                        sb.Append("</ContactPerson><RepresentPerson>");
                        sb.Append("</RepresentPerson><CusType>");
           
                        sb.Append("0");
                
                        sb.Append("</CusType></Customer>");

                        i = i+1;
                     
                        // Lọc trùng mã khách hàng
                        //if (search(arrMaKH, dr[MaKhachHang].ToString().Trim()) > 0)
                        //{

                        //}
                        //else
                        //{
                        //    arrMaKH[i] = dr[MaKhachHang].ToString().Trim();
                        //    rv.AppendLine(sb.ToString());
                        //}

                        rv.AppendLine(sb.ToString());
                       
                    }
                }
               
            }
            catch (Exception ex)
            {
                mesage = ex.ToString();
                return null;
            }
            if (failtCount != 0) mesage = Failed.ToString();
            rv.Append("</Customers>");
            return rv.ToString();
        }

        private void Compress(Stream data, Stream outData)
        {
            string str = "";
            try
            {
                using (ICSharpCode.SharpZipLib.Zip.ZipOutputStream zipStream = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(outData))
                {
                    zipStream.SetLevel(3);
                    ICSharpCode.SharpZipLib.Zip.ZipEntry newEntry = new ICSharpCode.SharpZipLib.Zip.ZipEntry("data.xml");
                    newEntry.DateTime = DateTime.UtcNow;                   
                    zipStream.PutNextEntry(newEntry);               
                    data.Position = 0;
                    int size = (data.CanSeek) ? Math.Min((int)(data.Length - data.Position), 0x2000) : 0x2000;
                    byte[] buffer = new byte[size];
                    int n;
                    do
                    {
                        n = data.Read(buffer, 0, buffer.Length);
                        zipStream.Write(buffer, 0, n);
                    } while (n != 0);
                    zipStream.CloseEntry();
                    zipStream.Flush();
                    zipStream.Close();
                }
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }
        }

        private void btnNenFile_Click(object sender, EventArgs e)
        {
             try
             {
                if (File.Exists(txtAddExcel.Text.Trim()))
                {
                    string strInvXML = "";
                    string strMess = "";
                    int iCount = 0;
                    byte[] bData = new byte[1] { 1 };
                    strInvXML = ParseIKhachHang(bData, 1201, "adminTestExamp", ref strMess, ref iCount);

                    if (strInvXML != null)
                    {
                        string line = strInvXML;
                     
                        string strStartupPath = Application.StartupPath;
                        string strXmlDataFilePath = strStartupPath + "\\" + "einv" + ".xml";
                        System.IO.StreamWriter file = new System.IO.StreamWriter(strXmlDataFilePath);
                        file.WriteLine(line);
                        file.Close();

                        DataTable dt = new DataTable();

                        if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\InvZipFiles"))
                            Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\InvZipFiles");

                        string fileName = "";

                        if (Directory.Exists(txtAddZip.Text))
                        {
                            fileName = txtAddZip.Text + @"\khachhang" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".zip";
                        }
                        else
                        {
                            string strPath = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
                            fileName = strPath + @"\khachhang" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".zip";
                        }                       
                        Stream CompStream = new FileStream(fileName, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);

                        using (Stream RawStream2 = new FileStream(strXmlDataFilePath.Trim(), FileMode.Open, FileAccess.ReadWrite))
                        {
                            Compress(RawStream2, CompStream);                           
                        }

                        CompStream.Close();
                        MessageBox.Show("Tạo file dữ liệu thành công.");
                    }
                }

             }
             catch (Exception ex)
             {
                MessageBox.Show("Có lỗi trong quá trình đọc dữ liệu:  " + ex.Message);
             }
              
            
    }

        private void btnChonZip_Click(object sender, EventArgs e)
        {       
            string selectedPath;
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                selectedPath = dlg.SelectedPath;
            }
            txtAddZip.Text = selectedPath;
        
        }
           

           
                
            
           
            



        
    }
}
