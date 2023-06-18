using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.Validations;

public class ModelValidation : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (!context.ModelState.IsValid)
        {
            context.Result = new UnprocessableEntityObjectResult(context.ModelState);
        }
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
    }
}
