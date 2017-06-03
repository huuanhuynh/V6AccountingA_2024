using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.PaymentMethod
{
    public interface IMappingRelatedToPaymentMethod
    {
        T MapToAlkh<T>(Models.Accounting.DTO.PaymentMethod model);
        T MapToPaymentMethod<T>(Alhttt model);
    }
}
