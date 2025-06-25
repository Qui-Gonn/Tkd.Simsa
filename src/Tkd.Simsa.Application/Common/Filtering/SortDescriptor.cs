namespace Tkd.Simsa.Application.Common.Filtering;

using System.Linq.Expressions;

public record SortDescriptor<TItem>(Expression<Func<TItem, object>> PropertyExpression, SortDirection Direction = SortDirection.Ascending);