using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace SportLife.Web.Atributes
{
    public class SecurityManagerAtribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            HttpCookie authCookie =
              filterContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                var identity = new GenericIdentity(authTicket.Name, "Forms");
                var principal = new GenericPrincipal(identity, new string[] { authTicket.UserData });
                filterContext.HttpContext.User = principal;
            }

            var Controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var Action = filterContext.ActionDescriptor.ActionName;
            var User = filterContext.HttpContext.User;
            var IP = filterContext.HttpContext.Request.UserHostAddress;

            if (!User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
                return;

            }

            var isAccessAllowed = PageAccessManager.IsAccessAllowed(Controller, Action, User, IP);

            if (!isAccessAllowed)
            {
                filterContext.Result = new RedirectToRouteResult(
                                       new RouteValueDictionary 
                                   {
                                       { "action", "forbidden" },
                                       { "controller", "home" }
                                   });

            }
        }
    }

    public class PageAccessManager
    {
        public static bool IsAccessAllowed(string Controller,
               string Action, IPrincipal User, string IP)
        {
            if (Controller == "Home")
                return true;
            if (Controller == "Admin" && User.IsInRole("admin"))
                return true;


            return false;
        }
    }
}