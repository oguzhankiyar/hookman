using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OK.Hookman.Core.Base;
using OK.Hookman.Core.Exceptions;

namespace OK.Hookman.API.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is RequestNotValidatedException requestNotValidatedException)
            {
                var hasAnyFailures = requestNotValidatedException.Failures != null && requestNotValidatedException.Failures.Any();
                var response = BaseResponse.Fail("Not Validated", hasAnyFailures ? requestNotValidatedException.Failures : new string[] { requestNotValidatedException.Message });
                context.Result = new ObjectResult(response) { StatusCode = (int)HttpStatusCode.BadRequest };
            }
            else if (context.Exception is EntityNotFoundException entityNotFoundException)
            {
                var response = BaseResponse.Fail("Not Found", new string[] { entityNotFoundException.Message });
                context.Result = new ObjectResult(response) { StatusCode = (int)HttpStatusCode.NotFound };
            }
            else
            {
                var response = BaseResponse.Fail("Unhandled Exception", new string[] { context.Exception.Message });
                context.Result = new ObjectResult(response) { StatusCode = (int)HttpStatusCode.InternalServerError };
            }

            context.ExceptionHandled = true;
        }
    }
}