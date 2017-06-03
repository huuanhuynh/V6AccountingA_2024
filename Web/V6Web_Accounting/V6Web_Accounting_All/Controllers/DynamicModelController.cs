using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V6Soft.Common.ModelFactory;
using V6Soft.Common.ModelFactory.Factories;
using V6Soft.Web.Common.Models;
using FieldNames = V6Soft.Common.ModelFactory.Constants.DefinitionName.Fields;


namespace V6Soft.Web.Accounting.Controllers
{
    public abstract class DynamicModelController : Controller
    {
        private Dictionary<string, IEnumerable<string>> DemoColumns;

        public DynamicModelController()
        {
            DemoColumns = new Dictionary<string, IEnumerable<string>>()
            {
                {
                    "Group",
                    new List<string> { FieldNames.Code, FieldNames.Name, FieldNames.Note,
                        FieldNames.Status}
                },
                {
                    "Customer",
                    new List<string> { FieldNames.Code, FieldNames.Name, FieldNames.Note,
                        FieldNames.Status}
                }
            };
        }
        
        //
        // GET: {{Names.BaseRoute}}/DynamicModel/List
        protected ActionResult List(string modelName, IDictionary<string, string> parameters)
        {
            var sb = new System.Text.StringBuilder();
            parameters.Select(pair =>
            {
                sb.Append(string.Format("data-{0}='{1}' ",
                    pair.Key, pair.Value));
                return string.Empty;
            }).ToList();
            

            var model = new DynamicTableModel()
            {
                Columns = DemoColumns[modelName],
                Settings = parameters
            };
            return View("~/Views/Shared/_List.cshtml", model);
        }

        //
        // GET: {{Names.BaseRoute}}/DynamicModel/Edit
        protected ActionResult Edit(DynamicModel model, ushort modelIndex,
            IDictionary<string, string> settings = null)
        {
            ModelDefinition modelDef = DynamicModelFactory.DefinitionLoader.Load(modelIndex);
            ModelMap modelMap = DynamicModelFactory.DefinitionLoader.GetMapping(modelIndex);
            ResolveDefinition(modelIndex, out modelDef, out modelMap);

            object value;
            var groups = new List<string>();
            var viewModel = new DynamicFormViewModel();
            DynamicFieldViewModel fieldModel;

            viewModel.Fields = new List<DynamicFieldViewModel>();
            viewModel.Groups = modelMap.FieldGroups;
            viewModel.Settings = settings ?? new Dictionary<string, string>();

            foreach (var fieldDef in modelDef.Fields)
            {
                value = null;
                if (model != null)
                {
                    model.TryGetMember(fieldDef.Name, out value);
                }
                fieldModel = new DynamicFieldViewModel(fieldDef, value);
                viewModel.Fields.Add(fieldModel);
            }
            
            viewModel.Settings["title"] = "Editing " + modelMap.NameMapping.AppName; //TODO: Should localize
            return View("~/Views/Shared/_Edit.cshtml", viewModel);
        }

        protected DynamicFormViewModel PrepareModel(DynamicModel model, ushort modelIndex)
        {
            ModelDefinition modelDef = DynamicModelFactory.DefinitionLoader.Load(modelIndex);
            ModelMap modelMap = DynamicModelFactory.DefinitionLoader.GetMapping(modelIndex);
            var viewModel = new DynamicFormViewModel();
            DynamicFieldViewModel fieldModel;
            object value;

            viewModel.Fields = new List<DynamicFieldViewModel>();
            viewModel.Groups = modelMap.FieldGroups;
            viewModel.Settings = new Dictionary<string, string>();

            foreach (var fieldDef in modelDef.Fields)
            {
                value = null;
                if (model != null)
                {
                    model.TryGetMember(fieldDef.Name, out value);
                }
                fieldModel = new DynamicFieldViewModel(fieldDef, value);
                viewModel.Fields.Add(fieldModel);
            }

            return viewModel;
        }


        private void ResolveDefinition(ushort modelIndex, out ModelDefinition modelDef, 
            out ModelMap modelMap)
        {
            modelDef = DynamicModelFactory.DefinitionLoader.Load(modelIndex);
            modelMap = DynamicModelFactory.DefinitionLoader.GetMapping(modelIndex);
        }
    }
}
