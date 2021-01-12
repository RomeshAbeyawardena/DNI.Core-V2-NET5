using System.Linq.Expressions;

namespace DNI.Core.Shared.ExpressionVisitors
{
    internal class ExpressionBuilderExpressionVisitor : ExpressionVisitor
    {
        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            return base.VisitLambda(node);
        }
    }
}
