using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using V6Soft.Models.Accounting;
using V6Soft.Dealers.Accounting.Customer;


namespace V6Soft.ServiceClients.Accounting.Customer.DataDealers.Tests
{
    [TestClass]
    public class CustomerDataDealerTest
    {
        private DirectCustomerDataDealer m_DataDealer;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
        }

        [TestInitialize]
        public void TestInitialize()
        {
            //RuntimeModelFactory.DefinitionLoader = new DbModelDefinitionManager(ConnectionString);
            //RuntimeModelFactory.DefinitionLoader.LoadAll();
            //m_DataFarmerMock = new Mock<ICustomerDataFarmer>();
            m_DataDealer = new DirectCustomerDataDealer(null, null);
        }

        #region AddCustomerGroup
        
        [TestMethod]
        public void AddCustomerGroup()
        {
            var newGroup = new CustomerGroup();
            newGroup.SetField(Models.Core.Constants.DefinitionIndex.Field.ID, "ID");
            try
            {
                m_DataDealer.AddCustomerGroup(newGroup);
            }
            catch (Exception ex)
            {
                var exc = ex;
            }
        }

        #endregion
    }
}
