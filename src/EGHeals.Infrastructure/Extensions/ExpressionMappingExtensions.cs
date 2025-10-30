using System.Linq.Expressions;

namespace EGHeals.Infrastructure.Extensions
{
    public static class ExpressionMappingExtensions
    {
        public static Expression<Func<TDestination, bool>> MapPredicate<TSource, TDestination>(
        Expression<Func<TSource, bool>> sourceExpression)
        {
            var parameter = Expression.Parameter(typeof(TDestination), sourceExpression.Parameters[0].Name);

            var body = new ReplaceParameterVisitor(sourceExpression.Parameters[0], parameter)
                .Visit(sourceExpression.Body);

            return Expression.Lambda<Func<TDestination, bool>>(body!, parameter);
        }

        private class ReplaceParameterVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _oldParam;
            private readonly ParameterExpression _newParam;

            public ReplaceParameterVisitor(ParameterExpression oldParam, ParameterExpression newParam)
            {
                _oldParam = oldParam;
                _newParam = newParam;
            }

            protected override Expression VisitParameter(ParameterExpression node)
                => node == _oldParam ? _newParam : base.VisitParameter(node);
        }
    }
}
