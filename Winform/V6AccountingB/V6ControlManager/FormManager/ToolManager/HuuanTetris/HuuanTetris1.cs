using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using V6Controls.Forms;


namespace HuuanTetris
{
    public partial class Form1 : V6Control
    {
        struct Vitri
        {
            public int y;
            public int x;
        }
        #region Variable
        
        Vitri vị_trí = new Vitri();
        byte dòng = 24, cột = 10; int cao = 0;
        
        byte[,] lò_gạch;// = new byte[24, 10];
        byte[,] lò_gạch_ghép;
        byte[,] cục_gạch_đang_rơi;
        byte[,] cục_gach_tiếp_theo;

        byte[,] cục_gạch_vuông = { { 1, 1 },            //
                                   { 1, 1 } };          //

        byte[,] cục_gạch_dài = { { 2, 2, 2, 2 } };

        byte[,] cục_gạch_L = { { 3, 3 ,3 },
                               { 3, 0 ,0}};

        byte[,] cục_gạch_J = { {4,4,4},
                               {0,0,4}};

        byte[,] cục_gạch_N = {{0,5,5},
                              {5,5,0}};

        byte[,] cục_gạch_Z = {{6,6,0},
                              {0,6,6}};

        byte[,] cục_gạch_T = {{7,7,7},
                              {0,7,0}};

        
        #endregion
        
        public Form1()
        {
            InitializeComponent();
            Khởi_tạo_lò_gạch();
            Khởi_tạo_G();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            Tạo_cục_gạch_tiếp_theo();
            Lấy_cục_gạch_tiếp_theo();
            Ghép_gạch(lò_gạch, cục_gạch_đang_rơi);
            this.Invalidate();
            timer1.Start();
            timer2.Start();

            ReadRecodes();
        }

        string Hencode(string s)
        {
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(s);

            string result = string.Empty;
            foreach (byte value in bytes)
            {
                string binarybyte = Convert.ToString(value, 2);
                while (binarybyte.Length < 8)
                {
                    binarybyte = "0" + binarybyte;
                }
                result += binarybyte;
            }

            //byte[] resultBytes = Encoding.ASCII.GetBytes(result);
            char[] resultChars = result.ToCharArray();

            for (int i = 0; i < resultChars.Length; i+=3)
            {
                if (resultChars[i] == '0') resultChars[i] = '1';
                else resultChars[i] = '0';
            }

            result = "";
            foreach (char item in resultChars)
            {
                result += item;
            }
            return result;
        }
        string Hdecode(string binaryString)
        {
            char[] binaryStringChars = binaryString.ToCharArray();
            for (int i = 0; i < binaryStringChars.Length; i += 3)
            {
                if (binaryStringChars[i] == '0') binaryStringChars[i] = '1';
                else binaryStringChars[i] = '0';
            }
            binaryString = "";
            foreach (char item in binaryStringChars)
            {
                binaryString += item;
            }

            List<byte> bytes = new List<byte>();

            for (int i = 0; i < binaryString.Length; i += 8)
            {
                if (binaryString.Length - i >= 8)
                {
                    String t = binaryString.Substring(i, 8);
                    bytes.Add(Convert.ToByte(t, 2));
                }
            }

            string rString = System.Text.Encoding.ASCII.GetString(bytes.ToArray());
            return rString;
        }

        SortedList<int, string> recodes = new SortedList<int,string>();
        string file = "recodes.rec";
        string name = "";
        void ReadRecodes()
        {   
            if (File.Exists(file))
            {
                FileStream fs = new FileStream(file, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string s = sr.ReadToEnd();
                sr.Close(); fs.Close();
                
                s = Hdecode(s);
                string[] sss = s.Split(';');
                recodes.Clear();
                foreach (string item in sss)
                {
                    string[] ss = item.Split(',');
                    int đ = Convert.ToInt32(ss[0]);
                    recodes.Add(đ,ss[1]);
                }
                ViewRecodes();
            }
        }
        void WriteRecodes()
        {
            FileStream fs = new FileStream(file, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //tạo chuỗi
            string s = "";
            foreach (var item in recodes)
            {
                s += ";" + item.Key + "," + item.Value;
            }
            if(s.Length>0)
                s = s.Substring(1);

            s = Hencode(s);
            sw.Write(s);
            sw.Close();
            fs.Close();
        }
        void Thêm_điểm(int điểm, string tên)
        {

            if (recodes.ContainsKey(điểm))
            {
                recodes[điểm] = tên;//Không hay lắm
            }
            else
            {
                recodes.Add(điểm, tên);
            }
            while (recodes.Count > 10)
            {
                recodes.RemoveAt(0);
            }
            WriteRecodes();
            ViewRecodes();
            
        }
        void ViewRecodes()
        {
            //Hiển thị lên form
            string viewString = "";
            foreach (var item in recodes)
            {
                viewString = item.Key + ":" + item.Value + "\r\n" + viewString;
            }
            lblKỷ_lục.Text = viewString;
        }


        void NewGame()
        {
            Khởi_tạo_lò_gạch();
            Khởi_tạo_G();
            đếm = 0; đếm_để_rơi = 100;
            điểm_dòng = 0;
            cấp_độ = 0;
            //thiếu thêm sau


            Tạo_cục_gạch_tiếp_theo();
            Lấy_cục_gạch_tiếp_theo();
            Ghép_gạch(lò_gạch, cục_gạch_đang_rơi);
            this.Invalidate();
            timer1.Start();
            timer2.Start();
            lbtPause.Visible = true;
        }

        byte[,] Xoay_cuc_gach(byte[,] cucgach, bool cungchieukimdongho)
        {

            int gạch_dọc = cucgach.GetLength(0), gạch_ngang = cucgach.GetLength(1);
            int ngang_tạm = gạch_dọc, dọc_tạm = gạch_ngang;
            //tao mot mang moi voi so dong va cot nguoc lai voi cucgach
            byte[,] cục_gạch_tạm = new byte[dọc_tạm, ngang_tạm];
            if (cungchieukimdongho) //xoay cung chieu kim dong ho
            {
                //[][][]      [][]          []      []
                //[]       =>   []  =>  [][][]  =>  []
                //              []                  [][]
                for (int i = 0; i < gạch_dọc; i++)
                {
                    for (int j = 0; j < gạch_ngang; j++)
                    {
                        cục_gạch_tạm[j, gạch_dọc-i - 1] = cucgach[i, j];
                    }
                }
            }
            else //xoay nguoc chieu kim dong ho
            {
                //[][][]        []              []      [][]
                //[]       =>   []      =>  [][][]  =>    []
                //              [][]                      []
                for (int i = 0; i < gạch_dọc; i++)
                {
                    for (int j = 0; j < gạch_ngang; j++)
                    {
                        cục_gạch_tạm[gạch_ngang-j-1, i] = cucgach[i, j];
                    }
                }
            }


            bool được_phép_xoay = true;
            Vitri v = new Vitri();
            v.x = vị_trí.x; v.y = vị_trí.y;
            if (ngang_tạm == 4 && dọc_tạm == 1)
            {
                v.x--; v.y++;
            }
            else if (ngang_tạm == 1 && dọc_tạm == 4)
            {
                v.x++; v.y--;
            }


            if (v.x < 0) v.x = 0;
            else if (v.x + ngang_tạm > cột) v.x = cột - ngang_tạm;
            if (v.y < 0) v.y = 0;
            else if (v.y + dọc_tạm > dòng) v.y = dòng - dọc_tạm;

            //Nếu xuống quá đáy thì false
            if (v.y + dọc_tạm > dòng) được_phép_xoay = false;

            for (int i = 0; i < dọc_tạm; i++)
            {
                for (int j = 0; j < ngang_tạm; j++)
                {
                    if (i + v.y < dòng && j + v.x < cột)
                    {
                        if (lò_gạch[i + v.y, j + v.x] != 0 && cục_gạch_tạm[i, j] != 0)
                        {
                            được_phép_xoay= false;
                        }
                    }
                    else
                    {
                        được_phép_xoay= false;
                    }
                }
            }



            if (được_phép_xoay)
            {
                vị_trí = v;
                return cục_gạch_tạm;
            }
            else
            {
                return cucgach;
            }
        }


        void Khởi_tạo_lò_gạch()
        {
            lò_gạch = new byte[dòng, cột];
            //Điền dữ liệu trống
            for (int i = 0; i < dòng; i++) //dòng cuối = lò_gạch.GetUppperBound(0);
            {
                for (int j = 0; j < cột; j++)
                {
                    lò_gạch[i, j] = 0;
                }
            }
            lò_gạch_ghép = (byte[,]) lò_gạch.Clone();
        }

        

        Graphics __G, __Gnext;
        Pen __P;
        SolidBrush __Br;
        SolidBrush[] __Brs;
        private Color BACKGROUND_COLOR = Color.White;
        private Color O_COLOR = Color.Blue;
        private Color I_COLOR = Color.Red;
        private Color L_COLOR = Color.DeepPink;
        private Color J_COLOR = Color.Orange;
        private Color N_COLOR = Color.Green;
        private Color Z_COLOR = Color.DarkBlue;
        private Color T_COLOR = Color.DarkGreen;
        private void Khởi_tạo_G()
        {
            __G = panel1.CreateGraphics();
            __Gnext = panel2.CreateGraphics();
            cao = (int)__G.VisibleClipBounds.Height / dòng;

            __P = new Pen(Color.Black);
            __Br = new SolidBrush(Color.Red);
            __Brs = new SolidBrush[8];
            __Brs[0] = new SolidBrush(BACKGROUND_COLOR);
            __Brs[1] = new SolidBrush(O_COLOR);
            __Brs[2] = new SolidBrush(I_COLOR);
            __Brs[3] = new SolidBrush(L_COLOR);
            __Brs[4] = new SolidBrush(J_COLOR);
            __Brs[5] = new SolidBrush(N_COLOR);
            __Brs[6] = new SolidBrush(Z_COLOR);
            __Brs[7] = new SolidBrush(T_COLOR);
        }

        void TEST()
        {
            
        }

        bool Ghép_gạch()
        {
            return Ghép_gạch(lò_gạch, cục_gạch_đang_rơi);
        }
        bool Ghép_gạch(byte[,] lg, byte[,] g)
        {
            int g_cao = g.GetLength(0);
            int g_rộng = g.GetLength(1);
            lò_gạch_ghép = (byte[,]) lò_gạch.Clone();
            Vitri v = new Vitri(); v.x = vị_trí.x; v.y = vị_trí.y;
            
            bool rb = true;

            for (int i = 0; i < g_cao; i++)
            {
                for (int j = 0; j < g_rộng; j++)
                {
                    if (i + v.y < dòng && j + v.x < cột)
                    {
                        if (lò_gạch_ghép[i + v.y, j + v.x] == 0 && g[i,j] != 0)
                        {
                            lò_gạch_ghép[i + v.y, j + v.x] = g[i, j];
                        }
                        else
                        {
                            //rb = false;
                            //break;
                        }
                    }
                    else
                    {
                        rb = false;
                    }
                }
                if (rb == false) break;
            }
            
            return rb;
        }
        bool được_phép_rơi_xuống
        {
            get
            {
                int g_dọc = cục_gạch_đang_rơi.GetLength(0);
                int g_ngang = cục_gạch_đang_rơi.GetLength(1);

                Vitri v = new Vitri();
                v.x = vị_trí.x;
                v.y = vị_trí.y + 1;
                //Nếu xuống quá đáy thì false
                if (v.y + g_dọc > dòng) return false;

                for (int i = 0; i < g_dọc; i++)
                {
                    for (int j = 0; j < g_ngang; j++)
                    {
                        if (i + v.y < dòng && j + v.x < cột)
                        {
                            if (lò_gạch[i + v.y, j + v.x] != 0 && cục_gạch_đang_rơi[i, j] != 0)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }
        bool được_phé_qua_trái
        {
            get
            {
                int g_dọc = cục_gạch_đang_rơi.GetLength(0);
                int g_ngang = cục_gạch_đang_rơi.GetLength(1);

                Vitri v = new Vitri();
                v.x = vị_trí.x - 1;
                v.y = vị_trí.y;
                //Nếu quá bên trái return false
                if (v.x < 0) return false;

                for (int i = 0; i < g_dọc; i++)
                {
                    for (int j = 0; j < g_ngang; j++)
                    {
                        if (i + v.y < dòng && j + v.x < cột)
                        {
                            if (lò_gạch[i + v.y, j + v.x] != 0 && cục_gạch_đang_rơi[i, j] != 0)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }
        bool được_phép_qua_phải
        {
            get
            {
                int g_dọc = cục_gạch_đang_rơi.GetLength(0);
                int g_ngang = cục_gạch_đang_rơi.GetLength(1);

                Vitri v = new Vitri();
                v.x = vị_trí.x + 1;
                v.y = vị_trí.y;
                //Nếu cục gạch quá bên phải return false
                if (v.x + g_ngang > cột) return false;

                for (int i = 0; i < g_dọc; i++)
                {
                    for (int j = 0; j < g_ngang; j++)
                    {
                        if (i + v.y < dòng && j + v.x < cột)
                        {
                            if (lò_gạch[i + v.y, j + v.x] != 0 && cục_gạch_đang_rơi[i, j] != 0)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }
        
        void Rơi_xuống()
        {
            vị_trí.y++;
            đếm = 0;
            Ghép_gạch();
        }
        void Qua_trái()
        {
            vị_trí.x--;
            Ghép_gạch();
        }
        void Qua_phải()
        {
            vị_trí.x++;
            Ghép_gạch();
        }
        void Xoay_gạch(bool b)
        {
            cục_gạch_đang_rơi = Xoay_cuc_gach(cục_gạch_đang_rơi, b);
            Ghép_gạch();
        }
        static Random r = new Random();

        void Tạo_cục_gạch_tiếp_theo()
        {
            int switch_int = r.Next(1000);
            switch (switch_int % 7)
            {
                case 0:
                    cục_gach_tiếp_theo = (byte[,])cục_gạch_vuông.Clone();
                    break;
                case 1:
                    cục_gach_tiếp_theo = (byte[,])cục_gạch_dài.Clone();
                    break;
                case 2:
                    cục_gach_tiếp_theo = (byte[,])cục_gạch_L.Clone();
                    break;
                case 3:
                    cục_gach_tiếp_theo = (byte[,])cục_gạch_J.Clone();
                    break;
                case 4:
                    cục_gach_tiếp_theo = (byte[,])cục_gạch_N.Clone();
                    break;
                case 5:
                    cục_gach_tiếp_theo = (byte[,])cục_gạch_Z.Clone();
                    break;
                case 6:
                    cục_gach_tiếp_theo = (byte[,])cục_gạch_T.Clone();
                    break;
                default:
                    cục_gach_tiếp_theo = (byte[,])cục_gạch_vuông.Clone();
                    break;
            }
        }
        bool Lấy_cục_gạch_tiếp_theo()
        {
            //Kiểm tra điểm trước
            Kiểm_tra_tính_điểm();

            //đặt lại vị trí
           
            cục_gạch_đang_rơi = (byte[,])cục_gach_tiếp_theo.Clone();
            vị_trí.x = cột / 2 - cục_gạch_đang_rơi.GetLength(1) / 2;
            vị_trí.y = 0;
            Ghép_gạch();//cho hiện hình cục gạch đang rơi
            //Kiểm_tra_tính_điểm();
            Tạo_cục_gạch_tiếp_theo();

            //Kiểm tra
            int g_dọc = cục_gạch_đang_rơi.GetLength(0);
            int g_ngang = cục_gạch_đang_rơi.GetLength(1);

            Vitri v = new Vitri();
            v.x = vị_trí.x;
            v.y = vị_trí.y;
            //Nếu cục gạch quá bên phải return false
            if (v.x + g_ngang > cột) return false;

            for (int i = 0; i < g_dọc; i++)
            {
                for (int j = 0; j < g_ngang; j++)
                {
                    if (i + v.y < dòng && j + v.x < cột)
                    {
                        if (lò_gạch[i + v.y, j + v.x] != 0 && cục_gạch_đang_rơi[i, j] != 0)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
            //Nếu false sẽ game over
        }

        int điểm_dòng = 0;
        int cấp_độ = 0;
        int điểm_khác = 0;
        void Kiểm_tra_tính_điểm()
        {
            //lặp qua các dòng trong đống gạch từ dưới lên (index cao đến index thấp)
            //Nếu dòng nào full thì + điểm, dời hết đám trên xuống 1 bậc, tiếp tục kiểm 
            //từ dòng đó.
            bool ăn_điểm = true;
            for (int i = dòng-1; i >=0; i--)
            {
                ăn_điểm = true;
                for (int j = 0; j < cột; j++)
                {
                    if (lò_gạch[i, j] == 0)
                    {
                        ăn_điểm = false;
                        break;
                    }
                }
                if (ăn_điểm)
                {
                    điểm_dòng++;
                    if (điểm_dòng % 30 == 0)
                    {
                        cấp_độ++; lblCấp_độ.Text = cấp_độ.ToString();
                        đếm_để_rơi = đếm_để_rơi * 3 / 4;
                    }
                    //Xụp gạch
                    for (int k = i; k > 0; k--)
                    {
                        for (int j = 0; j < cột; j++)
                        {
                            lò_gạch[k, j] = lò_gạch[k - 1, j];
                        }
                    }
                    for (int j = 0; j < cột; j++)
                    {
                        lò_gạch[0, j] = 0;
                    }
                    i++;//cộng để -- mất tác dụng
                }
            }
            this.Invalidate();
            lblĐiểm_dòng.Text = điểm_dòng.ToString();
        }
        

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if(lò_gạch_ghép != null)
                Vẽ_lò_gạch(__G, lò_gạch_ghép);
            if (cục_gach_tiếp_theo != null)
            {
                Vẽ_cục_gạch(__Gnext, cục_gach_tiếp_theo);
            }
        }

        private void Vẽ_cục_gạch(Graphics graphics, byte[,] gạch)
        {
            graphics.Clear(Form1.DefaultBackColor);
            int hàng = gạch.GetLength(0);
            int cột = gạch.GetLength(1);
            for (int i = 0; i < hàng; i++)
            {
                for (int j = 0; j < cột; j++)
                {
                    int ii = gạch[i, j];
                    if (ii == 0)
                    {
                        //SolidBrush sb = new SolidBrush(Color.White);
                        //graphics.FillRectangle(sb, j * cao + 1, i * cao + 1, cao - 2, cao - 2);
                    }
                    else
                    {
                        graphics.FillRectangle(__Brs[ii], j * cao + 1, i * cao + 1, cao - 2, cao - 2);
                    }
                }
            }
        }
        private void Vẽ_lò_gạch(Graphics graphics,byte[,] lg)
        {   
            for (int i = 0; i < dòng; i++)
            {
                for (int j = 0; j < cột; j++)
                {
                    int ii = lg[i, j];
                    if (ii == 0)
                    {
                        graphics.FillRectangle(__Brs[0], j * cao + 1, i * cao + 1, cao - 2, cao - 2);
                    }
                    else
                    {
                        graphics.FillRectangle(__Brs[ii], j * cao + 1, i * cao + 1, cao - 2, cao - 2);
                    }
                }
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Khởi_tạo_G();
        }


        int đếm_để_rơi = 100;
        int đếm = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            đếm++;
            if (đếm >= đếm_để_rơi)
            {
                
                //Xử lý chạm đáy, nối luôn cục gạch đang rơi vào lò gạch
                //Xử lý điểm.

                đếm = 0;
                if (được_phép_rơi_xuống)
                {
                    Rơi_xuống();
                    Ghép_gạch(lò_gạch, cục_gạch_đang_rơi);
                }
                else
                {   
                    lò_gạch = (byte[,]) lò_gạch_ghép.Clone();
                    
                    if (!Lấy_cục_gạch_tiếp_theo())
                    {
                        timer1.Stop(); timer2.Stop();
                        lbtPause.Visible = false;
                        //Form nhập tên
                        HuuanTetrisEnterName f = new HuuanTetrisEnterName();
                        f.ShowDialog();
                        name = f.inputName;
                        Thêm_điểm(this.điểm_dòng, name);
                        //MessageBox.Show("Game over!");
                    }
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        public override bool DoHotKey0(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                    if (timer1.Enabled)
                    {
                        if (được_phé_qua_trái)
                            Qua_trái();
                    }
                    break;
                case Keys.Right:
                    if (timer1.Enabled)
                    {
                        if (được_phép_qua_phải) Qua_phải();
                    }
                    break;
                case Keys.Down:
                    if (timer1.Enabled)
                    {
                        if (!vô_hiệu_bấm_xuống)
                        {
                            if (được_phép_rơi_xuống) Rơi_xuống();
                            else
                            {
                                vô_hiệu_bấm_xuống = true;
                                lò_gạch = lò_gạch_ghép;
                                Lấy_cục_gạch_tiếp_theo();
                            }
                        }
                    }
                    //vị_trí.y++;//chú ý
                    break;
                case Keys.Up:
                case Keys.X:
                    //Xoay theo chiều kim đồng hồ
                    //if(được_phép_xoay)
                    if (timer1.Enabled)
                    {
                        Xoay_gạch(true);
                    }
                    break;
                case Keys.Z:
                    if (timer1.Enabled)
                    {
                        Xoay_gạch(false);
                    }
                    break;
                case Keys.P:
                    if (timer1.Enabled) timer1.Stop();
                    else timer1.Start();
                    break;
                default:
                    break;
            }
            return false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }
        bool vô_hiệu_bấm_xuống = false;
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            vô_hiệu_bấm_xuống = false;
        }

        private void btnNhạc_Click(object sender, EventArgs e)
        {
            
        }

        private void label4_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void lbtPause_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled) timer1.Stop();
            else timer1.Start();
        }
    }
}
