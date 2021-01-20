using DNI.Core.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace DNI.Core.Shared.Extensions
{
    public static class SearchCriteriaExtensions
    {
        public static IEnumerable<Tuple<PropertyInfo, object>> GetSearchParameters<T>(this ISearchCriteria<T> searchCriteria)
        {
            var parameters = new List<Tuple<PropertyInfo, object>>();
            var entityType = typeof(T);

            var properties = entityType.GetProperties();

            foreach (var property in properties)
            {
                var value = property.GetValue(searchCriteria.Parameters);
                if (value.IsDefault())
                {
                    continue;
                }

                parameters.Add(Tuple.Create(property, value));
            }

            return parameters.ToArray();
        }

        public static Expression<Func<T, bool>> GetExpression<T>(this ISearchCriteria<T> searchCriteria,
            IEnumerable<Tuple<PropertyInfo, object>> searchParameters = default)
        {
            var entityType = typeof(T);

            var parameters = searchParameters ?? GetSearchParameters(searchCriteria);

            var parameterExpression = Expression.Parameter(entityType, entityType.Name.ToLower());
            Expression expression = default;
            foreach (var parameter in parameters)
            {

                var propertyInfo = parameter.Item1;
                var value = parameter.Item2;

                var constantExpression = Expression.Constant(value);
                var propertyOrFieldExpression = Expression.PropertyOrField(parameterExpression, propertyInfo.Name);
                var type = propertyOrFieldExpression.Type;
                Debug.WriteLine(type);

                Debug.WriteLine(type.BaseType);

                Expression equalExpression = default;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    var hasValueExpression = Expression.Property(propertyOrFieldExpression, "HasValue");
                    var valueExpression = Expression.Property(propertyOrFieldExpression, "Value");

                    var conditionalExpression = Expression.Equal(hasValueExpression, Expression.Constant(false));

                    var optionalExpression = Expression.Equal(valueExpression, constantExpression);

                    equalExpression = Expression.OrElse(conditionalExpression, optionalExpression);
                }
                else
                    equalExpression = Expression.Equal(propertyOrFieldExpression, constantExpression);

                expression = (expression == default)
                    ? equalExpression
                    : Expression.Or(expression, equalExpression);
            }

            return Expression.Lambda<Func<T, bool>>(expression, parameterExpression);
        }
    }
}
