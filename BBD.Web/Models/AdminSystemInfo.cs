using BBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBD.Web.Models
{
    public partial class AdminSystemInfo
    {

        /// <summary>
        /// 当前用户
        /// </summary>
        public static tb_Sys_UserInfo CurrentUser
        {
            get
            {
                return HttpContext.Current.Session["AdminCurrentUser"] as tb_Sys_UserInfo;

            }
            set
            {
                HttpContext.Current.Session["AdminCurrentUser"] = value;

            }
        }

        /// <summary>
        /// 当前登录用户的角色列表
        /// </summary>
        public static List<tb_Sys_Role> CurrentUserRoleList
        {
            get
            {
                List<tb_Sys_Role> roleList = HttpContext.Current.Session["CurrentUserRoleList"] as List<tb_Sys_Role>;

                return roleList;
            }
            set
            {
                HttpContext.Current.Session["CurrentUserRoleList"] = value;
            }
        }
        /// <summary>
        /// 获取按钮权限列表
        /// </summary>
        public static List<tb_Sys_Permission> ButtonActionList
        {
            get
            {
                List<tb_Sys_Permission> actionList = HttpContext.Current.Session["UserButtonAction"] as List<tb_Sys_Permission>;

                return actionList;
            }
            set
            {
                HttpContext.Current.Session["UserButtonAction"] = value;
            }
        }
        /// <summary>
        /// 获取菜单权限
        /// </summary>
        public static List<tb_Sys_Permission> MenuActionList
        {
            get
            {
                List<tb_Sys_Permission> actionList = HttpContext.Current.Session["UserMenuAction"] as List<tb_Sys_Permission>;
                return actionList;
            }
            set
            {
                HttpContext.Current.Session["UserMenuAction"] = value;
            }
        }
        /// <summary>
        /// 获取菜单列表（根据权限）
        /// </summary>
        public static List<tb_Sys_MenuInfo> MenuInfoList
        {
            get
            {
                List<tb_Sys_MenuInfo> menuList = HttpContext.Current.Session["UserMenuInfos"] as List<tb_Sys_MenuInfo>;
                return menuList;
            }
            set
            {
                HttpContext.Current.Session["UserMenuInfos"] = value;
            }
        }


        /// <summary>
        /// 获取当前登录用户指定权限下的ActionList
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="actionNum"></param>
        /// <returns></returns>
        public static List<tb_Sys_Permission> GetUserActionList(string actionNum)
        {

            var action = MenuActionList.Where(o => o.pActionNum == actionNum).FirstOrDefault();

            if (action == null)
            {
                return new List<tb_Sys_Permission>();
            }

            var actionList = ButtonActionList.Where(o => o.pParentId == action.pId);
            if (actionList != null && actionList.Count() > 0)
            {
                return actionList.ToList();
            }

            return new List<tb_Sys_Permission>();
        }

        /// <summary>
        /// 更新菜单权限
        /// </summary>
        /// <param name="actionList"></param>
        public static void UpdateActionList(List<tb_Sys_Permission> actionList)
        {

            AdminSystemInfo.ButtonActionList = actionList.Where(o => o.pOperationType == 0).ToList();
            AdminSystemInfo.MenuActionList = actionList.Where(o => o.pOperationType == 1).ToList();
        }

        /// <summary>
        /// 获取当前登录用户的角色数据
        /// </summary>
        /// <param name="user"></param>
        public static void UpdateUserRoleList(List<tb_Sys_Role> roleList)
        {

            AdminSystemInfo.CurrentUserRoleList = roleList;
        }
        /// <summary>
        /// 获取当前登录用户的菜单数据
        /// </summary>
        /// <param name="menuList"></param>
        public static void UpdateUserMenuList(List<tb_Sys_MenuInfo> menuList)
        {
            AdminSystemInfo.MenuInfoList = menuList;
        }
    }
}