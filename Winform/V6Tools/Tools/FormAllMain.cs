using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using V6Tools;
using V6Tools.V6Convert;

namespace Tools
{
    public partial class FormAllMain : Form
    {
        public FormAllMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new ToolExportSqlToExcel().Show();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            new FormHuuanEditText().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new FormConvertTable().Show();
        }

        private void btnDBF_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Dùng convert table!");
            //new FormDBF().Show();
        }

        private void btnModelHelp_Click(object sender, EventArgs e)
        {
            new FormModelHelp().Show();
        }

        private void btnUploadFTP_Click(object sender, EventArgs e)
        {
            var open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                UploadDownloadFTP ftp = new UploadDownloadFTP("118.69.183.160", "huuan", UtilityHelper.EnCrypt("_D21C2V62015"));
                ftp.Upload(open.FileName);
                MessageBox.Show("Test Xong");
            }
        }

        private void btnCopyToV6_Click(object sender, EventArgs e)
        {
            var open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                //118.69.183.160
                V6FileIO.CopyToVPN(open.FileName, "", "118.69.183.16", "huuan", UtilityHelper.EnCrypt("_D21C2V62015"));
            }
        }

        private void btnParseDecimal_Click(object sender, EventArgs e)
        {
            try
            {
                numericUpDown1.Value = Decimal.Parse(textBox1.Text, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFilterTextFiles_Click(object sender, EventArgs e)
        {
            try
            {
                new FormFilterTextFiles().Show(this);
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(GetType() + ".Filter text files", ex, "");
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTestHash_Click(object sender, EventArgs e)
        {
            try
            {
                string message_out = "";
                string message2_trung = "";
                var input_list_01 =
                    @"C# Corner
Free Book Chapter Download - Introduction to WPF
Become a member LoginPost Ask Question
C# Corner Search
 TECHNOLOGIESANSWERSLEARNNEWSBLOGSVIDEOSINTERVIEW PREPBOOKSEVENTSCAREERMEMBERSJOBS
How do I	
Convert char array to string in C#
Mahesh Chand Mahesh Chand  Updated date Dec 19, 2018
  436.9k 2 3
facebook
twitter
linkedIn
Reddit
Expand
Download Free .NET & JAVA Files API
Try Free File Format APIs for Word/Excel/PDF
The string class constructor takes an array of characters to create a new string from an array of characters. The following code snippet creates two strings. First from a string and second by direct passing the array in the constructor.

// Convert char array to string  
char[] chars = new char[10];  
chars[0] = 'M';  
chars[1] = 'a';  
chars[2] = 'h';  
chars[3] = 'e';  
chars[4] = 's';  
chars[5] = 'h';  
string charsStr = new string(chars);  
string charsStr2 = new string(new char[]  
{'T','h','i','s',' ','i','s',' ','a',' ','s','t','r','i','n','g'});  
Here is a complete sample code:
public void StringToCharArray()  
{  
  
    // Convert string to char array  
    string sentence =  Mahesh Chand ;  
    char[] charArr = sentence.ToCharArray();  
    foreach (char ch in charArr)  
    {  
  
        Console.WriteLine(ch);  
    }   
  
    // Convert char array to string  
    char[] chars = new char[10];  
    chars[0] = 'M';  
    chars[1] = 'a';  
    chars[2] = 'h';  
    chars[3] = 'e';  
    chars[4] = 's';  
    chars[5] = 'h';  
    string charsStr = new string(chars);  
    string charsStr2 = new string(new char[]  
    {'T','h','i','s',' ','i','s',' ','a',' ','s','t','r','i','n','g'});  
    Console.WriteLine( Chars to string: {0} , charsStr);  
    Console.WriteLine( Chars to string: {0} , charsStr2);  
}   
The output looks like the following:
 
Str2BtArr.png
C# char arrayC# char to stringC# stringcharacter array to stringconvert character array to stringconvert chart to string


Next Recommended Reading
URL Rewriting Middleware In ASP.NET Core - Day Eight
OUR BOOKS

Mahesh Chand	Mahesh Chand Admin
Founder C# Corner. Founder & CEO Mindcracker Inc. Investor, Advisor, Board member of several startups and non profit foundations. Try to implement emerging technologies when trying to solve the next problem.

https://www.c-sharpcorner.com193.5m114
3 2
	
Type your comment here and press Enter Key (Minimum 10 characters)
binod km	
hey thanks man for the article. but do the how will you do the same if the array is a multidimentional one. say i need to convert all rows of a 2D array arr[5,5] one after another into a string for some manipulation, how will i do that. thanks again
Oct 23, 2012binod km
2016 5 4.9k1  1Reply
Hi-Radical Hi-Radical	
Loop through the first dimension of the array and do nest a for loop/for loops inside of it to loop through the other dimension(s) and do whatever you want with the strings.
Mar 17, 2021Hi-Radical Hi-Radical
2019 2 00
FEATURED ARTICLES
Write Your First Smart Contract On Stratis Blockchain
Managing Files On GitHub Using Git Bash 📥📤 In Real-Time Scenario - Know About GitHub Reviewer
Managing Files On GitHub Using Git Bash 📥📤 In Real-Time Scenario - Owner Uploads Project In GitHub Repo
Using Azure Blob Storage In C#
Post GitHub Events To A Microsoft Teams Channel Using C#.NET
View All

TRENDING UP
01	Azure Blob Storage - Upload/Download/Delete File(s) Using .NET 5
02	What Is Storage Area Network And Storage Protocols
03	Managing Files On GitHub Using Git Bash 📥📤 In Real-Time Scenario - Owner Uploads Project In GitHub Repo
04	Getting Started with Image Analysis using Azure Cognitive Services
05	OData In .NET 5
06	Building Lightweight APIs In .NET 6
07	Using Azure Application Insights For Exception Logging In C#
08	Working With MudBlazor UI Component In Blazor Using .Net 5
09	Building End-to-End Custom Image Classifier using Azure Custom Vision
10	Post GitHub Events To A Microsoft Teams Channel Using C#.NET
View All

     
About Us Contact Us Privacy Policy Terms Media Kit Sitemap Report a Bug FAQ Partners
C# Tutorials Common Interview Questions Stories Consultants Ideas Certifications
©2021 C# Corner. All contents are copyright of their authors.Bỏ qua để đến phần nội dung chínhHỗ trợ truy cập
Phản hồi về hỗ trợ truy cập
Google
linhkienthihuong

Tất cả
Tin tứcHình ảnhVideoThêm
Cài đặt
Công cụ
Tìm kiếm an toàn đang bật
Khoảng 44.500 kết quả (0,74 giây) 
Đang hiển thị kết quả cho linh kien thy huong
Tìm kiếm thay thế cho linhkienthihuong


LINH KIỆN THY HƯƠNGhttps://linhkienbansi.com
Cung cấp sỉ loa nghe nhạc, pin dự phòng, camera, phụ kiện vi tính, thiết bị mạng, đèn LED, điện gia dụng, đồ chơi công nghệ.
Bảng giá sỉ
Cung cấp sỉ loa nghe nhạc, pin dự phòng, camera, phụ kiện vi tính ...
Chính sách & quy định
Cung cấp sỉ loa nghe nhạc, pin dự phòng, camera, phụ kiện vi tính ...
Sản phẩm
Trang chủ; Sản phẩm Open submenu; Bán chạy · Hướng ...
Chế độ bảo hành
Cung cấp sỉ loa nghe nhạc, pin dự phòng, camera, phụ kiện vi tính ...
LOA
Loa vi tính 2.1 Bosston T1800 điện 220V ... Loa Bluetooth mini A10 ...
Liên hệ
home Trang chủ / Liên hệ · LINH KIỆN THY HƯƠNG · Địa chỉ ...
Các kết quả khác từ linhkienbansi.com »

linh kien thy huonghttps://123requare.com › tag › linh-kien-thy-huong
Danh sách linh kien thy huong hot nhất hiện nay: Đầu cắm kết nối nguồn điện, IGBT 40N65/40T65 40A650V sò công suất, Miếng dán Sticker cho bình xăng, ...

THY HƯƠNG - SỈ PHỤ KIỆN, GIA DỤNG, ĐỒ CÔNG NGHỆhttp://muabanraovat.com › mua-ban › thy-huong-si-ph...
LINH KIỆN THY HƯƠNG PHÂN PHỐI SỈ LOA NGHE NHẠC. PHỤ KIỆN ĐT, PHỤ KIỆN VI TÍNH,. ĐỒ CHƠI CÔNG NGHỆ,. THIẾT BỊ ĐIỆN GIA DỤNG. GIÁ RẺ ...

Linh Kiện Thy Hương - Nhà Cung Cấp Linh Kiện Điện Tửhttps://linh-kien-thy-huong.business.site
- Micro Karaoke LiveStream C7 thiết bị hát livestream facebook nhỏ gọn, đặc biệt hát app karaoke trên điện thoại thì hay tuyệt vời, đầy đủ tính năng như một sound​ ...

Linh Kiện Thy Hương, 246A Nguyễn Thái Bình, Phường 12 ...https://vn.asiafirms.com › ... › Electronic Parts Supplier
Reviews about Linh Kiện Thy Hương, Hồ Chí Minh, phone numbers, addresses, hours. Leave your feedback.
 Xếp hạng: 3,4 · ‎5 đánh giá

Linh Kiện Thy Hương - Mapdyhttps://mapdy.com › linh-kien-thy-huong-fWeNr
Loa nghe nhạc, pin, cáp, sạc, USB, thẻ nhớ, tai nghe bluetooth, đồ chơi vi tính, dán màn hình, pin dự phòng.

linh kien thy huong xịn - Rẻ nữa nè 3https://renuane3.com › tag › linh-kien-thy-huong
Sản phẩm linh kien thy huong chính hãng với giá phải chăng khi mua sắm online​: Phụ kiện máy xay chính hãng, Linh kiện máy xay magic, okusanno,, Linh Kiện ...
Tìm kiếm có liên quan
Linhkiengiasi
Linh kiện Hùng Anh
Mytindigital loa bluetooth
Linh kiện mỹ tin
Linh kiện giá sỉ
CỬA HÀNG THY HƯƠNG
LINH kiện Mậu Nguyên
Linh kiện TH
1	
2
3
4
5
6
7
8
9
10
Tiếp

Xem ảnh
Bản đồ của Linh Kiện Thy Hương
Biểu trưng của người bán
Linh Kiện Thy Hương
3,8
16 đánh giá trên Google
Nhà cung cấp linh kiện điện tử ở Hồ Chí Minh
Địa chỉ: 246A Nguyễn Thái Bình, Phường 12, Tân Bình, Thành phố Hồ Chí Minh 70000
Các giờ: 
Sắp đóng cửa ⋅ 17:30 ⋅ Mở cửa lúc 8:00 Th 3
Tết Đoan Ngọ có thể ảnh hưởng đến những giờ này
Điện thoại: 090 917 37 30
Đề xuất chỉnh sửa · Bạn sở hữu doanh nghiệp này?
Bạn có biết địa điểm này không?Chia sẻ thông tin mới nhất
Sản phẩm
Xem tất cả
Sạc xe hơi Hoco Z29 2.4A 2 USB
Sạc xe hơi Hoco Z29 2.4A 2 USB
66.000 ₫
Tai nghe bluetooth Hoco E49
Tai nghe bluetooth Hoco E49
125.000 ₫
Dây đui đèn chống nước 10m, 20 đui
Dây đui đèn chống nước 10m, 20 đui
210.000 ₫
Bộ sạc Hoco C80 18W sạc nhanh (kèm cáp Type C - Lightning
Bộ sạc Hoco C80 18W sạc nhanh (kèm cáp Type C - Lightning
95.000 ₫
Loa bluetooth Borofone BR3
Loa bluetooth Borofone BR3
180.000 ₫
Kích wifi Pix-Link 2 anten
Kích wifi Pix-Link 2 anten
195.000 ₫
Mic trợ giảng không dây 2.4Ghz
Mic trợ giảng không dây 2.4Ghz
295.000 ₫
Camera Yoosee 5 anten 2.0MP
Camera Yoosee 5 anten 2.0MP
285.000 ₫
Loa vi tính Kisonli S-999
Loa vi tính Kisonli S-999
65.000 ₫
Pin dự phòng Romoss sense4
Pin dự phòng Romoss sense4
185.000-195.000 ₫
Xem tất cả
Khám phá các danh mục
Phụ kiện xe hơi
Tai nghe
Đồ gia dụng
Phụ kiện Điện Thoại
Hỏi và đáp
Xem tất cả câu hỏi (2)
Giờ đông khách
TH 2
TH 3
TH 4
TH 5
TH 6
TH 7
CN
17:00: Thường không quá đông đúc
Gửi tới điện thoại của bạn
Các bài đánh giá từ web
3,4/5Business Directory Vietnam - asiafirms.com · 5 đánh giá
Bài đánh giá
16 đánh giá trên Google
Từ Linh Kiện Thy Hương
 Chuyên cung cấp sỉ phụ kiện điện thoại, thiết bị ngoại vi máy tính, Loa nghe nhạc, điện gia dụng, đồ chơi công nghệ... 
Linh Kiện Thy Hương
Linh Kiện Thy Hương
trên Google
Micro cho máy tính bàn, laptop để ghi âm, học online https://linhkienbansi.com
13 thg 5, 2021
6 thg 3, 2021
6 thg 3, 2021
6 thg 3, 2021
14 thg 1, 2021
14 thg 1, 2021
- Micro Karaoke LiveStream C7 thiết bị hát livestream facebook nhỏ gọn, đặc biệt hát app…
6 thg 1, 2021
Đặt trước
6 thg 1, 2021
24 thg 12, 2020
24 thg 12, 2020
Xem tất cả
Mọi người cũng tìm kiếm
Xem hơn 15 mục khác

Cửa Hàng Phụ Kiện Mp3 Thy...
Cửa hàng

Linh Kiện Giá Sỉ
Cửa hàng bán đồ điện tử

Cửa Hàng Linh Kiện Điện Tử
Cửa hàng thiết bị điện

Linh Kiện HƯNG PHÁT
Cửa hàng đồ gia dụng

PHỤ KIỆN TÂN BÌNH
Cửa hàng bán đồ điện tử
Thông tin về dữ liệu này
Phản hồi
Việt NamTân Bình, Thành phố Hồ Chí Minh - Từ địa chỉ Internet của bạn - Sử dụng vị trí chính xác - Tìm hiểu thêm
Trợ giúpGửi phản hồiBảo mậtĐiều khoảnBigass NES Mapper List 0.1 - This document is intended to serve as some sort
of master list for ROMs.  For example, say you find something set to the
wrong mapper, but you have no idea where it should be - check here and save
yourself time.

What it is - A damn near complete list of all the .NES format ROMs out there,
             with a proverbial buttload of entries (don't feel like counting)
             and mapper info for each.  

What it's not - Entirely complete.  There's a rather large number of Japanese
                ROMs missing from this list (Hi, Keropi!) but I don't feel
                that their absence is that big of a deal, considering that
                those particular ROMs are only in the hands of a -very- few
                people.  Consider this a complete list for  findable  ROMs.
                Also, the mirroring info given isn't always to be trusted.
                These just happen to be what mine are set on, so if one doesn't
                work, flip it.

Suggestions - If you have some suggestions for the list, mail me at the address
              below.  Should I divide these up into US/Japanese/Euro, or leave
              it like it is?  Also, what about FDS games - any use in adding
              those?

If you have any questions or whatnot, you can mail me at lugnut@hotmail.com


-

Title                           Size                Mirroring   Mapper

10 Yard Fight                   32k PRG / 8k CHR        H       ---- (0)
10 Yard Fight (J)               16k PRG / 8k CHR        H       ---- (0)
100 In 1 - Contra Function 16   1024k PRG / 0k CHR      V       (15)
100 Kiyagyou                    128k PRG / 128k CHR     H       MMC1 (1)
100 Man Doru Kid                256k PRG / 0k CHR       H       UNROM (2)
110 in 1 Menu                   16k PRG / 8k CHR        V       ---- (0)
1200 In 1                       512k PRG / 0k CHR       H       (227)
1942 (J)                        32k PRG / 8k CHR        H       ---- (0)
1943                            128k PRG / 0k CHR       H       UNROM (2)
1943 (J)                        128k PRG / 0k CHR       H       UNROM (2)
1944                            128k PRG / 0k CHR       H       UNROM (2)
1999 Hore Mitakotoka Sekimatsu  256k PRG / 128k CHR     V       MMC3 (4)
3D Worldrunner                  256k PRG / 0k CHR       V       Nina-1 (34)
58 In 1                         1024k PRG / 512k CHR    H       (225)
58 In 1 Menu                    16k PRG / 8k CHR        V       ---- (0)
72 In 1                         1024k PRG / 512k CHR    H       (225)
720                             128k PRG / 0k CHR       V       MMC1 (1)
76 In 1 Menu                    32k PRG / 0k CHR        H       ---- (0)
8 Eyes                          128k PRG / 128k CHR     H       MMC3 (4)
8 Eyes (J)                      128k PRG / 128k CHR     H       MMC3 (4)
'89 Dennou Hosi Uranai          256k PRG / 0k CHR       H       MMC1 (1)


A-Ressya De Ikou                128k PRG / 16k CHR      H       MMC1 (1)
Abadox				128k PRG / 128k CHR	H	MMC1 (1)
Abadox (J)                      128k PRG / 128k CHR     H       MMC1 (1)
Abarenbou Tengu			128k PRG / 128k CHR	H	MMC3 (4)
Acchi Kocchi Socchi		128k PRG / 128k CHR	H	MMC1 (1)
Action 52                       1536k PRG / 512k CHR    H       (228)
Addams Family, The		128k PRG / 128k CHR	H	MMC1 (1)
Addams Family 2, The (Pugsley)  128k PRG / 128k CHR     H       MMC1 (1)
Adventure Island                32k PRG / 32k CHR       H       CNROM (3)
Adventure Island 2		128k PRG / 128k CHR	H	MMC3 (4)
Adventure Island 3		128k PRG / 128k CHR	H	MMC3 (4)
Adventures in the Magic Kingdom	128k PRG / 128k CHR	H	MMC1 (1)
Adventures of Bayou Billy	128k PRG / 128k CHR	V	MMC1 (1)
Adventures of Bayou Billy (PAL) 128k PRG / 128k CHR	H	MMC1 (1)
Adventures of Captain Comic	64k PRG / 64k CHR	V	Colour Dreams (11)
Adventures of Dino-Riki		32k PRG / 32k CHR	V	CNROM (3)
Adventures of Lolo              32k PRG / 32k CHR       H       MMC1 (1)
Adventures of Lolo (J)          32k PRG / 32k CHR       H       MMC3 (4)
Adventures of Lolo 2            32k PRG / 32k CHR       H       MMC3 (4)
Adventures of Lolo 3            128k PRG / 128k CHR     H       MMC1 (1)
Adventures of Rad Gravity, The  128k PRG / 128k CHR     H       MMC1 (1)
Adventures of Rad Gravity (PAL) 128k PRG / 128k CHR     H       MMC1 (1)
Adventures of Rocky & Bullwinkle128k PRG / 120k CHR     H       MMC3 (4)  <--missing 8k
Adventures of Tom Sawyer, The   128k PRG / 128k CHR     V       MMC1 (1)
Afterburner 2 (J)               128k PRG / 256k CHR     V       Sunsoft 4 (68)
Ai Sensei no Oshiete Watashi no Hosi    256k PRG / 128k CHR     H       Irem G-101 (32)
Aigiina no Yogen                128k PRG / 0k CHR       V       UNROM (2)        
Air Fortress			128k PRG / 32k CHR	H	MMC1 (1)
Air Fortress (J)                128k PRG / 128k CHR     H       MMC1 (1)
Airwolf				32k PRG / 128k CHR	H	MMC1 (1)
Airwolf (PAL)			32k PRG / 128k CHR	H	MMC1 (1)
Akira                           128k PRG / 256k CHR     V       MMC3 (4)
Akuma Kun                       128k PRG / 128k CHR     H       Bandai (16)
Akuma no Shoutaijou             128k PRG / 128k CHR     H       MMC3 (4)
Akumajo Dracula                 128k PRG / 0k CHR       V       UNROM (2)
Akumajo Dracula 3		256k PRG / 128k CHR	V	VRC6 (24)
Akumajo Dracula 3 [Hacked]	256k PRG / 128k CHR	V	Bandai (16)
Al Unser Jr's Turbo Racing	128k PRG / 128k CHR	V	MMC1 (1)
Alfred Chicken			128k PRG / 0k CHR	H	UNROM (2)
Algos no Senshi                 128k PRG / 0k CHR       V       UNROM (2)
Alien 3				128k PRG / 128k CHR	H	MMC3 (4)
Alien Syndrome (J)		128k PRG / 128k CHR	H	MMC1 (1)
All Pro Basketball		128k PRG / 128k CHR	V	MMC1 (1)
Alpha Mission                   32k PRG / 32k CHR       H       CNROM (3)
Altered Beast 			128k PRG / 128k CHR	H	MMC3 (4)
Amagon                          128k PRG / 0k CHR       V       UNROM (2)
America Oudan no Ultra Quiz     512k PRG / 0k CHR       V       MMC3 (4)
American Dream			128k PRG / 128k CHR	H	MMC1 (1)
American Gladiators             128k PRG / 128k CHR     H       MMC1 (1)
Ankoku Sinwa                    256k PRG / 0k CHR       H       MMC1 (1)
Anpan Man no Hiragana Daisuki	128k PRG / 128k CHR	H	MMC3 (4)
Antarctic Adventure             16k PRG / 8k CHR        H       ---- (0)
Anticipation                    32k PRG / 32k CHR       H       MMC1 (1)
Arabian Dream Sharezerd [hacked]256k PRG / 0k CHR       V       FFE F4xxx (6)
Arch Rivals                     128k PRG / 0k CHR       H       AOROM (7)
Archon                          128k PRG / 0k CHR       H       UNROM (2)
Arctic                          128k PRG / 0k CHR       V       UNROM (2)
Argus                           32k PRG / 16k CHR       V       CNROM (3)
Arkanoid                        32k PRG / 8k CHR        V       ---- (0)
Armored Scrum Object            32k PRG / 32k CHR       H       CNROM (3)
Artelius                        128k PRG / 32k CHR      H       MMC1 (1)
Asmikkun Land			128k PRG / 128k CHR	V	MMC3 (4)
Asterix (PAL)                   128k PRG / 0k CHR       V       UNROM (2)
Astro Robo Sasa                 32k PRG / 8k CHR        V       ---- (0)
Astyanax                        128k PRG / 128k CHR     H       MMC3 (4)
Athena                          128k PRG / 0k CHR       V       UNROM (2)
Athena (J)                      128k PRG / 0k CHR       V       UNROM (2)
Athletic World                  32k PRG / 32k CHR       H       CNROM (3)
Atlantis no Nazo                32k PRG / 24k CHR       V       MMC1 (1)
Attack Animal Gakuen            128k PRG / 0k CHR       V       UNROM (2)
Attack of the Killer Tomatoes   128k PRG / 128k CHR     H       MMC1 (1)
AV Pachi Slot                   32k PRG / 64k CHR       H       CNROM (3)


B-Wings                         32k PRG / 8k CHR        H       ---- (0)
Babel no Tou			32k PRG / 32k CHR	H	MMC3 (4)
Baby Boomer			64k PRG / 16k CHR	V	Colour Dreams (11)
Back to the Future              32k PRG / 8k CHR        V       ---- (0)  <--bad dump
Back to the Future 2/3		128k PRG / 128k CHR	V	MMC1 (1)
Bad Dudes			128k PRG / 128k CHR	H	MMC3 (4)
Bad News Baseball		128k PRG / 128k CHR	V	MMC1 (1) 
Bad Street Brawler		128k PRG / 0k CHR	V	MMC1 (1)
Baken Hissyougaku Gate-In       256k PRG / 0k CHR       H       MMC1 (1)        
Bakusyou Jinsei Gekijyou        256k PRG / 128k CHR     H       TC0190 / TC0350 (33)
Bakusyou Jinsei Gekijyou 3      256k PRG / 256k CHR     H       MMC3 (4)
Ballblazer			128k PRG / 0k CHR	V	UNROM (2)
Balloon Fight			16k PRG / 8k CHR	V	---- (0)
Balloon Fight (PAL)		16k PRG / 8k CHR	H	---- (0)
Baltron                         32k PRG / 8k CHR        V       ---- (0)
Banana				32k PRG / 32k CHR	V	CNROM (3)
Bananan no Ouiji Daibouken	128k PRG / 128k CHR	H	MMC3 (4)
Bandit Kings of Ancient China	256k PRG / 128k CHR	H	MMC5 (5)
Barbie				128k PRG / 128k CHR	H	MMC1 (1)
Bard's Tale, The		128k PRG / 0k CHR	H	MMC1 (1)  <-- Possible bad dump?
Bard's Tale, The (J)		256k PRG / 0k CHR	H	MMC1 (1)
Bard's Tale 2, The		256k PRG / 0k CHR	H	MMC1 (1)
Barker Bill's Trick Shooting	64k PRG / 112k CHR	H	MMC1 (1)
Bart vs. the Space Mutants      128k PRG / 128k CHR     V       MMC1 (1)  <-- perhaps missing 128k in the PRG
Bart vs. the World		256k PRG / 128k CHR	H	MMC3 (4)
Bartman Meets Radioactive Man   256k PRG / 128k CHR     H       MMC3 (4)
Base Wars			128k PRG / 128k CHR	H	MMC3 (4)
Baseball			16k PRG / 8k CHR	H	---- (0)
Baseball (J)			16k PRG / 8k CHR	H	---- (0)
Baseball Simulator 1000		128k PRG / 128k CHR	V	MMC1 (1)
Baseball Stars			128k PRG / 128k CHR	V	MMC1 (1)
Baseball Stars (J)		128k PRG / 128k CHR	V	MMC1 (1)
Baseball Stars 2		256k PRG / 128k CHR	H	MMC3 (4)
Bases Loaded			256k PRG / 64k CHR	H	MMC1 (1)
Bases Loaded 2			256k PRG / 128k CHR	H	MMC1 (1)
Bases Loaded 3			256k PRG / 128k CHR	H	MMC3 (4)
Bases Loaded 4			256k PRG / 128k CHR	H	MMC3 (4)
Batman				128k PRG / 128k CHR	H	MMC3 (4)
Batman (J)			128k PRG / 128k CHR	H	Sunsoft 5 (69)
Batman (PAL)			128k PRG / 128k CHR	H	MMC3 (4)
Batman - Return of the Joker	128k PRG / 256k CHR	H	Sunsoft 5 (69)
Batman Returns			128k PRG / 256k CHR	V	MMC3 (4)
Battle Chess			256k PRG / 0k CHR	H	MMC1 (1)
Battle City			16k PRG / 8k CHR	H	---- (0)
Battle of Olympus, The          128k PRG / 0k CHR       V       MMC1 (1)
Battle Stadium			128k PRG / 128k CHR	V	MMC1 (1)
Battle Storm			128k PRG / 128k CHR	V	MMC1 (1)
Battletoads			256k PRG / 0k CHR	H	AOROM (7)
Battletoads (PAL)		256k PRG / 0k CHR	H	AOROM (7)
Battletoads / Double Dragon	256k PRG / 0k CHR	H	AOROM (7)
Batu and Teree			128k PRG / 0k CHR	V	UNROM (2)
Be-Bop High School		128k PRG / 128k CHR	H	MMC1 (1)
Bee 52				65k PRG / 0k CHR	V	Camerica (71)  <-- Bad dump?
Beetlejuice			128k PRG / 0k CHR	H	AOROM (7)
Best of the Best		256k PRG / 0k CHR	V	UNROM (2)
Best Play Pro Yakyuu		128k PRG / 32k CHR	V	MMC1 (1)
Best Play Pro Yakyuu 2		256k PRG / 0k CHR	H	MMC1 (1)
Best Play Pro Yakyuu Special	256k PRG / 0k CHR	H	MMC1 (1)
Bible Adventures		64k PRG / 64k CHR	V	Colour Dreams (11)
Big Bird's Hide and Speak       256k PRG / 128k CHR     H       MMC1 (1)
Big Nose Freaks Out		256k PRG / 0k CHR	V	Camerica (71)
Bigfoot				128k PRG / 128k CHR	H	MMC1 (1)
Bikkuri Man World Gekitotu Sei Sensi	256k PRG / 0k CHR	V	MMC1 (1)
Bill and Ted's Excellent Adventure	128k PRG / 128k CHR	H	MMC1 (1)
Bill Elliot's NASCAR Challenge  128k PRG / 256k CHR     H       MMC3 (4)
Binary Land			16k PRG / 8k CHR	H	---- (0)
Bio Senshi Dan			128k PRG / 128k CHR	H	74161/32 (66)
Bionic Commando			256k PRG / 0k CHR	V	MMC1 (1)
Bird Week			16k PRG / 0k CHR	V	CNROM (3)
Birdy Rush			128k PRG / 128k CHR	H	MMC1 (1)
Black Bass, The			128k PRG / 0k CHR	H	UNROM (2)
Black Bass, The (J)		128k PRG / 0k CHR	H	UNROM (2)
Black Bass 2, The		128k PRG / 0k CHR	H	UNROM (2)
Blades of Steel			128k PRG / 0k CHR	V	UNROM (2)
Blades of Steel (PAL)		128k PRG / 0k CHR	V	UNROM (2)
Blaster Master			128k PRG / 128k CHR	H	MMC1 (1)
Blaster Master (PAL)		128k PRG / 128k CHR	H	MMC1 (1)
Bloody Warriors			256k PRG / 0k CHR	V	MMC1 (1)
Blue Marlin, The		128k PRG / 128k CHR	H	MMC3 (4)
Blue Shadow			128k PRG / 128k CHR	H	MMC3 (4)
Blue Train Satsujin Jiken	128k PRG / 256k CHR	H	Bandai (16)  <--this is possibly
										a hacked version,
										since Irem made
										the game.
Blues Brothers, The		128k PRG / 0k CHR	V	UNROM (2)
Bo Jackson Baseball		128k PRG / 184k CHR	H	MMC3 (4)
Bokosuka Wars                   32k PRG / 8k CHR        V       ---- (0)
Boku Dracula Kun                128k PRG / 128k CHR     V       VRC2B (23)
Bomber King			128k PRG / 0k CHR	V	UNROM (2)
Bomberman (J)			16k PRG / 8k CHR	V	---- (0)
Bomberman 2 (J)			128k PRG / 0k CHR	V	UNROM (2)
Booby Kids			128k PRG / 0k CHR	H	UNROM (2)
Boulder Dash			128k PRG / 128k CHR	H	MMC1 (1)
Boulder Dash (J)		32k PRG / 32k CHR	H	MMC1 (1)
Boy and His Blob, A		128k PRG / 128k CHR	H	MMC1 (1)
Breakthru			128k PRG / 128k CHR	H	MMC1 (1)
Brush Roller			16k PRG / 8k CHR	V	CNROM (3)
Bubble Bobble			128k PRG / 32k CHR	H	MMC1 (1)
Bubble Bobble (PAL)		128k PRG / 32k CHR	H	MMC1 (1)
Bubble Bobble 2			128k PRG / 128k CHR	H	MMC3 (4)
Bucky O' Hare			128k PRG / 128k CHR	H	MMC3 (4)
Bucky O' Hare (J)		128k PRG / 128k CHR	H	MMC3 (4)
Bucky O' Hare (PAL)		128k PRG / 128k CHR	H	MMC3 (4)
Buggy Popper			32k PRG / 32k CHR	V	CNROM (3)
Bugs Bunny's Crazy Castle	64k PRG / 32k CHR	H	MMC1 (1)
Bugs Bunny's Birthday Blowout	128k PRG / 128k CHR	H	MMC3 (4)
Bump N' Jump			32k PRG / 32k CHR	H	CNROM (3)
Burai Fighter			32k PRG / 32k CHR	H	MMC3 (4)
Burgertime (J)			16k PRG / 8k CHR	H	---- (0)


Cabal				128k PRG / 0k CHR	H	AOROM (7)
Cadillac			32k PRG / 32k CHR	H	CNROM (3)
Caesar's Palace			256k PRG / 0k CHR	H	AOROM (7)
California Games		128k PRG / 0k CHR	V	UNROM (2)
Capcom Barcelona '92		128k PRG / 128k CHR	H	MMC3 (4)
Captain America and the Avengers128k PRG / 128k CHR	H	MMC3 (4)	
Captain Ed			256k PRG / 0k CHR	V	MMC1 (1)
Captain Planet 			128k PRG / 128k CHR	H	MMC3 (4)
Captain Silver			128k PRG / 128k CHR	H	MMC1 (1)
Captain Skyhawk			128k PRG / 0k CHR	H	AOROM (7)
Captain Tsubasa			128k PRG / 128k CHR	V	MMC1 (1)
Captain Tsubasa 2		256k PRG / 128k CHR	V	MMC3 (4)
Casino Kid			128k PRG / 0k CHR	H	UNROM (2)
Castelian			128k PRG / 0k CHR	V	UNROM (2)
Castelian (PAL)			128k PRG / 0k CHR	V	UNROM (2)
Castle Excellent		32k PRG / 32k CHR	H	CNROM (3)
Castle of Deceit		64k PRG / 64k CHR	H	Colour Dreams (11)
Castle of Dragon		128k PRG / 0k CHR	V	UNROM (2)
Castlequest (J)			128k PRG / 128k CHR	H	MMC3 (4)
Castlevania			128k PRG / 0k CHR	V	UNROM (2)
Castlevania 2			128k PRG / 128k CHR	H	MMC1 (1)
Castlevania 2 (PAL)		128k PRG / 128k CHR	H	MMC1 (1)
Castlevania 3			256k PRG / 128k CHR	V	MMC5 (5)
Catts Nintouden Teyandey	128k PRG / 128k CHR	V	MMC3 (4)
Caveman Games			128k PRG / 128k CHR	H	MMC1 (1)
Caveman Ninja			128k PRG / 128k CHR	H	MMC3 (4)
Chack N' Pop			16k PRG / 8k CHR	H	---- (0)
Challenger                      32k PRG / 8k CHR        V       ---- (0)
Championship Bowling (J)	32k PRG / 32k CHR	H	CNROM (3)
Championship Lode Runner	16k PRG / 8k CHR	V	---- (0)
Championship Pool		128k PRG / 0k CHR	V	UNROM (2)
Chaos World			256k PRG / 128k CHR	H	MMC1 (1)
Chase HQ			128k PRG / 128k CHR	H	MMC3 (4)
Chester Field			128k PRG / 0k CHR	V	UNROM (2)
Chibi Marukochan Uki Uki Shopping	128k PRG / 128k CHR	H	MMC3 (4)
Chiller				32k PRG / 32k CHR	V	MMC1 (1)
Chip N' Dale - Rescue Rangers	128k PRG / 128k CHR	H	MMC1 (1)
Chip N' Dale - Rescue Rangers 2	128k PRG / 128k CHR	H	MMC1 (1)
Chip to Dale no Daisakusen	128k PRG / 128k CHR	H	MMC1 (1)
Chip to Dale no Daisakusen 2	128k PRG / 128k CHR	H	MMC1 (1)
Chiyo no Fuji			128k PRG / 128k CHR	H	MMC3 (4)
Choplifter 			32k PRG / 16k CHR	V	CNROM (3)
Choujin Sentai Jetman           128k PRG / 128k CHR     H       MMC1 (1)
Choujin Ultra Baseball		128k PRG / 128k CHR	V	MMC1 (1)
Chubby Cherub			32k PRG / 16k  CHR	H	CNROM (3)
Chugoku Jyansi Story Tonpuu     128k PRG / 128k CHR     H       MMC1 (1)
Chugoku Sensei Jyuutu           128k PRG / 0k CHR       H       UNROM (2)
Chuka Taisen			128k PRG / 128k CHR	V	MMC1 (1)
Circus Caper			128k PRG / 128k CHR	V	MMC1 (1)
Circus Charlie			16k PRG /  8k CHR	V	CNROM (3)
City Connection			32k PRG / 32k CHR	V	CNROM (3)
City Connection (J)		16k PRG / 16k CHR	V	CNROM (3)
Clash at Demonhead		128k PRG / 128k CHR	H	MMC1 (1)
Cliffhanger			128k PRG / 128k CHR	H	MMC3 (4)
Clu Clu Land			16k PRG / 16k CHR	H	CNROM (3)
Clu Clu Land disk-to-nes conv.	32k PRG / 8k CHR	H	-- (0) [uses .sav file to load]
Cobra Command			128k PRG / 128k CHR	H	MMC1 (1)
Cobra Command (J)		128k PRG / 128k CHR	H	MMC1 (1)
Cobra Triangle			128k PRG / 0k CHR	H	AOROM (7)
Cobra Triangle (PAL)		128k PRG / 0k CHR	H	AOROM (7)
Cocoron				256k PRG / 0k CHR	V	MMC1 (1)
Code Name Viper                 128k PRG / 128k CHR     H       MMC3 (4)
Columbus Ougon no Yoake		128k PRG / 128k CHR	V	MMC3 (4)
Commando			256k PRG / 0k CHR	H	AOROM (7)
Conan				128k PRG / 0k CHR	H	UNROM (2)
Conflict                        128k PRG / 128k CHR     H       MMC1 (1) <--bad dump
Conquest of the Crystal Palace	128k PRG / 128k CHR	H	MMC3 (4)
Contra				128k PRG / 0k CHR	V	UNROM (2)
Contra (J)			128k PRG / 128k CHR	H	VRC2b (23)
Contra Force			128k PRG / 128k CHR	V	MMC3 (4)
Cool World			128k PRG / 128k CHR	H	MMC1 (1)
Cosmic Epsilon			128k PRG / 256k CHR	H	MMC3 (4)
Cosmic Wars			128k PRG / 128k CHR	H	MMC1 (1)
Cosmo Genesis			32k PRG / 32k CHR	H	CNROM (3)
Cosmo Police Galivan            128k PRG / 128k CHR     H       MMC1 (1)
Crackout			128k PRG / 0k CHR	H	UNROM (2)
Crash N' The Boys		128k PRG / 128k CHR	H	MMC3 (4)
Crazy Climber			256k PRG / 0k CHR	H	Irem 74161/32 (97)
Crazy Climber [hacked]		256k PRG / 0k CHR	H	(15)
Crisis Force [hacked]		128k PRG / 128k CHR	H	MMC3 (4)
Crossfire			128k PRG / 128k CHR	H	MMC3 (4)
Crystal Mines 2			64k PRG / 16k CHR	V	Colour Dreams (11)
Crystalis			256k PRG / 128k CHR	H	MMC3 (4)
Cyberball			128k PRG / 128k CHR	H	MMC3 (4)
Cybernoid			32k PRG / 32k CHR	V	CNROM (3)


Dai Meiro			128k PRG / 128k CHR	H	MMC1 (1)
Dai Senryaku                    128k PRG / 32k CHR      V       MMC1 (1)
Daikaijyuu Deburas		128k PRG / 256k CHR	V	MMC3 (4)
Daiku no Gensan                 128k PRG / 128k CHR     H       MMC3 (4)
Daiku no Gensan 2               128k PRG / 128k CHR     H       Irem H3001 (65)
Daiva				128k PRG / 0k CHR	V	UNROM (2)
Dance Aerobics			64k PRG / 32k CHR	H	MMC1 (1)
Danny Sullivan's Indy Heat      128k PRG / 0k CHR       H       AOROM (7)
Dark Lord			256k PRG / 128k CHR	V	MMC3 (4)
Darkman				128k PRG / 128k CHR	H	MMC1 (1)
Darkwing Duck			128k PRG / 128k CHR	V	MMC1 (1)
Dash Galaxy in the Alien Asylum 32k PRG / 32k CHR       H       CNROM (3)
Dash Yarou			128k PRG / 0k CHR	V	UNROM (2)
Days of Thunder			128k PRG / 256k CHR	H	MMC3 (4)
Datach Dragonball Z		256k PRG / 0k CHR	H	Bandai (16)
Datsugoku			128k PRG / 128k CHR	H	MMC1 (1)
Day Dreamin' Davey		256k PRG / 128k CHR	H	MMC1 (1)
Deadly Towers			128k PRG / 0k CHR 	H	Nina-1 (34)
Deblock				32k PRG / 32k CHR	V	CNROM (3)
Deep Dungeon 3			256k PRG / 0k CHR	V	MMC1 (1)
Deep Dungeon 4			256k PRG / 0k CHR	V	MMC1 (1)
Defender 2			16k PRG / 8k CHR	V	---- (0)
Defender of the Crown		256k PRG / 0k CHR	V	MMC1 (1)
Defenders of Dynatron City	128k PRG / 128k CHR	H	MMC3 (4)
Deja Vu				128k PRG / 256k CHR	H	MMC3 (4)
Demon Sword			128k PRG / 128k CHR	H	MMC1 (1)
Dengeki Big Bang		128k PRG / 128k CHR	H	MMC1 (1)
Densetsu no Kisi Elrond		128k PRG / 0k CHR	V	AOROM (7)
Derby Stallion			128k PRG / 128k CHR	V	MMC1 (1)
Derby Stallion Zenkokuban	128k PRG / 128k CHR	H	MMC1 (1)
Desert Commander		128k PRG / 128k CHR	V	MMC1 (1)
Destiny of an Emperor		256k PRG / 0k CHR	V	MMC1 (1)
Devil World			16k PRG / 8k CHR	H	---- (0)
Dezaemon			128k PRG / 0k CHR	H	MMC1 (1)
Dick Tracy			128k PRG / 0k CHR	V	UNROM (2)
Die Hard (J)			128k PRG / 128k CHR	H	MMC1 (1)
Dig Dug				16k PRG / 8k CHR	H	---- (0)
Dig Dug 2 (J)                   32k PRG / 8k CHR        V       ---- (0)
Digger T. Rock			128k PRG / 0k CHR	H	AOROM (7)
Dirty Harry			256k PRG / 128k CHR	H	MMC3 (4)
Dizzy The Adventurer		128k PRG / 0k CHR	V	Camerica (71)
Dodge Danpei 2			256k PRG / 256k CHR	H	Sunsoft 5 (69)
Donkey Kong			16k PRG / 8k CHR	V	---- (0)
Donkey Kong 3			16k PRG / 8k CHR	V	---- (0)
Donkey Kong Classics		32k PRG / 32k CHR	V	CNROM (3)
Donkey Kong Jr.			16k PRG / 8k CHR	V	---- (0)
Donkey Kong Jr. Math		16k PRG / 8k CHR	V	---- (0)
Donkey Kong Jr. Sansuu Asobi	16k PRG / 8k CHR	V	---- (0)
Doki Doki Yuenchi		128k PRG / 128k CHR	H	MMC3 (4)
Dokugan Masamune [hacked]	256k PRG / 0k CHR	V	FFE F4xxx (6)
Don Doko Don                    128k PRG / 256k CHR     H       TC0190/TC0350 (33)
Don Doko Don 2			128k PRG / 256k CHR	V	TC0190/TC0350 (33)
Donald Duck			128k PRG / 128k CHR	H	MMC1 (1)
Donald Land			128k PRG / 128k CHR	H	MMC1 (1)
Door Door			16k PRG / 8k CHR	V	---- (0)
Doraemon Gigzombi no Gyakusyuu	256k PRG / 0k CHR	H	MMC1 (1)
Doraemon Kaitakuhen [hacked]	128k PRG / 32k CHR	V	FFE F3xxx (8)
Double Dare			256k PRG / 0k CHR	H	AOROM (7)
Double Dragon			128k PRG / 128k CHR	H	MMC1 (1)
Double Dragon (J)		128k PRG / 128k CHR	H	MMC1 (1)
Double Dragon (PAL)		128k PRG / 128k CHR	H	MMC1 (1)
Double Dragon 2			128k PRG / 128k CHR	H	MMC3 (4)
Double Dragon 2 (J)		128k PRG / 128k CHR	V	MMC3 (4)
Double Dragon 3			128k PRG / 128k CHR	H	MMC3 (4)
Double Dragon 3 (J)		128k PRG / 128k CHR	V	MMC3 (4)
Double Dragon 3 (PAL)		128k PRG / 128k CHR	H	MMC3 (4)
Double Dribble			128k PRG / 0k CHR	V	UNROM (2)
Double Strike			32k PRG / 32k CHR	H	AVE (79)
Doublemoon Densetsu		512k PRG / 0k CHR	H	MMC3 (4)
Dough Boy                       32k PRG / 8k CHR        V       ---- (0)
Downtown Nekketsu Jidaigekidayo 128k PRG / 128k CHR     H       MMC3 (4)
Downtown Nekketsu Kousinkyoko	128k PRG / 128k CHR	H	MMC3 (4)
Downtown Nekketsu Monogatari	128k PRG / 128k CHR	V	MMC3 (4)
Dr. Chaos			128k PRG / 0k CHR	V	UNROM (2)
Dr. Jekyll and Mr. Hyde		128k PRG / 32k CHR	H	MMC1 (1)
Dr. Mario			32k PRG / 32k CHR	H	MMC1 (1)
Dr. Mario (PAL)			32k PRG / 32k CHR	H	MMC1 (1)
Dracula				128k PRG / 128k CHR	H	MMC3 (4)
Dracula (PAL)			128k PRG / 128k CHR	H	MMC3 (4)
Dragon Buster                   128k PRG / 32k CHR      H       Namco 1xx (95)
Dragon Buster [hacked]		128k PRG / 32k CHR	V	MMC3 (4)
Dragon Buster 2			128k PRG / 64k CHR	V	MMC3 (4)
Dragon Fighter			128k PRG / 128k CHR	H	MMC1 (1)
Dragon Power			128k PRG / 32k CHR	H	74161/32 (66)
Dragon Quest			32k PRG / 32k CHR	V	CNROM (3)
Dragon Quest 2			128k PRG / 0k CHR	V	UNROM (2)
Dragon Quest 3			256k PRG / 0k CHR	V	UNROM (2)
Dragon Quest 4			1024k PRG / 0k CHR	V	MMC1 (1)
Dragon Scroll [hacked]		256k PRG / 0k CHR	V	FFE F4xxx (6)		
Dragon Slayer 4			128k PRG / 64k CHR	V	MMC3 (4)
Dragon Spirit			128k PRG / 128k CHR	H	MMC3 (4)
Dragon Spirit (J)		128k PRG / 128k CHR	H	Namco 118 (88)
Dragon Strike			256k PRG / 256k CHR	H	MMC3 (4)
Dragon Unit			128k PRG / 0k CHR	V	UNROM (2)
Dragon Warrior			64k PRG / 16k CHR	H	MMC1 (1)
Dragon Warrior 2		256k PRG / 0k CHR	V	MMC1 (1)
Dragon Warrior 3		512k PRG / 0k CHR	H	MMC1 (1)
Dragon Warrior 4		1024k PRG / 0k CHR	H	MMC1 (1)
Dragon Wars			256k PRG / 128k CHR	V	MMC3 (4)
Dragon's Lair			128k PRG / 0k CHR	V	UNROM (2)
Dragonball			128k PRG / 32k CHR	V	74161/32 (66)
Dragonball [hacked]		128k PRG / 32k CHR	V	Nina-1 (34)
Dragonball 2			128k PRG / 128k CHR	H	Bandai (16)
Dragonball 3			128k PRG / 256k CHR	H	Bandai (16)
Dragonball Z			256k PRG / 256k CHR	H	Bandai (16)
Dragonball Z 2			256k PRG / 256k CHR	V	Bandai (16)
Dragonball Z 2 [hacked]		256k PRG / 256k CHR	V	FFE F8xxx (17)
Dragonball Z 3			256k PRG / 256k CHR	V	Bandai (16)
Dragonball Z 3 [hacked]		256k PRG / 256k CHR	V	FFE F8xxx (17)
Dragonball Z Gaiden		256k PRG / 256k CHR	H	Bandai (16)
Dragons of Flame		128k PRG / 128k CHR	H	MMC1 (1)
Dream Master			256k PRG / 256k CHR	H	Namcot 106 (19)
Drop Zone			32k PRG / 32k CHR	V	CNROM (3)
Duck                            32k PRG / 8k CHR        H       ---- (0)
Duck Hunt			16k PRG / 8k CHR	V	---- (0)
Ducktales			128k PRG / 0k CHR	V	UNROM (2)
Ducktales 2			128k PRG / 0k CHR	V	UNROM (2)
Dudes With Attitudes		32k PRG / 32k CHR	V	AVE (79)
Dungeon and Magic		128k PRG / 128k CHR	V	MMC1 (1)
Dungeon Kid			128k PRG / 0k CHR	H	MMC1 (1)
Dungeon Magic			128k PRG / 128k CHR	V	MMC1 (1)
Dusty Diamond All-Star Softball 128k PRG / 128k CHR     H       MMC1 (1)
Dynablaster                     128k PRG / 0k CHR       H       MMC1 (1)
Dynamite Batman 2		128k PRG / 256k CHR	H	Sunsoft 5 (69)
Dynamite Batman 2 [Hacked]	128k PRG / 256k CHR	H	FFE F8xxx (17)
Dynamite Bowl			32k PRG / 32k CHR	V	CNROM (3)
Dynowarz			128k PRG / 128k CHR	H	MMC1 (1)


Earthbound			256k PRG / 256k CHR	V	MMC3 (4)
Eggerland			128k PRG / 32k CHR	V	MMC1 (1)
Egypt				32k PRG / 32k CHR	H	CNROM (3)
Elevator Action                 32k PRG / 8k CHR        H       ---- (0)
Eliminator Boat Duel		128k PRG / 128k CHR	V	MMC1 (1)
Elite				128k PRG / 0k CHR	H	MMC1 (1)
Elysion				128k PRG / 128k CHR	H	MMC1 (1)
Emo Yan no 10 Bai Pro Yakyuu	128k PRG / 128k CHR	V	MMC1 (1)
Empire Strikes Back, The	256k PRG / 256k CHR	H	MMC3 (4)
Erika to Satoru no Yume Bouken  128k PRG / 128k CHR     H       MMC3 (4)
Erunaaku no Zaihou		128k PRG / 0k CHR	V	UNROM (2)
Esupa Boukentai			256k PRG / 0k CHR	V	UNROM (2)
Excitebike			16k PRG / 8k CHR	V	---- (0)
Excitebike (PAL)		16k PRG / 8k CHR	V	---- (0)
Exciting Rally			128k PRG / 128k CHR	H	MMC1 (1)
Exed Exes                       32k PRG / 8k CHR        H       ---- (0)
Exerion				16k PRG / 8k CHR	H	---- (0)
Exodus				128k PRG / 128k CHR	V	Colour Dreams (11)


F1 Built to Win			128k PRG / 128k CHR	H	MMC1 (1)
F1 Circus			256k PRG / 128k CHR	H	MMC3 (4)
F1 Hero				128k PRG / 128k CHR	H	MMC3 (4)
F1 Hero 2			128k PRG / 128k CHR	H	MMC3 (4)
F1 Race				16k PRG / 8k CHR	V	---- (0)
F117A Stealth Fighter		256k PRG / 256k CHR	H	MMC3 (4)
F15 City War			32k PRG / 32k CHR	V	AVE (79)
Faibird				128k PRG / 128k CHR	V	MMC3 (4)
Famicom Jump			256k PRG / 128k CHR	H	Bandai (16)
Famicom Typing Tutor (Dr PC Jr) 32k PRG / 0k CHR        H       ---- (0)
Famicom Typing Tutor 2          32k PRG / 0k CHR        H       ---- (0)
Famicom Wars			128k PRG / 128k CHR	H	MMC4 (10)
Famicom Yakyuuban               128k PRG / 128k CHR     V       MMC1 (1)
Family BASIC                    32k PRG / 16k CHR       V       CNROM (3)
Family Block                    128k PRG / 32k CHR      H       74161/32 (66)
Family Boxing                   64k PRG / 64k CHR       V       MMC3 (4)
Family Circuit                  128k PRG / 32k CHR      V       MMC3 (4)
Family Circuit '91              512k PRG / 256k CHR     V       Namcot 106 (19)
Family Fued                     32k PRG / 128k CHR      H       MMC1 (1)
Family Jockey			32k PRG /  32k CHR	H	MMC3 (4)
Family Majyan                   128k PRG / 64k CHR      H       MMC3 (4)
Family Majyan 2                 128k PRG / 64k CHR      H       MMC3 (4)
Family Pinball			128k PRG / 64k CHR	H	MMC3 (4)
Family Quiz			128k PRG / 0k CHR	V	UNROM (2)
Family Stadium			64k PRG / 32k CHR	V	MMC3 (4)
Family Stadium '87		64k PRG / 32k CHR	V	MMC3 (4)
Family Stadium '88		128k PRG / 64k CHR	V	MMC3 (4)
Family Stadium '89		128k PRG / 64k CHR	V	MMC3 (4)
Family Stadium '90		128k PRG / 128k CHR	H	Namcot 106 (19)
Family Stadium '91		128k PRG / 128k CHR	V	MMC3 (4)
Family Stadium '92		128k PRG / 128k CHR	V	MMC3 (4)
Family Stadium '93		128k PRG / 128k CHR	V	MMC3 (4)
Family Stadium '94		128k PRG / 128k CHR	V	MMC3 (4)
Family Tennis			64k PRG / 64k CHR	V	MMC3 (4)
Family Trainer Running Stadium	32k PRG / 32k CHR	H	CNROM (3)
Fantastic Adventures of Dizzy, The [Aladdin vers.]	256k PRG / 0k CHR	V	Camerica (71)
Fantasy Zone (J)                128k PRG / 0k CHR       H       UNROM (2)
Faria				128k PRG / 128k CHR	H	MMC1 (1)
Faria (J)			128k PRG / 128k CHR	V	MMC1 (1)
Faxanadu                        256k PRG / 0k CHR       V       MMC1 (1)
Faxanadu (J)                    256k PRG / 0k CHR       H       UNROM (2)
FC Genjin			256k PRG / 128k CHR	H	MMC3 (4)
Felix the Cat                   128k PRG / 128k CHR     V       MMC3 (4)
Fester's Quest                  128k PRG / 128k CHR     H       MMC1 (1)
Field Combat                    16k PRG / 8k CHR        V       ---- (0)
Fighting Golf                   128k PRG / 128k CHR     H       MMC1 (1)
Final Fantasy                   256k PRG / 0k CHR       V       MMC1 (1)
Final Fantasy (J)               256k PRG / 0k CHR       V       UNROM (2)
Final Fantasy 2                 256k PRG / 0k CHR       V       UNROM (2)
Final Fantasy 3                 512k PRG / 0k CHR       V       MMC3 (4)
Final Fantasy I/II              256k PRG / 0k CHR       H       MMC1 (1) <--should be 512k
Final Lap                       128k PRG / 128k CHR     V       Namcot 106 (19)
Final Mission                   128k PRG / 128k CHR     H       MMC1 (1)
Fire Emblem                     256k PRG / 128k CHR     H       MMC2 (10)
Fire Emblem [hacked]            256k PRG / 128k CHR     V       MMC3 (4)
Fire Emblem Gaiden [hacked]     256k PRG / 128k CHR     H       MMC3 (4)
Firehawk                        128k PRG / 0k CHR       H       Camerica (71)
Fist of the North Star          128k PRG / 0k CHR       V       UNROM (2)
Flappy                          32k PRG / 8k CHR        V       ---- (0)
Fleet Commander                 32k PRG / 32k CHR       H       CNROM (3)
Flintstones, The                128k PRG / 256k CHR     H       MMC3 (3)
Flintstones, The (PAL)          128k PRG / 256k CHR     H       MMC3 (3)
Flipull                         32k PRG / 32k CHR       V       CNROM (3)
Flying Dragon                   128k PRG / 0k CHR       V       UNROM (2)
Flying Hero                     128k PRG / 0k CHR       V       UNROM (2)      
Flying Warriors                 128k PRG / 128k CHR     H       MMC1 (1)
Formation Z                     16k PRG / 8k CHR        H       ---- (0)
Foton                           128k PRG / 0k CHR       H       UNROM (2)
Four Card Games                 32k PRG / 8k CHR        V       ---- (0)
Frankenstein                    128k PRG / 128k CHR     V       MMC1 (1)
Freedom Force                   128k PRG / 128k CHR     H       MMC1 (1)
Friday The 13th                 32k PRG / 32k CHR       V       CNROM (3)
Front Line                      16k PRG / 8k CHR        H       ---- (0)
Fusigi no Umi no Nadia          128k PRG / 128k CHR     H       MMC3 (4)
Fuzzical Fighter                128k PRG / 128k CHR     V       MMC3 (4)


Galaga (J)                      16k PRG / 8k CHR        H       ---- (0)
Galaxian                        16k PRG / 8k CHR        H       ---- (0)
Galaxy 5000                     128k PRG / 128k CHR     V       MMC3 (4)
Galg                            32k PRG / 8k CHR        H       ---- (0)
Gambler Jikochusin Ha           256k PRG / 0k CHR       H       MMC1 (1)
Gambler Jikochusin Ha 2         256k PRG / 0k CHR       V       MMC1 (1)
Game Genie                      16k PRG / 8k CHR        H       ---- (0)
Ganbare Goemon [hacked]         256k PRG / 0k CHR       V       FFE F4xxx (6)
Ganbare Goemon 2 [hacked?]      128k PRG / 128k CHR     V       MMC3 (4)
Ganbare Goemon Gaiden           256k PRG / 256k CHR     V       VRC4 (25)
Ganbare Goemon Gaiden 2         256k PRG / 256k CHR     V       VRC4-2A (21)
Garfield                        128k PRG / 32k CHR      H       MMC1 (1)
Gargoyle's Quest 2              128k PRG / 128k CHR     H       MMC3 (4)
Garou Densetsu Special		256k PRG / 256k CHR	H	PC-Cony (83)
Garou Densetsu Special [hacked]	256k PRG / 256k CHR	H	MMC3 (4)
Gauntlet                        128k PRG / 64k CHR      V       MMC3 (4)
Gauntlet 2                      128k PRG / 128k CHR     H       MMC3 (4)
Gauntlet 2 (PAL)                128k PRG / 128k CHR     H       MMC3 (4)
Gaurdian Legend,  The           128k PRG / 0k CHR       V       UNROM (2)
Gaurdic Gaiden                  128k PRG / 0k CHR       V       UNROM (2)
Gegege no Kitarou               32k PRG / 32k CHR       V       CNROM (3)        
Gegege no Kitarou 2             128k PRG / 128k CHR     H       74161/32 (70)
Gegege no Kitarou 2 [Hacked]    256k PRG / 0k CHR       V       FFE F4xxx (6)
Gekikame Ninjaden               128k PRG / 128k CHR     H       MMC1 (1)
Gekitotu 4 Kubattle             128k PRG / 0k CHR       H       UNROM (2)
Gekitou Pro Wrestling           128k PRG / 128k CHR     V       MMC1 (1)
Gekitou Stadium			128k PRG / 128k CHR	V	MMC1 (1)
Geimos                          32k PRG / 8k CHR        H       ---- (0)      
Gemfire                         256k PRG / 256k CHR     H       MMC5 (5)
Genghis Khan                    256k PRG / 0k CHR       H       MMC1 (1)
Genghis Khan (J)                256k PRG / 0k CHR       V       MMC1 (1)        
Genpei Toumaden                 128k PRG / 64k CHR      H       MMC3 (4)
George Foreman's KO Boxing      128k PRG / 256k CHR     H       MMC3 (4)
Getsufuu Maden                  128k PRG / 128k CHR     H       VRC2b (23)                 
Ghostbusters                    32k PRG / 32k CHR       H       CNROM (3)
Ghostbusters (J)                32k PRG / 32k CHR       H       CNROM (3)
Ghostbusters 2                  128k PRG / 128k CHR     H       MMC1 (1)
Ghosts N' Goblins               128k PRG / 0k CHR       V       UNROM (2)
Ghoul School                    128k PRG / 128k CHR     V       MMC1 (1)
GI Joe                          128k PRG / 256k CHR     H       MMC3 (4)
GI Joe -  The Atlantis Factor   128k PRG / 256k CHR     H       MMC3 (4)
Gilligan's Island               128k PRG / 0k CHR       H       UNROM (2)
Gimmick!                        256k PRG / 128k CHR     V       Sunsoft 5 (69)
Ginga Eiyuu Densetsu            128k PRG / 128k CHR     V       MMC1 (1)
Ginga no Sannin                 128k PRG / 0k CHR       V       UNROM (2)
Goal                            256k PRG / 128k CHR     H       MMC1 (1)
God Slayer                      256k PRG / 128k CHR     H       MMC3 (4)
Godzilla                        128k PRG / 128k CHR     H       MMC1 (1)
Godzilla (J)                    128k PRG / 128k CHR     V       MMC1 (1)
Godzilla 2                      128k PRG / 128k CHR     H       MMC1 (1)
Gold Medal Challenge '92        128k PRG / 128k CHR     H       MMC3 (4)
Golf                            16k PRG / 8k CHR        V       ---- (0)
Golf '92, The                   128k PRG / 128k CHR     H       MMC1 (1)
Golf Grand Slam                 128k PRG / 128k CHR     H       MMC1 (1)
Golf Open                       128k PRG / 256k CHR     H       TC0190/TC0350 (33)
Golgo 13                        128k PRG / 128k CHR     H       MMC1 (1)
Golgo 13 (J)                    128k PRG / 128k CHR     H       MMC1 (1)
Golgo 13 - The Mafat Conspiracy 128k PRG / 128k CHR     H       MMC3 (4)
Golgo 13 - The Riddle of Icarus 128k PRG / 128k CHR     H       MMC3 (4)
Gomoku Narabe                   16k PRG / 8k CHR        V       ---- (0)
Goonies, The                    32k PRG / 16k CHR       V       CNROM (3)
Goonies 2, The                  128k PRG / 0k CHR       V       UNROM (2)
Goonies 2, The (J)              128k PRG / 0k CHR       V       UNROM (2)
Gorby no Pipeline Daisakusen    32k PRG / 32k CHR       H       CNROM (3)
Gotcha                          32k PRG / 32k CHR       V       CNROM (3)
Gozonji Yajikita Tindouchu      256k PRG / 128k CHR     H       MMC1 (1)
Gradius (J)                     32k PRG / 32k CHR       V       CNROM (3)
Gradius 2                       128k PRG / 128k CHR     V       VRC4 (25)
Grand Master                    256k PRG / 128k CHR     H       MMC1 (1)
Great Deal                      128k PRG / 0k CHR       V       MMC1 (1)
Great Tank                      128k PRG / 128k CHR     V       MMC1 (1)
Gremlins 2                      128k PRG / 256k CHR     H       MMC3 (4)
Guerilla War                    128k PRG / 128k CHR     H       MMC1 (1)
Gumshoe                         128k PRG / 32k CHR      H       74161/32 (66)
Gunhed                          128k PRG / 128k CHR     V       MMC1 (1)
Gunnac                          128k PRG / 128k CHR     V       MMC3 (4)
Gunnac (J)                      128k PRG / 128k CHR     V       MMC3 (4)
Gunsight                        128k PRG / 128k CHR     H       MMC5 (5)
Gunsmoke                        128k PRG / 0k CHR       H       UNROM (2)
Gunsmoke (PAL)                  128k PRG / 0k CHR       H       UNROM (2)
Gyrodine                        32k PRG / 8k CHR        V       ---- (0)
Gyruss                          32k PRG / 32k CHR       V       CNROM (3)


Haja no Fuuin                   128k PRG / 128k CHR     V       MMC1 (1)
Hana no Star Kaidou             128k PRG / 0k CHR       V       UNROM (2)
Hanjuku Hero                    128k PRG / 0k CHR       H       UNROM (2)
Harlem Globetrotters            128k PRG / 128k CHR     H       MMC1 (1)
Hatris                          128k PRG / 0k CHR       H       MMC1 (1)
Hatris (J)                      128k PRG / 0k CHR       V       UNROM (2)
Heavy Barrel                    128k PRG / 128k CHR     H       MMC3 (4)
Heavy Shreddin'                 128k PRG / 128k CHR     V       MMC1 (1)
Hebereke                        128k PRG / 128k CHR     V       Sunsoft 5 (69)
Hector '87                      128k PRG / 0k CHR       V       UNROM (2)
Hello Kitty no Ohana Batake     32k PRG / 32k CHR       V       CNROM (3)
Hello Kitty World               128k PRG / 0k CHR       V       UNROM (2)
Hercules no Eikou               128k PRG / 0k CHR       V       UNROM (2)
Hercules no Eikou 2             256k PRG / 0k CHR       V       MMC1 (1)
Heroes of the Lance             128k PRG / 128k CHR     H       MMC1 (1)
Heroes of the Lance (J)         128k PRG / 128k CHR     H       MMC1 (1)
Hi no Tori			128k PRG / 0k CHR	V	UNROM (2)
Higemaru Makaijima              128k PRG / 0k CHR       V       UNROM (2)
High Speed                      128k PRG / 64k CHR      H       TQROM (119)
High Speed (PAL)                128k PRG / 64k CHR      H       TQROM (119)
Highway Star                    128k PRG / 0k CHR       V       UNROM (2)
Hillsfar (J)                    256k PRG / 0k CHR       H       MMC1 (1)
Hirake Ponkikki                 32k PRG / 32k CHR       V       MMC1 (1)
Hiryuu no Ken                   128k PRG / 0k CHR       V       UNROM (2)
Hiryuu no Ken 2                 128k PRG / 128k CHR     H       MMC1 (1)
Hiryuu no Ken 3                 128k PRG / 128k CHR     V       MMC3 (4)
Hiryuu no Ken SP Fighting Wars  128k PRG / 128k CHR     V       MMC1 (1)
Hissatu Dojo Yaburi		256k PRG / 0k CHR	H	MMC1 (1)
Hissatu Sigotonin               256k PRG / 128k CHR     H       MMC3 (4)
Hittra no Fukkatu		256k PRG / 0k CHR	H	MMC1 (1)
Hogan's Alley                   16k PRG / 8k CHR        H       ---- (0)
Hokuto no Ken                   32k PRG / 32k CHR       V       CNROM (3)
Hokuto no Ken 2                 128k PRG / 0k CHR       V       UNROM (2)
Hokuto no Ken 3                 256k PRG / 0k CHR       H       MMC1 (1)
Hokuto no Ken 4                 256k PRG / 0k CHR       V       MMC1 (1)
Hollywood Squares               256k PRG / 0k CHR       H       AOROM (7)
Holy Diver                      128k PRG / 128k CHR     H       74161/32 (78)
Home Alone                      128k PRG / 104k CHR     H       MMC3 (4) <--Probably a bad dump.
Home Alone 2                    128k PRG / 128k CHR     H       MMC3 (4)
Home Run Nighter                128k PRG / 128k CHR     V       MMC1 (1)
Home Run Nighter '90            128k PRG / 128k CHR     V       MMC3 (4)
Hook                            128k PRG / 128k CHR     H       MMC1 (1)
Hook (J)                        128k PRG / 128k CHR     H       MMC1 (1)
Hoops                           128k PRG / 128k CHR     V       MMC1 (1)
Hosi no Kirby                   512k PRG / 256k CHR     H       MMC3 (4)
Hosi wo Miruhito                128k PRG / 0k CHR       V       UNROM (2)
Hostages                        128k PRG / 128k CHR     V       MMC1 (1)
Hot Slots                       32k PRG / 64k CHR       H       CNROM (3)
Hototogisu                      256k PRG / 0k CHR       V       MMC1 (1)
Hottaman no Titeitanken         32k PRG / 32k CHR       V       CNROM (3)
Houmaga Toki			128k PRG / 0k CHR	V	UNROM (2)
Hudson Hawk                     128k PRG / 128k CHR     V       MMC1 (1)
Hunt For Red October, The       128k PRG / 128k CHR     H       MMC3 (4)
Hyaku no Sekai no Monogatari    128k PRG / 128k CHR     V       MMC1 (1)
Hyauti Super Igo		32k PRG / 32k CHR	V	CNROM (3)
Hydlide                         32k PRG / 8k CHR        H       ---- (0)
Hydlide (J)                     32k PRG / 8k CHR        H       ---- (0)
Hydlide 3                       256k PRG / 128k CHR     H       Namcot 106 (19)
Hyokkori Hyoutan Jima           128k PRG / 128k CHR     H       MMC1 (1)
Hyper Olympic                   16k PRG / 8k CHR        V       ---- (0)
Hyper Olympic - Tonosoma Edition32k PRG / 8k CHR        V       ---- (0)
Hyper Sports                    16k PRG / 8k CHR        V       ---- (0)


I Love Softball                 128k PRG / 128k CHR     V       MMC1 (1)
Ice Climber                     16k PRG / 8k CHR        H       ---- (0)
Ice Climber (disk conversion)   32k PRG / 0k CHR        H       ---- (0) - (uses .sav file to load)
Ice Hockey                      32k PRG / 8k CHR        V       ---- (0)
Ice Hockey (disk coversion)     32k PRG / 8k CHR        V       ---- (0) - (uses .sav file to load)
Idol Hakkenden                  128k PRG / 128k CHR     H       MMC1 (1)
Idol Shisen Mahjongg            32k PRG / 64k CHR       V       CNROM (3)
Igo Sinan                       32k PRG / 8k CHR        H       ---- (0)
Igo Sinan '91                   128k PRG / 32k CHR      H       MMC1 (1)
Igo Sinan '93                   128k PRG / 32k CHR      H       MMC1 (1)
Ikari Warriors                  128k PRG / 0k CHR       H       UNROM (2)
Ikari Warriors (J)              128k PRG / 0k CHR       H       UNROM (2)
Ikari Warriors 2                256k PRG / 0k CHR       V       MMC1 (1)
Ikari Warriors 2 (J)            256k PRG / 0k CHR       H       UNROM (2)
Ikari Warriors 3                128k PRG / 128k CHR     H       MMC1 (1)
Ikari Warriors 3 (J)            128k PRG / 128k CHR     H       MMC1 (1)
Ike Ike Nekketsu Hockey         128k PRG / 128k CHR     H       MMC3 (4)
Ikinari Musician                32k PRG / 16k CHR       V       CNROM (3)
Ikki                            16k PRG / 8k CHR        V       ---- (0)
Image Fight                     128k PRG / 128k CHR     H       MMC3 (4)
Image Fight (J)                 128k PRG / 128k CHR     H       Irem G101 (32)
Immortal, The                   128k PRG / 256k CHR     H       MMC3 (4)
Impossible Mission 2            64k PRG / 64k CHR       H       Nina-1 (34)
Incredible Crash Dummies, The	128k PRG / 256k CHR	H	MMC3 (4)
Indiana Jones / Last Crusade    256k PRG / 0k CHR       V       MMC1 (1)
Indiana Jones / Temple of Doom  128k PRG / 128k CHR     V       MMC3 (4)
Indra no Hikari                 128k PRG / 0k CHR       H       UNROM (2)
Infiltrator                     128k PRG / 128k CHR     H       MMC3 (4)
Insector X                      128k PRG / 128k CHR     V       TC0190/TC0350 (33)
International Cricket           128k PRG / 128k CHR     H       MMC1 (1)
Iron Tank                       128k PRG / 128k CHR     H       MMC1 (1)
Isolated Warrior                128k PRG / 128k CHR     H       MMC3 (4)
Itadaki Street                  128k PRG / 128k CHR     H       MMC1 (1)


J League - King of Ace Strikers 128k PRG / 128k CHR     H       MMC3 (4)
Jack Nicklaus Golf              128k PRG / 0k CHR       V       UNROM (2)
Jackal                          128k PRG / 0k CHR       V       UNROM (2)
Jackie Chan                     128k PRG / 128k CHR     V       MMC3 (4)
Jackie Chan's Action Kung-Fu    128k PRG / 128k CHR     H       MMC3 (4)
Jajamaru no Daibouken           32k PRG / 32k CHR       V       CNROM (3)
Jajamaru no Gekimaden           128k PRG / 128k CHR     H       SS8806 (18)
Jajamaru no Ginga Daisakusen    128k PRG / 128k CHR     H       SS8806 (18)
James Bond Jr.                  128k PRG / 128k CHR     H       MMC3 (4)        
James Bond Jr. (PAL)            128k PRG / 128k CHR     H       MMC3 (4)
Jarinko Chie                    128k PRG / 256k CHR     H       VRC2b (23)
Jarvas                          128k PRG / 128k CHR     V       MMC3 (4)
Jaws                            32k PRG / 32k CHR       H       CNROM (3)
Jeopardy                        128k PRG / 0k CHR       V       AOROM (7)
Jeopardy 25th Anniversary Ed.   128k PRG / 0k CHR       H       AOROM (7)
Jeopardy Jr.                    128k PRG / 0k CHR       H       AOROM (7)
Jesus                           256k PRG / 0k CHR       V       UNROM (2)
Jetsons, The                    128k PRG / 256k CHR     H       MMC3 (4)
Jewelry                         16k PRG / 8k CHR        V       ---- (0)
Jikuuyuuden Debias		128k PRG / 64k CHR	V	MMC3 (4)
Jimmy Connors Tennis            128k PRG / 0k CHR       V       UNROM (2)
Jimmy Connors Tennis (PAL)      128k PRG / 0k CHR       V       UNROM (2)
Joe and Mac                     128k PRG / 128k CHR     H       MMC3 (4)
John Elway's Quarterback        32k PRG / 32k CHR       V       CNROM (3)        
Jongbou                         128k PRG / 0k CHR       V       UNROM (2)
Jordan vs Bird                  128k PRG / 0k CHR       V       UNROM (2)
Journey to Silius               128k PRG / 128k CHR     H       MMC1 (1)
Joust                           16k PRG / 8k CHR        V       ---- (0)
Jovei Quest                     256k PRG / 256k CHR     H       Namcot 106 (19)
Joy Mecha Fight                 256k PRG / 256k CHR     V       MMC3 (4)
Jumpin' Kid                     128k PRG / 128k CHR     H       MMC3 (4)
Jungle Book, The                128k PRG / 128k CHR     H       MMC3 (4)
Jurassic Park                   128k PRG / 128k CHR     H       MMC3 (4)
Just Breed                      512k PRG / 256k CHR     V       MMC5 (5)
Jyanbou Ozaki no Hole-in-One    128k PRG / 32k CHR      V       MMC1 (1)


Kabuki Quantum Fighter          128k PRG / 128k CHR     V       MMC3 (4)
Kabusiki Doujyou                256k PRG / 0k CHR       V       MMC1 (1)
Kage                            128k PRG / 128k CHR     H       MMC3 (4)
Kagerou Densetsu                128k PRG / 128k CHR     V       MMC3 (4)
Kaguya Hime Densetsu            256k PRG / 0k CHR       H       UNROM (2)
Kaijyu Monogatari [hacked]      128k PRG / 128k CHR     H       MMC3 (4)               
Kaiketsu Yanchamaru Karakuri Land 2 [hacked]    128k PRG / 128k CHR     V       FFE F8xxx (17)
Kaiketsu Yanchamaru Karakuri Land 3     128k PRG / 128k CHR     H       Irem H3001 (65)      
Kakefu-kun no Jump Tengoku      128k PRG / 0k CHR       V       UNROM (2)
Kame no Ongaesi                 128k PRG / 128k CHR     H       MMC1 (1)
Kamen no Ninja Akakage          128k PRG / 0k CHR       V       UNROM (2)
Kamen no Ninja Hanamaru         128k PRG / 128k CHR     H       MMC1 (1)
Kamen Rider Club                128k PRG / 128k CHR     H       74161/32 (70)
Kamen Rider SD                  128k PRG / 128k CHR     V       MMC3 (4)
Karate Champ                    32k PRG / 32k CHR       H       CNROM (3)
Karate Kid, The                 32k PRG / 32k CHR       V       CNROM (3)
Karateka                        16k PRG / 8k CHR        V       ---- (0)
Karnov                          128k PRG / 128k CHR     H       MMC3 (4)
Karnov (J)                      128k PRG / 64k CHR      H       MMC3 (4)
Kart Fighter                    128k PRG / 256k CHR     H       MMC3 (4)
Katteni Sirokuma                128k PRG / 128k CHR     H       MMC1 (1)
Kawa no Nushi Tsuri             128k PRG / 128k CHR     H       MMC3 (4)
Keiba Honmei                    128k PRG / 128k CHR     H       MMC3 (4)
Keisan Game Sansuu 2            32k PRG / 32k CHR       V       CNROM (3)
Keisan Game Sansuu 3            32k PRG / 16k CHR       V       CNROM (3)
Keisan Game Sansuu 4            32k PRG / 32k CHR       V       CNROM (3)
Keisan Game Sansuu 5/6 Nen      32k PRG / 32k CHR       V       CNROM (3)
Kero Kero Keroppi               32k PRG / 32k CHR       H       CNROM (3)
Kero Kero Keroppi 2             128k PRG / 128k CHR     H       MMC1 (1)
Keru Naguru                     128k PRG / 128k CHR     V       MMC3 (4)
Kick Master                     128k PRG / 128k CHR     H       MMC3 (4)
Kick Off                        128k PRG / 0k CHR       V       UNROM (2)
Kickle Cubicle                  128k PRG / 128k CHR     H       MMC3 (4)
Kid Icarus                      128k PRG / 0k CHR       V       MMC1 (1)
Kid Klown                       128k PRG / 128k CHR     H       MMC3 (4)
Kid Kool                        128k PRG / 0k CHR       V       UNROM (2)
Kid Niki                        256k PRG / 0k CHR       H       MMC1 (1)        
King Kong 2                     128k PRG / 128k CHR     V       MMC3 (4)
King of Kings                   128k PRG / 128k CHR     V       Colour Dreams (11)
King of Kings (J - diff. game)  128k PRG / 128k CHR     H       MMC3 (4)
King's Knight                   32k PRG / 32k CHR       H       CNROM (3)
King's Knight (J)               32k PRG / 32k CHR       H       CNROM (3)
King's Quest 5                  256k PRG / 256k CHR     V       MMC3 (4)
Kings of the Beach              32k PRG / 32k CHR       H       CNROM (3)
Kinniku Man                     16k PRG / 8k CHR        V       ---- (0)
Kirby's Adventure               512k PRG / 256k CHR     H       MMC3 (4)
Kiri no London Satsujin Jiken   256k PRG / 0k CHR       H       MMC1 (1)
Kiteretsu Daihyakka             128k PRG / 128k CHR     H       MMC1 (1)
Kiwi Kraze                      128k PRG / 128k CHR     H       MMC3 (4)
Klash Ball                      128k PRG / 0k CHR       H       UNROM (2)
Klax                            64k PRG / 64k CHR       H       Rambo-1 (64)
Klax (J)                        128K PRG / 64k CHR      H       MMC3 (4)
Knight Rider                    64k PRG / 128k CHR      H       MMC1 (1)
Konami Sport in Soul            128k PRG / 128k CHR     V       MMC3 (4)
Koushien                        128k PRG / 128k CHR     V       MMC1 (1)
Krazy Kreatures                 32k PRG / 32k CHR       V       AVE (79)
Krion Conquest, The             128k PRG / 128k CHR     H       MMC3 (4)
Krusty's Fun House              256k PRG / 128k CHR     H       MMC3 (4)        
Krusty's Fun House (PAL)        256k PRG / 128k CHR     H       MMC3 (4)
Kuja Kuou                       256k PRG / 0k CHR       V       MMC1 (1)
Kuja Kuou 2                     128k PRG / 128k CHR     H       MMC1 (1)
Kung-Fu                         32k PRG / 8k CHR        V       ---- (0)
Kung-Fu Heroes                  32k PRG / 32k CHR       H       CNROM (3)
Kureyon Shinchan                128k PRG / 128k CHR     H       Bandai (16)
Kurogane Hirosi no Keiba Densetu128k PRG / 128k CHR     H       MMC3 (4)
Kyoro-chan Land                 128k PRG / 0k CHR       H       UNROM (2)
Kyoto Hana no Missutu Satsujin Jiken    128k PRG / 128k CHR     V       MMC3 (4)
Kyoto Sesupensu Misa Yamamura Satsujin Jiken    128k PRG / 128k CHR     V       MMC3 (4)
Kyuukyoku Stadium               128k PRG / 128k CHR     H       Taito X117 (82)
Kyuukyoko Harikiri Stadium      128k PRG / 256k CHR     H       Taito X117 (82)
Kyuukyoku Harikiri Stadium 3    128k PRG / 256k CHR     H       Taito X117 (82)
Kyuukyoku Tiger			128k PRG / 128k CHR	V	MMC3 (1)


L'Emperuer                      256k PRG / 128k CHR     H       MMC5 (5)
Labyrinth                       128k PRG / 0k CHR       V       UNROM (2)
Lagrange Point                  512k PRG / 0k CHR       H       VRC7 (85)
Laser Invasion                  128k PRG / 128k CHR     H       MMC5 (5)
Last Armageddon                 512k PRG / 0k CHR       V       MMC3 (4)
Last Ninja, The                 128k PRG / 128k CHR     H       MMC3 (4)
Last Starfighter, The           32k PRG / 32k CHR       H       CNROM (3)
Law of the West                 128k PRG / 0k CHR       H       UNROM (2)
Layla                           128k PRG / 0k CHR       V       UNROM (2)
Lee Trevino's Fighting Golf     128k PRG / 128k CHR     H       MMC1 (1)
Legacy of the Wizard            128k PRG / 64k CHR      H       MMC3 (4)
Legend of Kage, The (J)         32k PRG / 16k CHR       H       CNROM (3)
Legend of the Ghost Lion, The   128k PRG / 128k CHR     H       MMC1 (1)
Legend of Zelda, The            128k PRG / 0k CHR       H       MMC1 (1)
Legend of Zelda (-1 PRG Rev.)   128k PRG / 0k CHR       H       MMC1 (1)
Legendary Wings                 128k PRG / 0k CHR       V       UNROM (2)
Legends of the Diamond          128k PRG / 256k CHR     H       MMC3 (4)
Lemmings                        128k PRG / 128k CHR     H       MMC1 (1)
Life Force                      128k PRG / 0k CHR       V       UNROM (2)
Linus Spacehead                 256k PRG / 0k CHR       V       Camerica (71)
Linus Spacehead [Aladdin Ver.]  256k PRG / 0k CHR       V       Camerica (71)
Lion King, The                  256k PRG / 0k CHR       H       AOROM (7)
Lipple Island                   128k PRG / 0k CHR       V       UNROM (2)
Little League Baseball          128k PRG / 128k CHR     H       MMC1 (1)
Little Mermaid, The             128k PRG / 0k CHR       V       UNROM (2)
Little Mermaid, The (J)         128k PRG / 0k CHR       V       UNROM (2)
Little Nemo The Dream Master    128k PRG / 128k CHR     H       MMC3 (4)
Little Ninja Brothers           128k PRG / 128k CHR     H       MMC3 (4)
Lode Runner (J)                 16k PRG / 8k CHR        V       ---- (0)
Lone Ranger, The                256k PRG / 128k CHR     H       MMC3 (4)
Loopz                           128k PRG / 0k CHR       H       UNROM (2)
Lord of King, The               128k PRG / 128k CHR     H       SS8806 (18)
Lost Word of Jenny, The         128k PRG / 0k CHR       V       UNROM (2)
Lot Lot                         32k PRG / 8k CHR        H       ---- (0)
Low G Man                       128k PRG / 128k CHR     H       MMC3 (4)
Lunar Ball                      16k PRG / 8k CHR        V       ---- (0)
Lupin 3rd Pandra Nolsan         128k PRG / 64k CHR      V       MMC3 (4)


Mach Rider                      32k PRG / 8k CHR        H       ---- (0)
Macross                         16k PRG / 8k CHR        V       ---- (0)
Mad City                        128k PRG / 128k CHR     H       MMC1 (1)
Mad Max                         128k PRG / 96k CHR      H       MMC3 (4)  <--probably a bad dump
Mag Max                         32k PRG / 32k CHR       V       CNROM (3)
Mag Max (J)                     32k PRG / 8k CHR        H       ---- (0)
Magic Darts			128k PRG / 128k CHR	H	MMC1 (1)
Magic John                      128k PRG / 128k CHR     H       SS8806 (18)
Magic Johnson's Fast Break	128k PRG / 128k CHR	H	MMC1 (1)
Magic of Scherezade, The        128k PRG / 128k CHR     V       MMC1 (1) 
Magical Doropie			128k PRG / 128k CHR	V	MMC3 (4)
Magical Taruruto-Kun            128k PRG / 128k CHR     H       Bandai (16)
Magical Taruruto-Kun 2          128k PRG / 128k CHR     H       Bandai (16)
Magician                        128k PRG / 128k CHR     H       MMC3 (4)
Magunamu Kikiipatu              128k PRG / 0k CHR       V       UNROM (2)
Maharaja                        128k PRG / 128k CHR     H       Sunsoft 4 (68)
Mahou no Princess Minky Momo    128k PRG / 128k CHR     V       MMC1 (1)
Maijin Eiyuuden Wataru Gaiden   256k PRG / 0k CHR       H       MMC1 (1)
Maison Ikkoku                   256k PRG / 0k CHR       H       UNROM (2)
Majaventure                     128k PRG / 128k CHR     H       MMC1 (1)
Major League                    128k PRG / 128k CHR     V       TC0190 / TC0350 (33)
Major League Baseball           32k PRG / 32k CHR       V       CNROM (3)
Majyan RPG Dora Dora Dora	128k PRG / 128k CHR	H	MMC1 (1)
Majyou Densetsu 2               128k PRG / 0k CHR       V       UNROM (2)  
Makaimura                       128k PRG / 0k CHR       H       UNROM (2)
Maniac Mansion                  256k PRG / 0k CHR       V       MMC1 (1)
Maniac Mansion (J)              256k PRG / 0k CHR       V       UNROM (2)
Maniac Mansion (PAL)            256k PRG / 0k CHR       H       MMC1 (1)
Mappy                           16k PRG / 8k CHR        V       ---- (0)
Mappy Kids                      128k PRG / 128k CHR     H       Namcot 106 (19)
Mappy Land                      128k PRG / 32k CHR      H       MMC3 (4)
Mappy Land (J)                  128k PRG / 32k CHR      V       MMC3 (4)
Marble Madness                  128k PRG / 0k CHR       V       AOROM (7)
Marble Madness (PAL)            128k PRG / 0k CHR       H       AOROM (7)
Mario Bros                      16k PRG / 8k CHR        V       ---- (0)
Mario China                     32k PRG / 32k CHR       V       CNROM (3)
Mario is Missing                128k PRG / 128k CHR     H       MMC3 (4)        
Mario Open Golf                 256k PRG / 0k CHR       H       MMC1 (1)
Mario's Time Machine            128k PRG / 128k CHR     H       MMC3 (4)
Marusa no Onna                  128k PRG / 128k CHR     H       MMC1 (1)
Master Chu and the Drunkard Hu  32k PRG / 32k CHR       H       Colour Dreams (11)
Masuzoe Youiti no Asamade       512k PRG / 0k CHR       H       MMC3 (4)
Masyou                          128k PRG / 0k CHR       H       Nina-1 (34)
Maten Douji                     128k PRG / 128k CHR     V       MMC3 (4)
Matsumoto Tooru no Kabusiki Hissyougaku         256k PRG / 0k CHR       V       UNROM (2)
Matsumoto Tooru no Kabusiki Hissyougaku 2       256K PRG / 0k CHR       V       MMC1 (1)
Max Warrior                     128k PRG / 128k CHR     V       MMC3 (4)
McKids                          128k PRG / 128k CHR     H       MMC3 (4)
Mechanized Attack               64k PRG / 128k CHR      H       MMC1 (1)
Mega Man                        128k PRG / 0k CHR       V       UNROM (2)
Mega Man 2                      256k PRG / 0k CHR       V       MMC1 (1)
Mega Man 3                      256k PRG / 128k CHR     H       MMC3 (4)
Mega Man 4                      512k PRG / 0k CHR       H       MMC3 (4)
Mega Man 4 (PAL)                512k PRG / 0k CHR       H       MMC3 (4)
Mega Man 5                      256k PRG / 256k CHR     H       MMC3 (4)
Mega Man 6                      512k PRG / 0k CHR       H       MMC3 (4)
Megami Tensei                   128k PRG / 128k CHR     V       MMC3 (4)
Megami Tensei 2                 256k PRG / 256k CHR     V       Namcot 106 (19)
Meiji Ishin                     256k PRG / 0k CHR       H       MMC1 (1)
Meikyu Jima                     128k PRG / 128k CHR     H       Irem G101 (32)
Meikyu Kumikyoko                32k PRG / 32k CHR       V       CNROM (3)
Meimom Takonishi Ouendan        128k PRG / 128k CHR     H       MMC3 (4)
Meitantei Holmes                256k PRG / 0k CHR       H       MMC1 (1)
Meitantei Holmes Kori no London Satsujin Jiken  256k PRG / 0k CHR       H       UNROM (2)
Melville's Flame                128k PRG / 128k CHR     V       MMC1 (1)
Mendel Palace                   128k PRG / 128k CHR     V       MMC3 (4)
Metal Fighter                   32k PRG / 32k CHR       H       Colour Dreams (11)
Metal Flame Psybuster           128k PRG / 128k CHR     V       MMC1 (1)
Metal Gear                      128k PRG / 0k CHR       V       UNROM (2)
Metal Gear (J)                  128k PRG / 0k CHR       H       UNROM (2)
Metal Max                       256k PRG / 256k CHR     H       MMC3 (4)
Metal Mech                      128k PRG / 128k CHR     V       MMC1 (1)
Metal Slader Glory              512k PRG / 512k CHR     V       MMC5 (5)
Metal Storm                     128k PRG / 256k CHR     H       MMC3 (4)
Metal Storm (J)                 128k PRG / 256k CHR     V       MMC3 (4)
Metro Cross                     32k PRG / 32k CHR       V       MMC3 (4)
Metroid                         128k PRG / 0k CHR       V       MMC1 (1)
Mezase Top Pro                  256k PRG / 128k CHR     H       SS8806 (18)        
Mickey Mouse                    32k PRG / 32k CHR       V       CNROM (3)
Mickey Mousecapade              32k PRG / 32k CHR       V       CNROM (3)
Micro Machines                  256k PRG / 0k CHR       V       Camerica (71)
Micro Machines [Aladdin Ver.]   256k PRG / 0k CHR       V       Camerica (71)
Might and Magic                 256k PRG / 256k CHR     H       MMC3 (4)
Might and Magic (J)             256k PRG / 256k CHR     H       MMC3 (4)
Mighty Bomb Jack (J)            32k PRG / 8k CHR        H       ---- (0)
Mighty Final Fight              128k PRG / 128k CHR     H       MMC3 (4)
Mighty Final Fight (J)          128k PRG / 128k CHR     H       MMC3 (4)
Mike Tyson's Punch-Out          128k PRG / 128k CHR     V       MMC2 (9)
Millipede                       16k PRG / 8k CHR        H       ---- (0)
Milon's Secret Castle           32k PRG / 32k CHR       V       CNROM (3)
Mindseeker                      128k PRG / 144k CHR     H       Namcot 106 (19) <--bad dump?
Minelvation Saga                128k PRG / 128k CHR     H       MMC3 (4)
Mini-Putt                       128k PRG / 128k CHR     H       MMC1 (1)        
Minnie no Talabou Nakayosi Daisakusen   32k PRG / 32k CHR       H       CNROM (3)
Miracle Piano System            256k PRG / 64k CHR      H       MMC1 (1)
Miracle Ropit's Adventure in 2100       128k PRG / 0k CHR       V       UNROM (2)
Mirai Sensei Lios               256k PRG / 0k CHR       V       MMC1 (1)
Mission Cobra                   32k PRG / 16k CHR       V       Colour Dreams (11)
Mission Impossible              128k PRG / 128k CHR     H       MMC3 (4)
Mission Impossible (PAL)        128k PRG / 128k CHR     H       MMC3 (4)
Mitsume ga Tooru                128k PRG / 128k CHR     V       MMC3 (4)        
Mizusima Senji no Dai Koushien  256k PRG / 0k CHR       V       MMC1 (1)
Moai Kun                        32k PRG / 32k CHR       V       CNROM (3)
Mobile Suit Gundam Z (hacked)   256k PRG / 0k CHR       H       (15)
Moeru Oniisan                   128k PRG / 128k CHR     V       MMC1 (1)
Moeru Pro Baseball '90		256k PRG / 128k CHR	H	SS8806 (18)
Momotarou Densetsu              256k PRG / 0k CHR       H       MMC1 (1)
Momotarou Densetsu Gaiden       512k PRG / 0k CHR       H       MMC3 (4)        
Momotarou Dentetsu              256k PRG / 0k CHR       V       UNROM (2)
Money Game, The [Hacked]        256k PRG / 0k CHR       H       FFE F4xxx (6)
Money Game 2, The               128k PRG / 128k CHR     H       MMC1 (1)
Monopoly                        128k PRG / 128k CHR     H       MMC1 (1)
Monopoly (J)                    256k PRG / 0k CHR       H       MMC1 (1)
Monster In My Pocket            128k PRG / 128k CHR     H       MMC3 (4)
Monster In My Pocket (PAL)      128k PRG / 128k CHR     H       MMC3 (4)
Monster Maker                   256k PRG / 0k CHR       H       MMC1 (1)
Monster Party                   128k PRG / 128k CHR     H       MMC1 (1)
Moon Crystal                    256k PRG / 256k CHR     H       MMC3 (4)
Morita no Shogi                 128k PRG / 8k CHR       H       MMC1 (1)
Mortal Kombat 3                 128k PRG / 512k CHR     V       PCJY?? (90)
Mother                          256k PRG / 128k CHR     H       MMC3 (4)
Motocross Champion              128k PRG / 128k CHR     H       MMC1 (1)
Motor City Patrol               128k PRG / 128k CHR     H       MMC1 (1)
Mottono Abunai Deka             128k PRG / 0k CHR       H       UNROM (2)
Mouryou Senki Madara            256k PRG / 256k CHR     V       VRC6v (26)
Ms. Pac-Man                     32k PRG / 8k CHR        H       ---- (0)
Mule                            128k PRG / 0k CHR       V       MMC1 (1)
Muppets Adventure               128k PRG / 0k CHR       V       MMC1 (1)
Murder Club                     128k PRG / 128k CHR     V       MMC3 (4)
Musashi no Bouken               128k PRG / 128k CHR     V       MMC1 (1)
Musashi no Ken                  32k PRG / 32k CHR       V       CNROM (3)
MUSCLE                          16k PRG / 32k CHR       V       CNROM (3)
My Life, My Love                256k PRG / 256k CHR     H       MMC3 (4)
Mystery Quest                   32k PRG / 32k CHR       V       CNROM (3)


Nagagutu wo Haita Neko          32k PRG / 32k CHR       V       CNROM (3)
Naitou 9 Dan Shogi Hiden	16k PRG / 8k CHR	H	---- (0)
Namco Classic                   256k PRG / 256k CHR     H       Namcot 106 (19)
Nangoku Seirei Spy vs Spy       128k PRG / 0k CHR       V       UNROM (2)        
Nantteta Baseball               128k PRG / 128k CHR     H       Sunsoft 4 (68)
NARC                            128k PRG / 0k CHR       H       AOROM (7)
Navy Blue                       128k PRG / 0k CHR       H       MMC1 (1)
Nekketsu Kakutou Densetsu       128k PRG / 128k CHR     V       MMC3 (4)
Nekketsu Kouha Kunio Kun        128k PRG / 0k CHR       V       UNROM (2)        
Nekketsu Koukou Dodgeball       128k PRG / 128k CHR     H       MMC1 (1)
Nekketsu Koukou Soccer          128k PRG / 128k CHR     V       MMC3 (4)
NES Open Tournament Golf        256k PRG / 0k CHR       H       MMC1 (1)
NES Play Action Football        128k PRG / 128k CHR     H       MMC3 (4)        
New Zealand Story, The          128k PRG / 128k CHR     H       MMC3 (4)
NFL Football                    128k PRG / 0k CHR       V       UNROM (2)        
Nightmare on Elm Street, A      128k PRG / 0k CHR       H       AOROM (7)
Nightshade                      256k PRG / 256k CHR     H       MMC3 (4)
Niji no Silkroad                256k PRG / 128k CHR     V       MMC3 (4)
Ningen Heiki Dead Fox		128k PRG / 128k CHR	V	MMC3 (4)
Ninja Cop Saizou                128k PRG / 128k CHR     H       MMC1 (1)
Ninja Gaiden                    128k PRG / 128k CHR     H       MMC1 (1)
Ninja Gaiden 2                  128k PRG / 128k CHR     H       MMC3 (4)
Ninja Gaiden 3                  128k PRG / 128k CHR     H       MMC3 (4)
Ninja Jajamaru Kun              32k PRG / 16k CHR       V       CNROM (3)
Ninja Kid                       32k PRG / 32k CHR       V       CNROM (3)
Ninja Kun                       16k PRG / 8k CHR        V       ---- (0)
Ninja Kun Asyura no Saiyou      128k PRG / 0k CHR       V       UNROM (2)
Ninja Rahoi                     512k PRG / 0k CHR       V       MMC1 (1)
Ninja Ryukenden                 128k PRG / 128k CHR     V       MMC1 (1)
Ninja Ryukenden 2               128k PRG / 128k CHR     V       MMC3 (4)
Ninja Ryukenden 3               128k PRG / 128k CHR     H       MMC3 (4)
Nintendo World Cup              128k PRG / 128k CHR     H       MMC3 (4)
Nintendo World Cup (PAL)        128k PRG / 128k CHR     H       MMC3 (4)
Noah's Ark                      128k PRG / 128k CHR     H       MMC3 (4)
Nobunaga no Yabou Zenkokuban    256k PRG / 0k CHR       V       MMC1 (1)
Nobunaga's Ambition             256k PRG / 0k CHR       H       MMC1 (1)
Nobunaga's Ambition 2           256k PRG / 128k CHR     H       MMC5 (5)
North and South                 128k PRG / 128k CHR     H       MMC3 (4)
Nuts and Milk                   16k PRG / 8k CHR        H       ---- (0)
Nyankies                        128k PRG / 128k CHR     H       MMC3 (4)


Obake no Qtarou                 32k PRG / 8k CHR        V       ---- (0)
Obochama Kun                    128k PRG / 128k CHR     H       MMC1 (1)
Ohotuku Ni Kiyu                 256k PRG / 0k CHR       V       MMC1 (1)        
Oishinbo                        128k PRG / 128k CHR     V       MMC1 (1)
Olympus no Tatakai              128k PRG / 0k CHR       V       UNROM (2)
Onyanko Town                    32k PRG / 8k CHR        V       ---- (0)
Operation Wolf                  128k PRG / 128k CHR     V       MMC1 (1)
Orb 3D                          64k PRG / 128k CHR      V       MMC1 (1)
Oryu San                        32k PRG / 32k CHR       H       CNROM (3)        
Osomatsu Kun                    128k PRG / 128k CHR     V       MMC1 (1)
Otaku no Seiza                  256k PRG / 128k CHR     H       MMC3 (4)
Othello (J)                     32k PRG / 8k CHR        V       ---- (0)
Othello (Disk conversion)       32k PRG / 8k CHR        H       ---- (0)  (Loads using .sav file)
Outlanders                      128k PRG / 0k CHR       V       UNROM (2)
Over Horizon                    128k PRG / 128k CHR     H       MMC3 (4)
Overlord                        256k PRG / 0k CHR       H       MMC1 (1)


Pac-Land                        32k PRG / 8k CHR        V       ---- (0)
Pac-Man                         16k PRG / 8k CHR        H       ---- (0)
Pac-Man (Unlicensed Version)    16k PRG / 8k CHR        J       ---- (0)
Pac-Man (J)                     16k PRG / 8k CHR        H       ---- (0)
Pachi Slot Adventure 2          128k PRG / 128k CHR     V       MMC3 (4)
Pachi Slot Adventure 3          128k PRG / 128k CHR     H       MMC3 (4)
Pachicom                        32k PRG / 8k CHR        H       ---- (0)
Pachinko Daisakusen             128k PRG / 128k CHR     V       MMC1 (1)
Pachinko Daisakusen 2           128k PRG / 128k CHR     H       MMC1 (1)
Pachio Kun 2                    256k PRG / 0k CHR       V       MMC1 (1)
Pachio Kun 3                    256k PRG / 128k CHR     H       MMC3 (4)
Pachio Kun 4                    512k PRG / 0k CHR       H       MMC3 (4)
Pachio Kun 5 (Pachio Kun Jr)    256k PRG / 128k CHR     H       MMC3 (4)
Palamedes (J)                   32k PRG / 32k CHR       H       MMC1 (1)
Palamedes 2                     32k PRG / 32k CHR       V       MMC1 (1)
Panic Restaurant                128k PRG / 128k CHR     H       MMC3 (4)
Paperboy                        32k PRG / 32k CHR       H       CNROM (3)
Paperboy 2                      256k PRG / 0k CHR       H       UNROM (2)
Parallel World                  128k PRG / 128k CHR     H       MMC3 (4)
Parasol Henbei                  128k PRG / 128k CHR     H       MMC3 (4)
Paris Dakar Rally [Hacked]      128k PRG / 32k CHR      V       FFE F3xxx (8)
Parman                          128k PRG / 128k CHR     H       Irem G101 (32)
Parodius [Hacked]               128k PRG / 128k CHR     H       FFE F8xxx (17)
Parodius (PAL)                  128k PRG / 128k CHR     H       MMC3 (4)
Parutena no Kagami              128k PRG / 0k CHR       V       MMC1 (1)
Peepar Time                     32k PRG / 16k CHR       H       CNROM (3)
Penguin Kun Wars                32k PRG / 8k CHR        V       ---- (0)
Perfect Bowl                    128k PRG / 32k CHR      H       MMC1 (1)
Pesterminator                   64k PRG / 64k CHR       V       Colour Dreams (11)
Peter Pan and the Pirates       128k PRG / 128k CHR     H       MMC1 (1)
Phantom Fighter                 256k PRG / 0k CHR       V       MMC1 (1)
Pictionary                      128k PRG / 128k CHR     V       MMC1 (1)
Pinball                         16k PRG / 8k CHR        H       ---- (0)
Pinball Quest                   128k PRG / 128k CHR     H       MMC1 (1)
Pinbot                          128k PRG / 56k CHR      V       TQROM (119)   <--bad dump
Pinbot (PAL)                    128k PRG / 64k CHR      H       TQROM (119)
Pipe Dream                      32k PRG / 32k CHR       V       CNROM (3)
Pirates                         128k PRG / 128k CHR     H       MMC1 (1)
Pizza Pop                       128k PRG / 128k CHR     H       SS8806 (18)
Platoon                         128k PRG / 128k CHR     H       MMC1 (1)
Plazma Ball                     128k PRG / 128k CHR     H       SS8806 (18)
Pole to Finish                  128k PRG / 128k CHR     V       MMC1 (1)
Poo-Yan                         16k PRG / 8k CHR        V       ---- (0)
Pool of Radiance                512k PRG / 128k CHR     V       MMC3 (4)
Pool of Radiance (J)		512k PRG / 128k CHR	V	MMC3 (4)
Popeye                          16k PRG / 8K CHR        V       ---- (0)
Popeye Eigo Asobi               16k PRG / 8k CHR        V       ---- (0)
Portpia Renzoku Satsujin Jiken  32k PRG / 8k CHR        V       ---- (0)
POW                             128k PRG / 128k CHR     H       MMC1 (1)
Power Blade                     128k PRG / 128k CHR     H       MMC3 (4)
Power Blade 2                   128k PRG / 128k CHR     H       MMC3 (4)
Power Punch 2                   128k PRG / 256k CHR     H       MMC3 (4)
Predator                        128k PRG / 128k CHR     V       MMC1 (1)
President no Sentaku            256k PRG / 0k CHR       H       MMC1 (1)
Prince of Persia                128k PRG / 0k CHR       V       UNROM (2)
Prince Valiant                  128k PRG / 128k CHR     H       MMC1 (1)
Princess Tomato in Salad Kingdom256k PRG / 0k CHR       H       MMC1 (1)
Pro Wrestling                   128k PRG / 0k CHR       V       UNROM (2)
Pro Yakyuu Satsujin Jiken       256k PRG / 0k CHR       V       UNROM (2)         
Probotector                     128k PRG / 0k CHR       V       UNROM (2)
Probotector 2                   128k PRG / 128k CHR     H       MMC3 (4)
Project Q                       256k PRG / 0k CHR       V       MMC3 (4)
Punch-Out                       128k PRG / 128k CHR     V       MMC2 (9)
Punch-Out Shohinban             128k PRG / 128k CHR     H       MMC2 (9)
Punisher, The                   128k PRG / 128k CHR     H       MMC3 (4)
Puss N' Boots                   128k PRG / 0k CHR       V       UNROM (2)
Puznic                          32k PRG / 32k CHR       V       CNROM (3)        
Puzslot                         128k PRG / 128k CHR     H       MMC1 (1)
Puzzle                          32k PRG / 32k CHR       H       CNROM (3)
Pyramid                         32k PRG / 8k CHR        V       ---- (0)


Q-Bert                          32k PRG / 32k CHR       H       CNROM (3)
Quarth                          32k PRG / 32k CHR       H       CNROM (3)
Quattro Adventure               256k PRG / 0k CHR       V       Camerica (71)
Quattro Sports                  256k PRG / 0k CHR       V       Camerica (71)
Quest of Kai, The               128k PRG / 64k CHR      V       MMC3 (4)
Quinty                          128k PRG / 128k CHR     H       MMC3 (4)


Racket Attack                   256k PRG / 128k CHR     H       MMC1 (1)
Race America                    128k PRG / 128k CHR     H       MMC1 (1)
Rad Racer                       128k PRG / 0k CHR       H       MMC1 (1)
Rad Racer (PAL)                 128k PRG / 0k CHR       H       MMC1 (1)
Rad Racer 2                     64k PRG / 64k CHR       H       MMC3 (4)
Radia Senki                     256k PRG / 128k CHR     V       MMC3 (4)
Raf World                       128k PRG / 128k CHR     V       MMC1 (1)
Raid 2020                       64k PRG / 32k CHR       V       Colour Dreams (11)
Raid on Bungling Bay		16k PRG / 8k CHR	V	---- (0)
Raid on Bungling Bay (J)        16k PRG / 8k CHR        V       ---- (0)
Rainbow Island                  128k PRG / 0k CHR       H       UNROM (2)
Rambo                           128k PRG / 0k CHR       V       UNROM (2)
Rambo (J)                       128k PRG / 0k CHR       V       UNROM (2)
Rampage                         128k PRG / 64k CHR      H       MMC3 (4)
Rampart                         128k PRG / 128k CHR     H       MMC3 (4)
Rampart (J)                     128k PRG / 0k CHR       H       UNROM (2)
Rasarulsii no Child's Quest	128k PRG / 64k CHR	V	MMC3 (4)
RBI Baseball                    64k PRG / 128k CHR      V       MMC3 (4)
RBI Baseball 2                  128k PRG / 128k CHR     V       MMC3 (4)
RBI Baseball 3                  128k PRG / 64k CHR      H       MMC3 (4)
RC Pro-Am                       32k PRG / 32k CHR       H       MMC1 (1)
RC Pro-Am (PAL)                 32k PRG / 32k CHR       H       MMC1 (1)
RC Pro-Am 2                     256k PRG / 0k CHR       H       AOROM (7)
Recca: Summer Carnival '92      128k PRG / 128k CHR     H       MMC3 (4)
Redalerma 2                     128k PRG / 128k CHR     V       MMC3 (4)
Reigen Dousi                    256k PRG / 0k CHR       V       MMC1 (1)
Remote Control                  128k PRG / 128k CHR     V       MMC1 (1)
Ren and Stimpy:  Buckaroo$      128k PRG / 128k CHR     H       MMC3 (4)
Renegade                        128k PRG / 0k CHR       V       UNROM (2)
Rescue:  The Embassy Mission    128k PRG / 128k CHR     H       MMC1 (1)
Ring King                       64k PRG / 128k CHR      V       MMC3 (4)
River City Ransom               128k PRG / 128k CHR     H       MMC3 (4)
Road Fighter                    16k PRG / 8k CHR        H       ---- (0)
Road Man                        128k PRG / 128k CHR     V       MMC1 (1)
Road Runner                     64k PRG / 128k CHR      H       MMC3 (4)
Roadblasters                    128k PRG / 128k CHR     H       MMC1 (1)
Robin Hood                      256k PRG / 0k CHR       H       MMC1 (1)
Robocco Wars                    256k PRG / 128k CHR     H       MMC3 (4)
Robocop                         128k PRG / 128k CHR     H       MMC3 (4)
Robocop 2                       128k PRG / 128k CHR     V       MMC1 (1)
Robocop 3                       128k PRG / 128k CHR     V       MMC1 (1)
Robocop Vs Terminator           128k PRG / 128k CHR     H       MMC3 (4)
Robot Block (Stack-Up)          32k PRG / 8k CHR        H       ---- (0)
Robot Gyro (Gyromite)           32k PRG / 8k CHR        V       ---- (0)
Robowarrior                     128k PRG / 0k CHR       H       UNROM (2)
Rock N' Ball                    128k PRG / 64k CHR      H       MMC3 (4)       
Rocket Ranger                   256k PRG / 0k CHR       H       MMC1 (1)
Rocketeer, The                  256k PRG / 0k CHR       V       MMC1 (1)
Rockin' Kats                    128k PRG / 128k CHR     H       MMC3 (4)
Rockman                         128k PRG / 0k CHR       V       UNROM (2)
Rockman 2                       256k PRG / 0k CHR       V       MMC1 (1)
Rockman 3                       256k PRG / 128k CHR     V       MMC3 (4)
Rockman 4                       512k PRG / 0k CHR       V       MMC3 (4)
Rockman 5                       256k PRG / 256k CHR     H       MMC3 (4)
Rockman 6                       512k PRG / 0k CHR       V       MMC3 (4)
Roger Clemens MVP Baseball      128k PRG / 256k CHR     H       MMC3 (4)
Rollerball                      128k PRG / 128k CHR     H       MMC1 (1)
Rollergames                     128k PRG / 128k CHR     H       MMC3 (4)
Rolling Thunder                 128k PRG / 128k CHR     H       MMC3 (4)
Rolling Thunder (J)             128k PRG / 256k CHR     H       Namcot 106 (19)
Romance of the Three Kingdoms   256k PRG / 0k CHR       V       MMC1 (1)
Romance of the Three Kingdoms 2 256k PRG / 256k CHR     H       MMC5 (5)
Romancia                        128k PRG / 0k CHR       V       MMC1 (1)
Roundball                       128k PRG / 128k CHR     H       MMC3 (4)
Route 16 Turbo                  32k PRG / 8k CHR        H       ---- (0)
RPG Life Game                   256k PRG / 128k CHR     H       MMC3 (4)
Rush N' Attack                  128k PRG / 0k CHR       V       UNROM (2)
Rush N' Attack (PAL)            128k PRG / 0k CHR       V       UNROM (2)
Rygar                           128k PRG / 0k CHR       V       UNROM (2)


Sabaku no Kitune                128k PRG / 128k CHR     V       MMC1 (1)
Saint Seiya [hacked]            256k PRG / 0k CHR       V       FFE F4xxx (6)
Saint Seiya 2 [hacked?]         128k PRG / 128k CHR     H       MMC1 (1)
Saiyuuki World                  128k PRG / 0k CHR       V       UNROM (2)
Saiyuuki World 2 [Hacked]       128k PRG / 128k CHR     V       FFE F8xxx (17)
Sakigake Otoko Jyuku            128k PRG / 128k CHR     H       Bandai (16)
Salamander                      128k PRG / 0k CHR       V       UNROM (2)
Sanada Jyuuyuusi                128k PRG / 128k CHR     H       MMC1 (1)
Sangokusi                       256k PRG / 0k CHR       V       MMC1 (1)
Sangokusi Chugen no Hasya       128k PRG / 128k CHR     V       Namcot 106 (19)
Sangokusi Chugen no Hasya 2 [Hacked]    256k PRG / 256k CHR     H       TC190V (48)
Sanma no Meitantei              128k PRG / 64k CHR      H       MMC3 (4)
Sanrio Carnival                 32k PRG / 32k CHR       V       CNROM (3)
Sanrio Cup Ponpon Bare          32k PRG / 32k CHR       V       CNROM (3)
Sansa-Rana-Ga                   256k PRG / 128k CHR     V       MMC3 (4)
Sarada no Kuni no Tomato Hime   256k PRG / 0k CHR       V       MMC1 (1)
Satomi Hakkenden                128k PRG / 128k CHR     V       MMC1 (1)
SCAT                            128k PRG / 128k CHR     H       MMC1 (1)
SD Gundam Gachapon Senshi 2     128k PRG / 128k CHR     V       MMC3 (4)
SD Gundam Gachapon Senshi 3     128k PRG / 128k CHR     H       MMC3 (4)
SD Gundam Gachapon Senshi 4     256k PRG / 256k CHR     V       MMC3 (4)
SD Gundam Gachapon Senshi 5     128k PRG / 128k CHR     V       MMC3 (4)
SD Gundam Knight Story          256k PRG / 128k CHR     H       Bandai (16)
SD Gundam Knight Story 2        256k PRG / 256k CHR     H       Bandai (16)
SD Gundam Knight Story 3        256k PRG / 256k CHR     V       Bandai (16)
SD Hatori Daisakusen            128k PRG / 128k CHR     H       MMC3 (4)
SD Hero Soukessen               128k PRG / 128k CHR     H       MMC3 (4)
SD Keiji Blader                 128k PRG / 128k CHR     H       Taito X117 (82)
SD Sengoku Busyou Retsuden      128k PRG / 128k CHR     H       MMC3 (4)
Section Z                       128k PRG / 0k CHR       V       UNROM (2)
Seicross                        32k PRG / 8k CHR        V       ---- (0)
Seicross (J)                    32k PRG / 8k CHR        V       ---- (0)
Seikima 2                       32k PRG / 32k CHR       H       CNROM (3)
Seirei Densetsu Lickle          256k PRG / 128k CHR     H       MMC3 (4)
Seirei Gari                     256k PRG / 0k CHR       V       UNROM (2)
Sekiryuu Ou                     128k PRG / 128k CHR     H       MMC1 (1)
Senjou no Ookami                128k PRG / 0k CHR       H       UNROM (2)
Sesame Street 123/ABC           128k PRG / 128k CHR     H       MMC1 (1)
Sesame Street ABC               32k PRG / 32k CHR       H       MMC1 (1)
Shadow Brain                    128k PRG / 256k CHR     V       MMC3 (4)
Shadow of the Ninja             128k PRG / 128k CHR     H       MMC3 (4)
Shadow Warrior                  128k PRG / 128k CHR     H       MMC1 (1)
Shadowgate                      128k PRG / 128k CHR     H       MMC3 (4)
Shadowgate (J)                  128k PRG / 128k CHR     H       MMC3 (4)
Shadowgate (Swedish)            128k PRG / 128k CHR     H       MMC3 (4)
Shaffle Fight                   256k PRG / 0k CHR       H       MMC3 (4)
Shancara                        128k PRG / 128k CHR     V       MMC3 (4)
Shanghai                        128k PRG / 0k CHR       H       UNROM (2)
Shanghai 2                      128k PRG / 0k CHR       V       UNROM (2)
Shatterhand                     128k PRG / 128K CHR     H       MMC3 (4)
Sherlock Holmes                 128k PRG / 0k CHR       H       UNROM (2)
Shikin Jyou                     128k PRG / 0k CHR       H       MMC1 (1)
Shin Satomi Hakkenden           256k PRG / 0k CHR       H       MMC1 (1)
Shingen The Ruler               256k PRG / 0k CHR       H       MMC1 (1)
Shinobi                         128k PRG / 128k CHR     H       Rambo-1 (64)
Shinsenden                      256k PRG / 0k CHR       V       MMC1 (1)
Shisen Mahjong Seifukuhen       32k PRG / 64k CHR       H       CNROM (3)
Shogun                          128k PRG / 0k CHR       H       MMC1 (1)
Shonen Ashibe                   128k PRG / 128k CHR     V       MMC3 (4)
Shufflepuck Cafe                128k PRG / 0k CHR       H       UNROM (2)        
Side Pocket                     128k PRG / 0k CHR       H       UNROM (2)
Side Pocket (J)                 128k PRG / 32k CHR      H       MMC3 (4)
Sijyou Saidai Quiz ou Ketteisen 128k PRG / 0k CHR       V       MMC1 (1)
Silent Assault                  64k PRG / 64k CHR       H       Colour Dreams (11)
Silent Service                  128k PRG / 0k CHR       V       UNROM (2)
Silent Service (PAL)            128k PRG / 0k CHR       V       UNROM (2)
Silkworm                        128k PRG / 128k CHR     V       MMC1 (1)
Silva Saga                      256k PRG / 256k CHR     H       MMC3 (4)
Silver Surfer                   128k PRG / 256k CHR     H       MMC3 (4)
Sinjinrui                       32k PRG / 32k CHR       V       CNROM (3)
Skate or Die                    128k PRG / 0k CHR       V       UNROM (2)
Skate or Die 2                  256k PRG / 128k CHR     V       MMC1 (1)
Ski or Die                      128k PRG / 128k CHR     H       MMC1 (1)
Sky Destroyer                   16k PRG / 8k CHR        H       ---- (0)
Sky Kid                         32k PRG / 64k CHR       H       MMC1 (1)
Sky Kid (J)                     32k PRG / 32k CHR       V       MMC3 (4) 
Sky Shark                       64k PRG / 128k CHR      H       MMC1 (1)
Slalom                          32k PRG / 8k CHR        H       ---- (0)
Slalom (PAL)                    32k PRG / 8k CHR        H       ---- (0)
Smash Ping Pong (Disk Conv.)    32k PRG / 16k CHR       H       CNROM (3)  (uses .sav file to load)
Smash TV                        128k PRG / 112k CHR     H       MMC3 (4)  <--probably an incomplete dump
SMB / Duck Hunt / WC Track Meet 128k PRG / 64k CHR      H       MMC1 (1)
SMB / Tetris / Nint. World Cup  64k PRG / 128k CHR      H       MMC3 (4)
Snake Rattle N' Roll            32k PRG / 32k CHR       H       MMC1 (1)
Snake Rattle N' Roll (PAL)      32k PRG / 32k CHR       H       MMC1 (1)
Snake's Revenge                 128k PRG / 128k CHR     H       MMC1 (1)
Snoopy's Magic Show             128k PRG / 128k CHR     V       MMC1 (1)
Snow Bros                       128k PRG / 128k CHR     H       MMC1 (1)
Soap Panic                      32k PRG / 32k CHR       V       CNROM (3)
Soccer                          32k PRG / 8k CHR        H       ---- (0)
Soccer (Disk Conversion)        32k PRG / 8k CHR        H       ---- (0)  (uses a .sav to load)
Softball Tengoku                128k PRG / 128k CHR     H       MMC1 (1)
Solar Jetman                    256k PRG / 0k CHR       H       AOROM (7)
Solar Jetman (PAL)              256k PRG / 0k CHR       H       AOROM (7)
Solomon no Kagi                 32k PRG / 32k CHR       H       CNROM (3)
Solomon no Kagi 2               128k PRG / 128k CHR     V       MMC3 (4)
Solomon's Key                   32k PRG / 32k CHR       H       CNROM (3)
Solstice                        128k PRG / 0k CHR       H       AOROM (7)
Solstice (J)                    128k PRG / 0k CHR       H       AOROM (7)
SoMari                          256k PRG / 256k CHR     H       MMC3 (4)
Son Son                         32k PRG / 8k CHR        H       ---- (0)
Space Harrier                   128k PRG / 0k CHR       V       UNROM (2)
Space Hunter                    32k PRG / 32k CHR       H       CNROM (3)
Space Invaders                  16k PRG / 8k CHR        V       ---- (0)
Space Shuttle Project           256k PRG / 0k CHR       V       MMC1 (1)
Spartan X                       32k PRG / 8k CHR        V       ---- (0)
Spartan X2  [Hacked]            128k PRG / 128k CHR     V       FFE F8xxx (17)
Spelunker (J)                   32k PRG / 8k CHR        V       ---- (0)
Spelunker 2                     128k PRG / 0k CHR       V       UNROM (2)
Spider-Man                      128k PRG / 208k CHR     H       MMC3 (4)  <--probable bad dump
Spiritual Warfare               128k PRG / 128k CHR     V       Colour Dreams (11)        
Splatterhouse                   128k PRG / 128k CHR     H       Namcot 106  (19)
Spot                            128k PRG / 0k CHR       V       MMC1 (1)
Spy Hunter                      32k PRG / 32k CHR       V       CNROM (3)
Spy Vs. Spy (J)                 32k PRG / 8k CHR        V       ---- (0)
Sqoon                           32k PRG / 16k CHR       V       CNROM (3)
Sqoon (J)                       32k PRG / 8k CHR        V       ---- (0)
Square no Tom Sawyer            256k PRG / 0k CHR       H       MMC1 (1)
Stanley                         128k PRG / 128k CHR     H       MMC3 (4)
Star Force                      32k PRG / 32k CHR       V       CNROM (3)
Star Force (J)                  16k PRG / 8k CHR        V       ---- (0)
Star Gate                       16k PRG / 8k CHR        V       ---- (0)
Star Luster                     32k PRG / 8k CHR        H       ---- (0)
Star Soldier (J)                32k PRG / 16k CHR       V       CNROM (3)
Star Trek                       256k PRG / 256k CHR     H       MMC3 (4)
Star Trek - The Next Generation 128k PRG / 0k CHR       H       UNROM (2)
Star Voyager                    32k PRG / 32k CHR       H       CNROM (3)
Star Wars                       128k PRG / 128k CHR     H       MMC3 (4)
Star Wars (J - Namco version)   128k PRG / 128k CHR     H       MMC3 (4)
Star Wars (PAL)                 128k PRG / 128k CHR     H       MMC3 (4)
Starship Hector                 128k PRG / 0k CHR       V       UNROM (2)
Startropics                     256k PRG / 256k CHR     V       MMC3 (4)  <--proper chip (MMC6) unsupported
Startropics 2                   256k PRG / 256k CHR     H       MMC3 (4)  <--proper chip (MMC6) unsupported
Stealth ATF                     128k PRG / 128k CHR     H       MMC1 (1)
Stealth ATF (PAL)               128k PRG / 128k CHR     H       MMC1 (1)
Sted Iseki Wakusei no Yabou     128k PRG / 128k CHR     H       MMC1 (1)
Stick Hunter                    128k PRG / 0k CHR       V       UNROM (2)
Stinger                         128k PRG / 0k CHR       V       UNROM (2)
Street Cop                      128k PRG / 128k CHR     H       MMC1 (1)
Street Fighter 2010             128k PRG / 128k CHR     H       MMC3 (4)
Street Fighter 3                128k PRG / 512k CHR     V       HK-SF3 (91)
Strider                         256k PRG / 0k CHR       V       MMC1 (1)
Sugoro Quest                    128k PRG / 128k CHR     V       MMC3 (4)
Sukeban Deka 3                  128k PRG / 0k CHR       H       UNROM (2)
Super Arabian                   16k PRG / 8k CHR        H       ---- (0)
Super Black Onyx		256k PRG / 0k CHR	V	MMC1 (1)
Super Chinese Land              32k PRG / 16k CHR       H       MMC3 (4)
Super Chinese Land 2            128k PRG / 128k CHR     H       MMC3 (4)
Super Chinese Land 3		128k PRG / 128k CHR	H	MMC1 (1)
Super C                         128k PRG / 120k CHR     H       MMC3 (4)  <--missing another 8k CHR page
Super Cars                      128k PRG / 0k CHR       H       UNROM (2)
Super Contra                    128k PRG / 128k CHR     V       MMC3 (4)
Super Dodge Ball                128k PRG / 128k CHR     H       MMC1 (1)        
Super Donkey Kong 2		128k PRG / 256k CHR	V	MMC3 (4)
Super Dynamix Badminton         32k PRG / 8k CHR        V       ---- (0)
Super Express Satsujin Jiken    128k PRG / 128k CHR     V       MMC3 (4)
Super Glove Ball                128k PRG / 0k CHR       V       UNROM (2)
Super Mario Bros                32k PRG / 8k CHR        V       ---- (0)
Super Mario Bros (PAL)          32k PRG / 8k CHR        V       ---- (0)
Super Mario Bros (Disk Conv.)   32k PRG / 8k CHR        H       ---- (0)  (loads using a .sav)
Super Mario Bros / Duck Hunt    64k PRG / 16K CHR       H       74161/32 (66)
Super Mario Bros 2              128k PRG / 128k CHR     H       MMC3 (4)
Super Mario Bros 2 (-1 PRG rev.)128k PRG / 128k CHR     H       MMC3 (4)
Super Mario Bros 3              256k PRG / 128k CHR     H       MMC3 (4)
Super Mario Bros 3 (-1 PRG Rev.)256k PRG / 128k CHR     H       MMC3 (4)
Super Mario Bros 3 (J)          256k PRG / 128k CHR     H       MMC3 (4)
Super Mario Bros 3 (PAL)        256k PRG / 128k CHR     H       MMC3 (4)
Super Mario USA                 128k PRG / 128k CHR     V       MMC3 (4)
Super Momotarou Densetsu        256k PRG / 0k CHR       H       MMC1 (1)
Super Monkey Daibouken          32k PRG / 32k CHR       V       CNROM (3)
Super Off Road                  128k PRG / 0k CHR       V       AOROM (7)
Super Pinball                   128k PRG / 0k CHR       H       UNROM (2)
Super Pitfall                   128k PRG / 0k CHR       V       UNROM (2)
Super Pitfall (J)               128k PRG / 0k CHR       V       UNROM (2)
Super Real Baseball             128k PRG / 128k CHR     H       MMC1 (1)
Super Robot Taisen 2            256k PRG / 256k CHR     H       MMC3 (4)
Super Spike V'Ball              128k PRG / 128k CHR     H       MMC3 (4)
Super Spike V'Ball/Nintendo World Cup   128k PRG / 128k CHR     H       MMC3 (4) <--missing about 256k?
Super Sprint                    128k PRG / 128k CHR     V       MMC3 (4)
Super Spy Hunter                128k PRG / 128k CHR     H       MMC3 (4)
Super Star Force                128k PRG / 0k CHR       V       UNROM (2)
Super Team Games                32k PRG / 32k CHR       H       CNROM (3)        
Super Turrican (PAL)            128k PRG / 128k CHR     H       MMC3 (4)
Super Xevious                   128k PRG / 32k CHR      H       MMC3 (4)
Superman                        128k PRG / 128k CHR     V       MMC1 (1)
Superman (J)                    128k PRG / 128k CHR     H       MMC1 (1)
Swamp Thing                     128k PRG / 128k CHR     H       MMC1 (1)
Swat                            128k PRG / 0k CHR       V       UNROM (2)
Sweet Home                      256k PRG / 0k CHR       H       MMC1 (1)
Sword Master                    128k PRG / 128k CHR     H       MMC3 (4)
Sword Master (J)                128k PRG / 128k CHR     H       MMC3 (4)
Swords and Serpents             128k PRG / 0k CHR       V       UNROM (2)
Syoukousi Sedy                  256k PRG / 0k CHR       V       MMC1 (1)


T&C Surf Design                 32k PRG / 32k CHR       V       CNROM (3)
T&C Surf Design 2               128k PRG / 128k CHR     H       MMC3 (4)
Taboo                           32k PRG / 32k CHR       H       MMC1 (1)
Tag Team Pro Wrestling          32k PRG / 8k CHR        H       ---- (0)
Tag Team Wrestling              32k PRG / 8k CHR        H       ---- (0)
Tagin' Dragon                   32k PRG / 16k CHR       V       Colour Dreams (11)
Taito Grand Prix                128k PRG / 128k CHR     H       Taito X005 (80)
Taiyou no Sinden                256k PRG / 0k CHR       V       MMC1 (1)
Takahasi Meijin no Boukenjima   32k PRG / 32k CHR       V       CNROM (3)
Takahasi Meijin no Boukenjima 2	128k PRG / 128k CHR	H	MMC3 (4)
Takahasi Meijin no Boukenjima 3	128k PRG / 128k CHR	H	MMC3 (4)
Takahasi Meijin no Boukenjima 4	256k PRG / 128k CHR	H	MMC3 (4)
Takahasi Meijin no Bug Hunny	128k PRG / 32k CHR	H	Nina-1 (34)  <--probably a mapper hack
Takeda Shingen                  128k PRG / 0k CHR       V       UNROM (2)
Takeda Shingen 2                256k PRG / 0k CHR       V       MMC1 (1)
Takeshi no Chousen Jyou         128k PRG / 0k CHR       V       UNROM (2)
Takeshi no Sengoku Fuunji       128k PRG / 128k CHR     H       MMC3 (4)
Tale Spin                       128k PRG / 128k CHR     H       MMC1 (1)
Tanikawa Kouji no Shogi Sinan 3 128k PRG / 0k CHR       H       MMC1 (1)
Tantei Jingujii Saburou Tokino Sugiyukumamani   128k PRG / 0k CHR       H       UNROM (2)
Tao                             128k PRG / 128k CHR     V       MMC1 (1)
Target Renegade                 128k PRG / 128k CHR     V       MMC1 (1)
Tashiro Musasi no Princess Galppai      128k PRG / 128k CHR     V       MMC1 (1)
Tatakae Ramen Man               128k PRG / 128k CHR     V       MMC1 (1)
Tatakai no Banka                128k PRG / 0k CHR       V       UNROM (2)        
Tecmo Baseball                  128k PRG / 0k CHR       V       MMC1 (1)
Tecmo Bowl                      128k PRG / 128k CHR     V       MMC1 (1)
Tecmo Cup Soccer                128k PRG / 128k CHR     H       MMC1 (1)
Tecmo NBA Basketball            128k PRG / 256k CHR     H       MMC3 (4)
Tecmo Super Bowl                256k PRG / 128k CHR     H       MMC3 (4)
Tecmo World Wrestling           128k PRG / 128k CHR     H       MMC1 (1)
Tecmo World Wrestling (PAL)     128k PRG / 128k CHR     H       MMC1 (1)
Teenage Mutant Hero Turtles     128k PRG / 128k CHR     H       MMC1 (1)
Teenage Mutant Ninja Turtles    128k PRG / 128k CHR     H       MMC1 (1)
Teenage Mutant Ninja Turtles PAL128k PRG / 128k CHR     H       MMC1 (1)
Teenage Mutant Ninja Turtles 2  256k PRG / 256k CHR     H       MMC3 (4)
Teenage Mutant Ninja Turtles 3  256k PRG / 256k CHR     H       MMC3 (4)
Tekken 2                        128k PRG / 512k CHR     V       PCJY?? (90)
Tenchi wo Kurau                 256k PRG / 0k CHR       V       MMC1 (1)
Tenchi wo Kurau 2               512k PRG / 0k CHR       V       MMC3 (4)
Tennis                          16k PRG / 8k CHR        V       ---- (0)
Terminator, The                 128k PRG / 128k CHR     H       MMC3 (4)
Terminator 2                    128k PRG / 128k CHR     H       MMC3 (4)
Terminator 2 (Beta Version)     128k PRG / 128k CHR     H       MMC3 (4)
Terminator 2 (J)                128k PRG / 128k CHR     H       MMC3 (4)      
Terra Cresta (J)                128k PRG / 0k CHR       H       UNROM (2)
Tetrastar                       256k PRG / 256k CHR     V       MMC3 (4)
Tetris                          32k PRG / 128k CHR      V       MMC1 (1)
Tetris (Tengen)                 32k PRG / 16K CHR       V       CNROM (3)
Tetris (J)			32k PRG / 32k CHR	V	CNROM (3)
Tetris 2                        128k PRG / 104k CHR     H       MMC3 (4)  <--missing 24k
Tetris 2 (PAL)                  128k PRG / 128k CHR     H       MMC3 (4)
Tetris 2 + Bombliss             128k PRG / 0k CHR       H       MMC1 (1)
Tetris Flash                    128k PRG / 128k CHR     H       MMC3 (4)
Tetsuwan Atom			128k PRG / 128k CHR	H	MMC3 (4)
Tetudouou			32k PRG / 32k CHR	V	CNROM (3)
Thexder                         32k PRG / 8k CHR        H       ---- (0)
Three Stooges, The              128k PRG / 128k CHR     H       MMC1 (1)
Thunder and Lightning           128k PRG / 128k CHR     V       MMC1 (1)  <--bad dump
Thunderbirds                    128k PRG / 128k CHR     H       MMC1 (1)
Thunderbirds (J)                128k PRG / 128k CHR     V       MMC1 (1)
Thundercade                     128k PRG / 0k CHR       H       UNROM (2)  
Tiger-Heli (J)                  32k PRG / 32k CHR       H       CNROM (3)
Time Lord                       128k PRG / 0k CHR       H       AOROM (7)
Time Lord (PAL)                 128k PRG / 0k CHR       H       AOROM (7)
Time Stranger                   128k PRG / 0k CHR       V       UNROM (2)
Time Zone                       128k PRG / 128k CHR     H       MMC3 (4)
Times of Lore                   128k PRG / 0k CHR       V       UNROM (2)
Tiny Toon Adventures            128k PRG / 128k CHR     H       MMC3 (4)
Tiny Toon Adventures 2          128k PRG / 128k CHR     H       MMC3 (4)
Tiny Toons Cartoon Workshop	128k PRG / 128k CHR	H	MMC3 (4)
Titan                           128k PRG / 0k CHR       V       MMC1 (1)
Titei Senkuu Bazoruda		128k PRG / 128k CHR	V	MMC3 (4)
TM Network Live At Power Bowl   128k PRG / 128k CHR     H       MMC3 (4)
Tobidase Daisakusen 2           128k PRG / 0k CHR       V       UNROM (2)
Toki                            128k PRG / 256k CHR     H       MMC3 (4)
Tokorosan no Mamoru             32k PRG / 32k CHR       V       CNROM (3)
Tokyukyuu Seirei Solbrain       128k PRG / 128k CHR     H       MMC3 (4)
Tom and Jerry                   128k PRG / 128k CHR     H       MMC3 (4)
Tombs and Treasure              256k PRG / 0k CHR       V       MMC1 (1)
Tonjan                          128k PRG / 128k CHR     H       MMC1 (1)        
Toobin'                         128k PRG / 64k CHR      H       MMC3 (4)
Top Gun                         128k PRG / 0k CHR       H       UNROM (2)
Top Gun - The Second Mission    128k PRG / 128k CHR     H       MMC3 (4)
Top Players Tennis              128k PRG / 128k CHR     V       MMC1 (1)
Total Recall                    128k PRG / 0k CHR       V       UNROM (2)
Totally Rad                     128k PRG / 128k CHR     H       MMC3 (4)
Totsuzen Macyoman               128k PRG / 0k CHR       V       UNROM (2)
Touch                           128k PRG / 0k CHR       V       UNROM (2)
Touchdown Fever                 128k PRG / 32k CHR      H       MMC1 (1)
Touchdown Fever (J)             128k PRG / 32k CHR      H       MMC1 (1)
Touhou Kenbunroku               128k PRG / 128k CHR     H       MMC1 (1)
Toukaidou 53 Tugi		32k PRG / 32k CHR	V	CNROM (3)
Tower of Druaga, The            32k PRG / 8k CHR        V       ---- (0)
Toxic Crusaders                 128k PRG / 128k CHR     H       MMC3 (4)
Track and Field 2               128k PRG / 128k CHR     H       MMC1 (1)        
Track and Field 2 (PAL)         128k PRG / 128k CHR     H       MMC1 (1)
Transformers Combo              32k PRG / 32k CHR       V       CNROM (3)
Triathron, The                  128k PRG / 128k CHR     V       MMC1 (1)
Trog                            128k PRG / 0k CHR       V       UNROM (2)
Trojan                          128k PRG / 0k CHR       V       UNROM (2)
Trojan (PAL)                    128k PRG / 0k CHR       V       UNROM (2)
Trolls on Treasure Island       32k PRG / 32k CHR       V       AVE (79)
Tsuppari Oozomou                32k PRG / 32k CHR       V       CNROM (3)
Tsuppari Wars                   128k PRG / 128k CHR     H       MMC1 (1)
Turbo Racing                    128k PRG / 128k CHR     H       MMC1 (1)
Turikiti Sanpei Blue		128k PRG / 0k CHR	V	UNROM (2)
Turupika Higemaru               128k PRG / 128k CHR     H       SS8806 (18)
Twin Cobra                      128k PRG / 128k CHR     H       MMC3 (4)
Twinbee                         32k PRG / 16k CHR       H       CNROM (3)
Twinbee (Disk Conversion)       32k PRG / 0k CHR        H       ---- (0)  (loads using .sav file)
Twinbee 3                       128k PRG / 128k CHR     H       VRC4-1B (22)


Uddy Poko                       128k PRG / 0k CHR       V       UNROM (2)
Ultima - Exodus                 256k PRG / 0k CHR       V       MMC1 (1)        
Ultima - Exodus (J)             256k PRG / 0k CHR       H       MMC1 (1)
Ultima - Quest of the Avatar    256k PRG / 0k CHR       V       MMC1 (1)
Ultima - Quest of the Avatar (J)256k PRG / 0k CHR       H       MMC1 (1)
Ultima - Warriors of Destiny    256k PRG / 0k CHR       H       MMC1 (1)
Ultimate Air Combat             256k PRG / 256k CHR     H       MMC3 (4)
Ultimate Basketball             128k PRG / 128k CHR     H       MMC3 (4)
Ultimate League Soccer          32k PRG / 64k CHR       H       AVE (79)
Ultimate Stuntman               256k PRG / 0k CHR       V       Camerica (71)
Ultraman Club 2                 128k PRG / 128k CHR     H       MMC3 (4)
Ultraman Club 3                 256k PRG / 128k CHR     H       MMC3 (4)
Uncharted Waters                512k PRG / 128k CHR     H       MMC5 (5)
Uninvited                       128k PRG / 128k CHR     H       MMC3 (4)
Untouchables, The               128k PRG / 128k CHR     H       MMC1 (1)        
Urban Champion                  16k PRG / 8k CHR        V       ---- (0)
Urusei Yatsura                  32k PRG / 32k CHR       V       CNROM (3)
US Championship V'Ball		128k PRG / 128k CHR	V	MMC3 (4)
Uturun Desu                     128k PRG / 128k CHR     V       MMC3 (4)


Valis                           128k PRG / 0k CHR       V       UNROM (2)
Valkyure no Bouken              32k PRG / 32k CHR       H       MMC3 (4)
Vegas Connection                256k PRG / 0k CHR       V       MMC1 (1)
Vegas Dream                     128k PRG / 128k CHR     H       MMC1 (1)
Venice Beach Volleyball         32k PRG / 32k CHR       V       AVE (79)
Venus Wars, The                 128k PRG / 128k CHR     V       MMC1 (1)
Vice - Project Doom             128k PRG / 128k CHR     H       MMC3 (4)
Villgust Gaiden                 128k PRG / 256k CHR     V       MMC3 (4)
Vindicators                     64k PRG / 32k CHR       V       MMC3 (4)
Viva Las Vegas                  128k PRG / 128k CHR     V       MMC1 (1)
Volguard 2                      32k PRG / 8k CHR        V       ---- (0)
Volleyball                      32k PRG / 16k CHR       V       CNROM (3)
Volleyball (Disk Conversion)    32k PRG / 8k CHR        H       ---- (0)  (loads with .sav)
Vs. Castlevania                 128k PRG / 0k CHR       V       Unisystem (99)
Vs. Ice Climber                 32k PRG / 16k CHR       V       Unisystem (99)
Vs. Super Mario Bros            32k PRG / 16k CHR       V       Unisystem (99)


Wacky Races                     128k PRG / 128k CHR     H       MMC3 (4)
Wagyan Land                     128k PRG / 64k CHR      V       MMC3 (4)
Wagyan Land 2                   256k PRG / 128k CHR     H       Namcot 106 (19)
Wagyan Land 3                   256k PRG / 256k CHR     H       Namcot 106 (19)
Wai Wai World                   128k PRG / 128k CHR     H       VRC2b (23)
Wai Wai World (Hacked)          128k PRG / 128k CHR     H       FFE F8xxx (17)
Wai Wai World 2                 256k PRG / 128k CHR     H       VRC4-2a (21)
Wairi no Write Rockboard        256k PRG / 0k CHR       H       MMC3 (4)
Wall Street Kid                 128k PRG / 0k CHR       H       UNROM (2)
Wanpaku Duck Yumebouken         128k PRG / 0k CHR       V       UNROM (2)
Wanpaku Kokkun no Gurume World  128k PRG / 128k CHR     H       MMC3 (4)
Wario no Mori                   256k PRG / 256k CHR     V       MMC3 (4)
Wario's Woods                   256k PRG / 256k CHR     H       MMC3 (4)
Warpman                         16k PRG / 8k CHR        H       ---- (0)
Warwolf                         128k PRG / 128k CHR     V       MMC3 (4)
Wayne Gretzky Hockey            128k PRG / 0k CHR       V       UNROM (2)
Wayne's World                   128k PRG / 128k CHR     H       MMC3 (4)
WCW Championship Wrestling      128k PRG / 128k CHR     H       MMC3 (4)
Werewolf                        128k PRG / 128k CHR     H       MMC3 (4)
Western Kids                    256k PRG / 256k CHR     V       MMC3 (4)
Wheel of Fortune                128k PRG / 0k CHR       H       AOROM (7)
Wheel of Fortune Family Edition 128k PRG / 0k CHR       H       AOROM (7)
Wheel of Fortune W/ Vanna White 128k PRG / 0k CHR       H       AOROM (7)
Where in Time is Carmen Sandiego128k PRG / 256k CHR     H       MMC3 (4)
Where's Waldo?                  128k PRG / 128k CHR     H       MMC3 (4)
White Lion Densetsu             128k PRG / 128k CHR     V       MMC1 (1)
Who Framed Roger Rabbit?        128k PRG / 0k CHR       H       AOROM (7)
Whomp 'Em                       128k PRG / 128k CHR     H       MMC3 (4)
Widget                          128k PRG / 128k CHR     H       MMC3 (4)
Wild Gunman                     16k PRG / 8k CHR        V       ---- (0)
Willow                          128k PRG / 128k CHR     H       MMC1 (1)
Willow (J)                      128k PRG / 128k CHR     H       MMC1 (1)
Win, Lose, or Draw              128k PRG / 0k CHR       H       MMC1 (1)
Wing of Madoola, The            32k PRG / 128k CHR      V       CNROM (3)
Winter Games                    128k PRG / 0k CHR       H       UNROM (2)
Wits                            128k PRG / 0k CHR       V       UNROM (2)
Wizardry - Knight of Diamonds   128k PRG / 128k CHR     H       MMC3 (4)
Wizardry - Knight of Diamonds(J)128k PRG / 128k CHR     V       MMC3 (4)
Wizardry - Legacy of Lyllgamyn  128k PRG / 128k CHR     H       MMC3 (4)
Wizardry - Proving Grounds      128k PRG / 128k CHR     V       MMC1 (1)
Wizardry - Proving Grounds (J)  128k PRG / 128k CHR     V       MMC1 (1)
Wizards and Warriors            128k PRG / 0k CHR       H       AOROM (7)
Wizards and Warriors (PAL)      128k PRG / 0k CHR       H       AOROM (7)
Wizards and Warriors 2          256k PRG / 0k CHR       H       AOROM (7)
Wizards and Warriors 3          256k PRG / 0k CHR       H       AOROM (7)
Wolverine                       128k PRG / 128k CHR     H       MMC3 (4)
World Boxing                    128k PRG / 128k CHR     H       MMC1 (1)
World Games                     128k PRG / 0k CHR       H       AOROM (7)
World Super Tennis              128k PRG / 128k CHR     V       MMC1 (1)
Wrath of the Black Manta        128k PRG / 128k CHR     V       MMC1 (1)
Wrecking Crew                   32k PRG / 8k CHR        H       ---- (0)
Wurm                            128k PRG / 128k CHR     H       MMC3 (4)
WWF King of the Ring            128k PRG / 256k CHR     H       MMC3 (4)
WWF Steel Cage Challenge        128k PRG / 256k CHR     H       MMC3 (4)
WWF Steel Cage Challenge (PAL)  128k PRG / 256k CHR     H       MMC3 (4)
WWF Wrestlemania                128k PRG / 0k CHR       H       AOROM (7)
WWF Wrestlemania Challenge      256k PRG / 0k CHR       H       AOROM (7)


X-Men                           128k PRG / 0k CHR       H       UNROM (2)
Xenophobe                       128k PRG / 128k CHR     H       MMC1 (1)
Xevious (J)                     32k PRG / 8k CHR        H       ---- (0)
Xexyz                           128k PRG / 128k CHR     H       MMC1 (1)


Y's                             256k PRG / 0k CHR       H       MMC1 (1)
Y's 2                           256k PRG / 128k CHR     V       MMC3 (4)
Y's 3                           256k PRG / 128k CHR     H       MMC3 (4)
Yie-Ar Kung-Fu                  16k PRG / 8k CHR        V       ---- (0)
Yo Nin Uti Majyan               16k PRG / 8k CHR        V       ---- (0)
Yo Noid                         128k PRG / 128k CHR     H       MMC1 (1)
Yokohama Renzoku Satsujin Jiken 128k PRG / 128k CHR     V       MMC1 (1)
Yoshi                           128k PRG / 128k CHR     H       MMC1 (1)
Yoshi no Cookie                 128k PRG / 64k CHR      V       MMC3 (4)
Yoshi no Tamago                 128k PRG / 32k CHR      H       MMC1 (1)
Yoshi's Cookie                  128k PRG / 64k CHR      H       MMC3 (4)
Youkai Club                     128k PRG / 32k CHR      V       74161/32 (66)
Youkai Douchuki                 128k PRG / 128k CHR     V       Namcot 106 (19)
Young Indiana Jones Chronicles  128k PRG / 128k CHR     H       MMC3 (4)
Yousike Ide's Battle Mahjongg   128k PRG / 0k CHR       V       UNROM (2)
Yousike Ide's Battle Mahjongg 2 256k PRG / 0k CHR       H       MMC1 (1)
Yume Penguin Monogatari (Hacked)128k PRG / 128k CHR     V       MMC3 (4)


Zaiteku Satsujin Jiken          128k PRG / 128k CHR     H       MMC3 (4)
Zanac                           128k PRG / 0k CHR       H       UNROM (2)
Zelda no Densetsu               128k PRG / 0k CHR       V       MMC1 (1)
Zelda 2 - The Adventure of Link 128k PRG / 112k CHR     H       MMC1 (1)  <--missing 16k
Zelda 2 (PAL)                   128k PRG / 128k CHR     H       MMC1 (1)
Zen - Intergalactic Ninja       128k PRG / 128k CHR     H       MMC3 (4)
Zenbei Pro Basket               128k PRG / 128k CHR     V       MMC1 (1)
Zippy Race                      16k PRG / 8k CHR        V       ---- (0)
Zoids                           128k PRG / 0k CHR       V       UNROM (2)
Zoids 2                         256k PRG / 0k CHR       V       MMC1 (1)
Zoids 3                         128k PRG / 128k CHR     H       MMC3 (4)
Zombie Hunter                   128k PRG / 32k CHR      V       MMC1 (1)
Zombie Nation                   128k PRG / 128k CHR     H       MMC3 (4)


--MISC STUFF--

These are just a few games that I have lying around that don't work because
they're either currently unsupported or just a regular bad dump.  This section
is totally useless.


76-in-1                         2048k PRG / 0k CHR      H       -none assigned-
110-in-1                        2048k PRG / 1024k CHR   H       -none assigned-
Cheetahmen 2                    32k PRG / 8k CHR        V       -none assigned- (probably a bad dump, too)
Deathbots                       64k PRG / 64k CHR       H       AVE (79)
Galactic Crusader               32k PRG / 32k CHR       H       Colour Dreams (11)
Saint Seiya                     64k PRG / 128k CHR      H       74161/32 (70)
Skull and Crossbones            128k PRG / 64k CHR      H       Rambo-1 (64)
Super Jeopardy                  128k PRG / 256k CHR     H       MMC1 (1)
Tiles of Fate                   32k PRG / 32k CHR       H       AVE (79)
".Replace("  ", " ").Replace(":", " ")
                        .Split(new []{' ', '-', '\n'});

                var input_list_02 = "A B C D E F B09MAD1898 B09MAD190 B09MAD1906 B09MAD1912 B09MAD1938 B09MAD1950 B09MAD197 B17PIC229 B19PIC226 B222PIC202 B227PIC198 B2MPIC1127 B41PIC227 B42KIL2311 B42PIC249 B44PIC212 B46PIC175 B60KIL187 B60KIL1934 B60MIR1495 B60S1KIL186 B60S1KIL200 B60S1PIC256 B60S2KIL1934 B60S2KIL205 B60S2KIL208 B60S2PIC227 B60SWI200 B61GRE180 B61GRE194 B61PIC228 B64KIL2248 B65PIC197 B67PIC184 BBCAUC202 BBCPIC240 BLMPIC164 BMOAMG194 BMPIC226 BNCKIL1698 BNCPIC184 BNHPIC186 BNVKIL203 BOACHILLESTONDONS BOBCMIRA917 BOBEEFACHILL118 BOBEEFPADDYWACK BOBGREEN20 BOBLADE2306 BOBLADE2308 BOCBBIG213 BOCBBIG216 BOCBBIG217 BOCBBIG218 BOCBBIG221 BOCBBIG222 BOCBBIG22275 BOCBBIG223 BOCBBIG226 BOCBBIG227 BOCBBIG228 BOCBBIG229 BOCBBIG230 BOCBBIG2317 BOCBBIG235 BOCBBIG238 BOCBBIG25.22 BOCBBIG25.45 BOCBBIG25.7 BOCBBIG25.77 BOCBBIG2507 BOCBBIG253 BOCBBIG2551 BOCBBIG2587 BOCBBIG2589 BOCBBIG2606 BOCBBIG2617 BOCBBIG2620 BOCBBIG2626 BOCBBIG2647 BOCBBIG2658 BOCBCRAL175 BOCBCRAL1985 BOCBCRAL200 BOCBCRAL202 BOCBCRAL206 BOCBCRAL2071 BOCBGBP185 BOCBGBP186 BOCBGBP190 BOCBGBP191 BOCBHAR193 BOCBHAR202 BOCBHAR204 BOCBHIL1922 BOCBMIRA20 BOCDEVE22.7 BOCDEVEILLE BOCHUCK2242 BOCHUCK2253 BOCMED20 BOCMON20 BOCSCRA181 BOCSCRA190 BOCSCRA191 BOCSCRA1921 BOCSCRA1964 BOCSCRA1969 BOCSCRA1985 BOCSPUI222 BOCSPUI236 BOCSPUI2493 BOCSPUI252 BOFLAN2035 BOFLAN3135 BOFLANKMEMBRAN BOGOI BOMIXEDTENDERS BOMIXEDTENDONS BOPADDYWACKS BOPICO1750 BOSHAN2038 BOSHAN2064 BOSHAN2075 BOSHAN2082 BOSHAN2154 BOSHAN218 BOSHAN2322 BOSHAN239 BOSHAN2574 BOSHAN6133 BOSHIN2073 BOSHIN2106 BOSHIN2120 BOSHIN2129 BOSHIN2156 BOSHIN218 BOSHIN2207 BOSHIN2223 BOSHIN2351 BOSHIN2452 BOSHIN6159 BOSHORT2066 BOSUON1529 BOSUON1623 BOSUON1635 BOSUON164 BOSUON17.59 BOSUON1731 BOSUON1759 BOSUON1785 BOSUON1803 BOSUON19.9 BOSUON1957 BOSUON198 BOSUON1981 BOSUON2719 BOSUONGBP1969 BOSUONGBP197 BOSUONGBP198 BOSUONGBP199 BOSUONGBP2009 BOSUONGBP201 BOSUONGBP203 BOSUONGBP204 BOSUONGBP205 BOSUONHAT14 BOSUONTARA1796 BOSUONTARA1813 BOXCGREEN1617 BOXDTARA193 BOXONG1215 BOXONG1448 BOXONG1462 BOXONG1464 BOXONG1479 BOXONG152 BOXONG1551 BOXONG1738 BOXONG1775 BOXONG1927 BOXONGFEM2629 BOXONGFEM263 BOXONGFEM2640 BOXONGFEM2641 BOXONGFEM2644 BOXONGFEM2650 BOXONGFEM2651 BOXONGFEM2652 BOXONGFEM2656 BOXONGFEM267 BOXONGFEM2687 BOXONGHIL2106 BOXONGHUM2626 BOXONGHUM2645 BOXONGHUM2653 BOXONGHUM266 BOXONGHUM2663 BOXONGHUM2668 BOXONGHUM267 BOXONGHUM2678 BOXONGHUM2692 BOXONGKILFEM135 BOXONGKILFEM140 BOXONGKILFEM143 BOXONGKILFEM1447 BOXONGKILFEM1465 BOXONGKILFEM148 BOXONGKILFEM1490 BOXONGKILFEM1494 BOXONGKILFEM150 BOXONGKILFEM1503 BOXONGKILFEM151 BOXONGKILFEM152 BOXONGKILFEM153 BOXONGKILFEM154 BOXONGKILFEM155 BOXONGKILHUM141 BOXONGKILHUM143 BOXONGKILHUM144 BOXONGKILHUM145 BOXONGKILHUM1459 BOXONGKILHUM1467 BOXONGKILHUM147 BOXONGKILHUM149 BOXONGKILHUM151 BOXONGKILHUM152 BOXONGKILHUM1524 BOXONGKILHUM153 BOXONGKILHUM155 BOXONGKILHUM156 BOXONGKILHUM157 BOXONGKILHUM158 BOXONGKILHUM159 BOXONGKILHUM160 BOXONGKILHUM161 BOXONGKILHUM162 BOXONGKILHUM164 BOXONGKILHUM165 BOXONGKILHUM167 BOXONGKILHUM168 BOXONGKILRAD148 BOXONGKILRAD151 BOXONGKILRAD152 BOXONGKILRAD1525 BOXONGKILRAD155 BOXONGKILRAD1553 BOXONGKILRAD157 BOXONGKILRAD159 BOXONGKILRAD160 BOXONGKILRAD161 BOXONGKILRAD1614 BOXONGKILRAD162 BOXONGKILRAD163 BOXONGKILRAD164 BOXONGKILRAD165 BOXONGKILRAD166 BOXONGKILRAD168 BOXONGKILRAD1697 BOXONGKILRAD170 BOXONGKILRAD172 BOXONGKILRAD175 BOXONGKILRAD178 BOXONGKILRAD179 BOXONGKILRAD180 BOXONGKILTIB147 BOXONGKILTIB1519 BOXONGKILTIB152 BOXONGKILTIB1536 BOXONGKILTIB154 BOXONGKILTIB159 BOXONGKILTIB1608 BOXONGKILTIB161 BOXONGKILTIB162 BOXONGKILTIB163 BOXONGKILTIB164 BOXONGKILTIB165 BOXONGKILTIB166 BOXONGKILTIB167 BOXONGKILTIB168 BOXONGKILTIB1684 BOXONGKILTIB170 BOXONGKILTIB172 BOXONGKILTIB173 BOXONGKILTIB176 BOXONGMIRA2644 BOXONGMIRA2660 BOXONGMIRA2664 BOXONGMIRA2675 BOXONGMIRA2677 BOXONGMIRA268 BOXONGMIRA2684 BOXONGMIRA270 BOXONGMIRA2706 BOXONGMIRA2725 BOXONGMIRA2742 BOXONGRAD2630 BOXONGRAD2632 BOXONGRAD2635 BOXONGRAD264 BOXONGRAD2648 BOXONGRAD266 BOXONGRAD2669 BOXONGRAD2670 BOXONGRAD2680 BOXONGRAD2684 BOXONGRAD2704 BOXONGRAD2710 BOXONGRAD278 BOXONGSHA159 BOXONGSHA1637 BOXONGSHA422 BOXONGSHA423 BOXONGSHA430 BOXONGSHA431 BOXONGSHAFEM135 BOXONGSHAFEM137 BOXONGSHAFEM139 BOXONGSHAFEM142 BOXONGSHAFEM147 BOXONGSHAFEM152 BOXONGSHAFEM153 BOXONGSHAFEM156 BOXONGSHAHUM147 BOXONGSHAHUM148 BOXONGSHAHUM154 BOXONGSHAHUM156 BOXONGSHAHUM157 BOXONGSHAHUM158 BOXONGSHAHUM159 BOXONGSHARAD143 BOXONGSHARAD147 BOXONGSHARAD153 BOXONGSHARAD154 BOXONGSHARAD155 BOXONGSHARAD157 BOXONGSHARAD16.1 BOXONGSHARAD163 BOXONGSHATIB139 BOXONGSHATIB146 BOXONGSHATIB151 BOXONGSHATIB152 BOXONGSHATIB158 BOXONGSHATIB162 BOXONGSHATIB17 BOXVTARA171 BPILPIC177 BRIBPIC147 BSBDMNOL139 BSBDMNOL145 BSBDMNOL146 BSBDMNOL151 BSBNOL171 BSUONARC182 BSUONARC189 BSUONCAG1826 BSUONENC206 BSUONGREEN1454 BSUONGREEN1505 BSUONGREEN1510 BSUONGREEN1513 BSUONGREEN152 BSUONGREEN1538 BSUONGREEN1576 BSUONGREEN161 BSUONGREEN165 BSUONGREEN166 BSUONGREEN168 BSUONGREEN169 BSUONGREEN170 BSUONGREEN173 BSUONGREEN174 BSUONGREEN175 BSUONGREEN181 BSUONHAR1695 BSUONHAR174 BSUONHAR181 BSUONHAR189 BSUONHAR192 BSUONHAR193 BSUONHARD1714 BSUONHEL1534 BSUONHIL171 BSUONJAC1738 BSUONKIL1813 BSUONLOC156 BSUONLOC160 BSUONLOC161 BSUONMED182 BSUONMED192 BSUONMED195 BSUONMID1279 BSUONMID1286 BSUONMID130 BSUONMID1329 BSUONMID1331 BSUONMIRA1479 BSUONNOL157 BSUONOAK115 BSUONOAK121 BSUONPAC174 BSUONPIC214 BSUONSHA156 BSUONSHA1617 BSUONSHA1645 BSUONSHA165 BSUONSHA166 BSUONSHA168 BSUONSHA1770 BSUONSKA155N BSUONSTAN1462 BSUONSWI120 BSUONTARA172 BSUONTARA174 BSUONTARA175 BSUONTARA176 BSUONTARA178 BSUONTARA181 BSUONTARA184 BSUONTARA1884 BSUONTARA19 BSUONTARA192 BSUONTARA1934 BSUONTEY160 BSUONTEY167 BSUONTEY168 BSUONTEY169 BSUONTEY1713 BSUONTEY1719 BSUONTEY172 BSUONWOO1606 BXCAUC196 BXCHIL149 BXCKIL182 BXCKIL183 BXCLHAR206 BXCMED188 BXCMED194 BXCMON188 BXCPIC199 BXGMED202 BXONGAMG112 BXONGAMG184 BXONGAMG185 BXONGAMG186 BXONGAMG189 BXONGAMG190 BXONGAMG194 BXONGAMG204 BXONGCAG149 BXONGCAG152 BXONGCAG157 BXONGCAG158 BXONGCAG162 BXONGCAG1639 BXONGCAG1648 BXONGCAG1669 BXONGCAG1697 BXONGCAG174 BXONGCOL106 BXONGGREEN140 BXONGGREEN1450 BXONGGREEN1464 BXONGGREEN164 BXONGGREEN1652 BXONGGREEN1660 BXONGGREEN167 BXONGGREEN168 BXONGGREEN169 BXONGGREEN171 BXONGGREEN172 BXONGGREEN19.91 BXONGHAR157 BXONGHAR158 BXONGHAR169 BXONGHAR173 BXONGHAR179 BXONGHIL128 BXONGHIL131 BXONGKIM1526 BXONGKIM1561 BXONGKIM160 BXONGKIM167 BXONGKIM169 BXONGKIM170 BXONGKIM171 BXONGKIM177 BXONGOAK102 BXONGOAK109 BXONGOAK110 BXONGOAK112 BXONGOAK117 BXONGPIC166 BXONGSHA1477 BXONGSHA1548 BXONGSHA1584 BXONGSHA1596 BXONGSWI102S BXONGSWI104T BXONGSWI105S BXONGSWI105T BXONGSWI106T BXONGSWI107T BXONGSWI108T BXONGSWI109T BXONGSWI110S BXONGSWI110T BXONGSWI111S BXONGSWI112S BXONGSWI112T BXONGSWI113S BXONGSWI114S BXONGSWI115S BXONGSWI115T BXONGSWI116S BXONGSWI116T BXONGSWI117S BXONGSWI117T BXONGSWI118S BXONGSWI118T BXONGSWI119S BXONGSWI120S BXONGSWI121S BXONGSWI121T BXONGSWI122T BXONGSWI123T BXONGSWI124S BXONGSWI124T BXONGSWI126T BXONGSWI128T BXONGSWI130T BXONGSWI131T BXONGSWI132T BXONGSWI96S BXONGSWI97S BXONGSWI98S BXONGTARA172 BXONGTARA1773 BXONGTARA179 BXONGTARA1795 BXONGTARA18 BXONGTARA181 BXONGTARA1815 BXONGTARA182 BXONGTARA183 BXONGTARA184 BXONGTHR131 BXONGTHR179 BXONGTHR181 BXQNOL119 BXQNOL125 BXQNOL137 BXQNOL139 BXQNOL151 CDCHMORAY CDCHVICI300 CDCHVICI400 CSABAPRE DCMSUBARG16 DNC1279 DNCSUBA1339 DNCSUBA1440 DNCSUBA9.93 DNCSUBARG DNCUTD149 DNCUTD153 GBAR10 GBAR11 GBAR12 GBAR13 GBAR14 GBAR15 GBAR16 GBAR9 GBARG115 GBARG1238 GBHQ15 GBHQ1576 GBHYM10 GBHYM12 GBHYM12+ GBHYS12 GBHYS1368 GBMAPLE10 GBSGL10 GBSGL1438 GBSGL145 GBSGL1462 GBSGL1464 GBSGL147 GBSGL1479 GBSGL148 GBSGLM12 GBSGLM1567 GBSGLM1573 GBSGLM1594 GBSGLM1608 GBSGLM162 GBSGLM163 GBSGLM164 GBSGLM167 GBSGLM1687 GBSGLON GBSGM12 GBSGM146 GBSGM1479 GBSGS12 GBSGS1342 GBSGS1352 GBSGS136 GBSGXL10 GBSGXL15 GBSGXL1608 GBSGXL1615 GBSGXL163 GBSGXL1631 GBSGXL1634 GBSGXL166 GBSGXL1673 GBSGXL1713 GBTRIR1716 GBY10 GCAADORO15 GCAALIS15 GCAANIMEX GCAARA15 GCAARGO15 GCAARI10 GCABONAR15 GCABONBR15 GCACARRE15 GCACEKXL10 GCACEUBR15 GCACEXL10 GCACKRES GCADROB GCADROKXL10 GCADROXL10 GCADUCPH10 GCAFABA15 GCAFADEL15 GCAFEPAS10 GCAFRI15 GCAFRINA15 GCAFRINA16 GCAKGWIP10 GCALAN15 GCALASXL15 GCALCCEKXL10 GCALITHUA10 GCALOIMOU1814 GCALU GCAMIE10 GCAMIE10KC GCAMIRA GCAMOU1814 GCANACHBI10 GCANACHDRO10 GCANACHLAR15 GCANACHMIR10 GCANACHSEA12 GCANACHSEA15 GCANACHWIP GCANACHZAK10 GCANICO15 GCANIKXL10 GCANUT150 GCAPERDI15 GCAPLU10 GCAPLUKON10 GCAPRI90 GCAREKXL10 GCARESXL10 GCAREYXL10 GCARICO15 GCARUI8 GCASADIA15 GCASAKXL10 GCASAN15 GCASAPXL10 GCASEARA15 GCATIP10 GCAUNION15 GCAURORA15 GCAVAN10 GCAVDB10 GCAWIKXL10 GCAWIKXL16 GCAWIPXL10 GCAWIPXL16 GCAXLAVI15 GCAYUJIN GCAZAKKXL10 GCCADORO15 GCCAMI15 GCCCARRE12 GCCCARRE15 GCCSEA15 GCCTY1814 GCFRIATO150 GCG1900 GCGA15 GCGAB15 GCGADO45 GCGADRO15 GCGARA15 GCGARI10 GCGAURORA12 GCGCARE15 GCGCARR15 GCGCED15 GCGDROB15 GCGDROBEX GCGDROSED GCGFRA15 GCGFRIA50 GCGIMEX GCGJAG15 GCGLANGU16 GCGMASTER GCGMOUNT15 GCGMY1814 GCGNGA12 GCGNUT15 GCGSAN15 GCGSEAP83 GCGSING7 GCGSING8 GCGSRA15 GCGSRA15P GCGSUP15 GCGTUR25 GCGTUR35 GCGTYS15 GCGTYS20 GCGWIPXL10 GCGYUJIN GCLANGUIRU15 GDGADORO15 GDGAMICK150 GDGARI15 GDGARRO10T GDGAVICOLA10 GDGBAI10 GDGCA145 GDGCAG015 GDGCAG14 GDGCASE GDGCF1361 GDGCF15 GDGCLAX1814 GDGCRID10 GDGDEL15 GDGDELN20 GDGFOF150 GDGGEO15 GDGGOCTU GDGHAR15 GDGHAR1814 GDGKEY150 GDGKEY150T GDGKEY15D GDGKEYN15 GDGKLPLU10 GDGKOC10 GDGKOC15 GDGLAR10 GDGLOBEX15 GDGMJAC15 GDGMOU15 GDGMOUN667 GDGMOUND15L GDGNAMICK1814 GDGOK1814 GDGOLMEL18 GDGOZA150 GDGPAL10 GDGPDUE_18 GDGPDUE10 GDGPDUE1814 GDGPDUE20 GDGPEC150 GDGPER10 GDGPER15 GDGPER20 GDGPERL15 GDGPERTR15 GDGPERTRN15 GDGPERXL15 GDGPERXN15 GDGPIL15 GDGPIL1814 GDGPILN15 GDGPILT15 GDGPILTT15 GDGPILV15 GDGRKH15 GDGROC15 GDGSAD10 GDGSAD1814 GDGSAN15 GDGSEARA10 GDGSEARA15 GDGSEN15 GDGSIM10 GDGSIM1150 GDGSIM150 GDGSIMN15 GDGSING GDGSINGN GDGTIPTOP10 GDGTIPTOP10T GDGTIPTOP15 GDGTYS_15 GDGTYS15 GDGTYS1814 GDGTYSON GDGVTYS15 GDGWAY10 GDGWAY15 GDGYUJIN10 GDNGCON GDTALL15 GDTALLEN1814 GDTAMICK150 GDTAMICKD15 GDTAMICKLON15 GDTANIMEX10 GDTCLAX1814 GDTDRO10 GDTFEI GDTFIAMI1814 GDTFOS1814 GDTFOS190 GDTFOS2145 GDTFOS221 GDTFOS223 GDTFOS2242 GDTFOS2245 GDTFOS2270 GDTFOS2297 GDTFOS2322 GDTFOS2324 GDTFOS236 GDTFOS239 GDTFOS241 GDTFOS242 GDTFOS243 GDTFOS252 GDTFOSTER15 GDTFRI10 GDTFRI15 GDTHALAN10 GDTHALANCL10 GDTHALANKL10 GDTHALANKL10-1500 GDTHALANKL10-1501 GDTHOU1814 GDTHOUSE15 GDTKEY150 GDTKEY15D GDTKEYN15 GDTKOCH15 GDTKXCAS136 GDTMOU15 GDTMOUN15 GDTMOUP3 GDTNO10 GDTOZA1814 GDTPDUE15 GDTPECO15 GDTPERDU18 GDTPERDUE15 GDTPERDUE1814 GDTPERDUE20 GDTPIL15 GDTPIL1814 GDTPILN1814 GDTPLUKON GDTPLUKON10 GDTSAN1814 GDTSANDER GDTSANDER15 GDTSEA12 GDTSTI6 GDTSTO10 GDTSUN237 GDTSUNLAND GDTTYS15 GDTTYS1814 GDTWAFR15 GDTWAY15 GMDCASE1814 GMDCLAX1814 GMDGALLEN1814 GMDKOCK15 GMDM0U15 GMDM0U1814 GMDMJAC15 GMDPD15 GMDPHAP10 GMDPIL20 GMDPIL20H GMDSIM15 GMDTYSON GMDTYSON1814 GMEGA1814 GMGADORO15 GMGAGRO12 GMGAGROA GMGALL16 GMGALLE1814 GMGARA15 GMGARRO GMGARRO15 GMGBEL10 GMGBEL18 GMGCVA18 GMGGEO1814 GMGJAG16 GMGLAN15 GMGMAR150 GMGMAR1814 GMGMOUD1814 GMGMOUN12 GMGMOUNP3 GMGMOUX1814 GMGNIC15 GMGPILGRIM GMGSANDER GMGSEA12 GMGTURI20 GNACHPLUKON GNCARI116 GNCARI117 GNCARI120 GNCARI121 GNCARI122 GNCARI136 GNCARI137 GNCARI138 GNCARI140 GNCARI142 GNCARIL138 GNCARIL140 GNCARIS120 GNCHAN115 GNCHAN138 GNCHAN1397 GNCHAN1399 GNCHAN1417 GNCHAN1419 GNCHAN145 GNCHAN1458 GNCHAN1510 GNCHAN152 GNCHAN153 GNCHAN154 GNCHAN1560 GNCHAN1575 GNCHAN16 GNCHAN163 GNCHAN187 GNCHANL152 GNCHANL153 GNCHANL154 GNCHANL155 GNCHANL156 GNCHANL158 GNCHANL159 GNCHANM152 GNCHANM154 GNCHANM158 GNCHANM159 GNCHANM161 GNCHANM162 GNCHANM163 GNCHANM164 GNCHANS137 GNCHANS138 GNCHANS139 GNCHANS140 GNCHANS142 GNCHANS143 GNCHANS144 GNCHANS146 GNCHANS148 GNCHANS151 GNCHANS157B GNCHANS163 GNCHANS165B GNCHANS166 GNCHANS169 GNCHANS169B GNCHANS170B GNCHANS171B GNCHANS172B GNCHANS179B GNCHANS181B GNCHANS184B GNCSAN116 GNCSAN13 GNCSAN131 GNCSANL12.8D GNCSANL128X GNCSANL129 GNCSANL129X GNCSANL131 GNCSANL134D GNCSANL134X GNCSANS103 GNCSANS103X GNCSANS107 GNCSANS107D GNCSANS107X GNCSANS112 GNCSANS116 GNCSANS117 GNCSANS120V GNCSHIN1294 GNCSHIN1316 GNCSHIN1320 GNCSHIN1328 GNCSHIN137 GNCSHIN1375 GNCSHIN1388 GNCSHIN139 GNCYUJIN1345 GNCYUJIN1366 GNCYUJIN1382 GNCYUJIN1383 GNCYUJIN1397 GNCYUJIN1417 GNCYUJIN1425 GNCYUJIN1435 GNCYUJIN1445 GNCYUJIN1469 GNCYUJIN1524 GNCYUJIN1637 GNCYUJIN1657 GNCYUJIN1674 GNCYUJIN1680 GNCYUJIN1682 GNCYUJIN1702 GNCYUJIN1776 GNCYUJINL134 GNCYUJINL135 GNCYUJINL136 GNCYUJINL138 GNCYUJINL140 GNCYUJINL141 GNCYUJINS138 GNCYUJINS139 GNCYUJINS140 GNCYUJINS142 GNCYUJINS143 GNCYUJINS144 GNCYUJINS145 GPCHALAN10 GPCSEA15 GPHILE1176 GPHILE15 GSGSEA12 GSUN GTGMOU1814 GTGSAD1814 GTGVXAY1814 GTGVXAY2267 GTGXAY15 GTGXAY20 GTNGX2458 GUCGA15 GUGFIBEL15 GUGFISUP12 GUGFOSTER1814 GUGKOYU15 GXGCEDROB10 GXGMOU1361 GXGMOU15 GXLG1814 GXSWAY20 H2MPRI267 H2MPRI287 H6MDDRIV255 H6MDRIV255 H6MGRIV264 H6MVRIV241 HBACHI HBACHIFOOD HBC1563 HBC2126 HBCAGRO1958 HBCAGRO220 HBCAPK194 HBCAPK196 HBCBELLY1277 HBCBELLY2598 HBCBRA2417 HBCCOR113 HBCCVAN20 HBCDSUONBLO228 HBCDSUONBLO231 HBCDSUONGOB20 HBCDSUONSEA HBCDSUONSEA162 HBCDSUONSEAB2269 HBCDSUONSEAB2277 HBCDSUONTYS3304 HBCDSUONVIS233 HBCFAR2276 HBCFOOD208 HBCFOOD2131 HBCGOB20 HBCHI189 HBCKDHAT2321 HBCMAR160 HBCMAR167 HBCMAR171 HBCMAR174 HBCMAR182 HBCMAR202 HBCMAR241 HBCMIR2229 HBCMIR252 HBCMIR262 HBCMIR265 HBCMIR266 HBCMIR267 HBCMIR270 HBCMIR271 HBCMIR272 HBCMIR273 HBCMIR274 HBCMIR275 HBCMIR276 HBCMIR277 HBCMIR278 HBCMIR279 HBCMIR28 HBCMIR281 HBCMIR282 HBCMIR283 HBCMIR284 HBCMIR285 HBCMIR286 HBCMIR287 HBCMIR288 HBCMIR289 HBCMIR290 HBCMIR291 HBCMIR292 HBCMIR293 HBCMIR294 HBCMIR296 HBCMIR297 HBCMIR299 HBCMIR30 HBCMIR302 HBCMIR306 HBCMIR310 HBCMPK2055 HBCMPK211 HBCOJS197 HBCOJS252 HBCOJS254 HBCOJS259 HBCOJS2592 HBCOJS264 HBCPINI1846 HBCPINI1861 HBCPINI1869 HBCROS21 HBCRSSEA16 HBCRSUON HBCRSUONAGR195 HBCRSUONAGR197 HBCRSUONAPK171 HBCRSUONAPK173 HBCRSUONAPK174 HBCRSUONAPK175 HBCRSUONAPK176 HBCRSUONAPK179 HBCRSUONAPK180 HBCRSUONAPK183 HBCRSUONBLO231 HBCRSUONFOO210 HBCRSUONMAR188 HBCRSUONMIR207 HBCRSUONMIR213 HBCRSUONMIR215 HBCRSUONMIR217 HBCRSUONMIR218 HBCRSUONMIR220 HBCRSUONMIR222 HBCRSUONMIR223 HBCRSUONMIR225 HBCRSUONMIR226 HBCRSUONMIR227 HBCRSUONMIR232 HBCRSUONMIR234 HBCRSUONMIR236 HBCRSUONMIR238 HBCRSUONOJS212 HBCRSUONOJS214 HBCRSUONOJS216 HBCRSUONOJS224 HBCRSUONOJS230 HBCRSUONOJS239 HBCRSUONPINI HBCRSUONSIB177 HBCRSUONSNO190 HBCSEA6 HBCSMI15 HBCSUPER94 HBCTON HBCVALESUL20 HBCVKD10 HBCVSOF272 HBCWES217 HCDART1557 HCDDAW25 HCDFRE10 HCDMIR920 HCDPAM12 HCDSOF10 HCGFHG10 HCGHARI10 HCGHBLAN_10 HCGHMY136 HCGHVIG HCHGIO_14 HCHGIO_20 HCHGIO2038 HCLCDMEN2512 HCLCXALE20 HCLCXALI2022 HCLCXAST3965 HCLCXAUR250 HCLCXBRF20 HCLCXDAL175 HCLCXHAT426 HCLCXHAT433 HCLCXHAT435 HCLCXMEN209 HCLCXMEN221 HCLCXMEN234 HCLCXMEN247 HCLCXMEN253 HCLCXOLY312 HCLCXOLY319 HCLCXOLY33751 HCLCXOLY3407 HCLCXSAD20 HCLCXSE181 HCLCXSE183 HCLCXSE184 HCLCXSE1840 HCLCXSE185 HCLCXSE186 HCLCXSE187 HCLCXSE188 HCLCXSE189 HCLCXSE191 HCLCXSE192 HCLCXSE193 HCLCXSE194 HCLCXSE195 HCLCXSE197 HCLCXSE25 HCLCXSEAB4034 HCLDALIA HCLOLY266 HCLPAMLON20 HCLSEARA1809 HCLSEARA1816 HCLSEARA1820 HCLSEARA1822 HCLSEARA1827 HCLSEARA1830 HCLSEARA1836 HDALE220 HDASEV221 HDASEV250 HDAUR25 HDAVAL20 HDBFAC15 HDCLAMI2314 HDCLIND257 HDCLIND259 HDCLIND262 HDCLIND263 HDCLIND266 HDCLIND272 HDCLIND273 HDCLRAN316 HDCLRAN325 HDCLSEAB288 HDCLSEAB293 HDCLSEAB304 HDCX1916 HDCXCON245 HDCXCON251 HDCXCON252 HDCXHYL231 HDCXHYL239 HDCXMEN241 HDCXOLY241 HDCXOLY359 HDCXOLY366 HDCXOLY388 HDCXOLY390 HDCXOLY4113 HDCXSMI232 HDCXSOF220 HDCXSWI212 HDCXSWI214 HDCXSWI217 HDCXSWI230 HDEST HDEST222 HDFRI25 HDHAGR15 HDHBALAN10 HDHCAN13 HDHOLY HDHOLY33 HDHOLY331 HDHOLY3652 HDLBLO202 HDLMEC112 HDLMIR296 HDLNOR2352 HDLVAL20 HDMAR223 HDMAR240 HDMIRA288 HDMIRA289 HDMIRA29 HDMIRA2928 HDMIRA293 HDMIRA294 HDMIRA295 HDMIRA296 HDMIRA297 HDMIRA298 HDMIRA299 HDMIRA30 HDMIRA303 HDMIRA304 HDMIRA305 HDMIRA306 HDMIRA308 HDMIRA311 HDMIRA312 HDMIRA317 HDMPK233 HDMPK240 HDSAGRO10 HDSANIMEX10 HDSANIMEX10A HDSAO10 HDSAPK10 HDSAUAO10 HDSBLO10 HDSBRAT HDSC10 HDSCAMAC HDSCDS10 HDSCOOPEL10 HDSCROW10 HDSDACH10 HDSDMEA222 HDSDUC10 HDSEA121 HDSEA201 HDSEA202 HDSEA203 HDSEA204 HDSEA207 HDSEA209 HDSEA210 HDSEARA HDSEARA18.9 HDSEARA19 HDSEARA19.93 HDSEARA1905 HDSEARA1907 HDSEARA1908 HDSEARA1910 HDSEARA1911 HDSEARA1914 HDSEARA1935 HDSEARA1939 HDSEARA1952 HDSEARA1962 HDSEARA202 HDSEARA2031 HDSEARA204 HDSEARA2046 HDSEARA209 HDSFAY216 HDSFHG10 HDSFLE10 HDSHAU10 HDSHIG10 HDSHUN10 HDSIB206 HDSIB214 HDSIB218 HDSIB219 HDSMAR10 HDSMI150 HDSMIR100 HDSNCMEA222 HDSPHAP10 HDSPINI10 HDSSEA18 HDSSERV HDSSNOWMAN HDSTRADI10 HDSULM10 HDSUPE94 HDSWES10 HDSY10 HDTANIME20A HDTANIMEX10 HDTANIMEX20 HDTAO10 HDTCAM206 HDTCDS10 HDTCOOPEL10 HDTDAC10 HDTDANI95 HDTDANI984 HDTDEBRA HDTDHYL171 HDTDHYL2361 HDTDHYL2373 HDTDMEA1950 HDTDMIRK10L HDTDMIRK10N HDTDMIRK151N HDTDMIRK2296N HDTDMIRK234N HDTDOLY143 HDTDPAC202 HDTDPAC206 HDTDULM1485 HDTDULM1575 HDTDULM1647 HDTDULM1657 HDTDULM1665 HDTDULM1714 HDTDWEST88 HDTDWESTL1043 HDTDWESTL105 HDTDWESTM90 HDTDWESTM99 HDTDWESTS88 HDTDWESTS892 HDTDWESTS90 HDTDWESTS91 HDTFAY1978 HDTFLE10 HDTFOOD15 HDTFOODSER10 HDTGOB10 HDTHBALAN10 HDTHIG10 HDTINDIANA136 HDTMAR10 HDTMARC10 HDTMIR10 HDTNCMIRK104 HDTOLY20 HDTPAC162 HDTPAN10 HDTSAU20 HDTSEA18 HDTSKIBA10 HDTSUIN10 HDTTAM10 HDTTON10 HDTY10 HDTZAKLA10 HDUSUN20 HDVAL20 HDVAL24 HEO1398 HGDAGR241 HGDAGR248 HGDAGR256 HGDAGR257 HGDH_2362 HGDH2255 HGDH2257 HGDH2261 HGDH2283 HGDH2293 HGDH2296 HGDH2300 HGDH2307 HGDH2338 HGDH2348 HGDH2353 HGDH2361 HGDH2362 HGDH2374 HGDH2384 HGDH2404 HGDH3227 HGDH3308 HGDH3317 HGDH3349 HGDH338 HGDH3401 HGDH3407 HGDH3414 HGDH3443 HGDH3446 HGDOLY357 HGDOLY363 HKBFRIG101 HKBOLY15 HKBSOF15 HKBTON141 HKBTON142 HKBTON142D HKBTON143 HKBTON143D HKBTON144 HKBTON144D HKBTON146 HKBTON146D HKBVIO10 HKBZAK10 HKBZAK95 HKG HKGAGRO_1361 HKGCDS10 HKGFRIGO HKGGOLON10 HKGGOLON2385 HKGHADEL10 HKGHBLAN_10 HKGHCAM222 HKGHCLEMEN HKGHCOL236 HKGHDU10 HKGHDU13 HKGHDU14 HKGHDUA10 HKGHDUR_133 HKGHELF1522 HKGHFOODSER1272 HKGHMC1042 HKGHMCAR1037 HKGHMIR149 HKGHMIR150 HKGHMIR151 HKGHMIR152 HKGHMIR154 HKGHMIR157 HKGHMIR157B HKGHMIR159 HKGHMIR182B HKGHNIK171 HKGHNIK1712 HKGHOLY15 HKGHPINI2059 HKGHROUS HKGHSERVICE10 HKGHSERVICEDEP10 HKGHSUIN10 HKGHTAM179 HKGHVIO10 HKGHYLIFE HKGIO_136 HKGOIGLO10 HKGOIHAU10 HKGPINI1018 HKGPINI1021 HKGPINI1031 HKGRANTOUL_1361 HKGZAKLA154 HKXDGOI10 HLTCARNI HLTCASI10 HLTCHE10A HLTCHE10B HLTCOS10 HLTDADY10 HLTGEL10 HLTGOBAR HLTIMA10 HLTMAC15 HLTMAR10 HLTPAMLON12 HLTSKIBA HLTTELFE HLTTIME HLTTMAR10 HLTTON10 HLTTRUS10 HLUOIBEN10 HLUOICOS10 HLUOICOS10N HLUOICRO10 HLUOIEXP10 HLUOIFRIG10 HLUOIMAR HLUOIPAMLO12 HLUOIPAN10 HLUOIPPS10 HLUOIPSS10 HLUOIROS10 HLUOIROS25 HLUOISB10 HLUOIULM10 HLUOIWES10 HMA1671 HMAROS25 HMATCOR10 HMATVIA15 HMCCOV181 HMCGOOD25 HMCWES221 HMCWES222 HMLBLO201 HMLBLO202 HMLBRA176 HMLFRE10 HMLMAR193 HMLMEC126 HMOHE272 HMOHEO_20 HMOHEO_2721 HMOHEO1814 HMOHEO20 HMOHEO2355 HMOHEO2721 HMOHEOMIKA HMUIMAC10 HMUIRUS10 HMVMAR179 HNAM47 HNAM50 HNAM50K HNAM75 HNCFRI25 HNDDAL217 HNDRAN31 HNDSEA192 HNDSEA194 HNDUISIB207 HNOAPK176 HNOCAM150 HNOCOS10 HNOCOS194X HNODUB212 HNODUB219 HNOFRE105 HNOFRE106 HNOFRE107 HNOFRE108 HNOFRE109 HNOGOO25 HNOHEO2074 HNOICE10 HNOINC1369 HNOIND2741 HNOMAC181 HNOMAR106 HNOMEN2371 HNONHE20 HNONHE2721 HNORIV130 HNVALE20 HNVBRA1761 HNVBRA1777 HNVBRA1875 HNVBRA20 HNVCOM HNVCOM198 HNVCOM199 HNVCON22 HNVCON322 HNVCON323 HNVCON329 HNVCXOLY327 HNVCXOLY331 HNVCXOLY334 HNVDAL200 HNVEST194 HNVEST20.4 HNVFRI25 HNVHOM HNVHOR20 HNVKFL2721 HNVKLPB2721 HNVKSMI_2721 HNVKSMI2721 HNVKX2721 HNVMAR243 HNVMAR246 HNVMIR10 HNVMIR25 HNVMIR262 HNVMIR263 HNVMIR264 HNVMIR265 HNVMIR268 HNVMIR270 HNVMIR273 HNVMIR274 HNVMIR276 HNVMIR277 HNVMIR279 HNVMIR28 HNVMIR281 HNVMIR283 HNVMIR284 HNVMIR285 HNVMIR29 HNVMIR291 HNVMIR30 HNVMPK198 HNVMPK207 HNVOLY22 HNVSAD180 HNVSEA18.40 HNVSEA1815 HNVSEA1822 HNVSEA1824 HNVSEA1831 HNVSEA1833 HNVSEA1836 HNVSEA1850 HNVSEA1851 HNVSEA1856 HNVSEA1857 HNVSEA1867 HNVSEA1883 HNVSEA1898 HNVSEA196 HNVSEA197 HNVSEA198 HNVSEA199 HNVSEA205 HNVSEA207 HNVSIB193 HNVSIB195 HNVSIB198 HNVSMIT HNVSMIT20 HNVSMIT2072 HNVSMIT21 HNVSMIT211 HNVSMIT212 HNVSMIT213 HNVSMIT225 HNVSMIT226 HNVSMIT227 HNVSMIT228 HNVSMIT229 HNVSMIT23.6 HNVSMIT230 HNVSMIT235 HNVSMIT242 HNVSUL180 HNVVALE24 HQD2066 HSBABB1361 HSBAGR16 HSBALI20 HSBAUR25 HSBBATALLE10 HSBBLO100 HSBCAL1814 HSBCAS100 HSBCAS940 HSBCAS970 HSBCON164 HSBDAN10B HSBDAN998 HSBDAN998B HSBELF121 HSBELF124 HSBELF126 HSBFRI18 HSBGLO10 HSBHYL153 HSBMIR100 HSBOLYMEL142 HSBOLYMEL146 HSBOLYMEL147 HSBOLYMEL148 HSBOLYMEL151 HSBOLYMEL152 HSBOLYMEL155 HSBOLYMEL156 HSBOLYMEL157 HSBOLYMEL158 HSBOLYMEL160 HSBOLYMEL161 HSBOLYMEL162 HSBOLYMEL167 HSBOLYMEL170 HSBOLYMEL173 HSBPAC107 HSBPAC1354 HSBPAC174 HSBRAN154 HSBRAN160 HSBSAD180 HSBSEA180 HSBSKIBA10 HSBSMI162 HSBSMI1684B HSBSOF176 HSBSOF181 HSBTON10 HSBVIO10 HSBWES10 HSGBALAN10 HSH HSHAGRO161 HSHALAN1176 HSHALAN182 HSHANI102 HSHANIMEX2055 HSHAO10 HSHBAL19 HSHBALAN HSHBATA_10 HSHBLAN_10 HSHBLAN_9775 HSHCAR5 HSHCDS10 HSHCLEMEN136 HSHCOOPER10 HSHCROWN10 HSHCROWNMEAT10 HSHDUB136 HSHDUC HSHDUR_136 HSHFAR1299 HSHFAR13 HSHHORMEL HSHHYLIFE HSHIND173 HSHIND174 HSHIND175 HSHJOB5 HSHNWT10 HSHPINI10 HSHPRI22 HSHSMITH1299 HSHSMITH13 HSHSMITH136 HSHSMITH1361 HSHXN HSHY10 HSSCON135 HSSCOS10 HSSFAY209 HSSHAU10 HSSHAU1042 HSSIND115 HSSIND173 HSSIND174 HSSIND175 HSSMIR103 HSSMIR965 HSSMIRA965 HSSRAN189 HSSSEAB173 HSSSEAB175 HSSSEAB183 HSSSEAB188 HSSSEAB190 HSSSEAB192 HSSSEAB193 HSSSEAB194 HSSSEAB195 HSSSMIT136 HSSSMIT1361 HSSTYS135 HSSTYS135W HSSTYS136 HSSTYS136W HSSTYS138 HSSTYS139 HSSTYS141 HSUCNWT10 HSUONCAS10 HSUONTBN5 HSUSUN200 HSVOLYMEL15 HSVRUS10 HTAIGOBAR HTDBRA1876 HTHAUSA10 HTHBATA10 HTHBLAN10 HTHBLO122 HTHBLOM8 HTHCAR10 HTHCAR14 HTHCART10 HTHCORAZ10 HTHCORENN10 HTHDIE10 HTHDUC10 HTHFRI10 HTHFRIBIN10 HTHFRIG HTHHAL10 HTHICP10 HTHJULIA10 HTHMAC10 HTHMAF10 HTHMAVE10 HTHNAVA10 HTHNOVACO HTHPAL10 HTHPATEL10 HTHPATEL15 HTHPPS10 HTHPPS10C HTHRODRI10 HTHROS10 HTHVIO10 HTHXFARM136 HTHXSUN140 HTMHEO2722 HTNHORM10 HTNSUN140 HTNVUN272 HTTFRI12 HTVBRI25 HTVFAC122 HTVUN267 HTXHORM136 HTXSEB136 HXCANN10 HXCHYLIFE HXCLDUC HXCSEA15 HXDGOIDUC HXOHBALAN10 HXONG10 HXONGMAR10S HXONGMAR20 HXONGMAR20S HXONGSKIBA HXOSOF10 HXQCDS10 HXSBANI10 HXSCOS HXSCOS10 HXSCOS580 HXSELFER10 HXSH20 HXSHANIMEX HXSHANIMEX10.12 HXSHBLANIMÏ HXSHMY148 HXSMIT13 HXSRANT1833 HXSRANT1838 HXSRANT1854 HXSRANT1859 HXUBALAN10 MUCA1D10-20 MUCA1D20-30 MUCA1D5-10 MUCA1D5-7 MUCA1D7-10 MUCA2X10-20 MUCA2X20-30 MUCA2X30 MUCA2X7-10 MUCNC7-10 SACHBO TRAU001AD TRAU001AR20 TRAU001NC TRAU001NIC TRAU001RUS20 TRAU001RUS25 TRAU001SP TRAU001TOP TRAU001WP TRAU008ALT20 TRAU008AMR20 TRAU008T TRAU011AD TRAU011ALM TRAU012ALM TRAU013ALM TRAU014ALM TRAU015AD TRAU015ALM TRAU015HMAX TRAU016A TRAU016BALM TRAU017ALM TRAU019ALM TRAU01BAD TRAU02ALM20 TRAU02HMA TRAU031AD TRAU03HMA TRAU041AD TRAU042AD TRAU044AD TRAU045AD TRAU046AD TRAU05AMB TRAU060AD TRAU060SAD TRAU061AD TRAU064AD TRAU065AD TRAU06620AD TRAU066AD TRAU066FRE TRAU067AD TRAU067ALM TRAU06HMA TRAU070AD TRAU07HMA TRAU07KA TRAU07NO TRAU08HMA TRAU09AL TRAU09ALM20 TRAU09AMB TRAU09FR TRAU09HMA TRAU09KA TRAU09KM TRAU09MALM20 TRAU09NALM20 TRAU09NGALM20 TRAU09TALM TRAU101ALM TRAU106AB TRAU106AD TRAU106AL TRAU106ALG TRAU106AWB TRAU106HMA TRAU106HMA18 TRAU106KA TRAU106KA18 TRAU106MH TRAU106UN TRAU106WB TRAU109VAR20 TRAU11AF TRAU11AFZ TRAU11AL TRAU11AL28 TRAU11ALG TRAU11ALI TRAU11ALT18 TRAU11ALT18N TRAU11AMR18 TRAU11CTHMA TRAU11FAIR TRAU11HAB TRAU11HB18 TRAU11HMA TRAU11KA TRAU11MH TRAU11MJ174 TRAU11MJ18 TRAU11NC TRAU11NIC18 TRAU11NO TRAU11RUS TRAU11SA TRAU11SP TRAU11TAS18 TRAU11TAST18N TRAU11UN TRAU11WB TRAU11WP TRAU11ZU TRAU123HB TRAU123HMA TRAU123KM TRAU123MH TRAU123NIC TRAU123WB TRAU12HMA20 TRAU132RUS25 TRAU13ALT TRAU13HMA TRAU13KA TRAU13MH TRAU13UN25 TRAU144BAL TRAU152AL TRAU15AF TRAU15AL TRAU15AL28 TRAU15ALT TRAU15HMA TRAU15HMA20 TRAU15NC TRAU15SP TRAU15UN TRAU15WB TRAU15WP TRAU16HB TRAU17AL TRAU17AR25 TRAU17MAR TRAU17MFA25 TRAU17NC TRAU17SP TRAU17UN TRAU18HMA TRAU19AF TRAU19ALM25 TRAU19AR25 TRAU19BALM25 TRAU19HB TRAU19HMA TRAU19HMA25 TRAU19KM TRAU19MAR20 TRAU19WB TRAU19WP TRAU20RUS25 TRAU21AL TRAU222AF TRAU222AFZ TRAU222AL TRAU222HMA20 TRAU222NC TRAU222NIC TRAU222TOP TRAU227AL TRAU227FR TRAU227NC TRAU227ZU TRAU22AL TRAU22HMA TRAU23HMA TRAU24HMA TRAU24KM TRAU25HMA TRAU29HMA TRAU31AF TRAU31AH TRAU31AL TRAU31ALG TRAU31ALM TRAU31ALT TRAU31HMA TRAU31HMA18 TRAU31ID TRAU31KA TRAU31NC TRAU31SA TRAU31SP TRAU31UN TRAU31WB TRAU31WP TRAU33HMA TRAU34HMA TRAU41AB TRAU41AF TRAU41AFZ TRAU41AL TRAU41ALG TRAU41ALM TRAU41ALT TRAU41AMB TRAU41AR20 TRAU41ETI TRAU41FAIR TRAU41FD TRAU41FR TRAU41HMA TRAU41HMAX TRAU41ID TRAU41KA TRAU41MAR TRAU41MFA20 TRAU41MH TRAU41MJ TRAU41NC TRAU41NIC TRAU41NO TRAU41ON TRAU41SA TRAU41SHI TRAU41SP TRAU41TOP TRAU41UN TRAU41WB TRAU41WP TRAU41ZU TRAU42AB TRAU42AF TRAU42AL TRAU42ALG TRAU42ALM TRAU42ALT20 TRAU42AMB TRAU42AR20 TRAU42ETI TRAU42FAIR TRAU42FD TRAU42FR TRAU42HBI TRAU42HMA TRAU42HMA20 TRAU42HMAX TRAU42ID TRAU42KA TRAU42MAR TRAU42MFA20 TRAU42MH TRAU42NC TRAU42NIC TRAU42NO TRAU42ON TRAU42SA TRAU42SHI TRAU42SP TRAU42TOP TRAU42UN TRAU42WB TRAU42WP TRAU42ZU TRAU44AB TRAU44AF TRAU44AL TRAU44ALG TRAU44ALM TRAU44ALT20 TRAU44AMB TRAU44AR20 TRAU44ETI TRAU44FAIR TRAU44FD TRAU44FR TRAU44HMA TRAU44HMAX TRAU44ID TRAU44KA TRAU44MAR TRAU44MFA20 TRAU44MH TRAU44NC TRAU44NIC TRAU44NO TRAU44ON TRAU44SA TRAU44SHI TRAU44SP TRAU44TOP TRAU44UN TRAU44WB TRAU44WL TRAU44ZU TRAU45AB TRAU45AF TRAU45AL TRAU45ALG TRAU45ALM TRAU45ALT20 TRAU45AMB TRAU45AR20 TRAU45ETI TRAU45FAIR TRAU45FD TRAU45FR TRAU45HB TRAU45HMA TRAU45HMAX TRAU45ID TRAU45KA TRAU45MAR TRAU45MFA20 TRAU45MH TRAU45NC TRAU45NIC TRAU45NO TRAU45ON TRAU45SA TRAU45SHI TRAU45SP TRAU45TOP TRAU45UN TRAU45WB TRAU45WL TRAU45ZU TRAU46AB TRAU46AF TRAU46AL TRAU46AL2 TRAU46ALG TRAU46ALG18 TRAU46ALM TRAU46ALM25 TRAU46ALT TRAU46ALT25 TRAU46AMB TRAU46AMR TRAU46AR20 TRAU46FAIR TRAU46FAIR2 TRAU46FAIR20 TRAU46HAB TRAU46HMA TRAU46ID TRAU46KA TRAU46KM TRAU46LATIF TRAU46MAR20 TRAU46MH TRAU46MH18 TRAU46NC TRAU46NO TRAU46SA TRAU46SHI20 TRAU46SHMA TRAU46SP TRAU46STAR TRAU46UN TRAU46VIB20 TRAU46WB TRAU46WL TRAU46ZU TRAU47AHMA TRAU47AL TRAU47ALM TRAU47ALM25 TRAU47AQ TRAU47HMA TRAU47HMA20 TRAU47HMAX TRAU47MAR20 TRAU47UN TRAU48ALT TRAU48FAIR TRAU48FR TRAU4OM TRAU52NIC TRAU52NO TRAU57AL TRAU57ALT TRAU57AQ TRAU57HMA TRAU57KA TRAU57KM TRAU57T TRAU60AAL TRAU60AL TRAU60ALT TRAU60B1 TRAU60B1AF TRAU60B1WB TRAU60B1WP TRAU60B2AF TRAU60B2WB TRAU60B2WP TRAU60BWB TRAU60BWL TRAU60HMA TRAU60KA TRAU60KM TRAU60MH TRAU60NO TRAU60S1AF TRAU60S1WB TRAU60S1WP TRAU60S2AF TRAU60S2WB TRAU60S2WP TRAU60SAL TRAU60SKA TRAU60SMH TRAU60SP TRAU60SSA TRAU60SWB TRAU60SWP TRAU60UN TRAU60ZU TRAU61AF TRAU61AL TRAU61ALG TRAU61HMA TRAU61NC TRAU61NIC TRAU61NO TRAU61SA TRAU61SP TRAU61UN TRAU61WB TRAU61WP TRAU62AF TRAU62AL TRAU62ALG TRAU62NC TRAU62NO TRAU62SA TRAU62SP TRAU62UN TRAU62WB TRAU62WP TRAU63AF TRAU63KM TRAU63WB TRAU63WP TRAU64AF TRAU64AL TRAU64ALG TRAU64ALT TRAU64AMB TRAU64AMR TRAU64FAIR TRAU64FAIR25 TRAU64HAB TRAU64HB TRAU64HMA TRAU64HMA20 TRAU64ID TRAU64KA TRAU64KM TRAU64MAR20 TRAU64MH TRAU64NC TRAU64NO TRAU64SA TRAU64SP TRAU64UN TRAU64WB TRAU64WL TRAU64ZU TRAU65AF TRAU65AL TRAU65AL20 TRAU65ALG20 TRAU65ALT TRAU65AR25 TRAU65HB TRAU65HMA TRAU65HMA25 TRAU65ID TRAU65KA TRAU65MAR20 TRAU65MH TRAU65NC TRAU65NO TRAU65SA TRAU65SP TRAU65UN TRAU65WB TRAU65WL TRAU65ZU TRAU66AL TRAU66AQ TRAU66HAB TRAU66HMA TRAU66HMA24 TRAU66KA TRAU66KA28 TRAU66MH TRAU66NO TRAU66UN20 TRAU67AAF TRAU67AF TRAU67AL TRAU67ALG TRAU67AWB TRAU67AWP TRAU67HMA TRAU67HMA20 TRAU67KA TRAU67KM TRAU67MH TRAU67NC TRAU67NO TRAU67SA TRAU67SP TRAU67UN TRAU67WB TRAU67WP TRAU68AF TRAU68WB TRAU68WP TRAU69AL TRAU69AR TRAU70ALT TRAU70AQ TRAU70ID TRAU70KA TRAU70MB TRAU70MH TRAU70NC TRAU70RIB TRAU70SP TRAU70UN TRAU70WB TRAU70WP TRAU75ANC TRAU75ASP TRAU75BNC TRAU75BSP TRAU78HMA TRAU83AL TRAU86AL TRAU86BAL TRAU88AL TRAUA75BSP TRAUAH11 TRAUAH13 TRAUAH31 TRAUAH41 TRAUAH42 TRAUAH44 TRAUAH45 TRAUAH46 TRAUAH47 TRAUAH60 TRAUAH61 TRAUAH62 TRAUAH64 TRAUAH65 TRAUAH66 TRAUAH67 TRAUAHA60 TRAUAHA67 TRAUAHA75 TRAUAHB44 TRAUAHB60 TRAUAHB75 TRAUAL65 TRAUAQ09 TRAUAQ11 TRAUAQ31 TRAUAQ41 TRAUAQ42 TRAUAQ44 TRAUAQ45 TRAUAQ46 TRAUAQ60S TRAUAQ61 TRAUAQ62 TRAUAQ64 TRAUAQ65 TRAUAQ67 TRAUAQA42 TRAUAQA44 TRAUAQA60 TRAUAQA75 TRAUAQB75 TRAUAR20 TRAUARRED20 TRAUB19KM TRAUB6HMA TRAUCHUCK TRAUCHUCKNC TRAUDIEMIAC TRAUDSLATIF TRAUFAIRACH TRAUFLANK TRAUFQSHI20 TRAUGANMAS20 TRAUGANNHB20 TRAUHEAD20 TRAUKASHILA TRAUKASILA TRAUKUR20 TRAULATIF TRAULUOIALM20 TRAULUOIHB20 TRAULUOIIAC TRAULUOIMAS20 TRAUNECK TRAUOM11 TRAUOM31 TRAUOM41 TRAUOM42 TRAUOM44 TRAUOM45 TRAUOM46 TRAUOM61 TRAUOM62 TRAUOM64 TRAUOM65 TRAUOM67 TRAUOMA11 TRAUOMA42 TRAUOMA44 TRAUOMA60 TRAUOMA75 TRAUOMB44 TRAUOMB60 TRAUOMB75 TRAUPLAIN20 TRAUREMMAS TRAURIBAB TRAURIBALT TRAURIBKA TRAURIBKM TRAURIBUN TRAUSHI20 TRAUSLICEBH18 TRAUTRANG TRAUTRIMHB28 TRAUTRIMMAR20 TRAUTRM20 TRAUTUYALM20 TRAUTUYIAC TRAUTUYMAS20 TRAUVEAL18 VCVMAP907 VCVTOMAS VCVTRAN15 VNCLAR10 VNCVTRAN XONGBO22 XONGBO23 XONGBO30 XONGBO31 XUONGBO "
                    .Split(' ');
                Dictionary<string,string> list_input_test = new Dictionary<string, string>();
                Dictionary<string,string> list_output_test2 = new Dictionary<string, string>();

                if (sender == btnTestHash)
                {
                    foreach (string s in input_list_01)
                    {
                        // chỉ lọc mã khác nhau làm đầu vào.
                        if (s.Length < 5) continue;
                        if (!list_input_test.ContainsKey(s))
                        {
                            list_input_test.Add(s, s);
                            message_out += " " + s + ":" + ObjectAndString.Hash1(s, (int)numOutLength.Value);
                        }
                    }
                }
                else if (sender == btnTestHash2)
                {
                    foreach (string s in input_list_01)
                    {
                        // chỉ lọc mã khác nhau làm đầu vào.
                        if (s.Length < 5) continue;
                        if (!list_input_test.ContainsKey(s))
                        {
                            list_input_test.Add(s, s);
                            message_out += " " + s + ":" + ObjectAndString.Hash2(s, (int)numOutLength.Value);
                        }
                    }
                }
                else if (sender == btnTestHash3)
                {
                    foreach (string s in input_list_02)
                    {
                        // chỉ lọc mã khác nhau làm đầu vào.
                        if (s.Length < 5) continue;
                        if (!list_input_test.ContainsKey(s))
                        {
                            list_input_test.Add(s, s);
                            try
                            {
                                message_out += " " + s + ":" + ObjectAndString.Hash3(s, (int)numOutLength.Value);
                            }
                            catch
                            {
                                MessageBox.Show("Exception:" +s);
                            }
                        }
                    }
                }
                else if (sender == btnTestHash4)
                {
                    foreach (string s in input_list_02)
                    {
                        // chỉ lọc mã khác nhau làm đầu vào.
                        if (s.Length < 5) continue;
                        if (!list_input_test.ContainsKey(s))
                        {
                            list_input_test.Add(s, s);
                            try
                            {
                                message_out += " " + s + ":" + ObjectAndString.Hash4(s, (int)numOutLength.Value);
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show("Exception:" +s + "\n" + ex.Message);
                            }
                        }
                    }
                }
                else if (sender == btnTestHash5)
                {
                    foreach (string s in input_list_02)
                    {
                        // chỉ lọc mã khác nhau làm đầu vào.
                        if (s.Length < 5) continue;
                        if (!list_input_test.ContainsKey(s))
                        {
                            list_input_test.Add(s, s);
                            try
                            {
                                message_out += " " + s + ":" + ObjectAndString.Hash5(s, (int)numOutLength.Value);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Exception:" + s + "\n" + ex.Message);
                            }
                        }
                    }
                }
                

                // check kết quả có trùng không?
                var list2 = message_out.Substring(1).Split(' ');
                int count2 = 0;
                foreach (string s in list2)
                {
                    string[] s_in_out = s.Split(':');
                    string s_in = s_in_out[0];
                    string s_out = s_in_out[1];
                    if (list_output_test2.ContainsKey(s_out))
                    {
                        count2++;
                        var old_item = list_output_test2[s_out];
                        message2_trung += "\n     OLD=" + old_item + ":" + s_out +  "  NEW=" + s;
                    }
                    else
                    {
                        list_output_test2.Add(s_out, s_in);
                    }
                }

                MessageBox.Show(list_input_test.Count + " code:" + message_out);
                MessageBox.Show(count2 + " trùng:" + message2_trung);
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(GetType() + ".btnTestHash_Click", ex, "");
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTestDatabaseConfig_Click(object sender, EventArgs e)
        {
            new V6DatabaseConfogTestForm().Show();
        }

        private void btnConvertExcel_Click(object sender, EventArgs e)
        {
            new V6Tools.V6Convert.FormConvertExcel().Show(this);
        }



        private void numNgang_ValueChanged(object sender, EventArgs e)
        {
            trackBarNgang.Value = (int)numNgang.Value;
            TinhDoDaiDayCung();
        }

        private void trackBarNgang_Scroll(object sender, EventArgs e)
        {
            numNgang.Value = trackBarNgang.Value;
            TinhDoDaiDayCung();
        }

        private void numCao_ValueChanged(object sender, EventArgs e)
        {
            //if (numCao.Value >= numNgang.Value) numCao.Value = numNgang.Value - 1;
            trackBarCao.Value = (int)numCao.Value;
            TinhDoDaiDayCung();
        }

        private void trackBarCao_Scroll(object sender, EventArgs e)
        {
            //if (trackBarCao.Value >= trackBarNgang.Value) trackBarCao.Value = trackBarNgang.Value - 1;
            numCao.Value = trackBarCao.Value;
            TinhDoDaiDayCung();
        }

        void TinhDoDaiDayCung()
        {
            try
            {
                double day_cung = TinhDoDaiMaiVom((double)numNgang.Value, (double)numCao.Value);
                txtCung.Text = "" + day_cung;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        double TinhDoDaiMaiVom(double D, double H)
        {
            double result = 0;
            try
            {
                double d = D / 2;
                double h = H, _h = 0;
                if (h > d)
                {
                    h = d;
                    _h = H - h;
                }

                double c = Căn(Bình_phương(d) + Bình_phương(h));

                double Arad = Math.Acos(d / c);

                double A2rad = Math.PI/2 - (Arad*2);
                double cosA2 = Math.Cos(A2rad);
                double R = d / cosA2;

                result = (4 * Arad) * R + 2*_h;

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Tinh: " + ex.Message);
            }
            return -1;
        }

        // viet hoa ham tinh toan
        double Căn(double a)
        {
            return Math.Sqrt(a);
        }

        double Bình_phương(double a)
        {
            return Math.Pow(a, 2);
        }

        private void btnFileFilter_Click(object sender, EventArgs e)
        {
            (new FileFilterForm()).ShowDialog();
        }
    }
}
