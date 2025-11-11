using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Security.Claims;
using System.Text.Json;


namespace Juhyna_Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // نحاول نجيب الكنترولر والأكشن اللي حصل فيهم الخطأ
                var routeData = context.GetRouteData();
                var controller = routeData?.Values["controller"]?.ToString() ?? "UnknownController";
                var action = routeData?.Values["action"]?.ToString() ?? "UnknownAction";// endpoint

                // نطبع كل حاجة في اللوج
                _logger.LogError(ex,
                    $"Exception caught in {controller}/{action}\n" +
                    $"Message: {ex.Message}\n" +
                    $"Type: {ex.GetType().FullName}\n" +
                    $"StackTrace: {ex.StackTrace}");

                // نبعت رد JSON مفصل للمستخدم
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    statusCode = context.Response.StatusCode,
                    controller,
                    action,
                    message = ex.Message,
                    exceptionType = ex.GetType().FullName,
                    stackTrace = ex.StackTrace
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            }
        }
    }
        }
