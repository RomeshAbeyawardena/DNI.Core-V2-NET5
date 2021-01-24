using System.Linq.Expressions;
using System.Reflection;

namespace DNI.Core.Shared.ExpressionVisitors
{
    internal class ModelExpressionVisitor : ExpressionVisitor
    {
        public MemberInfo GetLastVisitedMember(Expression exp)
        {
            Visit(exp);
            return member;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            member = node.Member;
            return base.VisitMember(node);
        }

        private MemberInfo member;
    }

}
