using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace OdontoBackend.Common.Logs
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate next;
        private readonly ILogger _loggerFactory;
        public ExceptionHandler(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this.next = next;
            _loggerFactory = loggerFactory.CreateLogger<ExceptionHandler>();
            loggerFactory.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
            {
                LogLevel = LogLevel.Information
            }));

           // loggerFactory.AddFile($"Logs/log.log");
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (HttpStatusCodeException StatusCodeEx)
            {
                await HandleExceptionAsync(context, StatusCodeEx);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, HttpStatusCodeException exception)
        {
            string result = null!;
            context.Response.ContentType = "application/json";
            if (exception is HttpStatusCodeException)
            {
                result = new ErrorDetails() { Message = exception.Message, StatusCode = (int)exception.StatusCode }.ToString();
                context.Response.StatusCode = (int)exception.StatusCode;
            }
            else
            {
                result = new ErrorDetails() { Message = "Error interno en el servidor, WebApi", StatusCode = (int)HttpStatusCode.InternalServerError }.ToString();
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            _loggerFactory.LogError($"{exception}");
            return context.Response.WriteAsync(result);
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string result = new ErrorDetails() { Message = "Error interno en el servidor, WebApi", StatusCode = (int)HttpStatusCode.InternalServerError }.ToString();
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            _loggerFactory.LogError($"{exception}");
            return context.Response.WriteAsync(result);
        }
    }
}
