using BBD.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBD.Web.Controllers
{
    public class RolePermissionController : Controller
    {
        //
        // GET: /RolePermission/
        OperContext oc = OperContext.CurrentContext;
        //
        // GET: /RoleAction/
        [AuthenticationAttribute]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取角色权限列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAppRolePermissionList()
        {
            string roleid = Request["roleid"];

            if (string.IsNullOrEmpty(roleid))
            {
                return Json("");
            }
            string errMsg = string.Empty;
            int count = 0;
            List<Hashtable> actionList = oc.iBllSession.Itb_Sys_RolePermission_Bo_BLL.GetAppRolePermissionTreeGrid(Convert.ToInt32(roleid), ref errMsg, ref count);

            var data = new
            {
                total = count,
                rows = actionList
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取设置权限时的权限列表框
        /// </summary>
        /// <returns></returns>
        public JsonResult GetRolePermissionTreeList()
        {
            string roleId = Request["roleid"];

            if (string.IsNullOrEmpty(roleId))
            {
                roleId = "0";
            }

            string errMsg = "";
            List<Hashtable> treeList = oc.iBllSession.Itb_Sys_RolePermission_Bo_BLL.GetRolePermissionRoleTreeList(Convert.ToInt32(roleId), ref errMsg);

            return Json(treeList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 设置角色的权限
        /// </summary>
        /// <returns></returns>
        public JsonResult SaveRolePermission()
        {
            string roleid = Request["roleid"];
            string actionids = Request["actionids"];
            if (string.IsNullOrEmpty(roleid) || string.IsNullOrEmpty(actionids))
            {
                return Json(new { result = "error", message = "参数错误" });
            }

            string errMsg = string.Empty;
            oc.iBllSession.Itb_Sys_RolePermission_Bo_BLL.SaveRolePermission(Convert.ToInt32(roleid), actionids.Split(','), ref errMsg);
            // 刷新当前用户的权限列表
            //AdminSystemInfo.UpdateActionList(userRole.GetAppUserActionList(AdminSystemInfo.CurrentUser.ID, SP.Models.HIS.AppActionType.AllAction));

            if (!string.IsNullOrEmpty(errMsg))
            {
                return Json(new { result = "error", message = "参数错误" });
            }

            //Common.LogHelper.InsertLog("设置权限", 52, "角色权限");

            return Json(new { result = "ok", message = "权限设置成功" });
        }
    }
}