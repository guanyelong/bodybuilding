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
    public class CustomerController : Controller
    {
        OperContext oc = OperContext.CurrentContext;
        //
        // GET: /Customer/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dialog(int Id)
        {
            tb_User_Info hw = new tb_User_Info();
            if (Id != 0)
            {
                hw = oc.iBllSession.Itb_User_Info_Bo_BLL.GetObjet(p => p.uId == Id);
            }
            return View(hw);
        }

        public ActionResult GetAppUserList()
        {
            int pageIndex = int.Parse(Request["page"]);  //当前页  
            int pageSize = int.Parse(Request["rows"]);  //页面行数
            string Name = Request["Name"];
            string Mobile = Request["Mobile"];
            string Female = Request["Female"];
            string ComeFrom = Request["ComeFrom"];
            string HospId = Request["HospId"];
            string uid = Request["uid"];
            int count = 0;
            tb_User_Info info = new tb_User_Info();
            info.Name = Name;
            info.Mobile = Mobile;
            info.Female = Female;
            info.ComeFrom = ComeFrom;
            if (string.IsNullOrWhiteSpace(HospId))
            {
                List<tb_Emp_Hos> hospList = AdminSystemInfo.EmpHospList;
                if (hospList!=null && hospList.Count>0)
                {
                    var hid = hospList.OrderBy(p=>p.hospid).Select(p => p.hospid).FirstOrDefault();
                    info.HospId = hid;
                }
                else
                {
                    info.HospId = -1;
                }
                
            }
            else
            {
                info.HospId = int.Parse(HospId);
            }
            //info.HospId = string.IsNullOrWhiteSpace(HospId) ? 0 : int.Parse(HospId);
            if (!string.IsNullOrWhiteSpace(uid) && uid != "0")
                info.uId = int.Parse(uid);
            else
                info.uId = 0;
            IList<tb_User_Info> query = oc.iBllSession.Itb_User_Info_Bo_BLL.GetAppUserList(pageIndex, pageSize, ref count, info);
            var data = new
            {
                total = count,
                rows = query
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteUser(int Id)
        {
            string errMsg = "";
            if (Id != 0)
            {
                var info = oc.iBllSession.Itb_User_Info_Bo_BLL.GetObjet(p => p.uId == Id);
                if (info != null)
                {
                    info.isDel = 1;
                    string[] prop = { "isDel" };
                    int num = oc.iBllSession.Itb_User_Info_Bo_BLL.Modify(info,prop);
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
        public JsonResult SaveUser(tb_User_Info ui)
        {
            if (ui == null)
            {
                return Json(new { result = "error", mesage = "数据为空" });
            }
            string errMsg = "";
            ui.Female = string.IsNullOrWhiteSpace(ui.Female) ? "男" : ui.Female;
            if (ui.Height != null && ui.Weight != null) ui.BMI = ui.Weight / (ui.Height * ui.Height);
            if (ui.uId == 0)
            {
                ui.isDel = 0;
                ui.CreatorId = AdminSystemInfo.CurrentUser.Uid;
                ui.Creator = AdminSystemInfo.CurrentUser.uName;
                int num = oc.iBllSession.Itb_User_Info_Bo_BLL.Add(ui);
                if (num < 1) errMsg = "添加失败";
            }
            else
            {
                string[] prop = { "uId", "C_time", "CreatorId", "Creator", "isDel", "HospName", "ComeFromName", "IndustryTypeName", "CityName" };
                int num = oc.iBllSession.Itb_User_Info_Bo_BLL.Modifyed(ui, prop);
                if (num < 1) errMsg = "修改失败";
            }
            var result = new { result = "ok", message = "操作成功" };

            if (!string.IsNullOrEmpty(errMsg))
            {
                result = new { result = "error", message = errMsg };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAppComeFromTree()
        {
            List<Hashtable> htlist = new List<Hashtable>();
            var query = oc.iBllSession.Itb_Dict_Bo_BLL.GetListBy(p => p.KeyName == "comfrom", p => p.Seq);
            Hashtable htq = new Hashtable();
            htq.Add("id", "");
            htq.Add("value", "");
            htq.Add("text", "请选择");
            htlist.Add(htq);
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

        public JsonResult GetAppCityTree()
        {
            List<Hashtable> htlist = new List<Hashtable>();
            var query = oc.iBllSession.Itb_Dict_Bo_BLL.GetListBy(p => p.KeyName == "city", p => p.Seq);
            Hashtable htq = new Hashtable();
            htq.Add("id", "");
            htq.Add("value", "");
            htq.Add("text", "请选择");
            htlist.Add(htq);
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
            List<Hashtable> htlist = new List<Hashtable>();
            var query = oc.iBllSession.Itb_Hosp_Info_Bo_BLL.GetListBy(p => p.IsDel == 0, p => p.HospId);
            List<tb_Emp_Hos> hospList = AdminSystemInfo.EmpHospList;
            if (hospList!=null && hospList.Count>0)
            {
                List<int?> hospIds = hospList.OrderBy(p=>p.hospid).Select(p => p.hospid).ToList();
                foreach (var item in query)
                {
                    if (hospIds.Contains(item.HospId))
                    {
                        Hashtable ht = new Hashtable();
                        ht.Add("id", item.HospId);
                        ht.Add("value", item.HospId);
                        ht.Add("text", item.Hname);
                        htlist.Add(ht);
                    }
                }
            }
            else//没有权限
            {
                Hashtable htq = new Hashtable();
                htq.Add("id", "-1");
                htq.Add("value", "");
                htq.Add("text", "请选择");
                htlist.Add(htq);
            }
            
            
            return Json(htlist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAppIndustryTypeTree()
        {
            List<Hashtable> htlist = new List<Hashtable>();
            var query = oc.iBllSession.Itb_Dict_Bo_BLL.GetListBy(p => p.KeyName == "IndustryType", p => p.Seq);
            Hashtable htq = new Hashtable();
            htq.Add("id", "");
            htq.Add("value", "");
            htq.Add("text", "请选择");
            htlist.Add(htq);
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
	}
}