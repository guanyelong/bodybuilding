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
    public class ServiceController : Controller
    {
        OperContext oc = OperContext.CurrentContext;
        //
        // GET: /HeiWei/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dialog(int Id)
        {
            tb_Serv_Info si = new tb_Serv_Info();
            if (Id != 0)
            {
                si = oc.iBllSession.Itb_Serv_Info_Bo_BLL.GetObjet(p => p.ID == Id);
            }
            return View(si);
        }


        public ActionResult GetAppServList()
        {
            int pageIndex = int.Parse(Request["page"]);  //当前页  
            int pageSize = int.Parse(Request["rows"]);  //页面行数
            string ServName = Request["ServName"];
            int count = 0;
            tb_Serv_Info info = new tb_Serv_Info();
            if (!string.IsNullOrWhiteSpace(ServName))
            {
                info.ServName = ServName;
            }
            List<tb_Emp_Hos> hospList = AdminSystemInfo.EmpHospList;
            if (hospList != null && hospList.Count > 0)
            {

                var hids = hospList.Select(p => p.hospid).ToList();
                info.HospIds = hids;
            }
            else
            {
                List<int?> a = new List<int?>();
                a.Add(0);
                info.HospIds = a;
            }
            var query = oc.iBllSession.Itb_Serv_Info_Bo_BLL.GetAppServList(pageIndex, pageSize, ref count,info);
            var data = new
            {
                total = count,
                rows = query
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteServ(int Id)
        {
            string errMsg = "";
            if (Id != 0)
            {
                var info = oc.iBllSession.Itb_Serv_Info_Bo_BLL.GetObjet(p => p.ID == Id);
                if (info != null)
                {
                    info.IsDel = 1;
                    string [] prop={"IsDel"};
                    int num = oc.iBllSession.Itb_Serv_Info_Bo_BLL.Modify(info, prop);
                    if (num < 1) errMsg = "删除失败";
                }
                else
                {
                    errMsg = "未找到数据";
                }
            }
            var result = new { result = "ok", message = "操作成功" };

            if (!string.IsNullOrEmpty(errMsg))
            {
                result = new { result = "error", message = errMsg };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveServ(tb_Serv_Info si)
        {
            if (si == null)
            {
                return Json(new { result = "error", mesage = "数据为空" });
            }
            string errMsg = "";

            if (si.ID == 0)
            {
                si.IsDel = 0;
                si.state = 0;
                si.Creator = AdminSystemInfo.CurrentUser.uName;
                si.CreatorId = AdminSystemInfo.CurrentUser.Uid;
                int num = oc.iBllSession.Itb_Serv_Info_Bo_BLL.Add(si);
                if (num < 1) { errMsg = "添加失败"; }
                else
                {
                    si.ServId = si.ID + 10;
                    string[] prop = { "ServId" };
                    oc.iBllSession.Itb_Serv_Info_Bo_BLL.Modify(si, prop);
                }
            }
            else
            {
                string[] prop = { "ID", "CTime", "CreatorId", "Creator", "ServTypeName", "TouchTimes", "InstrumentTimes", "HospIds", "HospName", "CityName", "HospStrIds", "state", "IsDel" };
                si.ModifyId = AdminSystemInfo.CurrentUser.Uid;
                si.LastModTime = DateTime.Now;
                int num = oc.iBllSession.Itb_Serv_Info_Bo_BLL.Modifyed(si, prop);
                if (num < 1) errMsg = "修改失败";
            }
            var result = new { result = "ok", message = "操作成功" };

            if (!string.IsNullOrEmpty(errMsg))
            {
                result = new { result = "error", message = errMsg };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAppServiceTree() 
        {
            var query = oc.iBllSession.Itb_Dict_Bo_BLL.GetListBy(p => p.KeyName == "ServType", p => p.Seq);
            List<Hashtable> htlist = new List<Hashtable>();
            Hashtable htsel = new Hashtable();
            htsel.Add("id", "");
            htsel.Add("value", "");
            htsel.Add("text", "请选择");
            htsel.Add("selected", true);
            htlist.Add(htsel);
            foreach (var item in query)
            {
                Hashtable ht = new Hashtable();
                ht.Add("id", item.KeyValue);
                ht.Add("value", item.KeyValue);
                ht.Add("text", item.KeyWords);
                htlist.Add(ht);
            }
            return Json(htlist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAppHospTree()
        {
            var query = oc.iBllSession.Itb_Hosp_Info_Bo_BLL.GetListBy(p => p.IsDel == 0 && p.state==0, p => p.HospId);
            List<tb_Emp_Hos> hospList = AdminSystemInfo.EmpHospList;
            List<Hashtable> htlist = new List<Hashtable>();
            if (hospList != null && hospList.Count > 0)
            {

                var hids = hospList.Select(p => p.hospid).ToList();
                foreach (var item in query)
                {
                    if (hids.Contains(item.HospId))
                    {
                        Hashtable ht = new Hashtable();
                        ht.Add("id", item.HospId);
                        ht.Add("value", item.HospId);
                        ht.Add("text", item.Hname);
                        htlist.Add(ht);
                    }
                }
            }
            else
            {
                Hashtable htsel = new Hashtable();
                htsel.Add("id", "");
                htsel.Add("value", "");
                htsel.Add("text", "请选择");
                htsel.Add("selected", true);
                htlist.Add(htsel);
            }
            return Json(htlist, JsonRequestBehavior.AllowGet);
        }
	}
}