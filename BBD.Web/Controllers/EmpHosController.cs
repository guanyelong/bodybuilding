using BBD.Models;
using BBD.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBD.Web.Controllers
{
    public class EmpHosController : Controller
    {
        OperContext oc = OperContext.CurrentContext;
        //
        // GET: /EmpHos/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dialog(int Id)
        {
            ViewBag.HospId = Id;
            return View();
        }

        public ActionResult GetAppUserList()
        {
            int pageIndex = int.Parse(Request["page"]);  //当前页  
            int pageSize = int.Parse(Request["rows"]);  //页面行数
            string name = Request["name"];
            int hospid = int.Parse(Request["hospId"]);
            tb_Sys_UserInfo info = new tb_Sys_UserInfo();
            info.uName = name;
            int count = 0;
            var query = oc.iBllSession.Itb_Emp_Hos_Bo_BLL.GetAppEmpHosList(pageIndex, pageSize, ref count, hospid, info);
            var data = new
            {
                total = count,
                rows = query
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAppEmpList()
        {
            int pageIndex = int.Parse(Request["page"]);  //当前页  
            int pageSize = int.Parse(Request["rows"]);  //页面行数
            string name = Request["name"];
            int hospid = int.Parse(Request["hospId"]);
            tb_Sys_UserInfo info = new tb_Sys_UserInfo();
            info.uName = name;
            int count = 0;
            var query = oc.iBllSession.Itb_Emp_Hos_Bo_BLL.GetAppEmpList(pageIndex, pageSize, ref count, hospid, info);
            var data = new
            {
                total = count,
                rows = query
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveEmp(int hospid,string ids) 
        {
            string errMsg = string.Empty;
            if (!string.IsNullOrWhiteSpace(ids))
            {
                //var del = oc.iBllSession.Itb_Emp_Hos_Bo_BLL.DelBy(p => p.hospid == hospid);
                string[] arry=ids.Split(',');
                for (int i = 0; i < arry.Length; i++)
                {
                    
                    if (!string.IsNullOrWhiteSpace(arry[i]))
                    {
                        //检查是否存在，存在则忽略，不存在则插入
                        int empid = int.Parse(arry[i]);
                        var info = oc.iBllSession.Itb_Emp_Hos_Bo_BLL.GetObjet(p => p.hospid == hospid && p.emp_id == empid);
                        if (info != null)
                        {
                            continue;
                        }
                        tb_Emp_Hos eh = new tb_Emp_Hos();
                        eh.emp_id = empid;
                        eh.hospid = hospid;
                        //eh.creator = AdminSystemInfo.CurrentUser.uName;
                        //eh.creatorid = AdminSystemInfo.CurrentUser.Uid;
                        oc.iBllSession.Itb_Emp_Hos_Bo_BLL.Add(eh);
                    }
                }
                //遍历数据库中的数据，数据库存在但是参数中没有的，需要删除
                var existList = oc.iBllSession.Itb_Emp_Hos_Bo_BLL.GetListBy(p => p.hospid == hospid);
                foreach (var item in existList)
                {
                    if (!arry.Contains(item.emp_id.ToString()))
                    {
                        oc.iBllSession.Itb_Emp_Hos_Bo_BLL.DelBy(p => p.Id == item.Id);
                    }
                }
            }
            else
            {
                var del = oc.iBllSession.Itb_Emp_Hos_Bo_BLL.DelBy(p => p.hospid == hospid);
            }
            var result = new { result = "ok", message = "操作成功" };

            if (!string.IsNullOrEmpty(errMsg))
            {
                result = new { result = "error", message = errMsg };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}