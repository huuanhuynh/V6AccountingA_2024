using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using V6Soft.Services.Accounting.DataBakers;
using V6Soft.Services.Accounting.Interfaces;
using V6Soft.Models.Core;
using V6Soft.Common.Utils;


namespace V6Soft.Services.Accounting.DataFarmers.Tests
{
    [TestClass]
    public class MenuDataBakerTest
    {
        private Mock<IMenuDataFarmer> m_MockFarmer;
        private MenuDataBaker m_Baker;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
        }

        [TestInitialize]
        public void TestInitialize()
        {
            m_MockFarmer = new Mock<IMenuDataFarmer>();
            m_Baker = new MenuDataBaker(m_MockFarmer.Object);
        }

        [TestMethod]
        public void GetMenuTree_NoItem_Null()
        {
            // Arrange
            var mockItems = new DisposableEnumerable<MenuItem>(null);            
            m_MockFarmer
                .Setup(f => f.GetMenuItems())
                .Returns(mockItems);

            // Act
            IList<MenuItem> actualItems = m_Baker.GetMenuTree();

            // Assert
            Assert.IsNull(actualItems);
        }
        
        [TestMethod]
        public void GetMenuTree_Level0_FullMenuTree()
        {
            // Arrange
            var mockItems = new DisposableEnumerable<MenuItem>(
                CreateMockMenuItems());
            m_MockFarmer
                .Setup(f => f.GetMenuItems())
                .Returns(mockItems);

            // Act
            IList<MenuItem> actualItems = m_Baker.GetMenuTree();

            // Assert
            Assert.IsNotNull(actualItems);

            // Assert level 1 existence
            MenuItem item = actualItems.FirstOrDefault(i => i.Label == "PhaiThu");
            Assert.IsNotNull(item);

            // Assert level 2 existence
            Assert.IsNotNull(item.Descendants);
            item = item.Descendants.FirstOrDefault(i => i.Label == "CacDanhMucTuDien");
            Assert.IsNotNull(item);

            // Assert level 3 existence
            Assert.IsNotNull(item.Descendants);
            item = item.Descendants.FirstOrDefault(i => i.Label == "KhachHang");
            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void GetMenuTree_Level2_MenuTreeWith2Levels()
        {
            // Arrange
            byte level = 2;
            var mockItems = new DisposableEnumerable<MenuItem>(
                CreateMockMenuItems());
            m_MockFarmer
                .Setup(f => f.GetMenuItems())
                .Returns(mockItems);

            // Act
            IList<MenuItem> actualItems = m_Baker.GetMenuTree(level);

            // Assert
            Assert.IsNotNull(actualItems);

            // Assert level 1 existence
            MenuItem item = actualItems.FirstOrDefault(i => i.Label == "PhaiThu");
            Assert.IsNotNull(item);

            // Assert level 2 existence
            Assert.IsNotNull(item.Descendants);
            item = item.Descendants.FirstOrDefault(i => i.Label == "CacDanhMucTuDien");
            Assert.IsNotNull(item);

            // Assert level 3 not existence
            Assert.IsNull(item.Descendants);
        }


        private List<MenuItem> CreateMockMenuItems()
        {
            var menuItems = new List<MenuItem>();

            menuItems.Add(new MenuItem()
            {
                OID = 1,
                Label = "TienMat",
                Position = 0
            });

            menuItems.Add(new MenuItem()
            {
                OID = 2,
                Label = "PhaiThu",
                Position = 1
            });

            menuItems.Add(new MenuItem()
            {
                OID = 9,
                Label = "CapNhatSoLieu",
                Position = 0,
                ParentOID = 2
            });

            menuItems.Add(new MenuItem()
            {
                OID = 10,
                Label = "CapNhatSoDu",
                Position = 1,
                ParentOID = 2
            });

            menuItems.Add(new MenuItem()
            {
                OID = 11,
                Label = "CacDanhMucTuDien",
                Position = 2,
                ParentOID = 2
            });

            menuItems.Add(new MenuItem()
            {
                OID = 26,
                Label = "KhachHang",
                Position = 0,
                ParentOID = 11
            });

            menuItems.Add(new MenuItem()
            {
                OID = 27,
                Label = "PhanNhomKhachHang",
                Position = 1,
                ParentOID = 11
            });

            return menuItems;
        }
    }
}
