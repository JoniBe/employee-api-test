using Aptude.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;


namespace Aptude.Infrastructure.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _Next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this._Next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await _Next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            string messageCode = null;
            var message = exception.Message;

            var responseModel = new ApiErrorResponseModel();

            if (exception is SecurityException)
            {
                message = "Unauthorized access.";

                code = HttpStatusCode.Unauthorized;
            }
            else if (exception is AggregateException)
            {
                if (!string.IsNullOrEmpty(exception.Message))
                {
                    message = "An error occurred on the server while the request was being validated.";

                    code = HttpStatusCode.BadRequest;
                }
            }
            else if (exception is ArgumentNullException)
            {
                if (!string.IsNullOrEmpty(exception.Message))
                {
                    message = exception.Message;
                }
                else
                {
                    message = "Error in received parameters.";
                }

                code = HttpStatusCode.BadRequest;
            }
            else
            {
                switch (exception)
                {
                    case UnauthorizedAccessException _:
                        if (!string.IsNullOrEmpty(exception.Message))
                        {
                            message = exception.Message;
                        }
                        else
                        {
                            message = "Invalid operation.";
                        }

                        code = HttpStatusCode.Unauthorized;
                        break;


                    case InvalidOperationException _:
                        if (!string.IsNullOrEmpty(exception.Message))
                        {
                            message = exception.Message;
                        }
                        else
                        {
                            message = "Invalid operation.";
                        }

                        code = HttpStatusCode.BadRequest;
                        break;

                    case ArgumentException _:
                        if (!string.IsNullOrEmpty(exception.Message))
                        {
                            message = exception.Message;
                        }
                        else
                        {
                            message = "Error in received parameters.";
                        }

                        code = HttpStatusCode.BadRequest;
                        break;

                    case UriFormatException _:
                        message = "Invalid path format.";

                        code = HttpStatusCode.BadRequest;

                        break;

                    case HttpRequestException _:
                        message = "Communication error.";

                        code = HttpStatusCode.BadRequest;

                        break;

                    default:
                        break;
                }
            }

            responseModel.MessageCode = messageCode;
            responseModel.Message = message;

            var _errors = new Dictionary<string, IEnumerable<string>>();

            _errors.Add("errors", new List<string>() {
                message
            });

            responseModel.Errors = _errors;

            var result = JsonConvert.SerializeObject(responseModel);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
