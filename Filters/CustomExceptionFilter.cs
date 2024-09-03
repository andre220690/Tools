/// <summary>
/// Атрибут для установки фильтра.
/// </summary>
/// <remarks>
/// Фильтр для отправки ответа с ошибкой на фронт, в случае ее возникновения
/// </remarks>
public class CustomExceptionFilter : IExceptionFilter
{
	private readonly string ERROR_MESSAGE = "Ошибка при обработке данных";
	private readonly ILogger _logger;

	public CustomExceptionFilter(ILoggerFactory loggerFactory)
	{
		ArgumentNullException.ThrowIfNull(loggerFactory);

		_logger = loggerFactory.CreateLogger<CustomExceptionFilter>();
	}

	/// <summary>
	/// Действие при возникновении ошибки
	/// </summary>
	/// <param name="context">Контекст запроса</param>
	public void OnException(ExceptionContext context)
	{
		_logger.LogError(context.Exception, ERROR_MESSAGE);

		var response = new Response<object>(ERROR_MESSAGE);
		context.Result = new OkObjectResult(response);
	}
}
