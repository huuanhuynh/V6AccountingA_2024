
using V6Soft.Common.ModelFactory;

using FieldIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Field;


namespace V6Soft.Models.Core.RuntimeModelExtensions
{
    /// <summary>
    ///     Provides some methods to work particularly with V6 models
    /// </summary>
    public static class RuntimeModelExtensions
    {
        /// <summary>
        ///     Gets value of the field at specified index.
        /// </summary>
        public static object GetField(this RuntimeModel model, FieldIndex index)
        {
            return model.GetField((byte)index);
        }
                
        /// <summary>
        ///     Sets value for the field at specified index.
        /// </summary>
        public static void SetField(this RuntimeModel model, FieldIndex index, 
            object value)
        {
            model.SetField((byte)index, value);
        }
    }
}
