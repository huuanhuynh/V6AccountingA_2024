using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.MenuManager
{
    public partial class Menu2Control : V6Control
    {
        private MenuButton _menu1Button;
        private string _moduleID;
        private string _v2ID;
        public Menu2Control()
        {
            InitializeComponent();
            MyInit();
        }

        public Menu2Control(MenuButton menu1Button, string title, string moduleID, string v2ID)
        {   
            InitializeComponent();
            _menu1Button = menu1Button;
            Title = title;
            _moduleID = moduleID;
            _v2ID = v2ID;
            MyInit();
        }

        private void MyInit()
        {
            //Tao tab
            DataTable data = V6Menu.GetMenu2TableFilter(V6Login.UserId, _moduleID, _v2ID);
            foreach (DataRow row in data.Rows)
            {
                AddTab(row);
            }
            currentTabIndex = tabControl.SelectedIndex;
        }

        private void AddTab(DataRow row)
        {
            var tabTex = row[V6Setting.IsVietnamese ? "vbar" : "vbar2"].ToString().Trim().Replace("\\<", "");
            TabPage tabPage = new TabPage(tabTex);
            
            tabPage.BackColor = Color.LightSkyBlue;
            
            //Tao menu3control
            string jobID = row["jobID"].ToString().Trim();
            var menu3 = new Menu3Control(_menu1Button, tabPage, _moduleID, _v2ID, jobID) { Dock = DockStyle.Fill };
            tabPage.Controls.Add(menu3);
            tabControl.TabPages.Add(tabPage);
        }
        
        /// <summary>
        /// Không dùng nữa.
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        public Menu3Control CurrentMenu3Control { get; set; }

        public override void DoHotKey (Keys keyData)
        {
            try
            {
                if (keyData == (Keys.Control | Keys.Left))
                {
                    currentTabIndex--;
                    if (tabControl.TabPages.Count > 0 && currentTabIndex < 0)
                        currentTabIndex = tabControl.TabPages.Count - 1;
                    tabControl.SelectedIndex = currentTabIndex;
                }
                else if (keyData == (Keys.Control | Keys.Right))
                {
                    currentTabIndex++;
                    if (tabControl.TabPages.Count > 0 && currentTabIndex >= tabControl.TabPages.Count)
                        currentTabIndex = 0;
                    tabControl.SelectedIndex = currentTabIndex;
                }
                else// if ((keyData & Keys.Alt) != 0)
                {
                    //goi vao menu 3
                    if (tabControl.SelectedTab.Controls.Count > 0)
                    {
                        var menu3 = tabControl.SelectedTab.Controls[0];
                        var control = menu3 as Menu3Control;
                        if (control != null)
                        {
                            control.DoHotKey(keyData);
                        }
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        private int currentTabIndex = -1;
        //Đổi tab
        private void ctab_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                currentTabIndex = tabControl.SelectedIndex;
                var menu3 = tabControl.SelectedTab.Controls[0];
                if (menu3 is Menu3Control)
                {
                    CurrentMenu3Control =(Menu3Control)menu3;
                    FormManagerHelper.CurrentMenu3Control = CurrentMenu3Control;
                }
            }
            catch
            {
                // ignored
            }
        }

    }
}
