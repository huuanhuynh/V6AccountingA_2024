using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.XPath;
using System.IO;

namespace Encrypt_Decrypt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            string strEncrypt = "";
            UtilityHelper UHelper = new UtilityHelper();
            strEncrypt = UHelper.EnCrypt(txtEncrypt.Text.Trim(), "MrV6@0936976976");
            txtEncrypt_Result.Text = strEncrypt;
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            string strDecrypt = "";
            UtilityHelper UHelper = new UtilityHelper();
            strDecrypt = UHelper.DeCrypt(txtDecrypt.Text.Trim(), "MrV6@0936976976");
            txtDecrypt_Result.Text = strDecrypt;
        }
    }
}
