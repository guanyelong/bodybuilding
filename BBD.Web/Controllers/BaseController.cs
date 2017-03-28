using BBD.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BBD.Web.Controllers
{
    public class BaseController : Controller
    {

    }
    /// <summary>
    /// 需要登录的Controller，如果没有登录，返回登录页，登录后自动跳转到相应的页面，适用于controller下面所有action都需要登录的情况
    /// </summary>
    public class AuthenticationController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //排除不需验证的action
            object[] actionFilter = filterContext.ActionDescriptor.GetCustomAttributes(typeof(NoAuthenticationAttribute), false);
            object[] controllerFilter = filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(NoAuthenticationAttribute), false);
            if (actionFilter.Length > 0 || controllerFilter.Length > 0)
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            if (AdminSystemInfo.CurrentUser == null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.StatusCode = 401;
                    filterContext.Result = new HttpUnauthorizedResult();
                    filterContext.HttpContext.Response.End();
                    return;
                }
                else
                {
                    filterContext.Result = RedirectToAction("Index", "Login", new { returnUrl = HttpUtility.UrlEncode(Request.Url.ToString()) });
                }
            }
            else if (AdminSystemInfo.MenuInfoList == null)
            {
                filterContext.Result = RedirectToAction("Main", "Home", new { returnUrl = HttpUtility.UrlEncode(Request.Url.ToString()) });
            }
            else if (Request.Url.ToString().ToLower().IndexOf("home") < 0)
            {
                var isPer = AdminSystemInfo.MenuInfoList.Where(p => p.mUrl != string.Empty && p.mPId != 0 && Request.Url.ToString().Contains(p.mUrl)).FirstOrDefault();
                if (isPer == null)
                {
                    //没有权限
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                     {
                         {"controller", "Home"},
                         {"action", "Main"},
                         {"returnUrl",  HttpUtility.UrlEncode(filterContext.HttpContext.Request.Url.ToString())}
                     });
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }

    /// <summary>
    /// 需要登录的ActionFilterAttribute，如果没有登陆返回登录页，登录后自动跳转到相应的页面，适用于Action
    /// </summary>
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //排除不需验证的action
            object[] actionFilter = filterContext.ActionDescriptor.GetCustomAttributes(typeof(NoAuthenticationAttribute), false);
            object[] controllerFilter = filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(NoAuthenticationAttribute), false);
            if (actionFilter.Length > 0 || controllerFilter.Length > 0)
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            if (AdminSystemInfo.CurrentUser == null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.StatusCode = 401;
                    filterContext.Result = new HttpUnauthorizedResult();
                    filterContext.HttpContext.Response.End();
                    return;
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                     {
                         {"controller", "Login"},
                         {"action", "Index"},
                         {"returnUrl",  HttpUtility.UrlEncode(filterContext.HttpContext.Request.Url.ToString())}
                     });
                }
            }
            else if (AdminSystemInfo.MenuInfoList == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                     {
                         {"controller", "Home"},
                         {"action", "Main"},
                         {"returnUrl",  HttpUtility.UrlEncode(filterContext.HttpContext.Request.Url.ToString())}
                     });
            }
            else if (filterContext.HttpContext.Request.Url.ToString().ToLower().IndexOf("home") < 0)
            {
                var isPer = AdminSystemInfo.MenuInfoList.Where(p => !string.IsNullOrWhiteSpace(p.mUrl) && p.mPId != 0 && filterContext.HttpContext.Request.Url.ToString().Contains(p.mUrl)).FirstOrDefault();
                if (isPer == null)
                {
                    //没有权限
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                     {
                         {"controller", "Home"},
                         {"action", "Main"},
                         {"returnUrl",  HttpUtility.UrlEncode(filterContext.HttpContext.Request.Url.ToString())}
                     });
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }

    /// <summary>
    /// 无需验证的Action
    /// </summary>
    public class NoAuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
    }
}