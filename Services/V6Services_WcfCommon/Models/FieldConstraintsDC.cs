using System.Runtime.Serialization;


namespace V6Soft.Services.Wcf.Common.Models
{
    public interface IFieldConstraintDC
    {
    }

    [DataContract]
    public class NotNullFieldConstraintDC : IFieldConstraintDC
    {
    }

    [DataContract]
    public class LengthConstraintDC : IFieldConstraintDC
    {
        /// <summary>
        ///     Gets or sets minimum length constraint.
        /// </summary>
        [DataMember]
        public int MinLength { get; set; }

        /// <summary>
        ///     Gets or sets maximum length constraint.
        /// </summary>
        [DataMember]
        public int MaxLength { get; set; }
        
    }
}
