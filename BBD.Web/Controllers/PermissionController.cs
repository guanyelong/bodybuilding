using BBD.Models;
using BBD.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBD.Web.Controllers
{
    public class PermissionController : Controller
    {
        OperContext oc = OperContext.CurrentContext;
        [AuthenticationAttribute]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RolePermission()
        {

            return View();
        }
        /// <summary>
        /// 新增编辑弹窗
        /// </summary>
        /// <returns></returns>
        public ActionResult Dialog()
        {
            string actionId = Request["Id"];
            if (string.IsNullOrEmpty(actionId) || actionId == "0")
            {
                tb_Sys_Permission ac = new tb_Sys_Permission() { pActionNum = "自动生成" };
                return View(ac);
            }
            string errMsg = string.Empty;
            tb_Sys_Permission actionModel = oc.iBllSession.Itb_Sys_Permission_Bo_BLL.GetPermissionByID(Convert.ToInt32(actionId), ref errMsg);
            if (string.IsNullOrEmpty(errMsg))
            {
                //记录操作日志
                //Common.LogHelper.InsertLog(String.Format("新增权限,ID-{0}", actionModel.ID.ToString()), 51, "权限列表");
                return View(actionModel);
            }
            return View(new tb_Sys_Permission());
        }
        /// <summary>
        /// 角色权限页面，设置权限弹窗
        /// </summary>
        /// <returns></returns>
        public ActionResult SetRolePermission()
        {
            string roleID = Request["roleid"];

            if (string.IsNullOrEmpty(roleID))
            {
                return View(new tb_Sys_Role());
            }
            string errMsg = string.Empty;
            tb_Sys_Role role = oc.iBllSession.Itb_Sys_Role_Bo_BLL.GetAppRoleByID(Convert.ToInt32(roleID), ref errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                return View(new tb_Sys_Role());
            }

            return View(role);
        }
        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetPermissionGridTreeList()
        {
            int count = 0;
            List<Hashtable> actionList = oc.iBllSession.Itb_Sys_Permission_Bo_BLL.GetPermissionTreeGridList(ref count);

            if (actionList == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            var data = new
            {
                total = count,
                rows = actionList
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="actionItem"></param>
        /// <returns></returns>
        public JsonResult SavePermission(tb_Sys_Permission actionItem)
        {
            if (actionItem == null)
            {
                return Json(new { result = "error", mesage = "角色数据为空" });
            }
            string errMsg = "";
            if (actionItem.pId == 0)
            {
                actionItem.pOperTime = DateTime.Now;
                //add
                oc.iBllSession.Itb_Sys_Permission_Bo_BLL.AddPermission(actionItem, ref errMsg);
            }
            else
            {
                //edit
                oc.iBllSession.Itb_Sys_Permission_Bo_BLL.EditPermission(actionItem, ref errMsg);
            }

            var result = new { result = "ok", message = "操作成功" };

            if (!string.IsNullOrEmpty(errMsg))
            {
                result = new { result = "error", message = errMsg };
            }
            //Common.LogHelper.InsertLog(String.Format("修改权限,ID-{0}", actionItem.ID.ToString()), 51, "权限列表");
            //兼容ie
            return Json(result, "text/html", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取下拉框数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetPermissionComboTree()
        {
            int count = 0;
            var actionList = oc.iBllSession.Itb_Sys_Permission_Bo_BLL.GetPermissionComboTreeList(ref count);

            if (actionList == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            return Json(actionList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除权限,将同时删除子级权限
        /// </summary>
        /// <param name="actionid"></param>
        /// <returns></returns>
        public JsonResult DeletePermission(int actionid)
        {
            if (actionid == 0)
            {
                return Json(new { result = "error", mesage = "权限编号为空" });
            }

            string errMsg = string.Empty;
            oc.iBllSession.Itb_Sys_Permission_Bo_BLL.DeletePermission(actionid, ref errMsg);

            var result = new { result = "ok", message = "操作成功" };
            if (!string.IsNullOrEmpty(errMsg))
            {
                result = new { result = "error", message = errMsg };
            }

            //Common.LogHelper.InsertLog(String.Format("删除权限,ID-{0}", actionid.ToString()), 51, "权限列表");

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}