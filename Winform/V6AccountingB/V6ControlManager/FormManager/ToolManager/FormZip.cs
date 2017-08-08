using System;
using System.Windows.Forms;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.ToolManager
{
    public partial class FormZip : V6Form
    {
        public FormZip()
        {
            InitializeComponent();
        }

        private void buttonZipFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Title = "Mở tập tin cần nén!";
            if (o.ShowDialog(this) == DialogResult.OK)
            {
                txtFile1.Text = o.FileName;
                SaveFileDialog s = new SaveFileDialog();
                s.Title = "Đặt tên tập tin nén bạn muốn!";
                if (s.ShowDialog(this) == DialogResult.OK)
                {
                    txtFile2.Text = s.FileName;
                    V6Tools.V67z.ZipFile(txtFile2.Text, txtFile1.Text);
                    V6Message.Show("Ok");
                }
            }
        }

        private void buttonZipFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            f.Description = "Chọn một thư mục cần nén!";
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                txtFolder1.Text = f.SelectedPath;
                SaveFileDialog s = new SaveFileDialog();
                s.Title = "Đặt tên tập tin nén bạn muốn!";
                if (s.ShowDialog(this) == DialogResult.OK)
                {
                    txtFolder2.Text = s.FileName;

                    V6Tools.V67z.ZipFolder(txtFolder2.Text, txtFolder1.Text);
                    V6Message.Show("Ok");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            V6Tools.V67z.WriteAllResources();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = V6Tools.ChuyenMaTiengViet.UNICODEtoVNI(textBox1.Text);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = V6Tools.ChuyenMaTiengViet.HoaDauTu(textBox1.Text);
        }

       
    }
}
