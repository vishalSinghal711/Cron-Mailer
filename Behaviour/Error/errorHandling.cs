using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace OnBusyness.Behaviour.Error {
    public class ErrorHandeling {
        private readonly RequestDelegate next;
        private IWebHostEnvironment _env;
        private ILogger<ErrorHandeling> _logger;
        public ErrorHandeling (RequestDelegate next) {
            this.next = next;
        }
        public async Task InvokeAsync (HttpContext context , IWebHostEnvironment env  /* other dependencies */ , ILogger<ErrorHandeling> logger ) {
            this._logger = logger;
            this._env = env;
            try {
                await next (context);
            } catch (Exception ex) {
                await HandleExceptionAsync (context, ex);
            }
        }
        private Task HandleExceptionAsync (HttpContext context, Exception exception) {
            // var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            // if (exception is MyNotFoundException) code = HttpStatusCode.NotFound;
            // else if (exception is MyUnauthorizedException) code = HttpStatusCode.Unauthorized;
            // else if (exception is MyException) code = HttpStatusCode.BadRequest;

            // var result = JsonConvert.SerializeObject (new { error = exception.StackTrace });
            context.Response.ContentType = "application/json";

            // context.Response.StatusCode = (int) code;
            string errorMsg = exception.Message;
            string innerMessage = "" + exception.InnerException;
            string Stacktrace = exception.StackTrace;
            string Source = exception.Source;
            
            if(_env.IsDevelopment()){
                _logger.LogTrace(Stacktrace);
                _logger.LogError(errorMsg);
                _logger.LogError(innerMessage);
                return context.Response.WriteAsync ($"{errorMsg}");
            }else{
                //! IF NOT DEVELOPMENT Environment - add error into db 
                ErrorExtension obj = new ErrorExtension ();
                obj.errorLogger (errorMsg, innerMessage, Stacktrace, Source);
                return context.Response.WriteAsync ($"Something Went Wrong");
            }
        }

    }

}