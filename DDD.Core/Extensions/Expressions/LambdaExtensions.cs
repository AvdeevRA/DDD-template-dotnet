using System.Linq.Expressions;

namespace DDD.Core.Extensions.Expressions;

public static class LambdaExtensions
{
    public static Expression<T> Compose<T>(
        this Expression<T> leftExpression,
        Expression<T> rightExpression,
        Func<Expression, Expression, Expression> merge
    )
    {
        var map = leftExpression
            .Parameters.Select((left, i) => new { left, right = rightExpression.Parameters[i] })
            .ToDictionary(p => p.right, p => p.left);

        var rightBody = ExpressionRebinder.ReplacementExpression(map, rightExpression.Body);

        return Expression.Lambda<T>(
            merge(leftExpression.Body, rightBody),
            leftExpression.Parameters
        );
    }

    public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right
    )
    {
        return left.Compose(right, Expression.And);
    }

    public static Expression<Func<T, bool>> Or<T>(
        this Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right
    )
    {
        return left.Compose(right, Expression.Or);
    }
}
