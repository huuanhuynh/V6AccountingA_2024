using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using V6Soft.Models.Accounting;
using V6Soft.ServiceClients.Accounting.Assistant.DataDealers;
using V6Soft.Common.Logging;


namespace V6Soft.ServiceClients.Accounting.Assistant.DataDealers.Tests
{
    [TestClass]
    public class AssistantDataDealerTest
    {
        private DirectAssistantDataDealer m_DataDealer;
        private ILogger logger; 
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
            m_DataDealer = new AssistantDataDealer(logger);
        }


        #region AddProvince
        
        [TestMethod]
        public void AddProvince()
        {
            var newProvince = new Province();
            try
            {
                m_DataDealer.AddProvince(newProvince);
            }
            catch (Exception ex)
            {
                var exc = ex;
            }
        }

        #endregion
    }
}
