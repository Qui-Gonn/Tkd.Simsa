namespace Tkd.Simsa.Application.Common;

using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

using Tkd.Simsa.Application.Common.Filtering;
using Tkd.Simsa.Application.Extensions;

public class ExpressionConverter<TEntity, TModel>
{
    private readonly IPropertyMapper<TEntity, TModel> propertyMapper;

    public ExpressionConverter(IPropertyMapper<TEntity, TModel> propertyMapper)
    {
        this.propertyMapper = propertyMapper;
    }

    public Expression<Func<TEntity, TProperty>> Convert<TProperty>(Expression<Func<TModel, TProperty>> expression)
    {
        var visitor = new MemberReplacer(this.propertyMapper);
        var newBody = visitor.Visit(expression.Body);
        return Expression.Lambda<Func<TEntity, TProperty>>(newBody, visitor.Parameter);
    }

    private class MemberReplacer : ExpressionVisitor
    {
        private const string ParameterName = "x";

        private readonly IPropertyMapper<TEntity, TModel> propertyMapper;

        public MemberReplacer(IPropertyMapper<TEntity, TModel> propertyMapper)
        {
            this.propertyMapper = propertyMapper;
            this.Parameter = Expression.Parameter(typeof(TEntity), ParameterName);
        }

        public ParameterExpression Parameter { get; }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (this.TryGetEntityPropertyInfo(node.Member.Name, out var entityProperty))
            {
                return Expression.Property(this.Parameter, entityProperty);
            }

            return base.VisitMember(node);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return this.Parameter;
        }

        private bool TryGetEntityPropertyInfo(string propertyName, [NotNullWhen(true)] out PropertyInfo? entityProperty)
        {
            try
            {
                entityProperty = this.propertyMapper.ToEntityPropertyExpression(propertyName).GetPropertyInfo();
                return true;
            }
            catch (NotSupportedException)
            {
                entityProperty = null;
                return false;
            }
        }
    }
}