using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using V6Soft.Models.Core;
using V6Soft.Common.Utils;


namespace V6Soft.Services.Accounting.DataFarmers.Tests
{
    [TestClass]
    public class MenuDataFarmerTest
    {
        private const string ConnectionString = @"Data Source=.;Initial Catalog=V6Accounting;Integrated Security=True";

        private MenuDataFarmer m_Farmer;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
        }

        [TestInitialize]
        public void TestInitialize()
        {
            m_Farmer = new MenuDataFarmer(ConnectionString);
        }

        //[TestMethod]
        public void GetMenuTree_AllItems()
        {
            DisposableEnumerable<MenuItem> menuItems = m_Farmer.GetMenuItems();

            Assert.IsNotNull(menuItems);
            Assert.IsTrue(menuItems.Count() > 0);
        }

        //[TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetMenuTree_DisposeBeforeAccess_ThrowsException()
        {
            DisposableEnumerable<MenuItem> menuItems = m_Farmer.GetMenuItems();
            menuItems.Dispose();
            menuItems.Any();
        }
    }
}
