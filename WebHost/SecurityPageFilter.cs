using System.Linq;
using System.Reflection;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;
using AccountManagement.Domain.RoleAgg;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebHost
{
    //Add to startup too
    public class SecurityPageFilter:IPageFilter
    {
        private readonly IAuthHelper _authHelper;

        public SecurityPageFilter(IAuthHelper authHelper)
        {
            _authHelper = authHelper;
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var handlerPermission =
              (NeedPermissionAttribute) context.HandlerMethod.MethodInfo.GetCustomAttribute(
                    typeof(NeedPermissionAttribute));

            if (handlerPermission == null) return;

            var currentPermission = _authHelper.GetPermissions();

            if (currentPermission.All(x => handlerPermission != null && x != handlerPermission.Permission))
                context.HttpContext.Response.Redirect("/Account?handler=Login");

        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
           
        }
    }
}
