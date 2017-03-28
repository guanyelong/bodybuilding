using BBD.Common;
using BBD.Models;
using BBD.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BBD.Web.Controllers
{
    public class LoginController : Controller
    {
        OperContext oc = OperContext.CurrentContext;
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(string userName, string password)
        {
            JsonResult ret = new JsonResult();

            //userName admin password Ebooom9966
            #region 用户信息验证
            if (string.IsNullOrEmpty(userName))
            {
                ret.Data = new { ifOk = false, errorMsg = "请输入用户名！" };
                return ret;
            }

            if (string.IsNullOrEmpty(password))
            {
                ret.Data = new { ifOk = false, errorMsg = "请输入密码！" };
                return ret;
            }
            tb_Sys_UserInfo findUser = oc.iBllSession.Itb_Sys_UserInfo_Bo_BLL.GetAppUserByLoginName(userName);

            if (findUser == null)
            {
                ret.Data = new { ifOk = false, errorMsg = "用户名不存在！" };
                return ret;
            }

            if (findUser.uPwd != BBD.Common.MD5Helper.MD5Encrypt32bit(password))
            {
                ret.Data = new { ifOk = false, errorMsg = "密码错误！" };
                return ret;
            }
            #endregion
            AdminSystemInfo.CurrentUser = findUser;
            FormsAuthentication.SetAuthCookie(userName, false);
            string url = FormsAuthentication.GetRedirectUrl(userName, false);
            ret.Data = new { ifOk = true, url = url };

            //获取用户的所有按钮权限列表存放到Session

            LoginSuccess(findUser.Uid);
            tb_Sys_LoginLog sl = new tb_Sys_LoginLog()
            {
                loginCity = findUser.CityId,
                loginName = findUser.uLoginName,
                LoginIp = IPHelper.GetIP()
            };
            oc.iBllSession.Itb_Sys_LoginLog_Bo_BLL.Add(sl);
            if (Request.Cookies["HHYS_COOKIE_LOGIN"] == null)
            {
                Response.Cookies["HHYS_COOKIE_LOGIN"].Value = findUser.uLoginName;
                Response.Cookies["HHYS_COOKIE_LOGIN"].Expires.AddDays(7);
            }
            //Common.LogHelper.InsertLog("登录成功！");
            return ret;
        }

        [HttpPost]
        public ActionResult LoginUser(tb_Sys_UserInfo info)
        {
            JsonResult ret = new JsonResult();
            tb_Sys_UserInfo findUser = oc.iBllSession.Itb_Sys_UserInfo_Bo_BLL.ValidateLogin(info);
            if (findUser != null)
            {
                AdminSystemInfo.CurrentUser = findUser;
                FormsAuthentication.SetAuthCookie(findUser.uLoginName, false);
                string url = FormsAuthentication.GetRedirectUrl(findUser.uLoginName, false);
                ret.Data = new { ifOk = true, url = url };
                //获取用户的所有按钮权限列表存放到Session
                LoginSuccess(findUser.Uid);
                //Common.LogHelper.InsertLog("登录成功！");
            }
            return ret;
        }

        private void LoginSuccess(int userId)
        {
            //设置用户的登录按钮权限 
            List<tb_Sys_Permission> actionList = oc.iBllSession.Itb_Sys_UserRole_Bo_BLL.GetAppUserActionList(userId, BBD.Common.AppActionType.AllAction);
            if (actionList == null)
            {
                actionList = new List<tb_Sys_Permission>();
            }
            AdminSystemInfo.UpdateActionList(actionList);

            //设置当前用户的角色信息
            string errMsg = string.Empty;
            List<tb_Sys_Role> roleList = oc.iBllSession.Itb_Sys_UserRole_Bo_BLL.GetAppUserRoleList(userId, ref errMsg);
            AdminSystemInfo.UpdateUserRoleList(roleList);

            //设置当前用户的菜单信息
            List<tb_Sys_MenuInfo> menuList = oc.iBllSession.Itb_Sys_UserRole_Bo_BLL.GetAppUserMenuList(userId);
            AdminSystemInfo.UpdateUserMenuList(menuList);
        }
	}
}