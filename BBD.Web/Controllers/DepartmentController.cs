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
    public class DepartmentController : Controller
    {
        //
        // GET: /Department/
        OperContext oc = OperContext.CurrentContext;
        //
        // GET: /Department/
        [AuthenticationAttribute]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dialog(int Id)
        {

            if (Id < 1)
            {
                tb_Sys_Department dept = new tb_Sys_Department() { DepNum = "自动生成" };
                return View(dept);
            }
            string errMsg = string.Empty;
            tb_Sys_Department department = oc.iBllSession.Itb_Sys_Department_Bo_BLL.GetDepartmentByID(Id, ref errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                return View(new tb_Sys_Department());
            }
            return View(department);
        }

        /// <summary>
        /// 获取Department数据Tree
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAppDeparmtTree()
        {

            string errMsg = string.Empty;
            int count = 0;
            //构造combobox tree 需要的数据
            var departmentTree = oc.iBllSession.Itb_Sys_Department_Bo_BLL.GetDepartmentTree(ref count, ref errMsg);

            if (!string.IsNullOrEmpty(errMsg))
            {
                departmentTree = new List<Hashtable>();
            }
            return Json(departmentTree, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAppCityTree()
        {
            oc.iBllSession.Itb_Dict_Bo_BLL.DbName = "hhm";
            var cityTree = oc.iBllSession.Itb_Dict_Bo_BLL.GetListBy(p => p.KeyName == "city" && p.state == 1);
            List<Hashtable> listht = new List<Hashtable>();
            foreach (var item in cityTree)
            {
                Hashtable ht = new Hashtable();
                ht.Add("id", item.KeyValue);
                ht.Add("value", item.KeyValue);
                ht.Add("text", item.KeyWords);
                listht.Add(ht);
            }
            return Json(listht, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取Department TreeGrid 数据格式
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAppDepartmentTreeGrid()
        {

            int count = 0;
            string errMsg = string.Empty;


            var departmentTree = oc.iBllSession.Itb_Sys_Department_Bo_BLL.GetDepartmentTreeGridList(ref count, ref errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                departmentTree = new List<Hashtable>();
            }

            return Json(departmentTree, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveDepartment(tb_Sys_Department depmentItem)
        {
            if (depmentItem == null)
            {
                return Json(new { result = "error", message = "部门数据不正确" });
            }
            string errMsg = "";
            if (depmentItem.DepId == 0)
            {
                depmentItem.DepAddtime = DateTime.Now;
                //add
                oc.iBllSession.Itb_Sys_Department_Bo_BLL.AddDepartment(depmentItem, ref errMsg);
                //Common.LogHelper.InsertLog(String.Format("新增部门,ID-{0}", depmentItem.ToString()), 44, "部门列表");
            }
            else
            {
                //edit
                oc.iBllSession.Itb_Sys_Department_Bo_BLL.EditDepartment(depmentItem, ref errMsg);
                //Common.LogHelper.InsertLog(String.Format("编辑部门,ID-{0}", depmentItem.ToString()), 44, "部门列表");
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
        public JsonResult DeleteDepartment(int departmentID)
        {
            if (departmentID == 0)
            {
                return Json(new { result = "error", mesage = "用户编号为空" });
            }

            string errMsg = string.Empty;
            oc.iBllSession.Itb_Sys_Department_Bo_BLL.DeleteDepartment(departmentID, ref errMsg);

            var result = new { result = "ok", message = "操作成功" };
            if (!string.IsNullOrEmpty(errMsg))
            {
                result = new { result = "error", message = errMsg };
            }

            //Common.LogHelper.InsertLog(String.Format("删除部门,ID-{0}", departmentID.ToString()), 44, "部门列表");
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}