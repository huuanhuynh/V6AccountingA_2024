﻿using System;
using System.Collections.Generic;
using System.Linq;


namespace V6Soft.WebApi.Accounting.Models
{
    public class ModelQueryable : IOrderedQueryable<object>
    {

        public IEnumerator<object> GetEnumerator()
        {
 	        throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
 	        throw new NotImplementedException();
        }

        public Type ElementType
        {
	        get { throw new NotImplementedException(); }
        }

        public System.Linq.Expressions.Expression Expression
        {
	        get { throw new NotImplementedException(); }
        }

        public IQueryProvider Provider
        {
	        get { throw new NotImplementedException(); }
        }
    }





    public class DTOContext : ISummerBreezeDbContext
    {
        public void SaveChanges(SaveWorkState saveWorkState)
        {
            //get your custom saving logic here, for demo purposes this will be very simplistic...
            EntityInfo info = saveWorkState.EntitiesWithAutoGeneratedKeys.Where(e => e.Entity.GetType() == typeof(object)).FirstOrDefault();

            var dto = (info.Entity as object);

            //get dbcontext
            var context = new object();

            //map dto to product
            //context.Products.Add(new object()
            //{
            //    Name = dto.Name,
            //    PicUrl = dto.PicUrl,
            //    Price = dto.Price,
            //    RegisteredDate = DateTime.Now,
            //    Tags = dto.Name
            //});

            ////save changes on the db
            //context.SaveChanges();



            // Add new keymappings to breeze (remember we told breeze to generate a temp id, this is the way breeze knows about the new generated id)
            List<KeyMapping> mappings = new List<KeyMapping>();
            mappings.Add(new KeyMapping()
            {
                EntityTypeName = dto.GetType().FullName,
                //RealValue = context.Products.Where(p => p.Name == dto.Name).FirstOrDefault().ProductId,
                //TempValue = dto.ProductDTOId

            });

            saveWorkState.KeyMappings = mappings;



        }

        public Dictionary<Type, List<EntityInfo>> BeforeSaveEntities(Dictionary<Type, List<EntityInfo>> saveMap)
        {
            return saveMap;
        }

        public bool BeforeSaveEntity(EntityInfo entityInfo)
        {
            return true;
        }

        public List<string> GetTrackedEntities()
        {
            var list = new List<string>();
            list.Add("ProductDTO");

            return list;
        }
    }
}
