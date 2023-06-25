using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System;

namespace Application.Validations;

public class ModelValidation : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        try
        {
            if (!context.ModelState.IsValid)
            {
                Log.Warning("Model is not valid");
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
            Log.Information("Model is valid");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error");
        }
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

   
}
