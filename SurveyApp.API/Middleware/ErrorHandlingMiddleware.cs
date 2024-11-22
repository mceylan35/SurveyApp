using FluentValidation;
using SurveyApp.Application.Common.Results;
using SurveyApp.Domain.Exceptions;
using System.Text.Json;

namespace SurveyApp.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
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
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var (statusCode, message) = exception switch
            {
                ValidationException => (StatusCodes.Status400BadRequest, "Validation error occurred"),
                UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized"),
                NotFoundException => (StatusCodes.Status404NotFound, "Data not found"),
                DomainException domainEx => (StatusCodes.Status400BadRequest, domainEx.Message),
                _ => (StatusCodes.Status500InternalServerError, "An error occurred")
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var result = JsonSerializer.Serialize(Result<bool>.Failure(message));

            await context.Response.WriteAsync(result);
        }
    }
}
