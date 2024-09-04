using System.Linq.Expressions;

/// <summary>
/// Класс для получение предиката с возможностью объединяться с другими предикатами по условию "и" и "или"
/// </summary>
public static class PredicateBuilder
{
	/// <summary>
	/// Получение базового предиката с начальным значением False
	/// </summary>
	public static Expression<Func<T, bool>> False<T>() => f => false;

	/// <summary>
	/// Расширение для предиката. Добавляет к текущему предикату другой предикат, ставя между ними условие "ИЛИ"
	/// </summary>
	public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
	{
		var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
		return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
	}
}
