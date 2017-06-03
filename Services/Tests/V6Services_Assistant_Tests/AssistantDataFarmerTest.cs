using Microsoft.VisualStudio.TestTools.UnitTesting;

using ModelIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Model;


namespace V6Services_Assistant_Tests
{
    [TestClass]
    public class AssistantDataFarmerTest
    {
        //private readonly Guid VictimGuid = Guid.Parse("A3F49DC3-E1A6-4B27-8D53-4B943880FD16");

        //private const string ConnectionString = @"Data Source=.;Initial Catalog=V6Accounting;Integrated Security=True";

        ///// <summary>
        /////     Value: "A!B#C$"
        ///// </summary>
        //private const string MockString = "A!B#C$";

        ///// <summary>
        /////     Value: 10
        ///// </summary>
        //private const int MockCount = 10;

        ///// <summary>
        /////     Value: 0
        ///// </summary>
        //private const int UidFieldIndex = 0;


        //private IAssistantDataFarmer m_Farmer;

        //[ClassInitialize]
        //public static void InitializeClass(TestContext context)
        //{
        //}

        //[TestInitialize]
        //public void TestInitialize()
        //{
        //    RuntimeModelFactory.DefinitionLoader = new DbModelDefinitionManager(ConnectionString);
        //    RuntimeModelFactory.DefinitionLoader.LoadAll();
        //    m_Farmer = new AssistantDataFarmer(ConnectionString);
        //}

        
        //#region GetProvinces

        ////[TestMethod]
        //public void SearchProvinces_NoCriteria_FullList()
        //{
        //    // Arrange
        //    ushort pageIndex = 1;
        //    ushort pageSize = 10;

        //    // Act
        //    ulong total;
        //    IList<RuntimeModel> actualList = m_Farmer.SearchProvinces(new List<FieldIndex> { FieldIndex.Name, FieldIndex.Code, FieldIndex.Status }, null, pageIndex, pageSize, out total);

        //    // Assert
        //    Assert.IsNotNull(actualList);
        //    Assert.IsTrue(actualList.Count <= pageSize);
        //}

        ////[TestMethod]
        //public void SearchProvinces_ContainCriteria_MatchedList()
        //{
        //    // Arrange
        //    var criteria = new List<SearchCriterion>()
        //    {
        //        new SearchCriterion() {
        //            FieldIndex = (byte)FieldIndex.Status,
        //            CompareOperator = CompareOperator.Equal,
        //            ConditionValue = true
        //        },
        //        new SearchCriterion() {
        //            FieldIndex = (byte)FieldIndex.Name,
        //            CompareOperator = CompareOperator.Contain,
        //            ConditionValue = @"Nội"
        //        }
        //    };
        //    ushort pageIndex = 1;
        //    ushort pageSize = 10;

        //    // Act
        //    ulong total;
        //    IList<RuntimeModel> actualList = m_Farmer.SearchProvinces(new List<FieldIndex> { FieldIndex.Name, FieldIndex.Code, FieldIndex.Status }, criteria, pageIndex, pageSize, out total);

        //    // Assert
        //    Assert.IsNotNull(actualList);
        //    Assert.IsTrue(actualList.Count <= pageSize);
        //}

        ////[TestMethod]
        //public void SearchProvinces_BeginCriteria_MatchedList()
        //{
        //    // Arrange
        //    var criteria = new List<SearchCriterion>()
        //    {
        //        new SearchCriterion() {
        //            FieldIndex = (byte)FieldIndex.Status,
        //            CompareOperator = CompareOperator.Equal,
        //            ConditionValue = true
        //        },
        //        new SearchCriterion() {
        //            FieldIndex = (byte)FieldIndex.Name,
        //            CompareOperator = CompareOperator.BeginWith,
        //            ConditionValue = @"Hà"
        //        }
        //    };
        //    ushort pageIndex = 1;
        //    ushort pageSize = 10;

        //    // Act
        //    ulong total;
        //    IList<RuntimeModel> actualList = m_Farmer.SearchProvinces(new List<FieldIndex> { FieldIndex.Name }, criteria, pageIndex, pageSize, out total);

        //    // Assert
        //    Assert.IsNotNull(actualList);
        //    Assert.IsTrue(actualList.Count <= pageSize);
        //}

        ////[TestMethod]
        //public void SearchProvinces_Paging_MatchedList()
        //{
        //    // Arrange
        //    var criteria = new List<SearchCriterion>()
        //    {
        //        new SearchCriterion() {
        //            FieldIndex = (byte)FieldIndex.Status,
        //            CompareOperator = CompareOperator.NotEqual,
        //            ConditionValue = false
        //        }
        //    };
        //    ushort pageIndex = 2;
        //    ushort pageSize = 3;

        //    // Act
        //    ulong total;
        //    IList<RuntimeModel> actualList = m_Farmer.SearchProvinces(new List<FieldIndex> { FieldIndex.Name }, criteria, pageIndex, pageSize, out total);

        //    // Assert
        //    Assert.IsNotNull(actualList);
        //    Assert.IsTrue(actualList.Count <= pageSize);
        //}

        //#endregion


        //#region GetDistricts

        ////[TestMethod]
        //public void SearchDistricts_NoCriteria_FullList()
        //{
        //    // Arrange
        //    ushort pageIndex = 1;
        //    ushort pageSize = 10;

        //    // Act
        //    ulong total;
        //    IList<RuntimeModel> actualList = m_Farmer.SearchDistricts(new List<FieldIndex> { FieldIndex.Name, FieldIndex.Code, FieldIndex.Status }, null, pageIndex, pageSize, out total);

        //    // Assert
        //    Assert.IsNotNull(actualList);
        //    Assert.IsTrue(actualList.Count <= pageSize);
        //}

        ////[TestMethod]
        //public void SearchDistricts_ContainCriteria_MatchedList()
        //{
        //    // Arrange
        //    var criteria = new List<SearchCriterion>()
        //    {
        //        new SearchCriterion() {
        //            FieldIndex = (byte)FieldIndex.Status,
        //            CompareOperator = CompareOperator.Equal,
        //            ConditionValue = true
        //        },
        //        new SearchCriterion() {
        //            FieldIndex = (byte)FieldIndex.Name,
        //            CompareOperator = CompareOperator.Contain,
        //            ConditionValue = @"Nội"
        //        }
        //    };
        //    ushort pageIndex = 1;
        //    ushort pageSize = 10;

        //    // Act
        //    ulong total;
        //    IList<RuntimeModel> actualList = m_Farmer.SearchDistricts(new List<FieldIndex> { FieldIndex.Name, FieldIndex.Code, FieldIndex.Status }, criteria, pageIndex, pageSize, out total);

        //    // Assert
        //    Assert.IsNotNull(actualList);
        //    Assert.IsTrue(actualList.Count <= pageSize);
        //}

        ////[TestMethod]
        //public void SearchDistricts_BeginCriteria_MatchedList()
        //{
        //    // Arrange
        //    var criteria = new List<SearchCriterion>()
        //    {
        //        new SearchCriterion() {
        //            FieldIndex = (byte)FieldIndex.Status,
        //            CompareOperator = CompareOperator.Equal,
        //            ConditionValue = true
        //        },
        //        new SearchCriterion() {
        //            FieldIndex = (byte)FieldIndex.Name,
        //            CompareOperator = CompareOperator.BeginWith,
        //            ConditionValue = @"Hà"
        //        }
        //    };
        //    ushort pageIndex = 1;
        //    ushort pageSize = 10;

        //    // Act
        //    ulong total;
        //    IList<RuntimeModel> actualList = m_Farmer.SearchDistricts(new List<FieldIndex> { FieldIndex.Name }, criteria, pageIndex, pageSize, out total);

        //    // Assert
        //    Assert.IsNotNull(actualList);
        //    Assert.IsTrue(actualList.Count <= pageSize);
        //}

        ////[TestMethod]
        //public void SearchDistricts_Paging_MatchedList()
        //{
        //    // Arrange
        //    var criteria = new List<SearchCriterion>()
        //    {
        //        new SearchCriterion() {
        //            FieldIndex = (byte)FieldIndex.Status,
        //            CompareOperator = CompareOperator.NotEqual,
        //            ConditionValue = false
        //        }
        //    };
        //    ushort pageIndex = 2;
        //    ushort pageSize = 3;

        //    // Act
        //    ulong total;
        //    IList<RuntimeModel> actualList = m_Farmer.SearchDistricts(new List<FieldIndex> { FieldIndex.Name }, criteria, pageIndex, pageSize, out total);

        //    // Assert
        //    Assert.IsNotNull(actualList);
        //    Assert.IsTrue(actualList.Count <= pageSize);
        //}

        //#endregion


        //#region GetWards

        ////[TestMethod]
        //public void SearchWards_NoCriteria_FullList()
        //{
        //    // Arrange
        //    ushort pageIndex = 1;
        //    ushort pageSize = 10;

        //    // Act
        //    ulong total;
        //    IList<RuntimeModel> actualList = m_Farmer.SearchDistricts(new List<FieldIndex> { FieldIndex.Name, FieldIndex.Code, FieldIndex.Status }, null, pageIndex, pageSize, out total);

        //    // Assert
        //    Assert.IsNotNull(actualList);
        //    Assert.IsTrue(actualList.Count <= pageSize);
        //}

        ////[TestMethod]
        //public void SearchWards_ContainCriteria_MatchedList()
        //{
        //    // Arrange
        //    var criteria = new List<SearchCriterion>()
        //    {
        //        new SearchCriterion() {
        //            FieldIndex = (byte)FieldIndex.Status,
        //            CompareOperator = CompareOperator.Equal,
        //            ConditionValue = true
        //        },
        //        new SearchCriterion() {
        //            FieldIndex = (byte)FieldIndex.Name,
        //            CompareOperator = CompareOperator.Contain,
        //            ConditionValue = @"Nội"
        //        }
        //    };
        //    ushort pageIndex = 1;
        //    ushort pageSize = 10;

        //    // Act
        //    ulong total;
        //    IList<RuntimeModel> actualList = m_Farmer.SearchWards(new List<FieldIndex> { FieldIndex.Name, FieldIndex.Code, FieldIndex.Status }, criteria, pageIndex, pageSize, out total);

        //    // Assert
        //    Assert.IsNotNull(actualList);
        //    Assert.IsTrue(actualList.Count <= pageSize);
        //}

        ////[TestMethod]
        //public void SearchWards_BeginCriteria_MatchedList()
        //{
        //    // Arrange
        //    var criteria = new List<SearchCriterion>()
        //    {
        //        new SearchCriterion() {
        //            FieldIndex = (byte)FieldIndex.Status,
        //            CompareOperator = CompareOperator.Equal,
        //            ConditionValue = true
        //        },
        //        new SearchCriterion() {
        //            FieldIndex = (byte)FieldIndex.Name,
        //            CompareOperator = CompareOperator.BeginWith,
        //            ConditionValue = @"Hà"
        //        }
        //    };
        //    ushort pageIndex = 1;
        //    ushort pageSize = 10;

        //    // Act
        //    ulong total;
        //    IList<RuntimeModel> actualList = m_Farmer.SearchWards(new List<FieldIndex> { FieldIndex.Name }, criteria, pageIndex, pageSize, out total);

        //    // Assert
        //    Assert.IsNotNull(actualList);
        //    Assert.IsTrue(actualList.Count <= pageSize);
        //}

        ////[TestMethod]
        //public void SearchWards_Paging_MatchedList()
        //{
        //    // Arrange
        //    var criteria = new List<SearchCriterion>()
        //    {
        //        new SearchCriterion() {
        //            FieldIndex = (byte)FieldIndex.Status,
        //            CompareOperator = CompareOperator.NotEqual,
        //            ConditionValue = false
        //        }
        //    };
        //    ushort pageIndex = 2;
        //    ushort pageSize = 3;

        //    // Act
        //    ulong total;
        //    IList<RuntimeModel> actualList = m_Farmer.SearchWards(new List<FieldIndex> { FieldIndex.Name }, criteria, pageIndex, pageSize, out total);

        //    // Assert
        //    Assert.IsNotNull(actualList);
        //    Assert.IsTrue(actualList.Count <= pageSize);
        //}

        //#endregion


        //#region AddProvince

        ////[TestMethod]
        //public void AddProvince_InsertAllFields_Success()
        //{
        //    var newModel = RuntimeModelFactory.CreateModel((ushort)ModelIndex.Province);

        //    // This field is expected to be replaced by DataFarmer
        //    newModel.SetField((byte)FieldIndex.ID, Guid.NewGuid());
        //    newModel.SetField((byte)FieldIndex.Code, "Code0");
        //    newModel.SetField((byte)FieldIndex.Name, "Name0");
        //    newModel.SetField((byte)FieldIndex.Status, 1);
        //    newModel.SetField((byte)FieldIndex.Note, "This is a very long note. Oh, sorry, not very long.");

        //    bool result = m_Farmer.AddProvince(newModel);

        //    Assert.IsTrue(result);
        //    Assert.IsNotNull(newModel.GetField(UidFieldIndex));
        //}

        ////[TestMethod]
        //public void AddProvince_InsertOnlyMandatoryFields_Success()
        //{
        //    var newModel = RuntimeModelFactory.CreateModel((ushort)ModelIndex.Province);

        //    // This field is expected to be removed by DataFarmer
        //    newModel.SetField((byte)FieldIndex.ID, Guid.NewGuid());
        //    newModel.SetField((byte)FieldIndex.Code, "Code1");
        //    newModel.SetField((byte)FieldIndex.Name, "Name1");
        //    newModel.SetField((byte)FieldIndex.Status, false);

        //    bool result = m_Farmer.AddProvince(newModel);

        //    Assert.IsTrue(result);
        //    Assert.IsNotNull(newModel.GetField(UidFieldIndex));
        //}

        ////[TestMethod]
        //public void AddProvince_NotInsertMandatoryFields_SqlException()
        //{
        //    // Arrange
        //    var newModel = RuntimeModelFactory.CreateModel((ushort)ModelIndex.Province);
        //    Exception expectedException = null;

        //    // Act
        //    newModel.SetField((byte)FieldIndex.Code, "Code2");

        //    try
        //    {
        //        m_Farmer.AddProvince(newModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        expectedException = ex;
        //    }
            
        //    // Assert
        //    Assert.IsInstanceOfType(expectedException, typeof(SqlException));
        //    Assert.IsTrue(expectedException.Message.Contains("Cannot insert the value NULL into column"));
        //}

        ////[TestMethod]
        //public void AddProvince_ExistedCode_SqlException()
        //{
        //    // Arrange
        //    var newModel = RuntimeModelFactory.CreateModel((ushort)ModelIndex.Province);
        //    Exception expectedException = null;

        //    // Act
        //    newModel.SetField((byte)FieldIndex.Code, "CodeAA");
        //    newModel.SetField((byte)FieldIndex.Name, "NameAA");
        //    newModel.SetField((byte)FieldIndex.Status, false);
        //    m_Farmer.AddProvince(newModel);

        //    try
        //    {
        //        // Adds again to cause a unique key conflict
        //        m_Farmer.AddProvince(newModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        expectedException = ex;
        //    }

        //    // Assert
        //    Assert.IsInstanceOfType(expectedException, typeof(SqlException));
        //    Assert.IsTrue(expectedException.Message.Contains("Cannot insert duplicate key"));
        //}

        //#endregion


        //#region AddDistrict

        ////[TestMethod]
        //public void AddDistrict_InsertAllFields_Success()
        //{
        //    var newModel = RuntimeModelFactory.CreateModel((ushort)ModelIndex.District);

        //    // This field is expected to be replaced by DataFarmer
        //    newModel.SetField((byte)FieldIndex.ID, Guid.NewGuid());
        //    newModel.SetField((byte)FieldIndex.Code, "Code0");
        //    newModel.SetField((byte)FieldIndex.Name, "Name0");
        //    newModel.SetField((byte)FieldIndex.Status, 1);
        //    newModel.SetField((byte)FieldIndex.Note, "This is a very long note. Oh, sorry, not very long.");

        //    bool result = m_Farmer.AddDistrict(newModel);

        //    Assert.IsTrue(result);
        //    Assert.IsNotNull(newModel.GetField(UidFieldIndex));
        //}

        ////[TestMethod]
        //public void AddDistrict_InsertOnlyMandatoryFields_Success()
        //{
        //    var newModel = RuntimeModelFactory.CreateModel((ushort)ModelIndex.District);

        //    // This field is expected to be removed by DataFarmer
        //    newModel.SetField((byte)FieldIndex.ID, Guid.NewGuid());
        //    newModel.SetField((byte)FieldIndex.Code, "Code1");
        //    newModel.SetField((byte)FieldIndex.Name, "Name1");
        //    newModel.SetField((byte)FieldIndex.Status, false);

        //    bool result = m_Farmer.AddDistrict(newModel);

        //    Assert.IsTrue(result);
        //    Assert.IsNotNull(newModel.GetField(UidFieldIndex));
        //}

        ////[TestMethod]
        //public void AddDistrict_NotInsertMandatoryFields_SqlException()
        //{
        //    // Arrange
        //    var newModel = RuntimeModelFactory.CreateModel((ushort)ModelIndex.District);
        //    Exception expectedException = null;

        //    // Act
        //    newModel.SetField((byte)FieldIndex.Code, "Code2");

        //    try
        //    {
        //        m_Farmer.AddDistrict(newModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        expectedException = ex;
        //    }

        //    // Assert
        //    Assert.IsInstanceOfType(expectedException, typeof(SqlException));
        //    Assert.IsTrue(expectedException.Message.Contains("Cannot insert the value NULL into column"));
        //}

        ////[TestMethod]
        //public void AddDistrict_ExistedCode_SqlException()
        //{
        //    // Arrange
        //    var newModel = RuntimeModelFactory.CreateModel((ushort)ModelIndex.District);
        //    Exception expectedException = null;

        //    // Act
        //    newModel.SetField((byte)FieldIndex.Code, "CodeAA");
        //    newModel.SetField((byte)FieldIndex.Name, "NameAA");
        //    newModel.SetField((byte)FieldIndex.Status, false);
        //    m_Farmer.AddDistrict(newModel);

        //    try
        //    {
        //        // Adds again to cause a unique key conflict
        //        m_Farmer.AddDistrict(newModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        expectedException = ex;
        //    }

        //    // Assert
        //    Assert.IsInstanceOfType(expectedException, typeof(SqlException));
        //    Assert.IsTrue(expectedException.Message.Contains("Cannot insert duplicate key"));
        //}

        //#endregion


        //#region AddWard

        ////[TestMethod]
        //public void AddWard_InsertAllFields_Success()
        //{
        //    var newModel = RuntimeModelFactory.CreateModel((ushort)ModelIndex.Ward);

        //    // This field is expected to be replaced by DataFarmer
        //    newModel.SetField((byte)FieldIndex.ID, Guid.NewGuid());
        //    newModel.SetField((byte)FieldIndex.Code, "Code0");
        //    newModel.SetField((byte)FieldIndex.Name, "Name0");
        //    newModel.SetField((byte)FieldIndex.Status, 1);
        //    newModel.SetField((byte)FieldIndex.Note, "This is a very long note. Oh, sorry, not very long.");

        //    bool result = m_Farmer.AddWard(newModel);

        //    Assert.IsTrue(result);
        //    Assert.IsNotNull(newModel.GetField(UidFieldIndex));
        //}

        ////[TestMethod]
        //public void AddWard_InsertOnlyMandatoryFields_Success()
        //{
        //    var newModel = RuntimeModelFactory.CreateModel((ushort)ModelIndex.Ward);

        //    // This field is expected to be removed by DataFarmer
        //    newModel.SetField((byte)FieldIndex.ID, Guid.NewGuid());
        //    newModel.SetField((byte)FieldIndex.Code, "Code1");
        //    newModel.SetField((byte)FieldIndex.Name, "Name1");
        //    newModel.SetField((byte)FieldIndex.Status, false);

        //    bool result = m_Farmer.AddWard(newModel);

        //    Assert.IsTrue(result);
        //    Assert.IsNotNull(newModel.GetField(UidFieldIndex));
        //}

        ////[TestMethod]
        //public void AddWard_NotInsertMandatoryFields_SqlException()
        //{
        //    // Arrange
        //    var newModel = RuntimeModelFactory.CreateModel((ushort)ModelIndex.Ward);
        //    Exception expectedException = null;

        //    // Act
        //    newModel.SetField((byte)FieldIndex.Code, "Code2");

        //    try
        //    {
        //        m_Farmer.AddWard(newModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        expectedException = ex;
        //    }

        //    // Assert
        //    Assert.IsInstanceOfType(expectedException, typeof(SqlException));
        //    Assert.IsTrue(expectedException.Message.Contains("Cannot insert the value NULL into column"));
        //}

        ////[TestMethod]
        //public void AddWard_ExistedCode_SqlException()
        //{
        //    // Arrange
        //    var newModel = RuntimeModelFactory.CreateModel((ushort)ModelIndex.Ward);
        //    Exception expectedException = null;

        //    // Act
        //    newModel.SetField((byte)FieldIndex.Code, "CodeAA");
        //    newModel.SetField((byte)FieldIndex.Name, "NameAA");
        //    newModel.SetField((byte)FieldIndex.Status, false);
        //    m_Farmer.AddWard(newModel);

        //    try
        //    {
        //        // Adds again to cause a unique key conflict
        //        m_Farmer.AddWard(newModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        expectedException = ex;
        //    }

        //    // Assert
        //    Assert.IsInstanceOfType(expectedException, typeof(SqlException));
        //    Assert.IsTrue(expectedException.Message.Contains("Cannot insert duplicate key"));
        //}

        //#endregion


        //#region ModifyProvince

        ////[TestMethod]
        //public void ModifyProvince_ChangeAllFields_Success()
        //{
        //    var model = RuntimeModelFactory.CreateModel((ushort)ModelIndex.Province);
        //    model.SetField((byte)FieldIndex.ID, VictimGuid);
        //    model.SetField((byte)FieldIndex.Code, "CdCode0");
        //    model.SetField((byte)FieldIndex.Name, "Changed Name0");
        //    model.SetField((byte)FieldIndex.Status, true);
        //    model.SetField((byte)FieldIndex.Note, "Changed note to make it shorter.");

        //    bool result = m_Farmer.ModifyProvince(model);
        //    Assert.IsTrue(result);
        //}

        //#endregion


        //#region ModifyDistrict

        ////[TestMethod]
        //public void ModifyDistrict_ChangeAllFields_Success()
        //{
        //    var model = RuntimeModelFactory.CreateModel((ushort)ModelIndex.District);
        //    model.SetField((byte)FieldIndex.ID, VictimGuid);
        //    model.SetField((byte)FieldIndex.Code, "CdCode0");
        //    model.SetField((byte)FieldIndex.Name, "Changed Name0");
        //    model.SetField((byte)FieldIndex.Status, true);
        //    model.SetField((byte)FieldIndex.Note, "Changed note to make it shorter.");

        //    bool result = m_Farmer.ModifyDistrict(model);
        //    Assert.IsTrue(result);
        //}

        //#endregion


        //#region ModifyWard

        ////[TestMethod]
        //public void ModifyWard_ChangeAllFields_Success()
        //{
        //    var model = RuntimeModelFactory.CreateModel((ushort)ModelIndex.Ward);
        //    model.SetField((byte)FieldIndex.ID, VictimGuid);
        //    model.SetField((byte)FieldIndex.Code, "CdCode0");
        //    model.SetField((byte)FieldIndex.Name, "Changed Name0");
        //    model.SetField((byte)FieldIndex.Status, true);
        //    model.SetField((byte)FieldIndex.Note, "Changed note to make it shorter.");

        //    bool result = m_Farmer.ModifyWard(model);
        //    Assert.IsTrue(result);
        //}

        //#endregion


        //#region RemoveProvince

        ////[TestMethod]
        //public void RemoveProvince_ChangeAllFields_Success()
        //{
        //    var model = RuntimeModelFactory.CreateModel((ushort)ModelIndex.Province);
        //    model.SetField((byte)FieldIndex.ID, VictimGuid);

        //    bool result = m_Farmer.RemoveProvince(VictimGuid);
        //    Assert.IsTrue(result);
        //}

        //#endregion


        //#region RemoveDistrict

        ////[TestMethod]
        //public void RemovDistrict_ChangeAllFields_Success()
        //{
        //    var model = RuntimeModelFactory.CreateModel((ushort)ModelIndex.District);
        //    model.SetField((byte)FieldIndex.ID, VictimGuid);

        //    bool result = m_Farmer.RemoveDistrict(VictimGuid);
        //    Assert.IsTrue(result);
        //}

        //#endregion


        //#region RemoveWard

        ////[TestMethod]
        //public void RemoveWard_ChangeAllFields_Success()
        //{
        //    var model = RuntimeModelFactory.CreateModel((ushort)ModelIndex.Ward);
        //    model.SetField((byte)FieldIndex.ID, VictimGuid);

        //    bool result = m_Farmer.RemoveWard(VictimGuid);
        //    Assert.IsTrue(result);
        //}

        //#endregion
        
    }
}
