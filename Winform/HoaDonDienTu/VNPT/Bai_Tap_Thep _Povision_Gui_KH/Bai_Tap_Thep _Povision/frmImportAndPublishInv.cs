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
using Bai_Tap_Thep__Povision.PublishServices;
using Bai_Tap_Thep__Povision.BusinessServices;
using Bai_Tap_Thep__Povision.PortalServices;
using Bai_Tap_Thep__Povision.App_Code;

namespace Bai_Tap_Thep__Povision
{
    public partial class frmImportAndPublishInv : Form
    {
        public frmImportAndPublishInv()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();

        private void btnLoadExcel_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;
                dt.Clear();
                openFileDialog1.ShowDialog();
               
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

        //private string[] arrMaKH;
        //public string ParseIKhachHang(byte[] data, int comID, string accountName, ref string mesage, ref int failtCount)
        //{
        //    StringBuilder rv = new StringBuilder("<Invoices>\n");
        //    mesage = "";
        //    failtCount = 0;
        //    StringBuilder Failed = new StringBuilder("<Inv>");
        //    try
        //    {
        //        const string Fkey = "Fkey";
               
        //        //const string ArasingDate = "NgayHoaDon";

        //     //  const string CusCode = "MAKH";
        //        const string CusName = "CusName"; // "TenKhachHang";
        //        const string CusAddress = "CusAddress"; // "DiaChiKhachHang";
        //        const string CusPhone = "CusPhone"; // "SDT";
        //        const string CusTaxCode = "CusTaxCode"; // "MST";
        //        const string PaymentMethod = "PaymentMethod"; // "PhuongThucThanhToan";


        //        const string ProdName = "ProdName"; // "TenSanPham";
        //        const string ProdUnit = "ProdUnit"; // "DonViTinh";
        //        const string ProdQuantity = "ProdQuantity"; // "SoLuong";
        //        const string ProdPrice = "ProdPrice"; //DonGia               


        //        const string Total = "Total"; // "TongTienTruocThue";
        //        const string VATRate = "VATRate"; // "ThueVAT";
        //        const string VATAmount = "VATAmount"; // "TienThueVAT";               
        //        const string Amount = "Amount"; // "TongTien";

        //        //const string TienThue = "TienThue";
        //        //const string TongCong = "TongCong";
        //        //const string SoSanPham = "SoSanPham";
                
        //        Guid guid = Guid.NewGuid();

        //        string b_AppPath = Application.ExecutablePath;
        //  //      string b_ten = txtAddExcel.Text;

        //        //  string b_sheet = tbExcelSheet.Text;

        //        //  b_dt_kq = Fdt_Excel(b_ten, b_sheet);

        //        if (dt == null) { return null; }
        //        else
        //        {
        //            DataTable objTable = dt;

        //            // Khởi tạo mảng lưu trữ mã khách hàng
        //            arrMaKH = new string[objTable.Rows.Count];
        //            for (int x = 0; x < arrMaKH.Length; x++)
        //            {
        //                arrMaKH[x] = "";
        //            }
        //            int i = 0;
        //            while (i < objTable.Rows.Count)
        //            {
        //                DataRow dr = objTable.Rows[i];

        //                StringBuilder sb = new StringBuilder("<Inv><key>");//Key
        //                sb.Append(convertSpecialCharacter(dr[Fkey].ToString().Trim()));

        //                sb.Append("</CusName><CusAddress>");//Ten KH
        //                sb.Append(convertSpecialCharacter(dr[CusName].ToString().Trim()));

        //                sb.Append("</CusName><CusAddress>");//Dia Chi KH
        //                sb.Append(convertSpecialCharacter(dr[CusAddress].ToString().Trim()));

        //                sb.Append("</CusAddress><CusPhone>");//SDT KH
        //                sb.Append(convertSpecialCharacter(dr[CusPhone].ToString().Trim()));

        //                sb.Append("</CusPhone><CusTaxCode>");//MST KH
        //                sb.Append(convertSpecialCharacter(dr[CusTaxCode].ToString().Trim()));

        //                sb.Append("</CusTaxCode><PaymentMethod>");//PhuongThucThanhToanKH
        //                sb.Append(convertSpecialCharacter(dr[PaymentMethod].ToString().Trim()));

        //                sb.Append("</PaymentMethod><PaymentStatus>0</PaymentStatus><KindOfService></KindOfService>");

        //                sb.Append("<Products>\n");

                        

        //                //string cuscode = dr[MaKhachHang].ToString().Trim().Replace(" ", "");
        //                //sb.Append(convertSpecialCharacter(cuscode.ToUpper()));

        //                //sb.Append("</Code><TaxCode>");
        //                //if (dr.Table.Columns.Contains(MaSoThue))
        //                //{
        //                //    sb.Append(dr[MaSoThue].ToString().Trim());
        //                //}

        //                //sb.Append("</TaxCode><Address>");//Dia Chi Khach Hang
        //                //if (dr.Table.Columns.Contains(DiaChiKhachHang))
        //                //{
        //                //    sb.Append(convertSpecialCharacter(dr[DiaChiKhachHang].ToString().Trim()));
        //                //}

        //                //sb.Append("</Address><BankAccountName>");
        //                //sb.Append("</BankAccountName><BankName>");
        //                //sb.Append("</BankName><BankNumber>");
        //                //sb.Append("</BankNumber><Email>");
        //                //if (dr.Table.Columns.Contains(Email))
        //                //{
        //                //    sb.Append(dr[Email].ToString().Trim());
        //                //}

        //                //sb.Append("</Email><Fax>");
        //                //sb.Append("</Fax><Phone>");
        //                //if (dr.Table.Columns.Contains(DienThoaiKhachHang))
        //                //{
        //                //    sb.Append(dr[DienThoaiKhachHang].ToString().Trim());
        //                //}

        //                sb.Append("</Phone><ContactPerson>");
        //                sb.Append("</ContactPerson><RepresentPerson>");
        //                sb.Append("</RepresentPerson><CusType>");

        //                sb.Append("0");

        //                sb.Append("</CusType></Customer>");

        //                i = i + 1;

        //                // Lọc trùng mã khách hàng
        //                //if (search(arrMaKH, dr[MaKhachHang].ToString().Trim()) > 0)
        //                //{

        //                //}
        //                //else
        //                //{
        //                //    arrMaKH[i] = dr[MaKhachHang].ToString().Trim();
        //                //    rv.AppendLine(sb.ToString());
        //                //}

        //                rv.AppendLine(sb.ToString());

        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        mesage = ex.ToString();
        //        return null;
        //    }
        //    if (failtCount != 0) mesage = Failed.ToString();
        //    rv.Append("</Customers>");
        //    return rv.ToString();
        //}

        public int Randomm()
        {
              Random rnd = new Random();
              int myRandomNo = rnd.Next(10000000, 99999999);
              return myRandomNo;        
        }      

        private string[] arrMaKH;
        private void btnImportAndPublishInv_Click(object sender, EventArgs e)
        {
            
            StringBuilder rv = new StringBuilder("<Invoices>\n");
            //mesage = "";
            //failtCount = 0;
            StringBuilder Failed = new StringBuilder("<Inv>");
            try
            {
                const string Fkey = "Fkey";

                //const string ArasingDate = "NgayHoaDon";

                const string InvoiceNo = "InvoiceNo";
                const string CusCode = "CusCode"; // "MaKH";
                const string CusName = "CusName"; // "TenKhachHang";
                const string CusAddress = "CusAddress"; // "DiaChiKhachHang";
                const string CusPhone = "CusPhone"; // "SDT";
                const string CusTaxCode = "CusTaxCode"; // "MST";
                const string PaymentMethod = "PaymentMethod"; // "PhuongThucThanhToan";
                
                const string ProdName = "ProdName"; // "TenSanPham";
                const string ProdUnit = "ProdUnit"; // "DonViTinh";
                const string ProdQuantity = "ProdQuantity"; // "SoLuong";
                const string ProdPrice = "ProdPrice"; //DonGia               
                const string Amount = "Amount"; // "ThanhTien";

               // const string Total = "Total"; // "TongTienTruocThue";
                const string VATRate = "VATRate"; // "ThueVAT";
                //const string VATAmount = "VATAmount"; // "TienThueVAT";               
                

                //const string TienThue = "TienThue";
                //const string TongCong = "TongCong";
                //const string SoSanPham = "SoSanPham";

                Guid guid = Guid.NewGuid();

                double Total = 0;
                double VATAmount = 0;
                double VATRate2 = 0;
                double AmountTong = 0;

                string b_AppPath = Application.ExecutablePath;
              
                StringBuilder sb = new StringBuilder("<Invoices>\n");//Key
                if (dt == null) {   }
                else
                {
                    DataTable objTable = dt;
                    // Khởi tạo mảng lưu trữ mã khách hàng
                    arrMaKH = new string[objTable.Rows.Count];
                    for (int x = 0; x < arrMaKH.Length; x++)
                    {
                        arrMaKH[x] = "";
                    }
                    int i = 0;
                    
                    while (i < objTable.Rows.Count-1)
                    {                       
                        DataRow dr = objTable.Rows[i];                      

                        sb.Append("<Inv><key>");//Key            
                        sb.Append(Randomm().ToString());

                        sb.Append("</key><Invoice><CusCode>");//MAKH
                        sb.Append(convertSpecialCharacter(dr[CusCode].ToString().Trim()));

                        sb.Append("</CusCode><CusName>");//Ten KH
                        sb.Append(convertSpecialCharacter(dr[CusName].ToString().Trim()));

                        sb.Append("</CusName><CusAddress>");//Dia Chi KH
                        sb.Append(convertSpecialCharacter(dr[CusAddress].ToString().Trim()));

                        sb.Append("</CusAddress><CusPhone>");//SDT KH
                        sb.Append(convertSpecialCharacter(dr[CusPhone].ToString().Trim()));

                        sb.Append("</CusPhone><CusTaxCode>");//MST KH
                        sb.Append(convertSpecialCharacter(dr[CusTaxCode].ToString().Trim()));

                        sb.Append("</CusTaxCode><PaymentMethod>");//PhuongThucThanhToanKH
                        sb.Append(convertSpecialCharacter(dr[PaymentMethod].ToString().Trim()));

                        sb.Append("</PaymentMethod><PaymentStatus>0</PaymentStatus><KindOfService></KindOfService>");

                        sb.Append("<Products>");

                        int j = 0;
                        j = i + j;
                        while (j < objTable.Rows.Count)
                        {
                            string tam = dr[InvoiceNo].ToString().Trim();

                            DataRow dr2 = objTable.Rows[j];
                            if (tam == (dr2[InvoiceNo].ToString().Trim()))
                            {
                                sb.Append("<Product><ProdName>");//TenSanPham
                                sb.Append(convertSpecialCharacter(dr2[ProdName].ToString().Trim()));

                                sb.Append("</ProdName><ProdUnit>");//DonViTinh
                                sb.Append(convertSpecialCharacter(dr2[ProdUnit].ToString().Trim()));

                                sb.Append("</ProdUnit><ProdQuantity>");//SoLuong
                                sb.Append(dr2[ProdQuantity].ToString().Trim());

                                sb.Append("</ProdQuantity><ProdPrice>");//DonGia
                                sb.Append(dr2[ProdPrice].ToString().Trim());

                                sb.Append("</ProdPrice><Amount>");//ThanhTien
                                sb.Append(dr2[Amount].ToString().Trim());
                                sb.Append("</Amount></Product>");//ThanhTien

                                VATRate2 = int.Parse(dr2[VATRate].ToString().Trim());
                                Total = Total + double.Parse(dr2[Amount].ToString().Trim());

                                j = j + 1;
                            }
                            else
                            {
                                j = objTable.Rows.Count;
                            }
                            i = i + 1;                          
                        }

                        sb.Append("</Products><Total>");
                        sb.Append(Total);
                        sb.Append("</Total><VATRate>");
                        sb.Append(VATRate2);
                       
                        sb.Append("</VATRate><VATAmount>");
                        VATAmount = ((Total * VATRate2) / 100);
                        sb.Append(VATAmount);
                       
                        sb.Append("</VATAmount><Amount>");
                        AmountTong= (Total + ((Total * VATRate2) / 100));
                        sb.Append(AmountTong);

                        sb.Append("</Amount><AmountInWords>");
                        Int64 tongtien;
                        string tienBangChu = "";
                        NumberToLeter numToLetter = new NumberToLeter();
                        if (Int64.TryParse(AmountTong.ToString().Trim(), out tongtien))
                            tienBangChu = numToLetter.DocTienBangChu(tongtien, tienBangChu);
                        sb.Append(tienBangChu);

                        sb.Append("</AmountInWords></Invoice></Inv>\n");

                        i = i - 1;

                        //string cuscode = dr[MaKhachHang].ToString().Trim().Replace(" ", "");
                        //sb.Append(convertSpecialCharacter(cuscode.ToUpper()));
                        //sb.Append("</Code><TaxCode>");
                        //if (dr.Table.Columns.Contains(MaSoThue))
                        //{
                        //    sb.Append(dr[MaSoThue].ToString().Trim());
                        //}
                        //sb.Append("</TaxCode><Address>");//Dia Chi Khach Hang
                        //if (dr.Table.Columns.Contains(DiaChiKhachHang))
                        //{
                        //    sb.Append(convertSpecialCharacter(dr[DiaChiKhachHang].ToString().Trim()));
                        //}
                        //sb.Append("</Address><BankAccountName>");
                        //sb.Append("</BankAccountName><BankName>");
                        //sb.Append("</BankName><BankNumber>");
                        //sb.Append("</BankNumber><Email>");
                        //if (dr.Table.Columns.Contains(Email))
                        //{
                        //    sb.Append(dr[Email].ToString().Trim());
                        //}
                        //sb.Append("</Email><Fax>");
                        //sb.Append("</Fax><Phone>");
                        //if (dr.Table.Columns.Contains(DienThoaiKhachHang))
                        //{
                        //    sb.Append(dr[DienThoaiKhachHang].ToString().Trim());
                        //}
                        //sb.Append("</Phone><ContactPerson>");
                        //sb.Append("</ContactPerson><RepresentPerson>");
                        //sb.Append("</RepresentPerson><CusType>");
                        //sb.Append("0");
                        //sb.Append("</CusType></Customer>");
                        //i = i + 1;
                        // Lọc trùng mã khách hàng
                        //if (search(arrMaKH, dr[MaKhachHang].ToString().Trim()) > 0)
                        //{
                        //}
                        //else
                        //{
                        //    arrMaKH[i] = dr[MaKhachHang].ToString().Trim();
                        //    rv.AppendLine(sb.ToString());
                        //}
                     //   rv.AppendLine(sb.ToString());

                    }
                    if (i < objTable.Rows.Count)
                    {
                        sb.Append("</Invoices>");
                    }
                }
               txtxml.Text = sb.ToString();

              

            }
            catch (Exception ex)
            {
            //   string  mesage = ex.ToString();
               
            }
            //if (failtCount != 0) mesage = Failed.ToString();
            //rv.Append("</Customers>");
            //return rv.ToString();

            PublishService ps = new PublishService();
            string rs = ps.ImportAndPublishInv(txtAccount.Text, txtACPass.Text, txtxml.Text, txtUsername.Text, txtPassword.Text, txtParten.Text, txtSeri.Text, Int32.Parse(txtConvert.Text));
            MessageBox.Show(rs.ToString());
           
        }

        private void frmImportAndPublishInv_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void btnDieuChinhHoaDon_Click(object sender, EventArgs e)
        {
           // StringBuilder rv = new StringBuilder("<AdjustInv>\n");  
            //StringBuilder Failed = new StringBuilder("<Inv>");
            try
            {
                const string Fkey = "Fkey";

                const string InvoiceNo = "InvoiceNo";
                const string CusCode = "CusCode"; // "MaKH";
                const string CusName = "CusName"; // "TenKhachHang";
                const string CusAddress = "CusAddress"; // "DiaChiKhachHang";
                const string CusPhone = "CusPhone"; // "SDT";
                const string CusTaxCode = "CusTaxCode"; // "MST";
                const string PaymentMethod = "PaymentMethod"; // "PhuongThucThanhToan";

                const string ProdName = "ProdName"; // "TenSanPham";
                const string ProdUnit = "ProdUnit"; // "DonViTinh";
                const string ProdQuantity = "ProdQuantity"; // "SoLuong";
                const string ProdPrice = "ProdPrice"; //DonGia               
                const string Amount = "Amount"; // "ThanhTien";

                const string VATRate = "VATRate"; // "ThueVAT";
                        
                Guid guid = Guid.NewGuid();

                double Total = 0;
                double VATAmount = 0;
                double VATRate2 = 0;
                double AmountTong = 0;

                string b_AppPath = Application.ExecutablePath;

                StringBuilder sb = new StringBuilder("<AdjustInv>\n");//Key
                if (dt == null) { }
                else
                {
                    DataTable objTable = dt;
                    // Khởi tạo mảng lưu trữ mã khách hàng
                    arrMaKH = new string[objTable.Rows.Count];
                    for (int x = 0; x < arrMaKH.Length; x++)
                    {
                        arrMaKH[x] = "";
                    }
                    int i = 0;

                    while (i < objTable.Rows.Count - 1)
                    {
                        DataRow dr = objTable.Rows[i];

                        sb.Append("<key>");//Key            
                        sb.Append(Randomm().ToString());

                        sb.Append("</key><CusCode>");//MAKH
                        sb.Append(convertSpecialCharacter(dr[CusCode].ToString().Trim()));

                        sb.Append("</CusCode><CusName>");//Ten KH
                        sb.Append(convertSpecialCharacter(dr[CusName].ToString().Trim()));

                        sb.Append("</CusName><CusAddress>");//Dia Chi KH
                        sb.Append(convertSpecialCharacter(dr[CusAddress].ToString().Trim()));

                        sb.Append("</CusAddress><CusPhone>");//SDT KH
                        sb.Append(convertSpecialCharacter(dr[CusPhone].ToString().Trim()));

                        sb.Append("</CusPhone><CusTaxCode>");//MST KH
                        sb.Append(convertSpecialCharacter(dr[CusTaxCode].ToString().Trim()));

                        sb.Append("</CusTaxCode><PaymentMethod>");//PhuongThucThanhToanKH
                        sb.Append(convertSpecialCharacter(dr[PaymentMethod].ToString().Trim()));

                        sb.Append("</PaymentMethod><PaymentStatus>0</PaymentStatus><KindOfService></KindOfService><Type>");
                        int tam2 = 0;
                        if (cmbType.Text=="Điều Chỉnh Tăng")
                        {
                             tam2 = 2;
                        }
                        else if (cmbType.Text=="Điều Chỉnh Giảm")
                        {
                              tam2 = 3;
                        }
                        else if (cmbType.Text == "Điều Chỉnh Giảm")
                        {
                             tam2 = 4;
                        }
                        sb.Append(tam2);
                        sb.Append("</Type><Products>");

                        int j = 0;
                        j = i + j;
                        while (j < objTable.Rows.Count)
                        {
                            string tam = dr[InvoiceNo].ToString().Trim();

                            DataRow dr2 = objTable.Rows[j];
                            if (tam == (dr2[InvoiceNo].ToString().Trim()))
                            {
                                sb.Append("<Product><ProdName>");//TenSanPham
                                sb.Append(convertSpecialCharacter(dr2[ProdName].ToString().Trim()));

                                sb.Append("</ProdName><ProdUnit>");//DonViTinh
                                sb.Append(convertSpecialCharacter(dr2[ProdUnit].ToString().Trim()));

                                sb.Append("</ProdUnit><ProdQuantity>");//SoLuong
                                sb.Append(dr2[ProdQuantity].ToString().Trim());

                                sb.Append("</ProdQuantity><ProdPrice>");//DonGia
                                sb.Append(dr2[ProdPrice].ToString().Trim());

                                sb.Append("</ProdPrice><Amount>");//ThanhTien
                                sb.Append(dr2[Amount].ToString().Trim());
                                sb.Append("</Amount></Product>");//ThanhTien

                                VATRate2 = int.Parse(dr2[VATRate].ToString().Trim());
                                Total = Total + double.Parse(dr2[Amount].ToString().Trim());

                                j = j + 1;
                            }
                            else
                            {
                                j = objTable.Rows.Count;
                            }
                            i = i + 1;
                        }

                        sb.Append("</Products><Total>");
                        sb.Append(Total);
                        sb.Append("</Total><VATRate>");
                        sb.Append(VATRate2);

                        sb.Append("</VATRate><VATAmount>");
                        VATAmount = ((Total * VATRate2) / 100);
                        sb.Append(VATAmount);

                        sb.Append("</VATAmount><Amount>");
                        AmountTong = (Total + ((Total * VATRate2) / 100));
                        sb.Append(AmountTong);

                        sb.Append("</Amount><AmountInWords>");
                        sb.Append(0);
                        sb.Append("</AmountInWords>");

                        i = i - 1;                      

                    }
                    if (i < objTable.Rows.Count)
                    {
                        sb.Append("\n</AdjustInv>");
                    }
                }
                txtxml.Text = sb.ToString();

            }
            catch (Exception ex)
            {
               
            }
           
            BusinessService ps = new BusinessService();
            string rs = ps.AdjustInvoiceAction(txtAccount.Text, txtACPass.Text, txtxml.Text, txtUsername.Text, txtPassword.Text, txtKey.Text, "", Int32.Parse(txtConvert.Text), txtParten.Text.Trim(), txtSeri.Text.Trim());            
            MessageBox.Show(rs.ToString());

        }      

        private void btnThayTheHoaDon_Click(object sender, EventArgs e)
        {            
            try
            {
                const string Fkey = "Fkey";

                const string InvoiceNo = "InvoiceNo";
                const string CusCode = "CusCode"; // "MaKH";
                const string CusName = "CusName"; // "TenKhachHang";
                const string CusAddress = "CusAddress"; // "DiaChiKhachHang";
                const string CusPhone = "CusPhone"; // "SDT";
                const string CusTaxCode = "CusTaxCode"; // "MST";
                const string PaymentMethod = "PaymentMethod"; // "PhuongThucThanhToan";

                const string ProdName = "ProdName"; // "TenSanPham";
                const string ProdUnit = "ProdUnit"; // "DonViTinh";
                const string ProdQuantity = "ProdQuantity"; // "SoLuong";
                const string ProdPrice = "ProdPrice"; //DonGia               
                const string Amount = "Amount"; // "ThanhTien";

                const string VATRate = "VATRate"; // "ThueVAT";

                Guid guid = Guid.NewGuid();

                double Total = 0;
                double VATAmount = 0;
                double VATRate2 = 0;
                double AmountTong = 0;

                string b_AppPath = Application.ExecutablePath;

                StringBuilder sb = new StringBuilder("<ReplaceInv>\n");//Key
                if (dt == null) { }
                else
                {
                    DataTable objTable = dt;
                    // Khởi tạo mảng lưu trữ mã khách hàng
                    arrMaKH = new string[objTable.Rows.Count];
                    for (int x = 0; x < arrMaKH.Length; x++)
                    {
                        arrMaKH[x] = "";
                    }
                    int i = 0;

                    while (i < objTable.Rows.Count - 1)
                    {
                        DataRow dr = objTable.Rows[i];

                        sb.Append("<key>");//Key            
                        sb.Append(Randomm().ToString());

                        sb.Append("</key><CusCode>");//MAKH
                        sb.Append(convertSpecialCharacter(dr[CusCode].ToString().Trim()));

                        sb.Append("</CusCode><CusName>");//Ten KH
                        sb.Append(convertSpecialCharacter(dr[CusName].ToString().Trim()));

                        sb.Append("</CusName><CusAddress>");//Dia Chi KH
                        sb.Append(convertSpecialCharacter(dr[CusAddress].ToString().Trim()));

                        sb.Append("</CusAddress><CusPhone>");//SDT KH
                        sb.Append(convertSpecialCharacter(dr[CusPhone].ToString().Trim()));

                        sb.Append("</CusPhone><CusTaxCode>");//MST KH
                        sb.Append(convertSpecialCharacter(dr[CusTaxCode].ToString().Trim()));

                        sb.Append("</CusTaxCode><PaymentMethod>");//PhuongThucThanhToanKH
                        sb.Append(convertSpecialCharacter(dr[PaymentMethod].ToString().Trim()));

                        sb.Append("</PaymentMethod><KindOfService></KindOfService>");
                      
                        sb.Append("<Products>");

                        int j = 0;
                        j = i + j;
                        while (j < objTable.Rows.Count)
                        {
                            string tam = dr[InvoiceNo].ToString().Trim();

                            DataRow dr2 = objTable.Rows[j];
                            if (tam == (dr2[InvoiceNo].ToString().Trim()))
                            {
                                sb.Append("<Product><ProdName>");//TenSanPham
                                sb.Append(convertSpecialCharacter(dr2[ProdName].ToString().Trim()));

                                sb.Append("</ProdName><ProdUnit>");//DonViTinh
                                sb.Append(convertSpecialCharacter(dr2[ProdUnit].ToString().Trim()));

                                sb.Append("</ProdUnit><ProdQuantity>");//SoLuong
                                sb.Append(dr2[ProdQuantity].ToString().Trim());

                                sb.Append("</ProdQuantity><ProdPrice>");//DonGia
                                sb.Append(dr2[ProdPrice].ToString().Trim());

                                sb.Append("</ProdPrice><Amount>");//ThanhTien
                                sb.Append(dr2[Amount].ToString().Trim());
                                sb.Append("</Amount></Product>");//ThanhTien

                                VATRate2 = int.Parse(dr2[VATRate].ToString().Trim());
                                Total = Total + double.Parse(dr2[Amount].ToString().Trim());

                                j = j + 1;
                            }
                            else
                            {
                                j = objTable.Rows.Count;
                            }
                            i = i + 1;
                        }

                        sb.Append("</Products><Total>");
                        sb.Append(Total);
                        sb.Append("</Total><VATRate>");
                        sb.Append(VATRate2);

                        sb.Append("</VATRate><VATAmount>");
                        VATAmount = ((Total * VATRate2) / 100);
                        sb.Append(VATAmount);

                        sb.Append("</VATAmount><Amount>");
                        AmountTong = (Total + ((Total * VATRate2) / 100));
                        sb.Append(AmountTong);

                        sb.Append("</Amount><AmountInWords>");
                        sb.Append(0);
                        sb.Append("</AmountInWords>");

                        i = i - 1;

                    }
                    if (i < objTable.Rows.Count)
                    {
                        sb.Append("\n</ReplaceInv>");
                    }
                }
                txtxml.Text = sb.ToString();

            }
            catch (Exception ex)
            {

            }
            BusinessService ps = new BusinessService();
            string rs = ps.ReplaceInvoiceAction(txtAccount.Text, txtACPass.Text, txtxml.Text, txtUsername.Text, txtPassword.Text, txtKey.Text,"", Int32.Parse(txtConvert.Text), txtParten.Text.Trim(), txtSeri.Text.Trim());
            MessageBox.Show(rs.ToString());

        }

        private void btnHuyHoaDon_Click(object sender, EventArgs e)
        {
            BusinessService ps = new BusinessService();
            string rs = ps.cancelInv(txtAccount.Text, txtACPass.Text, txtKey.Text, txtUsername.Text, txtPassword.Text);
            MessageBox.Show(rs.ToString());
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
           

        }
       
    }
}
