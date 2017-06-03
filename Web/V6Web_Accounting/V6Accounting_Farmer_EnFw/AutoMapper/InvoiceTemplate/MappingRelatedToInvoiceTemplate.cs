using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.InvoiceTemplate
{
    public class MappingRelatedToInvoiceTemplate : IMappingRelatedToInvoiceTemplate
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.InvoiceTemplate model)
        {
            return Mapper.Map<Models.Accounting.DTO.InvoiceTemplate, T>(model);
        }

        public T MapToInvoiceTemplate<T>(ALMAUHD model)
        {
            return Mapper.Map<ALMAUHD, T>(model);
        }
    }
}
