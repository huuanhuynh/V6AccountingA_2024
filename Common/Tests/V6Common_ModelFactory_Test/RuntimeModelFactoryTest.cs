using System;
using System.Web.Http.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using V6Soft.Common.ModelFactory;
using V6Soft.Common.ModelFactory.Factories;
using V6Soft.Common.ModelFactory.Managers;
using V6Soft.Models.Core;


namespace V6Soft.Common.ModelFactory.Test
{
    [TestClass]
    public class RuntimeModelFactoryTest
    {
        private const string ConnectionString = @"Data Source=.;Initial Catalog=V6Accounting;Integrated Security=True";

        //*
        [TestMethod]
        [ExpectedException(typeof(NoDefinitionException))]
        public void CreateModel_NonExistModelName_Exception()
        {
            // Arrange
            byte definitionIndex = 99;
            IModelDefinitionManager loader = PrepareDefinitionLoader();
            RuntimeModelFactory.DefinitionLoader = loader;
            //RuntimeModelFactory.ModelValidator = new ModelValidator();
            //RuntimeModelFactory.FieldValidator = new FieldValidator();
            
            // Act
            loader.LoadAll();
            RuntimeModel model = RuntimeModelFactory.CreateModel(definitionIndex);
        }

        [TestMethod]
        [ExpectedException(typeof(NoDefinitionException))]
        public void CreateModel_NoPreLoadAll_Exception()
        {
            // Arrange
            byte definitionIndex = 99;
            IModelDefinitionManager loader = PrepareDefinitionLoader();
            RuntimeModelFactory.DefinitionLoader = loader;
            //RuntimeModelFactory.ModelValidator = new ModelValidator();
            //RuntimeModelFactory.FieldValidator = new FieldValidator();

            // Act
            //loader.LoadAll(); // No preloading!
            RuntimeModel model = RuntimeModelFactory.CreateModel(definitionIndex);
        }

        [TestMethod]
        public void CreateModel_ModelName_ModelInstance()
        {
            // Arrange
            byte definitionIndex = 1;
            IModelDefinitionManager loader = PrepareDefinitionLoader();
            RuntimeModelFactory.DefinitionLoader = loader;
            //RuntimeModelFactory.ModelValidator = new ModelValidator();
            //RuntimeModelFactory.FieldValidator = new FieldValidator();

            // Act
            loader.LoadAll();
            RuntimeModel model = RuntimeModelFactory.CreateModel(definitionIndex);

            // Assert
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Definition);
            Assert.IsNotNull(model.Fields);
        }

        [TestMethod]
        public void CreateDynamicModel_ModelName_ModelInstance()
        {
            // Arrange
            byte definitionIndex = 0;
            IModelDefinitionManager loader = PrepareDefinitionLoader();
            DynamicModelFactory.DefinitionLoader = loader;
            //RuntimeModelFactory.ModelValidator = new ModelValidator();
            //RuntimeModelFactory.FieldValidator = new FieldValidator();

            // Act
            loader.LoadAll();
            DynamicModel model = DynamicModelFactory.CreateModel(definitionIndex);

            // Assert
            Assert.IsNotNull(model);
            //Assert.IsNotNull(model.Definition);
            Assert.IsNotNull(model.Fields);
        }


        private IModelDefinitionManager PrepareDefinitionLoader()
        {
            return new DbModelDefinitionManager(ConnectionString);
        }
        //*/
    }
}
