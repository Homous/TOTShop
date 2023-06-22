using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System;

namespace Application.Validations;

public class ModelValidation : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        try
        {
            if (!context.ModelState.IsValid)
            {
                Log.Information("Model is valid");
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex,"Model is not valid");
        }

    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
    }
}
