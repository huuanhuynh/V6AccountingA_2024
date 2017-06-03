using System.Collections.Generic;


namespace V6Soft.Common.ModelFactory.Validators
{
    /// <summary>
    ///     Should be called before submiting model instance to service,
    ///     or before inserting to database.
    /// </summary>
    public interface IModelValidator
    {
        /// <summary>
        ///     When implemented by derived class, should use the constraints of
        ///     model field definition to check model's field values, but not
        ///     neccessary. Validation can be done with custom rules, depending
        ///     on implementation.
        ///     <para/>Returns true when all field values are validated. False if not.
        /// </summary>
        /// <param name="model">Must not be null and neither are its fields.</param>
        bool Validate(DynamicModel model);

        bool Validate(DynamicModel model, IList<byte> excludedFieldIndeses);
    }
}
