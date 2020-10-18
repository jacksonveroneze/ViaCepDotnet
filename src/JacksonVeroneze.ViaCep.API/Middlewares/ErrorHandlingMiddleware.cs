using System;
using System.Net;
using System.Threading.Tasks;
using JacksonVeroneze.ViaCep.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace JacksonVeroneze.ViaCep.API.Middlewares
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

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                string result = JsonConvert.SerializeObject(new {error = e.Message});

                context.Response.ContentType = "application/json";

                context.Response.StatusCode = e.GetBaseException() is DomainException
                    ? (int)HttpStatusCode.BadRequest
                    : (int)HttpStatusCode.InternalServerError;

                _logger.LogError(result);

                await context.Response.WriteAsync(result);
            }
        }
    }
}
