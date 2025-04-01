using MusicBookingApp.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MusicBookingApp.Infrastructure.Web.Attributes
{
    /// <summary>
    /// Custom attribute to handle permission-based authorization for specific actions or controllers.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AuthorizeRoleAttribute(string role) : Attribute, IAsyncAuthorizationFilter
    {
        /// <summary>
        /// Handles authorization logic asynchronously by checking if the current user has the required role for a given community.
        /// </summary>
        /// <param name="context">Authorization filter context that provides access to the current HTTP request.</param>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            var currentUser = context.HttpContext.RequestServices.GetService(typeof(ICurrentUser)) as ICurrentUser;

            if (currentUser == null || currentUser.Role != role)
            {
                context.Result = new ForbidResult(); // Return forbidden if the user doesn't have the required role
            }
        }
    }
}
