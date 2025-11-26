using BuildingBlocks.DataAccessAbstraction.Models;
using BuildingBlocks.DataAccessAbstraction.Services;
using BuildingBlocks.Domain.Security;
using System.Linq.Expressions;
using System.Reflection;

namespace BuildingBlocks.DataAccessAbstraction.Queries
{
    public class QueryFilters<T> where T : class
    {
        private readonly IUserContextService _currentUserService;

        public QueryFilters(/*ICurrentUserService currentUserService*/)
        {
            //_currentUserService = currentUserService;
        }

        public List<FilterExpression> Filters { get; set; } = new();
        public bool UseOrLogic { get; set; } = false;

        public Expression<Func<T, bool>> BuildFilterExpression()
        {
            if (!Filters.Any()) return x => true;

            var parameter = Expression.Parameter(typeof(T), "x");

            Expression combinedExpression = null;

            foreach (var filter in Filters)
            {
                if (!IsFilterAllowed(filter)) continue; // skip disallowed filters

                var expression = filter.BuildExpression<T>(parameter);

                if (combinedExpression == null)
                {
                    combinedExpression = expression;
                }
                else
                {
                    combinedExpression = UseOrLogic ? Expression.OrElse(combinedExpression, expression) : Expression.AndAlso(combinedExpression, expression);
                }
            }

            return Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);
        }

        private bool IsFilterAllowed(FilterExpression filter)
        {
            var propertyInfo = GetPropertyInfo(filter.PropertyName);

            if (propertyInfo == null) return false;

            var filterableAttr = propertyInfo.GetCustomAttribute<FilterableAttribute>();

            if (filterableAttr?.IsFilterable == false) return false;

            // Check role-based access
            if (filterableAttr?.AllowedRoles?.Length > 0)
            {
                var userRoles = _currentUserService.GetRoles();

                if (!filterableAttr.AllowedRoles.Any(role => userRoles.Contains(role))) return false;
            }

            return true;
        }

        private PropertyInfo? GetPropertyInfo(string propertyName)
        {
            var properties = typeof(T).GetProperties();

            var propInfo = properties.FirstOrDefault(p => p.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));

            return propInfo;
        }
    }
}
