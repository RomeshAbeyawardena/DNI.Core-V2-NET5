using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions.Services
{
    public class DefaultModelEncryptionService<T> : IModelEncryptionService<T>
    {
        public DefaultModelEncryptionService(IFluentEncryptionConfiguration<T> fluentEncryptionConfiguration)
        {
            this.fluentEncryptionConfiguration = fluentEncryptionConfiguration;
            modelExpressionVisitor = new ModelExpressionVisitor();
        }

        public void Encrypt(T model)
        {
            var modelType = typeof(T);
             foreach (var option in fluentEncryptionConfiguration.Options)
            {
                var memberName = modelExpressionVisitor.GetLastVisitedMember(option.PropertyExpression.Body);

                var property = modelType.GetProperty(memberName);

                var value = property.GetValue(model);
                if(value != null)
                { 
                    property.SetValue(model, Convert.ToBase64String(Encoding.ASCII.GetBytes(value.ToString())));
                }
            }
            
        }

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

        private readonly ModelExpressionVisitor modelExpressionVisitor;
        private readonly IFluentEncryptionConfiguration<T> fluentEncryptionConfiguration;
    }
}
