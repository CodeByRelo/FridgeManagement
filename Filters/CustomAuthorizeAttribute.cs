using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FridgeManagement.Filters
{
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute(string role) : base(typeof(CustomAuthorizationFilter))
        {
            Arguments = new object[] { role };
        }
    }

    public class CustomAuthorizationFilter : IAuthorizationFilter
    {
        private readonly string _role;

        public CustomAuthorizationFilter(string role)
        {
            _role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Get the user's role from the database (or User claims if added in claims)
            var userRole = context.HttpContext.User.FindFirst("UserRole")?.Value;

            if (userRole != _role)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
