using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using V6Soft.Common.ModelFactory;
using V6Soft.Common.ModelFactory.Constants;
using V6Soft.Common.ModelFactory.Factories;
using V6Soft.Common.ModelFactory.Managers;
using V6Soft.Services.Accounting.DataFarmers;
using V6Soft.Services.Accounting.Interfaces;

using ModelIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Model;


namespace V6Services_Accounting_Tests
{
    [TestClass]
    public class CustomerDataFarmerTest
    {
        private readonly Guid VictimGuid = Guid.Parse("39CA37A8-FB68-46D6-A6FB-104044B99A5C");

        private const string ConnectionString = @"Data Source=.;Initial Catalog=V6Accounting;Integrated Security=True";

        /// <summary>
        /// Value: "A!B#C$"
        /// </summary>
        private const string MockString = "A!B#C$";

        /// <summary>
        /// Value: 10
        /// </summary>
        private const int MockCount = 10;

        /// <summary>
        ///     Value: 0
        /// </summary>
        private const int UidFieldIndex = 0;


        private ICustomerDataFarmer m_Farmer;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
        }

        [TestInitialize]
        public void TestInitialize()
        {
            DynamicModelFactory.DefinitionLoader = new DbModelDefinitionManager(ConnectionString);
            DynamicModelFactory.DefinitionLoader.LoadAll();
            m_Farmer = new CustomerDataFarmer(ConnectionString);
        }

        
        #region GetCustomerGroups

        [TestMethod]
        public void Search_NoCriteria_FullList()
        {
            // Arrange
            ushort pageIndex = 1;
            ushort pageSize = 10;

            // Act
            ulong total;
            IList<DynamicModel> actualList = m_Farmer.SearchCustomerGroups(
                new List<string> { DefinitionName.Fields.Name, DefinitionName.Fields.Code, DefinitionName.Fields.Status }, 
                null, pageIndex, pageSize, out total);

            // Assert
            Assert.IsNotNull(actualList);
            Assert.IsTrue(actualList.Count <= pageSize);
            dynamic firstElem = actualList[0];
            firstElem.Name = "Tri";
            object value;
            string field = "Ten";
            firstElem.TryGetMember("Ten", out value);
            /*
             proj Model.Core
             * static class V6Field 
             * |_ public static string {CodeName} = "{DbName}";
             * |_ public static string Name = "Ten";
             */
        }

        [TestMethod]
        public void Search_ContainCriteria_MatchedList()
        {
            // Arrange
            var criteria = new List<SearchCriterion>()
            {
                new SearchCriterion() {
                    FieldName = DefinitionName.Fields.Status,
                    CompareOperator = CompareOperator.Equal,
                    ConditionValue = true
                },
                new SearchCriterion() {
                    FieldName = DefinitionName.Fields.Name,
                    CompareOperator = CompareOperator.Contain,
                    ConditionValue = @"Nội"
                }
            };
            ushort pageIndex = 1;
            ushort pageSize = 10;

            // Act
            ulong total;
            IList<DynamicModel> actualList = 
                m_Farmer.SearchCustomerGroups(new List<string>
                {
                    DefinitionName.Fields.Name, DefinitionName.Fields.Code, DefinitionName.Fields.Status
                }, criteria, pageIndex, pageSize, out total);

            // Assert
            Assert.IsNotNull(actualList);
            Assert.IsTrue(actualList.Count <= pageSize);
        }

        [TestMethod]
        public void Search_BeginCriteria_MatchedList()
        {
            // Arrange
            var criteria = new List<SearchCriterion>()
            {
                new SearchCriterion() {
                    FieldName = DefinitionName.Fields.Status,
                    CompareOperator = CompareOperator.Equal,
                    ConditionValue = true
                },
                new SearchCriterion() {
                    FieldName = DefinitionName.Fields.Name,
                    CompareOperator = CompareOperator.BeginWith,
                    ConditionValue = @"Hà"
                }
            };
            ushort pageIndex = 1;
            ushort pageSize = 10;

            // Act
            ulong total;
            IList<DynamicModel> actualList = 
                m_Farmer.SearchCustomerGroups(new List<string> { DefinitionName.Fields.Name }, criteria, pageIndex, pageSize, out total);

            // Assert
            Assert.IsNotNull(actualList);
            Assert.IsTrue(actualList.Count <= pageSize);
        }

        [TestMethod]
        public void Search_Paging_MatchedList()
        {
            // Arrange
            var criteria = new List<SearchCriterion>()
            {
                new SearchCriterion() {
                    FieldName = DefinitionName.Fields.Status,
                    CompareOperator = CompareOperator.NotEqual,
                    ConditionValue = false
                }
            };
            ushort pageIndex = 2;
            ushort pageSize = 3;

            // Act
            ulong total;
            IList<DynamicModel> actualList =
                m_Farmer.SearchCustomerGroups(new List<string> { DefinitionName.Fields.Name }, criteria, pageIndex, pageSize, out total);

            // Assert
            Assert.IsNotNull(actualList);
            Assert.IsTrue(actualList.Count <= pageSize);
        }

        #endregion


        #region AddCustomerGroup

        [TestMethod]
        public void AddCustomerGroup_InsertAllFields_Success()
        {
            dynamic newModel = DynamicModelFactory.CreateModel((ushort)ModelIndex.CustomerGroup);
            newModel.Code = "Code0";
            newModel.Name = "Name0";
            newModel.Status = true;
            newModel.Note = "This is a very long note. Oh, sorry, not very long.";
            bool result = m_Farmer.AddCustomerGroup(newModel);

            Assert.IsTrue(result);
            Assert.IsNotNull(newModel.GetField(UidFieldIndex));
        }

        [TestMethod]
        public void AddCustomerGroup_InsertOnlyMandatoryFields_Success()
        {
            dynamic newModel = DynamicModelFactory.CreateModel((ushort)ModelIndex.CustomerGroup);

            // This field is expected to be removed by DataFarmer
            newModel.Code = "Code1";
            newModel.Name = "Name1";
            newModel.Status = false;

            bool result = m_Farmer.AddCustomerGroup(newModel);

            Assert.IsTrue(result);
            Assert.IsNotNull(newModel.GetField(UidFieldIndex));
        }

        [TestMethod]
        public void AddCustomerGroup_NotInsertMandatoryFields_SqlException()
        {
            // Arrange
            dynamic newModel = DynamicModelFactory.CreateModel((ushort)ModelIndex.CustomerGroup);
            Exception expectedException = null;

            // Act
            newModel.Code = "Code2";

            try
            {
                m_Farmer.AddCustomerGroup(newModel);
            }
            catch (Exception ex)
            {
                expectedException = ex;
            }
            
            // Assert
            Assert.IsInstanceOfType(expectedException, typeof(SqlException));
            Assert.IsTrue(expectedException.Message.Contains("Cannot insert the value NULL into column"));
        }

        [TestMethod]
        public void AddCustomerGroup_ExistedCode_SqlException()
        {
            // Arrange
            dynamic newModel = DynamicModelFactory.CreateModel((ushort)ModelIndex.CustomerGroup);
            Exception expectedException = null;

            // Act
            newModel.Code = "CodeAA";
            newModel.Name = "NameAA";
            newModel.Status = false;
            m_Farmer.AddCustomerGroup(newModel);

            try
            {
                // Adds again to cause a unique key conflict
                m_Farmer.AddCustomerGroup(newModel);
            }
            catch (Exception ex)
            {
                expectedException = ex;
            }

            // Assert
            Assert.IsInstanceOfType(expectedException, typeof(SqlException));
            Assert.IsTrue(expectedException.Message.Contains("Cannot insert duplicate key"));
        }

        #endregion


        #region ModifyCustomerGroup

        [TestMethod]
        public void ModifyCustomerGroup_ChangeAllFields_Success()
        {
            dynamic model = DynamicModelFactory.CreateModel((ushort)ModelIndex.CustomerGroup);
            model.UID  = VictimGuid;
            model.Code = "CdCode0";
            model.Name = "Changed Name0";
            model.Status = true;
            model.Note = "Changed note to make it shorter.";

            bool result = m_Farmer.ModifyCustomerGroup(model);
            Assert.IsTrue(result);
        }

        #endregion


        #region RemoveCustomerGroup

        [TestMethod]
        public void RemoveCustomerGroup_ChangeAllFields_Success()
        {
            dynamic model = DynamicModelFactory.CreateModel((ushort)ModelIndex.CustomerGroup);
            model.UID = VictimGuid;

            bool result = m_Farmer.RemoveCustomerGroup(VictimGuid);
            Assert.IsTrue(result);
        }

        #endregion
    }
}
