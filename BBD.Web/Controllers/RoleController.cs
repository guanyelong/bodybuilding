using BBD.Models;
using BBD.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBD.Web.Controllers
{
    public class RoleController : Controller
    {
        OperContext oc = OperContext.CurrentContext;
        //
        // GET: /Role/
        //[AuthenticationAttribute]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 角色弹窗编辑
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Dialog(int Id)
        {
            if (Id < 1)
            {
                tb_Sys_Role r = new tb_Sys_Role() { rNum = "自动生成" };
                return View(r);
            }

            string errMsg = string.Empty;
            tb_Sys_Role role = oc.iBllSession.Itb_Sys_Role_Bo_BLL.GetAppRoleByID(Id, ref errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                return View(new tb_Sys_Role());
            }

            return View(role);
        }
        /// <summary>
        /// 角色用户界面
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleUser()
        {
            return View();
        }
        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <returns></returns>
        public ActionResult SetUserRole()
        {

            string userid = Request["userid"];
            if (string.IsNullOrEmpty(userid))
            {
                return View(new tb_Sys_UserInfo());
            }
            tb_Sys_UserInfo user = oc.iBllSession.Itb_Sys_UserInfo_Bo_BLL.GetAllAppUserById(Convert.ToInt32(userid));

            return View(user);
        }


        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAppRoleList()
        {
            int pageIndex = int.Parse(Request["page"]);  //当前页  
            int pageSize = int.Parse(Request["rows"]);  //页面行数
            string name = Request["name"];
            int count = 0;
            List<tb_Sys_Role> appRoleList = oc.iBllSession.Itb_Sys_Role_Bo_BLL.GetAppRoleList(pageIndex, pageSize, ref count, name);
            if (appRoleList != null)
            {
                var result = from appRole in appRoleList
                             select new
                             {
                                 rId = appRole.rId,
                                 appRole.rName,
                                 rAddTime = Convert.ToDateTime(appRole.rAddTime).ToString("yyyy-MM-dd HH:mm:ss"),
                                 appRole.rRemark,
                                 appRole.rNum,
                                 appRole.rIsDel
                             };

                var data = new
                {
                    total = count,
                    rows = result
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    total = 0,
                    rows = new List<tb_Sys_Role>()
                }, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        public JsonResult SaveRole(tb_Sys_Role roleInfo)
        {
            if (roleInfo == null)
            {
                return Json(new { result = "error", mesage = "角色数据为空" });
            }
            string errMsg = "";
            if (roleInfo.rId == 0)
            {
                roleInfo.rAddTime = DateTime.Now;
                //add
                oc.iBllSession.Itb_Sys_Role_Bo_BLL.AddRole(roleInfo, ref errMsg);

                //Common.LogHelper.InsertLog(String.Format("新增角色,ID-{0}", roleInfo.ID.ToString()), 52, "角色列表");
            }
            else
            {
                //edit
                oc.iBllSession.Itb_Sys_Role_Bo_BLL.EditRole(roleInfo, ref errMsg);

                //Common.LogHelper.InsertLog(String.Format("编辑角色,ID-{0}", roleInfo.ID.ToString()), 52, "角色列表");
            }

            var result = new { result = "ok", message = "操作成功" };

            if (!string.IsNullOrEmpty(errMsg))
            {
                result = new { result = "error", message = errMsg };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public JsonResult DeleteRole(int roleId)
        {
            if (roleId == 0)
            {
                return Json(new { result = "error", mesage = "角色编号为空" });
            }

            string errMsg = string.Empty;
            oc.iBllSession.Itb_Sys_Role_Bo_BLL.DeleteRole(roleId, ref errMsg);

            var result = new { result = "ok", message = "操作成功" };
            if (!string.IsNullOrEmpty(errMsg))
            {
                result = new { result = "error", message = errMsg };
            }
            //Common.LogHelper.InsertLog(String.Format("删除角色,ID-{0}", roleId.ToString()), 52, "角色列表");
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}