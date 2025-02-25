using Microsoft.AspNetCore.Mvc.Filters;

namespace BlogManagementAPI.Filters
{
    public class User_ValidateUserIdFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var userId = context.ActionArguments["id"] as int?;
            if (userId.HasValue)
            {
                if (userId.Value <= 0)
                {
                    context.ModelState.AddModelError("UserId", "UserID is invalid");
                }
            }
        }
    }
}
