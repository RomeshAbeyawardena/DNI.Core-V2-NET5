using DNI.Core.Shared.ExpressionVisitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Extensions
{
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Gets <see cref="PropertyInfo"/> for the specified expression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static PropertyInfo GetProperty<T, TKey>(this Expression<Func<T, TKey>> expression)
        {
            var modelType = typeof(T);
            var member = GetMember(expression);
            return modelType.GetProperty(member.Name);
        }

        /// <summary>
        /// Gets <see cref="MemberInfo"/> for the specified expression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        internal static MemberInfo GetMember<T, TKey>(this Expression<Func<T, TKey>> expression)
        {
            var visitor = new ModelExpressionVisitor();
            return visitor.GetLastVisitedMember(expression);
        }
    }
}
