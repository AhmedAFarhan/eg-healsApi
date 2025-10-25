using BuildingBlocks.Responses.Factories;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handler
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError("Error Message : {exceptionMessage}, Time of occurrence {time}", exception.Message, DateTime.Now);

            (string Title, int StatusCode, List<string> Errors) details = exception switch
            {
                NotFoundException => (exception.GetType().Name, 404, new List<string> { exception.Message }),
                BadRequestException => (exception.GetType().Name, 400, new List<string> { exception.Message }),
                InternalServerException => (exception.GetType().Name, 500, new List<string> { exception.Message }),
                ValidationException => HandleValidationException((ValidationException)exception),
                SqlException => HandleSqlException((SqlException)exception),
                _ => (exception.GetType().Name, 500, new List<string> { exception.Message }),
            };

            var response = EGResponseFactory.Fail<object>(message: details.Title,
                                                          errors: details.Errors,
                                                          traceId: context.TraceIdentifier,
                                                          code: details.StatusCode);

            context.Response.StatusCode = details.StatusCode;

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(response, cancellationToken: cancellationToken);

            return true;
        }

        /******************************************** Helpers **********************************************/

        private (string Title, int StatusCode, List<string> Errors) HandleValidationException(ValidationException validationException)
        {
            var errors = validationException.Errors.Select(error => $"{error.Severity}").ToList();

            return ("Validation Error", 400, errors);
        }

        private (string Title, int StatusCode, List<string> Errors) HandleSqlException(SqlException sqlException)
        {
            return sqlException.Number switch
            {
                2601 or 2627 => ("Unique Constraint Violation", 409, new List<string> { "A record with the same value already exists." }), // Unique constraint
                547 => ("Foreign Key Violation", 409, new List<string> { "A related record is preventing this operation." }), // Foreign key violation
                _ => ("Database Error", 500, new List<string> { "A database error occurred." })
            };
        }
    }
}
