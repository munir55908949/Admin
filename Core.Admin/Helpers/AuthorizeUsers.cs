using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Security.Cryptography;
using System.IO;
using Microsoft.AspNet.Identity;
using Core.AccessLayer;
using System.Data.Entity;
using System.Reflection;

namespace Core.Admin.Helpers
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class MultipleButtonAttribute : ActionNameSelectorAttribute
    {
        public string Name { get; set; }
        public string Argument { get; set; }

        public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
        {
            var isValidName = false;
            var keyValue = string.Format("{0}:{1}", Name, Argument);
            var value = controllerContext.Controller.ValueProvider.GetValue(keyValue);

            if (value != null)
            {
                controllerContext.Controller.ControllerContext.RouteData.Values[Name] = Argument;
                isValidName = true;
            }

            return isValidName;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeUsers : ActionFilterAttribute
    {
        #region public Feilds
        public DatabseEntities context = new DatabseEntities();
        #endregion
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (!HttpContext.Current.User.IsInRole("Admin"))
                {
                    string actName = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
                    string ctrName = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
                    string UserId = HttpContext.Current.User.Identity.GetUserId();

                    if (actName != "EditProfile" && actName != "ChangePassword")
                    {
                        var CheckPageUser = context.Users_Pages
                       .Include(f => f.Pages)
                       .Where(f => f.Pages.Url == ctrName && f.UserId == UserId)
                       .FirstOrDefault();
                        if (CheckPageUser == null)
                        {
                            filterContext.Result = new RedirectResult("~/Account/Login");
                        }
                    }
                }
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Account/Login");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}