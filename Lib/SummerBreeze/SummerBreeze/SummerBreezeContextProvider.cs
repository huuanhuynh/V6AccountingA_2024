using Breeze.WebApi2;
using SummerBreeze;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Breeze.ContextProvider;


namespace SummerBreeze

{
    public class SummerBreezeContextProvider : ContextProvider
    {        
        private Assembly _assembly = null;
        private string _schemaNamespace;

        public ISummerBreezeDbContext Context { get; private set; }
        
       

        public SummerBreezeContextProvider(ISummerBreezeDbContext context, string metadataNamespace, string assemblyName = null)
        {
            Context = context;
            _schemaNamespace = metadataNamespace;

            if (assemblyName == null)
            {
                _assembly = Assembly.GetCallingAssembly();
            }
            else
            {
                try
                {
                    var an = Assembly.GetCallingAssembly().GetReferencedAssemblies().Where(a => a.Name == assemblyName).FirstOrDefault();
                    _assembly = Assembly.Load(an);
                   
                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
            }

        }

        /*base overrides*/

        protected override string BuildJsonMetadata()
        {
            return GetMetadataFromAssembly();
        }

        //protected override List<KeyMapping> SaveChangesCore(Dictionary<Type, List<EntityInfo>> saveMap)
        //{
        //    return _kernel.Get<ISummerBreezeDbContext>().SaveChanges(saveMap);
        //}

        protected override void SaveChangesCore(SaveWorkState saveWorkState)
        {
            Context.SaveChanges(saveWorkState);
            
        }

        protected override Dictionary<Type, List<EntityInfo>> BeforeSaveEntities(Dictionary<Type, List<EntityInfo>> saveMap)
        {
            return Context.BeforeSaveEntities(saveMap);
        }

        protected override bool BeforeSaveEntity(EntityInfo entityInfo)
        {
            return Context.BeforeSaveEntity(entityInfo);
        }

        public List<string> GetTrackedEntities()
        {
            return Context.GetTrackedEntities();
        }

        
        private string GetMetadataFromAssembly()
        {
            var entityMedatataList = new List<BreezeEntityTypeMetadata>();

            var entity = _assembly.GetTypes().Where(x => x.GetCustomAttributes(typeof(BreezeLocalizableAttribute), false).Length > 0).ToList();
            
            entity.ForEach((e) =>
            {
                entityMedatataList.Add(GetEntityTypeMetadata(e));
            });

            var entityMetaData = new BreezeMetadataSchema()
            {
                @namespace = _schemaNamespace,
                entityType = entityMedatataList
            };

            return JsonConvert.SerializeObject(new { schema = entityMetaData });
        }

        private BreezeEntityTypeMetadata GetEntityTypeMetadata(Type t)
        {
            var autogeneratedKeyAttr = t.GetCustomAttributes(typeof(BreezeAutoGeneratedKeyTypeAttribute), false) != null ? (t.GetCustomAttributes(typeof(BreezeAutoGeneratedKeyTypeAttribute), false).FirstOrDefault() as BreezeAutoGeneratedKeyTypeAttribute).AutoGeneratedKeyType.ToString() : null;
            var allUnmapped = ((t.GetCustomAttributes(typeof(BreezeLocalizableAttribute), false).FirstOrDefault() as BreezeLocalizableAttribute).UnMapAll);
            var metadata = new BreezeEntityTypeMetadata();
            
            metadata.name = t.Name;
            metadata.@namespace = t.Namespace;
            metadata.autoGeneratedKeyType = autogeneratedKeyAttr ?? SummerBreezeEnums.AutoGeneratedKeyType.None.ToString();

            var datapropertyMetadataList = new List<BreezeDataPropertyMetadata>();
            var navigationpropertyMetadataList = new List<BreezeNavigationPropertyMetadata>();
            var propertyInfo = GetPropertyInfo(t);
            var keyList = new List<object>();

            propertyInfo.ForEach((i) =>
            {
                
                //Navigation Properties
                var nav = i.GetCustomAttributes(typeof(BreezeNavigationPropertyAttribute), false).FirstOrDefault();
                if (nav != null)
                {
                    navigationpropertyMetadataList.Add(new BreezeNavigationPropertyMetadata
                     {
                         name = i.Name,
                         entityTypeName = i.PropertyType.GetGenericArguments().FirstOrDefault() == null ? i.PropertyType.Name + ":#" + i.PropertyType.Namespace : i.PropertyType.GetGenericArguments().FirstOrDefault().Name + ":#" + i.PropertyType.GetGenericArguments().FirstOrDefault().Namespace,
                         isScalar = (nav as BreezeNavigationPropertyAttribute).ForeignKeyNames != null && (nav as BreezeNavigationPropertyAttribute).ForeignKeyNames.Count() > 0,
                         associationName = (nav as BreezeNavigationPropertyAttribute).Association,
                         foreignKeyNames = (nav as BreezeNavigationPropertyAttribute).ForeignKeyNames
                       

                     });

                  

                }
                else
                {
                    var propType = i.PropertyType;
                    //Data Properties
                    datapropertyMetadataList.Add(new BreezeDataPropertyMetadata
                    {
                        name = i.Name,
                        type = "Edm." + (i.PropertyType.Name.Contains("Nullable") ? Nullable.GetUnderlyingType(i.PropertyType).Name : i.PropertyType.GetGenericArguments().FirstOrDefault() == null ? i.PropertyType.Name : i.PropertyType.GetGenericArguments().FirstOrDefault().Name),
                        nullable = i.GetCustomAttributes(typeof(RequiredAttribute), false) == null || i.GetCustomAttributes(typeof(RequiredAttribute), false).Count() == 0,
                        //propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(Nullable<>),
                        isPartOfKey = i.GetCustomAttributes(typeof(KeyAttribute), false) != null && i.GetCustomAttributes(typeof(KeyAttribute), false).Count() > 0,
                        isUnmapped = allUnmapped ? allUnmapped : i.GetCustomAttributes(typeof(BreezeUnmappedAttribute), false) != null && i.GetCustomAttributes(typeof(BreezeUnmappedAttribute), false).Count() > 0,
                        validators = GetValidatorsForProperty(i)

                    });

                    if (i.GetCustomAttributes(typeof(KeyAttribute), false) != null && i.GetCustomAttributes(typeof(KeyAttribute), false).Count() > 0)
                    {
                        keyList.Add(CreatePropRef(i.Name));
                    }                  
                }
                
                //if (i.GetGetMethod().IsVirtual)
                //{
                //    navigationpropertyMetadataList.Add(new NoDbNavigationPropertyMetadata 
                //    { 
                //         name = i.Name,
                //         entityTypeName = i.PropertyType.Name,
                //         isScalar = true,
                //         associationName = "
                //    });
                //}


                //add navigation properties
                if (navigationpropertyMetadataList.Count > 0)
                {
                    metadata.navigationProperties = navigationpropertyMetadataList;
                }

                //add data properties
                if (datapropertyMetadataList.Count > 0)
                {
                    metadata.property = datapropertyMetadataList;
                }

                if (keyList.Count > 0)
                {
                    metadata.key.propertyRef = keyList;
                }
                
            });

           

            return metadata;

        }

        private object CreatePropRef(string propName)
        {
            return new { name = propName };
        }

        private List<string> GetValidatorsForProperty(PropertyInfo i)
        {
            var validatorsList = new List<string>();

            //Required
            var required = i.GetCustomAttributes(typeof(RequiredAttribute), false);

            if (required != null && required.Length > 0)
            {
                validatorsList.Add("breeze.Validator.required()");
            }

            var maxLength = i.GetCustomAttributes(typeof(MaxLengthAttribute), false);

            //MaxLength
            if (maxLength != null && maxLength.Length > 0)
            {
                validatorsList.Add("breeze.Validator.maxLength({ maxLength:" + (maxLength.FirstOrDefault() as MaxLengthAttribute).Length + "})");
            }

            return validatorsList;

        }

        private List<PropertyInfo> GetPropertyInfo(Type t)
        {
            List<PropertyInfo> info = null;

            t.GetCustomAttributes(typeof(BreezeLocalizableAttribute), true).ToList().ForEach((a) =>
            {
                var attr = (a as BreezeLocalizableAttribute);

                if (attr.Include == null || attr.Include.Length == 0)
                    info = t.GetProperties().ToList(); //all properties
                else
                    info = t.GetProperties().Where(i => attr.Include.Any(o => o.Equals(i.Name))).ToList();  //cherry picked properties
            });


            return info;
        }

        protected override void CloseDbConnection()
        {
           
        }

        public override System.Data.IDbConnection GetDbConnection()
        {
            throw new NotImplementedException();
        }

        protected override void OpenDbConnection()
        {
           
        }

       
    }
}
