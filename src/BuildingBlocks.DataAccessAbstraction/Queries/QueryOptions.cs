using System.Linq.Expressions;

namespace BuildingBlocks.DataAccessAbstraction.Queries
{
    public class QueryOptions<T> where T : class
    {
        private int _pageSize = 20;
        private int _maxPageSize = 100;
        private string _sortBy = "CreatedAt";

        public int PageIndex { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > _maxPageSize) ? _maxPageSize : value;
        }

        public string SortBy
        {
            get => _sortBy;
            set => _sortBy = string.IsNullOrWhiteSpace(value) ? _sortBy : value;
        }

        public bool SortDescending { get; set; } = false;

        public int Skip => (PageIndex - 1) * PageSize;

        public int Take => PageSize;

        public QueryFilters<T> QueryFilters { get; set; } = default!;

        public IQueryable<T> ApplySorting(IQueryable<T> query)
        {
            if (string.IsNullOrEmpty(SortBy)) return query;

            // Check if the property exists (case-insensitive)
            var hasProperty = typeof(T).GetProperties().Any(p => p.Name.Equals(SortBy, StringComparison.OrdinalIgnoreCase));

            if (!hasProperty) return query;

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, SortBy);
            var lambda = Expression.Lambda(property, parameter);

            var methodName = SortDescending ? "OrderByDescending" : "OrderBy";

            var resultExpression = Expression.Call(typeof(Queryable), methodName, new[] { typeof(T), property.Type }, query.Expression, Expression.Quote(lambda));

            return query.Provider.CreateQuery<T>(resultExpression);
        }

    }
}
