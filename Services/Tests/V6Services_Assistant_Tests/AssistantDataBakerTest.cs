using System;
using System.Collections.Generic;

using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using V6Soft.Common.ModelFactory;
using V6Soft.Common.ModelFactory.Factories;
using V6Soft.Common.ModelFactory.Managers;
using V6Soft.Common.ModelFactory.Validators;
using V6Soft.Services.Assistant.Interfaces;
using V6Soft.Services.Assistant.DataBakers;


namespace V6Soft.Services.Assistant.DataFarmers.Tests
{
    [TestClass]
    public class AssistantDataFarmerTest
    {

        //private readonly Guid VictimGuid = Guid.Parse("A3F49DC3-E1A6-4B27-8D53-4B943880FD16");

        //private const string ConnectionString = @"Data Source=.;Initial Catalog=V6Accounting;Integrated Security=True";


        //private Mock<IAssistantDataFarmer> m_DataFarmerMock;
        //private AssistantDataBaker m_DataBaker;

        //[ClassInitialize]
        //public static void InitializeClass(TestContext context)
        //{
        //}

        //[TestInitialize]
        //public void TestInitialize()
        //{
        //    DynamicModelFactory.DefinitionLoader = new DbModelDefinitionManager(ConnectionString);
        //    DynamicModelFactory.DefinitionLoader.LoadAll();
        //    m_DataFarmerMock = new Mock<IAssistantDataFarmer>();
        //    m_DataBaker = new AssistantDataBaker(m_DataFarmerMock.Object);
        //}


        //#region AddProvince

        //[TestMethod]
        //public void AddProvince_InsertInvalidFields_Fail()
        //{
        //    // Arrange
        //    DynamicModelFactory.DefinitionLoader = new MockModelDefinitionManager();
        //    //DynamicModelFactory.DynamicModelValidator = new DefaultModelValidator();
        //    var model = DynamicModelFactory.CreateModel(0);
        //    Exception expectedException = null;
        //    bool result = false;

        //    model.SetField(0, Guid.NewGuid()); // Field 0 (UID) will be overriden by real DataFarmer
        //    model.SetField(1, ""); // this field has not null constraint, but allows empty string.
        //    model.SetField(2, "12"); // this field has length constraint from 5 to 10.

        //    // Act
        //    try
        //    {
        //        result = m_DataBaker.AddProvince(model);
        //    }
        //    catch (Exception cve)
        //    {
        //        expectedException = cve;
        //    }

        //    // Assert
        //    Assert.IsInstanceOfType(expectedException, typeof(ConstraintViolationException));
        //    Assert.IsFalse(result);
        //}

        //[TestMethod]
        //public void AddProvince_InsertValidFields_CallDataFarmer()
        //{
        //    // Arrange
        //    RuntimeModelFactory.DefinitionLoader = new MockModelDefinitionManager();
        //    RuntimeModelFactory.ModelValidator = new DefaultModelValidator();
        //    var model = RuntimeModelFactory.CreateModel(0);
        //    Exception expectedException = null;
        //    bool result = false;

        //    model.SetField(0, Guid.NewGuid()); // Field 0 (UID) will be overriden by real DataFarmer
        //    model.SetField(1, ""); // this field has not null constraint, but allows empty string.
        //    model.SetField(2, "12345"); // this field has length constraint from 5 to 10.

        //    m_DataFarmerMock
        //        .Setup(farmer => farmer.AddProvince(model))
        //        .Returns(true);

        //    // Act
        //    try
        //    {
        //        result = m_DataBaker.AddProvince(model);
        //    }
        //    catch (Exception cve)
        //    {
        //        expectedException = cve;
        //    }

        //    // Assert
        //    Assert.IsNull(expectedException);
        //    Assert.IsTrue(result);
        //    m_DataFarmerMock.Verify(farmer => farmer.AddProvince(model), 
        //        Times.Once());
        //}

        //#endregion


        //#region AddDistrict

        //[TestMethod]
        //public void AddDistrict_InsertInvalidFields_Fail()
        //{
        //    // Arrange
        //    RuntimeModelFactory.DefinitionLoader = new MockModelDefinitionManager();
        //    RuntimeModelFactory.ModelValidator = new DefaultModelValidator();
        //    var model = RuntimeModelFactory.CreateModel(0);
        //    Exception expectedException = null;
        //    bool result = false;

        //    model.SetField(0, Guid.NewGuid()); // Field 0 (UID) will be overriden by real DataFarmer
        //    model.SetField(1, ""); // this field has not null constraint, but allows empty string.
        //    model.SetField(2, "12"); // this field has length constraint from 5 to 10.

        //    // Act
        //    try
        //    {
        //        result = m_DataBaker.AddDistrict(model);
        //    }
        //    catch (Exception cve)
        //    {
        //        expectedException = cve;
        //    }

        //    // Assert
        //    Assert.IsInstanceOfType(expectedException, typeof(ConstraintViolationException));
        //    Assert.IsFalse(result);
        //}

        //[TestMethod]
        //public void AAddDistrict_InsertValidFields_CallDataFarmer()
        //{
        //    // Arrange
        //    RuntimeModelFactory.DefinitionLoader = new MockModelDefinitionManager();
        //    RuntimeModelFactory.ModelValidator = new DefaultModelValidator();
        //    var model = RuntimeModelFactory.CreateModel(0);
        //    Exception expectedException = null;
        //    bool result = false;

        //    model.SetField(0, Guid.NewGuid()); // Field 0 (UID) will be overriden by real DataFarmer
        //    model.SetField(1, ""); // this field has not null constraint, but allows empty string.
        //    model.SetField(2, "12345"); // this field has length constraint from 5 to 10.

        //    m_DataFarmerMock
        //        .Setup(farmer => farmer.AddDistrict(model))
        //        .Returns(true);

        //    // Act
        //    try
        //    {
        //        result = m_DataBaker.AddDistrict(model);
        //    }
        //    catch (Exception cve)
        //    {
        //        expectedException = cve;
        //    }

        //    // Assert
        //    Assert.IsNull(expectedException);
        //    Assert.IsTrue(result);
        //    m_DataFarmerMock.Verify(farmer => farmer.AddDistrict(model), Times.Once());
        //}

        //#endregion


        //#region AddWard

        //[TestMethod]
        //public void AddWard_InsertInvalidFields_Fail()
        //{
        //    // Arrange
        //    RuntimeModelFactory.DefinitionLoader = new MockModelDefinitionManager();
        //    RuntimeModelFactory.ModelValidator = new DefaultModelValidator();
        //    var model = RuntimeModelFactory.CreateModel(0);
        //    Exception expectedException = null;
        //    bool result = false;

        //    model.SetField(0, Guid.NewGuid()); // Field 0 (UID) will be overriden by real DataFarmer
        //    model.SetField(1, ""); // this field has not null constraint, but allows empty string.
        //    model.SetField(2, "12"); // this field has length constraint from 5 to 10.

        //    // Act
        //    try
        //    {
        //        result = m_DataBaker.AddWard(model);
        //    }
        //    catch (Exception cve)
        //    {
        //        expectedException = cve;
        //    }

        //    // Assert
        //    Assert.IsInstanceOfType(expectedException, typeof(ConstraintViolationException));
        //    Assert.IsFalse(result);
        //}

        //[TestMethod]
        //public void AddWard_InsertValidFields_CallDataFarmer()
        //{
        //    // Arrange
        //    RuntimeModelFactory.DefinitionLoader = new MockModelDefinitionManager();
        //    RuntimeModelFactory.ModelValidator = new DefaultModelValidator();
        //    var model = RuntimeModelFactory.CreateModel(0);
        //    Exception expectedException = null;
        //    bool result = false;

        //    model.SetField(0, Guid.NewGuid()); // Field 0 (UID) will be overriden by real DataFarmer
        //    model.SetField(1, ""); // this field has not null constraint, but allows empty string.
        //    model.SetField(2, "12345"); // this field has length constraint from 5 to 10.

        //    m_DataFarmerMock.Setup(farmer => farmer.AddWard(model)).Returns(true);

        //    // Act
        //    try
        //    {
        //        result = m_DataBaker.AddWard(model);
        //    }
        //    catch (Exception cve)
        //    {
        //        expectedException = cve;
        //    }

        //    // Assert
        //    Assert.IsNull(expectedException);
        //    Assert.IsTrue(result);
        //    m_DataFarmerMock.Verify(farmer => farmer.AddWard(model), Times.Once());
        //}

        //#endregion
         

        //#region ModifyProvince

        //[TestMethod]
        //public void ModifyProvince_NoSpefifyUIDFields_Fail()
        //{
        //    // Arrange
        //    RuntimeModelFactory.DefinitionLoader = new MockModelDefinitionManager();
        //    RuntimeModelFactory.ModelValidator = new DefaultModelValidator();
        //    var model = RuntimeModelFactory.CreateModel(0);
        //    Exception expectedException = null;
        //    bool result = false;

        //    model.SetField(1, ""); // Valid - this field has not null constraint, but allows empty string.
        //    model.SetField(2, "12345"); // Valid - this field has length constraint from 5 to 10.

        //    // Act
        //    try
        //    {
        //        result = m_DataBaker.ModifyProvince(model);
        //    }
        //    catch (Exception cve)
        //    {
        //        expectedException = cve;
        //    }

        //    // Assert
        //    Assert.IsInstanceOfType(expectedException, typeof(ArgumentNullException));
        //    Assert.IsFalse(result);
        //}

        //[TestMethod]
        //public void ModifyProvince_InsertInvalidFields_Fail()
        //{
        //    // Arrange
        //    RuntimeModelFactory.DefinitionLoader = new MockModelDefinitionManager();
        //    RuntimeModelFactory.ModelValidator = new DefaultModelValidator();
        //    var model = RuntimeModelFactory.CreateModel(0);
        //    Exception expectedException = null;
        //    bool result = false;
            
        //    model.SetField(0, Guid.NewGuid()); // Field 0 (UID).
        //    model.SetField(1, ""); // Valid - this field has not null constraint, but allows empty string.
        //    model.SetField(2, "12"); // INVALID - this field has length constraint from 5 to 10.

        //    // Act
        //    try
        //    {
        //        result = m_DataBaker.ModifyProvince(model);
        //    }
        //    catch (Exception cve)
        //    {
        //        expectedException = cve;
        //    }

        //    // Assert
        //    Assert.IsInstanceOfType(expectedException, typeof(ConstraintViolationException));
        //    Assert.IsFalse(result);
        //}

        //[TestMethod]
        //public void ModifyProvince_InsertValidFields_CallDataFarmer()
        //{
        //    // Arrange
        //    RuntimeModelFactory.DefinitionLoader = new MockModelDefinitionManager();
        //    RuntimeModelFactory.ModelValidator = new DefaultModelValidator();
        //    var model = RuntimeModelFactory.CreateModel(0);
        //    Exception expectedException = null;
        //    bool result = false;

        //    model.SetField(0, Guid.NewGuid()); // Field 0 (UID).
        //    model.SetField(1, ""); // Valid - this field has not null constraint, but allows empty string.
        //    model.SetField(2, "12345"); // Valid - this field has length constraint from 5 to 10.

        //    m_DataFarmerMock.Setup(farmer => farmer.ModifyProvince(model)).Returns(true);

        //    // Act
        //    try
        //    {
        //        result = m_DataBaker.ModifyProvince(model);
        //    }
        //    catch (Exception cve)
        //    {
        //        expectedException = cve;
        //    }

        //    // Assert
        //    Assert.IsNull(expectedException);
        //    Assert.IsTrue(result);
        //    m_DataFarmerMock.Verify(farmer => farmer.ModifyProvince(model), Times.Once());
        //}

        //#endregion


        //#region ModifyDistrict

        //[TestMethod]
        //public void ModifyDistrict_NoSpefifyUIDFields_Fail()
        //{
        //    // Arrange
        //    RuntimeModelFactory.DefinitionLoader = new MockModelDefinitionManager();
        //    RuntimeModelFactory.ModelValidator = new DefaultModelValidator();
        //    var model = RuntimeModelFactory.CreateModel(0);
        //    Exception expectedException = null;
        //    bool result = false;

        //    model.SetField(1, ""); // Valid - this field has not null constraint, but allows empty string.
        //    model.SetField(2, "12345"); // Valid - this field has length constraint from 5 to 10.

        //    // Act
        //    try
        //    {
        //        result = m_DataBaker.ModifyDistrict(model);
        //    }
        //    catch (Exception cve)
        //    {
        //        expectedException = cve;
        //    }

        //    // Assert
        //    Assert.IsInstanceOfType(expectedException, typeof(ArgumentNullException));
        //    Assert.IsFalse(result);
        //}

        //[TestMethod]
        //public void ModifyDistrict_InsertInvalidFields_Fail()
        //{
        //    // Arrange
        //    RuntimeModelFactory.DefinitionLoader = new MockModelDefinitionManager();
        //    RuntimeModelFactory.ModelValidator = new DefaultModelValidator();
        //    var model = RuntimeModelFactory.CreateModel(0);
        //    Exception expectedException = null;
        //    bool result = false;

        //    model.SetField(0, Guid.NewGuid()); // Field 0 (UID).
        //    model.SetField(1, ""); // Valid - this field has not null constraint, but allows empty string.
        //    model.SetField(2, "12"); // INVALID - this field has length constraint from 5 to 10.

        //    // Act
        //    try
        //    {
        //        result = m_DataBaker.ModifyDistrict(model);
        //    }
        //    catch (Exception cve)
        //    {
        //        expectedException = cve;
        //    }

        //    // Assert
        //    Assert.IsInstanceOfType(expectedException, typeof(ConstraintViolationException));
        //    Assert.IsFalse(result);
        //}

        //[TestMethod]
        //public void ModifyDistrict_InsertValidFields_CallDataFarmer()
        //{
        //    // Arrange
        //    RuntimeModelFactory.DefinitionLoader = new MockModelDefinitionManager();
        //    RuntimeModelFactory.ModelValidator = new DefaultModelValidator();
        //    var model = RuntimeModelFactory.CreateModel(0);
        //    Exception expectedException = null;
        //    bool result = false;

        //    model.SetField(0, Guid.NewGuid()); // Field 0 (UID).
        //    model.SetField(1, ""); // Valid - this field has not null constraint, but allows empty string.
        //    model.SetField(2, "12345"); // Valid - this field has length constraint from 5 to 10.

        //    m_DataFarmerMock.Setup(farmer => farmer.ModifyDistrict(model)).Returns(true);

        //    // Act
        //    try
        //    {
        //        result = m_DataBaker.ModifyDistrict(model);
        //    }
        //    catch (Exception cve)
        //    {
        //        expectedException = cve;
        //    }

        //    // Assert
        //    Assert.IsNull(expectedException);
        //    Assert.IsTrue(result);
        //    m_DataFarmerMock.Verify(farmer => farmer.ModifyDistrict(model), Times.Once());
        //}

        //#endregion


        //#region ModifyWard

        //[TestMethod]
        //public void ModifyWard_NoSpefifyUIDFields_Fail()
        //{
        //    // Arrange
        //    RuntimeModelFactory.DefinitionLoader = new MockModelDefinitionManager();
        //    RuntimeModelFactory.ModelValidator = new DefaultModelValidator();
        //    var model = RuntimeModelFactory.CreateModel(0);
        //    Exception expectedException = null;
        //    bool result = false;

        //    model.SetField(1, ""); // Valid - this field has not null constraint, but allows empty string.
        //    model.SetField(2, "12345"); // Valid - this field has length constraint from 5 to 10.

        //    // Act
        //    try
        //    {
        //        result = m_DataBaker.ModifyWard(model);
        //    }
        //    catch (Exception cve)
        //    {
        //        expectedException = cve;
        //    }

        //    // Assert
        //    Assert.IsInstanceOfType(expectedException, typeof(ArgumentNullException));
        //    Assert.IsFalse(result);
        //}

        //[TestMethod]
        //public void ModifyWard_InsertInvalidFields_Fail()
        //{
        //    // Arrange
        //    RuntimeModelFactory.DefinitionLoader = new MockModelDefinitionManager();
        //    RuntimeModelFactory.ModelValidator = new DefaultModelValidator();
        //    var model = RuntimeModelFactory.CreateModel(0);
        //    Exception expectedException = null;
        //    bool result = false;

        //    model.SetField(0, Guid.NewGuid()); // Field 0 (UID).
        //    model.SetField(1, ""); // Valid - this field has not null constraint, but allows empty string.
        //    model.SetField(2, "12"); // INVALID - this field has length constraint from 5 to 10.

        //    // Act
        //    try
        //    {
        //        result = m_DataBaker.ModifyWard(model);
        //    }
        //    catch (Exception cve)
        //    {
        //        expectedException = cve;
        //    }

        //    // Assert
        //    Assert.IsInstanceOfType(expectedException, typeof(ConstraintViolationException));
        //    Assert.IsFalse(result);
        //}

        //[TestMethod]
        //public void ModifyWard_InsertValidFields_CallDataFarmer()
        //{
        //    // Arrange
        //    RuntimeModelFactory.DefinitionLoader = new MockModelDefinitionManager();
        //    RuntimeModelFactory.ModelValidator = new DefaultModelValidator();
        //    var model = RuntimeModelFactory.CreateModel(0);
        //    Exception expectedException = null;
        //    bool result = false;

        //    model.SetField(0, Guid.NewGuid()); // Field 0 (UID).
        //    model.SetField(1, ""); // Valid - this field has not null constraint, but allows empty string.
        //    model.SetField(2, "12345"); // Valid - this field has length constraint from 5 to 10.

        //    m_DataFarmerMock.Setup(farmer => farmer.ModifyWard(model)).Returns(true);

        //    // Act
        //    try
        //    {
        //        result = m_DataBaker.ModifyWard(model);
        //    }
        //    catch (Exception cve)
        //    {
        //        expectedException = cve;
        //    }

        //    // Assert
        //    Assert.IsNull(expectedException);
        //    Assert.IsTrue(result);
        //    m_DataFarmerMock.Verify(farmer => farmer.ModifyWard(model), Times.Once());
        //}

        //#endregion


        //private class MockModelDefinitionManager : IModelDefinitionManager
        //{
        //    private IList<ModelDefinition> m_CachedDefinitions;


        //    public MockModelDefinitionManager()
        //    {
        //        LoadAll();
        //    }


        //    public ModelDefinition this[string modelName]
        //    {
        //        get { return Load(modelName); }
        //    }

        //    public ModelDefinition this[ushort definitionIndex]
        //    {
        //        get { return Load(definitionIndex); }
        //    }

        //    public bool AddField(ushort modelIndex, IList<ModelFieldDefinition> newFields)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public ModelDefinition Load(string modelName)
        //    {
        //        return m_CachedDefinitions[0];
        //    }

        //    public ModelDefinition Load(ushort definitionIndex)
        //    {
        //        return m_CachedDefinitions[(int)definitionIndex];
        //    }

        //    public IList<ModelDefinition> LoadAll()
        //    {
        //        m_CachedDefinitions = new List<ModelDefinition>();
        //        var modelDefinition = new ModelDefinition("MockDefinition1");

        //        // 0. Mock UID field
        //        var fieldDef = new ModelFieldDefinition("MockUID", "GuidField", 
        //            typeof(Guid), null);
                
        //        modelDefinition.Fields.Add(fieldDef);

        //        // 1. Mock not-null string field
        //        var constraints = new List<IFieldConstraint>()
        //        {
        //            new NotNullFieldConstraint()
        //        };

        //        fieldDef = new ModelFieldDefinition("MockField1", "StringField",
        //            typeof(string), null, constraints);

        //        modelDefinition.Fields.Add(fieldDef);

        //        // 2. Mock length-limited string field
        //        constraints = new List<IFieldConstraint>()
        //        {
        //            new LengthConstraint(5, 10)
        //        };

        //        fieldDef = new ModelFieldDefinition("MockField2", "LimitedStringField",
        //            typeof(string), null, constraints);
                
        //        modelDefinition.Fields.Add(fieldDef);

        //        m_CachedDefinitions.Add(modelDefinition);
        //        return m_CachedDefinitions;
        //    }

        //    public ModelMap GetMapping(ushort index)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public IList<ModelMap> GetAllMappings()
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
    }
}
