using FluentValidation;
using FoxPhonebook.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System;
using System.Collections.Generic;
using System.Security.Authentication;

namespace FoxPhonebook.API.API.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public ApiExceptionFilterAttribute()
        {
            // Register known exception types and handlers.
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                //{ typeof(ValidationException), HandleValidationException },
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(FluentValidation.ValidationException), HandleValidationException },
                { typeof(InvalidOperationException), HandleInvalidOperationException },
                { typeof(AuthenticationException), HandleAuthenticationException },
            };
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();

            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            HandleUnknownException(context);
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            Log.Error(context.Exception, "An error occurred while processing your request.");

            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = context.Exception.Message,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception as NotFoundException;

            Log.Error(exception, nameof(NotFoundException));

            var details = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "The specified resource was not found.",
                Detail = exception.Message
            };

            context.Result = new NotFoundObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var exception = context.Exception as FluentValidation.ValidationException;

            Log.Error(exception, "ValidationException");

            var details = new ValidationProblemDetails()
            {
                Detail = exception.Message,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleAuthenticationException(ExceptionContext context)
        {
            var exception = context.Exception as AuthenticationException;

            Log.Error(exception, nameof(AuthenticationException));

            var details = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = exception.Message,
                Detail = exception.Message
            };

            context.Result = new UnauthorizedObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleInvalidOperationException(ExceptionContext context)
        {
            var exception = context.Exception as InvalidOperationException;

            Log.Error(exception, nameof(InvalidOperationException));

            var details = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = exception.Message,
                Detail = exception.Message
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }
    }
}
