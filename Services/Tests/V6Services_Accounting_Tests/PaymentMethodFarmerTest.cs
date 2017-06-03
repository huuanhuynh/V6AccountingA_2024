using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using V6Soft.Common.ModelFactory;
using V6Soft.Common.ModelFactory.Factories;
using V6Soft.Common.ModelFactory.Managers;
using V6Soft.Models.Core.Constants;
using V6Soft.Services.Accounting.Interfaces;

using FieldIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Field;
using ModelIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Model;

namespace V6Soft.Services.Accounting.DataFarmers.Tests
{
    [TestClass]
    public class PaymentMethodFarmerTest
    {
        //private readonly Guid VictimGuid = Guid.Parse("A3F49DC3-E1A6-4B27-8D53-4B943880FD16");
        //private const string ConnectionString = @"Data Source=.;Initial Catalog=V6Accounting;Integrated Security=True";
        //private const ushort PageIndex = 1;
        //private const ushort PageSize = 10;

        //private IPaymentMethodDataFarmer m_Farmer;

        //[TestInitialize]
        //public void TestInitialize()
        //{
        //    RuntimeModelFactory.DefinitionLoader = new DbModelDefinitionManager(ConnectionString);
        //    RuntimeModelFactory.DefinitionLoader.LoadAll();
        //    m_Farmer = new PaymentMethodFarmer(ConnectionString);
        //}

        //[TestMethod]
        //public void Search_NoCriteria_FullList()
        //{
        //    // Arrage

        //    // Act
        //    ulong total;
        //    IList<RuntimeModel> actualList = m_Farmer.SearchPaymentMethod(
        //        new List<FieldIndex>{FieldIndex.Code, FieldIndex.Name, FieldIndex.Status}, 
        //        null, PageIndex, PageSize, out total);

        //    // Assert
        //    Assert.IsNotNull(actualList);
        //    Assert.AreEqual(6,actualList.Count);
        //}

        //[TestMethod]
        //public void Search_ContainCriteria_MatchedList()
        //{
        //    // Arrage
        //    var criteria = new List<SearchCriterion>()
        //    {
        //        new SearchCriterion() {
        //            FieldIndex = (byte)FieldIndex.Code,
        //            CompareOperator = CompareOperator.Equal,
        //            ConditionValue = "CK"
        //        }
        //    };
        //    // Act
        //    ulong total;
        //    IList<RuntimeModel> matchedList = m_Farmer.SearchPaymentMethod(
        //        new List<FieldIndex>{FieldIndex.Code, FieldIndex.Name, FieldIndex.Status},
        //        criteria,PageIndex,PageSize,out total);

        //    // Assert
        //    Assert.IsNotNull(matchedList);
        //    Assert.AreEqual(1,matchedList.Count);
        //}

        //[TestMethod]
        //public void Search_Pagging_MatchedList()
        //{
        //    // Arrange
        //    var criteria = new List<SearchCriterion>()
        //    {
        //        new SearchCriterion()
        //        {
        //            FieldIndex = (byte)FieldIndex.Status,
        //            CompareOperator = CompareOperator.Equal,
        //            ConditionValue = 1
        //        }
        //    };
        //    ulong total;
        //    // Act
        //    IList<RuntimeModel> matchedList = m_Farmer.SearchPaymentMethod(
        //        new List<FieldIndex> { FieldIndex.Code, FieldIndex.Name},
        //        criteria, PageIndex, 3, out total);
            
        //    // Assert
        //    Assert.AreEqual(3,matchedList.Count);
        //    Assert.AreEqual(2,total/3);
        //}

        //[TestMethod]
        //public void Insert_AllFields_Success()
        //{
        //    // Arrage
        //    var newModel = RuntimeModelFactory.CreateModel((ushort)ModelIndex.PaymentMethod);
        //    newModel.SetField((byte)FieldIndex.ID, Guid.NewGuid());
        //    newModel.SetField((byte)FieldIndex.Code,"UT");
        //    newModel.SetField((byte)FieldIndex.Name, "Hạn Thanh Toán - UnitTest");
        //    newModel.SetField((byte)FieldIndex.Status, 0);
        //    newModel.SetField((byte)FieldIndex.Note, "Hạn Thanh Toán - Ghi Chú - Unit Test");
        //    newModel.SetField((byte)FieldIndex.OtherName, "Tên khác - Hạn thanh toán ghi chú 1 - Unit Test");
        //    newModel.SetField((byte)FieldIndex.Maturity, 0);

        //    // Act
        //    bool result = m_Farmer.AddPaymentMethod(newModel);

        //    // Assert
        //    Assert.AreEqual(true,result);
        //}

        //[TestMethod]
        //public void Insert_MandantoryFields_Success()
        //{
        //    // Arrange
        //    var newModel = RuntimeModelFactory.CreateModel((ushort) ModelIndex.PaymentMethod);
        //    newModel.SetField((byte)FieldIndex.ID, Guid.NewGuid());
        //    newModel.SetField((byte)FieldIndex.Code, "TT");
        //    newModel.SetField((byte)FieldIndex.Name, "Hạn Thanh Toán - UnitTest");
        //    newModel.SetField((byte)FieldIndex.Status, 1);
        //    newModel.SetField((byte)FieldIndex.OtherName, "Tên Khác - UnitTest");

        //    // Act
        //    bool result = m_Farmer.AddPaymentMethod(newModel);

        //    // Assert
        //    Assert.AreEqual(true,result);
        //}
    }
}
