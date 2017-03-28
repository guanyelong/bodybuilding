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
    public class MenuController : Controller
    {
        OperContext oc = OperContext.CurrentContext;
        //
        // GET: /Menu/
        [AuthenticationAttribute]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dialog(int Id)
        {
            if (Id < 1)
            {
                tb_Sys_MenuInfo addItem = new tb_Sys_MenuInfo() { mNum = "自动生成" };
                return View(addItem);
            }

            string errMsg = string.Empty;
            tb_Sys_MenuInfo menuItem = oc.iBllSession.Itb_Sys_MenuInfo_Bo_BLL.GetAppMenuByID(Id, ref errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                return View(new tb_Sys_MenuInfo());
            }
            return View(menuItem);

        }
        public JsonResult GetMenuComboTree()
        {

            string errMsg = string.Empty;
            int count = 0;
            //构造combobox tree 需要的数据
            var menuTree = oc.iBllSession.Itb_Sys_MenuInfo_Bo_BLL.GetMenuComboTree(ref count, ref errMsg);

            if (!string.IsNullOrEmpty(errMsg))
            {
                menuTree = new List<Hashtable>();
            }
            return Json(menuTree, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取Department TreeGrid 数据格式
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAppMenuGridTree()
        {

            int count = 0;
            string errMsg = string.Empty;

            var menuTree = oc.iBllSession.Itb_Sys_MenuInfo_Bo_BLL.GetAppMenuGridTree(ref count, ref errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                menuTree = new List<Hashtable>();
            }

            return Json(menuTree, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 保存部门
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveMenu(tb_Sys_MenuInfo menuItem)
        {
            if (menuItem == null)
            {
                return Json(new { result = "error", message = "部门数据不正确" });
            }
            string errMsg = "";
            if (menuItem.mPId == null)
            {
                menuItem.mPId = 0;
            }
            if (menuItem.mId == 0)
            {
                menuItem.mAddtime = DateTime.Now;
                //add
                oc.iBllSession.Itb_Sys_MenuInfo_Bo_BLL.AddMenu(menuItem, ref errMsg);
                //Common.LogHelper.InsertLog(String.Format("新增菜单,ID-{0}", menuItem.ToString()), 50, "菜单列表");
            }
            else
            {
                //edit
                oc.iBllSession.Itb_Sys_MenuInfo_Bo_BLL.EditMenu(menuItem, ref errMsg);
                //Common.LogHelper.InsertLog(String.Format("编辑菜单,ID-{0}", menuItem.ToString()), 50, "菜单列表");
            }

            var result = new { result = "ok", message = "操作成功" };

            if (!string.IsNullOrEmpty(errMsg))
            {
                result = new { result = "error", message = errMsg };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        public JsonResult DeleteMenu(int menuId)
        {
            if (menuId == 0)
            {
                return Json(new { result = "error", mesage = "用户编号为空" });
            }

            string errMsg = string.Empty;
            oc.iBllSession.Itb_Sys_MenuInfo_Bo_BLL.DeleteMenu(menuId, ref errMsg);

            var result = new { result = "ok", message = "操作成功" };
            if (!string.IsNullOrEmpty(errMsg))
            {
                result = new { result = "error", message = errMsg };
            }
            //Common.LogHelper.InsertLog(String.Format("删除菜单,ID-{0}", menuId.ToString()), 50, "菜单列表");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}