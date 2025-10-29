using System.Text.Json;
using CodebridgeDogs.Exceptions;

namespace CodebridgeDogs.Middleware;

public class ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = ex switch
            {
                DogAlreadyExistsException => StatusCodes.Status409Conflict,
                DogCreationFailedException => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };
            
            var response = new
            {
                error = ex.Message,
                status = context.Response.StatusCode,
            };
            
            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
            
        }
    }

}