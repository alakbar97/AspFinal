using OnlineCv.WebUI.AppCode.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineCv
{
    public class OnlineAuthorizationAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }

            if (filterContext.HttpContext.Session[SessionKey.User] == null)
                filterContext.Result = new RedirectResult("resume");
            //base.OnAuthorization(filterContext);
        }
    }
}