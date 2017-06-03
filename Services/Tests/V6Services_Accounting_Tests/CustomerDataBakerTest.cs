using System;
using System.Collections.Generic;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using V6Soft.Common.ModelFactory;
using V6Soft.Common.ModelFactory.Factories;
using V6Soft.Common.ModelFactory.Managers;
using V6Soft.Common.ModelFactory.Validators;
using V6Soft.Services.Accounting.Interfaces;
using V6Soft.Services.Accounting.DataBakers;


namespace V6Soft.Services.Accounting.DataFarmers.Tests
{
    [TestClass]
    public class CustomerDataFarmerTest
    {

        private readonly Guid VictimGuid = Guid.Parse("A3F49DC3-E1A6-4B27-8D53-4B943880FD16");

        private const string ConnectionString = @"Data Source=.;Initial Catalog=V6Accounting;Integrated Security=True";


        private Mock<ICustomerDataFarmer> m_DataFarmerMock;
        private CustomerDataBaker m_DataBaker;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
        }

        [TestInitialize]
        public void TestInitialize()
        {
            DynamicModelFactory.DefinitionLoader = new DbModelDefinitionManager(ConnectionString);
            DynamicModelFactory.DefinitionLoader.LoadAll();
            m_DataFarmerMock = new Mock<ICustomerDataFarmer>();
            m_DataBaker = new CustomerDataBaker(m_DataFarmerMock.Object);
        }


        #region AddCustomerGroup

        [TestMethod]
        public void AddCustomerGroup_InsertInvalidFields_Fail()
        {
            // Arrange
            DynamicModelFactory.DefinitionLoader = new MockModelDefinitionManager();
            DynamicModelFactory.DynamicModelValidator = new DefaultModelValidator();
            var model = DynamicModelFactory.CreateModel(0);
            Exception expectedException = null;
            bool result = false;

            model.SetField(0, Guid.NewGuid()); // Field 0 (UID) will be overriden by real DataFarmer
            model.SetField(1, ""); // this field has not null constraint, but allows empty string.
            model.SetField(2, "12"); // this field has length constraint from 5 to 10.

            // Act
            try
            {
                result = m_DataBaker.AddCustomerGroup(model);
            }
            catch (Exception cve)
            {
                expectedException = cve;
            }

            // Assert
            Assert.IsInstanceOfType(expectedException, typeof(ConstraintViolationException));
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddCustomerGroup_InsertValidFields_CallDataFarmer()
        {
            // Arrange
            DynamicModelFactory.DefinitionLoader = new MockModelDefinitionManager();
            DynamicModelFactory.DynamicModelValidator = new DefaultModelValidator();
            var model = DynamicModelFactory.CreateModel(0);
            Exception expectedException = null;
            bool result = false;

            model.SetField(0, Guid.NewGuid()); // Field 0 (UID) will be overriden by real DataFarmer
            model.SetField(1, ""); // this field has not null constraint, but allows empty string.
            model.SetField(2, "12345"); // this field has length constraint from 5 to 10.

            m_DataFarmerMock
                .Setup(farmer => farmer.AddCustomerGroup(model))
                .Returns(true);

            // Act
            try
            {
                result = m_DataBaker.AddCustomerGroup(model);
            }
            catch (Exception cve)
            {
                expectedException = cve;
            }

            // Assert
            Assert.IsNull(expectedException);
            Assert.IsTrue(result);
            m_DataFarmerMock.Verify(farmer => farmer.AddCustomerGroup(model),
                Times.Once());
        }

        #endregion



        #region ModifyCustomerGroup

        [TestMethod]
        public void ModifyCustomerGroup_NoSpefifyUIDFields_Fail()
        {
            // Arrange
            DynamicModelFactory.DefinitionLoader = new MockModelDefinitionManager();
            DynamicModelFactory.DynamicModelValidator = new DefaultModelValidator();
            var model = DynamicModelFactory.CreateModel(0);
            Exception expectedException = null;
            bool result = false;

            model.SetField(1, ""); // Valid - this field has not null constraint, but allows empty string.
            model.SetField(2, "12"); // Valid - this field has length constraint from 5 to 10.

            // Act
            try
            {
                result = m_DataBaker.ModifyCustomerGroup(model);
            }
            catch (Exception cve)
            {
                expectedException = cve;
            }

            // Assert
            Assert.IsInstanceOfType(expectedException, typeof(ArgumentNullException));
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ModifyCustomerGroup_InsertInvalidFields_Fail()
        {
            // Arrange
            DynamicModelFactory.DefinitionLoader = new MockModelDefinitionManager();
            DynamicModelFactory.DynamicModelValidator = new DefaultModelValidator();
            var model = DynamicModelFactory.CreateModel(0);
            Exception expectedException = null;
            bool result = false;

            model.SetField(0, Guid.NewGuid()); // Field 0 (UID).
            model.SetField(1, ""); // Valid - this field has not null constraint, but allows empty string.
            model.SetField(2, "12"); // INVALID - this field has length constraint from 5 to 10.

            // Act
            try
            {
                result = m_DataBaker.ModifyCustomerGroup(model);
            }
            catch (Exception cve)
            {
                expectedException = cve;
            }

            // Assert
            Assert.IsInstanceOfType(expectedException, typeof(ConstraintViolationException));
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ModifyCustomerGroup_InsertValidFields_CallDataFarmer()
        {
            // Arrange
            DynamicModelFactory.DefinitionLoader = new MockModelDefinitionManager();
            DynamicModelFactory.DynamicModelValidator = new DefaultModelValidator();
            var model = DynamicModelFactory.CreateModel(0);
            Exception expectedException = null;
            bool result = false;

            model.SetField(0, Guid.NewGuid()); // Field 0 (UID).
            model.SetField(1, ""); // Valid - this field has not null constraint, but allows empty string.
            model.SetField(2, "12456"); // Valid - this field has length constraint from 5 to 10.

            m_DataFarmerMock
                .Setup(farmer => farmer.ModifyCustomerGroup(model))
                .Returns(true);

            // Act
            try
            {
                result = m_DataBaker.ModifyCustomerGroup(model);
            }
            catch (Exception cve)
            {
                expectedException = cve;
            }

            // Assert
            Assert.IsNull(expectedException);
            Assert.IsTrue(result);
            m_DataFarmerMock.Verify(farmer => farmer.ModifyCustomerGroup(model),
                Times.Once());
        }

        #endregion


        private class MockModelDefinitionManager : IModelDefinitionManager
        {
            private IList<ModelDefinition> m_CachedDefinitions;


            public MockModelDefinitionManager()
            {
                LoadAll();
            }


            public ModelDefinition this[string modelName]
            {
                get { return Load(modelName); }
            }

            public ModelDefinition this[ushort definitionIndex]
            {
                get { return Load(definitionIndex); }
            }

            public bool AddField(ushort modelIndex, IList<ModelFieldDefinition> newFields)
            {
                throw new NotImplementedException();
            }

            public ModelDefinition Load(string modelName)
            {
                return m_CachedDefinitions[0];
            }

            public ModelDefinition Load(ushort definitionIndex)
            {
                return m_CachedDefinitions[(int)definitionIndex];
            }

            public IList<ModelDefinition> LoadAll()
            {
                m_CachedDefinitions = new List<ModelDefinition>();
                var modelDefinition = new ModelDefinition("MockDefinition1");

                // 0. Mock UID field
                var fieldDef = new ModelFieldDefinition("MockUID", "GuidField",
                    typeof(Guid), null);

                modelDefinition.Fields.Add(fieldDef);

                // 1. Mock not-null string field
                var constraints = new List<IFieldConstraint>()
                {
                    new NotNullFieldConstraint()
                };

                fieldDef = new ModelFieldDefinition("MockField1", "StringField",
                    typeof(string), null, constraints);

                modelDefinition.Fields.Add(fieldDef);

                // 2. Mock length-limited string field
                constraints = new List<IFieldConstraint>()
                {
                    new LengthConstraint(5, 10)
                };

                fieldDef = new ModelFieldDefinition("MockField2", "LimitedStringField",
                    typeof(string), null, constraints);

                modelDefinition.Fields.Add(fieldDef);

                m_CachedDefinitions.Add(modelDefinition);
                return m_CachedDefinitions;
            }

            public ModelMap GetMapping(ushort index)
            {
                throw new NotImplementedException();
            }

            public IList<ModelMap> GetAllMappings()
            {
                throw new NotImplementedException();
            }
        }
    }
}
