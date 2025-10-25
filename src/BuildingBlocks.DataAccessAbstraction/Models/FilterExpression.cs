using BuildingBlocks.DataAccessAbstraction.Enums;
using System.Linq.Expressions;

namespace BuildingBlocks.DataAccessAbstraction.Models
{
    public class FilterExpression
    {
        public string PropertyName { get; set; } = default!;
        public FilterOperator Operator { get; set; }
        public object Value { get; set; } = default!;

        public Expression BuildExpression<T>(ParameterExpression parameter)
        {
            var property = Expression.Property(parameter, PropertyName);
            var constant = Expression.Constant(Value);

            return Operator switch
            {
                FilterOperator.EQUAL => Expression.Equal(property, constant),
                FilterOperator.CONTAINS => BuildContainsExpression(property, constant),
                FilterOperator.GREATER_THAN => Expression.GreaterThan(property, constant),
                FilterOperator.LESS_THAN => Expression.LessThan(property, constant),
                _ => Expression.Equal(property, constant)
            };
        }

        private Expression BuildContainsExpression(MemberExpression property, ConstantExpression constant)
        {
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            return Expression.Call(property, containsMethod, constant);
        }
    }
}
