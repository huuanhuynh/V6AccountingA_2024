using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.PaymentMethod
{
    public class MappingRelatedToPaymentMethod : IMappingRelatedToPaymentMethod
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.PaymentMethod model)
        {
            return Mapper.Map<Models.Accounting.DTO.PaymentMethod, T>(model);
        }

        public T MapToPaymentMethod<T>(Alhttt model)
        {
            return Mapper.Map<Alhttt, T>(model);
        }
    }
}
