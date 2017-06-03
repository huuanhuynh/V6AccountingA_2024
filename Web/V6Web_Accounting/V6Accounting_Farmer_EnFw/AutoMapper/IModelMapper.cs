namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper
{
    public interface IModelMapper
    {
        /// <summary>
        ///     Translates from a type of model to another type.
        /// </summary>
         TTo Map<TFrom, TTo>(TFrom source);
    }
}
