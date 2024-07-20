using FixedDemo.Application.Core.Dtos;
using System.Linq.Expressions;

namespace FixedDemo.Application.Core.Extentions
{
    internal static class FilterExtention
    {
        public static Expression<Func<T, bool>> ToExpression<T>(this FilterDto filter)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, filter.Field);
            var value = Expression.Constant(filter.Value, typeof(string));
            Expression body = filter.Operator switch
            {
                "Equals" => Expression.Equal(property, value),
                "NotEquals" => Expression.NotEqual(property, value),
                "Contains" => Expression.Call(property, "Contains", null, value),
                _ => throw new NotSupportedException($"Operator '{filter.Operator}' is not supported.")
            };
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}
