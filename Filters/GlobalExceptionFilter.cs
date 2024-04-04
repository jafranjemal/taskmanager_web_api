using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Http.Filters;
using Serilog;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {

            // Create error response
            var errorResponse = new ErrorResponseModel
            {
                ErrorCode = (int)HttpStatusCode.InternalServerError,
                ErrorMessage = "An unexpected error occurred, please try again later.",
                ErrorDetails = context.Exception.Message // Include exception message in error details
            };

            // Set response content
            context.Response = context.Request.CreateResponse(
                HttpStatusCode.InternalServerError,
                errorResponse
            );
            // Log the exception
            LogException(context.Exception);
        }

        private void LogException(Exception ex)
        {
            Log.Error(ex, "An exception occurred");
        }
    }
}
