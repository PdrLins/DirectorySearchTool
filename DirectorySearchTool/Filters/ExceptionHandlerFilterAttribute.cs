using DirectorySearchTool.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DirectorySearchTool.Filters
{
    public class ExceptionHandlerFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
        public ExceptionHandlerFilterAttribute(ILogger<ExceptionHandlerFilterAttribute> logger)
        {
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>()
            {
                {typeof(UnknowUrlException), HandleUnknowUrlException},
                {typeof(FindMemberException), HandleFindMemberException},
                {typeof(ValidationException), HandleValidationException}
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

            if (!context.ModelState.IsValid)
            {
                HandleInvalidModelStateException(context);
                return;
            }

            HandleUnknownException(context);
        }
        private void HandleInvalidModelStateException(ExceptionContext context)
        {
            var details = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };
            context.Result = new BadRequestObjectResult(details);
            context.ExceptionHandled = true;
        }
        private void HandleUnknownException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status403Forbidden,
                Title = "An error occurred while processing your request.",
            };
            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status403Forbidden,
                Value = context.Exception.Message
            };

            context.ExceptionHandled = true;
        }
        private void HandleValidationException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request.",
            };
            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }
        private void HandleUnknowUrlException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "An error occurred while processing your request.",
            };
            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }
        private void HandleFindMemberException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "An error occurred while processing your request.",
            };
            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }

    }
}
