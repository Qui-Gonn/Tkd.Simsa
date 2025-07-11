﻿namespace Tkd.Simsa.Application.Common.Filtering;

using System.Linq.Expressions;

public abstract record FilterDescriptor<TModel>
{
    public abstract Expression<Func<TEntity, bool>> ToExpression<TEntity>(
        IPropertyMapper<TEntity, TModel> propertyMapper,
        IComparisonFunctions comparisonFunctions);
}