
namespace V6Soft.Models.Core.Filters
{
    /// <summary>
    /// Represents a filter condition used to fetch a list of records.
    /// </summary>
    public enum StatusFilter : sbyte
    {
        All = -1,
        Inactive = 0,
        Active = 1
    }
}
