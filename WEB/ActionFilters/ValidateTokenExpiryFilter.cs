using Business.Manager.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WEB.ActionFilters
{
    public class ValidateTokenExpiryFilter : ActionFilterAttribute, IAsyncActionFilter
    {
        private IUserManager _userManager;

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _userManager = context.HttpContext.RequestServices.GetService<IUserManager>();

            if (context.ActionArguments.TryGetValue("token", out var tokenValue) && context.ActionArguments.TryGetValue("email", out var emailValue))
            {
                string email = emailValue as string;
                string token = tokenValue as string;

                var user = await _userManager.FindUserByEmailAsync(email);
                if (user != null) 
                {
                    var isTokenValid = await _userManager.IsTokenValid(user, token);
                    if (isTokenValid) 
                    {
                        await next();
                        return;
                    }

                    context.Result = new BadRequestObjectResult("Bu bağlantının süresi geçmiş!");
                }
            }

        }
    }
}
