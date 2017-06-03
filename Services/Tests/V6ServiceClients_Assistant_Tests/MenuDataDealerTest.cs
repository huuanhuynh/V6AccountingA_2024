using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using Moq;

using V6Soft.Models.Core;
using V6Soft.Dealers.Accounting.Assistant;
using V6Soft.Interfaces.Accounting.Assistant.DataFarmers;


namespace V6Soft.ServiceClients.Accounting.Customer.DataDealers.Tests
{
    [TestClass]
    public class MenuDataDealerTest
    {
        private Mock<IMenuDataFarmer> m_MenuServiceMock;
        private DirectMenuDataDealer m_DataDealer;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
        }

        [TestInitialize]
        public void TestInitialize()
        {
            m_MenuServiceMock = new Mock<IMenuDataFarmer>();
            m_DataDealer = new DirectMenuDataDealer(null, m_MenuServiceMock.Object);
        }


        public void GetMenuTree_Success_NotNullList()
        {

        }


        public void GetMenuTree_Success_NullList()
        {

        }

        public void GetMenuTree_FaultContract_V6Exception()
        {

        }


        private List<MenuItem> CreateMockMenuItems()
        {
            var menuItems = new List<MenuItem>();
            MenuItem parentItem;

            menuItems.Add(new MenuItem()
            {
                OID = 1,
                Label = "TienMat",
                Position = 0,
                Level = 1
            });

            //
            // Adds 3rd-level items
            //
            parentItem = new MenuItem()
            {
                OID = 11,
                Label = "CacDanhMucTuDien",
                Position = 2,
                ParentOID = 2,
                Level = 2,
                Descendants = new List<MenuItem>()
            };


            parentItem.Descendants.Add(new MenuItem()
            {
                OID = 26,
                Label = "KhachHang",
                Position = 0,
                ParentOID = 11,
                Parent = parentItem,
                Level = 3
            });

            parentItem.Descendants.Add(new MenuItem()
            {
                OID = 27,
                Label = "PhanNhomKhachHang",
                Position = 1,
                ParentOID = 11,
                Parent = parentItem,
                Level = 3
            });


            //
            // Adds 3rd-level items
            //
            parentItem = new MenuItem()
            {
                OID = 2,
                Label = "PhaiThu",
                Position = 1,
                Level = 1,
                Descendants = new List<MenuItem>() { parentItem } // Adds above 2nd-level parent item
            };

            parentItem.Descendants.Add(new MenuItem()
            {
                OID = 9,
                Label = "CapNhatSoLieu",
                Position = 0,
                ParentOID = 2,
                Level = 2
            });

            parentItem.Descendants.Add(new MenuItem()
            {
                OID = 10,
                Label = "CapNhatSoDu",
                Position = 1,
                ParentOID = 2,
                Level = 2
            });

            return menuItems;
        }
    }
}
