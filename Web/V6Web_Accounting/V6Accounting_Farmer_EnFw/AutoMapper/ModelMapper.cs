using System.Collections.Generic;
using AutoMapper;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper
{
    public class ModelMapper : IModelMapper
    {
        public TTo Map<TFrom, TTo>(TFrom source)
        {
            return Mapper.Map<TFrom, TTo>(source);
        }
    }
}
