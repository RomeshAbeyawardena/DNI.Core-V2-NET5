using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
