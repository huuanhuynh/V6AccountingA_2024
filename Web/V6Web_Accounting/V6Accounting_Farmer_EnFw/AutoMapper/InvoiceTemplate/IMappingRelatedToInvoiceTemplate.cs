using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.InvoiceTemplate
{
    public interface IMappingRelatedToInvoiceTemplate
    {
        T MapToAlkh<T>(Models.Accounting.DTO.InvoiceTemplate model);
        T MapToInvoiceTemplate<T>(ALMAUHD model);
    }
}
