using System.Linq.Expressions;

namespace DNI.Core.Shared.ExpressionVisitors
{
    internal class ModelExpressionVisitor : ExpressionVisitor
    {
        public string GetLastVisitedMember(Expression exp)
        {
            Visit(exp);
            return memberName;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            memberName = node.Member.Name;
            return base.VisitMember(node);
        }

        private string memberName;
    }

}
