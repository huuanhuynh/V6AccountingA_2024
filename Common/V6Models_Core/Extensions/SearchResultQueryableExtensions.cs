using System;
using System.Linq;

using V6Soft.Models.Core.ViewModels;


namespace V6Soft.Models.Core.Extensions
{
    public static class SearchResultQueryableExtensions
    {
        [Obsolete]
        public static PagedSearchResult<T> ToPagedSearchResult<T>(this IQueryable<T> source, SearchCriteria criteria)
        {
            var query = source;
            if (criteria.Filter != null && criteria.Filter.Filters != null && criteria.Filter.Filters.Count > 0)
            {
                query = FilterExpressionBuilder.ApplyFiltering(query, criteria.Filter.Filters);
            }

            var total = query.Count();

            if (criteria.Sort != null && criteria.Sort.Count > 0)
            {
                query = SortExpressionBuilder.ApplySorting(query, criteria.Sort);
            }

            query = query.Skip((criteria.Page - 1) * criteria.PageSize).Take(criteria.PageSize);

            return new PagedSearchResult<T>(query.ToList(), total);
        }
    }
}
