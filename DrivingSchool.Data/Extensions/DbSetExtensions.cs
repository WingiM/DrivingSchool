using System.Linq.Expressions;

namespace DrivingSchool.Data.Extensions;

public static class DbSetExtensions
{
    public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> query, Expression<Func<TSource, TKey>> property, bool desc)
    {
        return !desc ? query.OrderBy(property) : query.OrderByDescending(property);
    }
}