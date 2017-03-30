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

        public ActionResult GetAppUserList()
        {
            int pageIndex = int.Parse(Request["page"]);  //当前页  
            int pageSize = int.Parse(Request["rows"]);  //页面行数
            string Hname = Request["Name"];
            string tel = Request["Mobile"];
            int count = 0;
            tb_Hosp_Info info = new tb_Hosp_Info();
            info.Hname = Hname;
            info.tel = tel;
            IList<tb_Hosp_Info> query = oc.iBllSession.Itb_Hosp_Info_Bo_BLL.GetAppHospList(pageIndex, pageSize, ref count, info);

            var data = new
            {
                total = count,
                rows = query
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Dialog(int Id)
        {
            tb_User_Info hi = new tb_User_Info();
            if (Id != 0)
            {
                hi = oc.iBllSession.Itb_Hosp_Info_Bo_BLL.GetObjet(p => p.HospId == Id);
            }
            return View(hi);
        }

        public JsonResult GetAppComeFrom() {
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

        public JsonResult GetAppHosp()
        {
            List<Hashtable> htlist = new List<Hashtable>();
            var query = oc.iBllSession.Itb_Hosp_Info_Bo_BLL.GetListBy(p => p.IsDel == 0, p => p.C_Time, false);
            Hashtable htq = new Hashtable();
            htq.Add("id", "");
            htq.Add("value", "");
            htq.Add("text", "请选择");
            htlist.Add(htq);
            foreach (var item in query)
            {
                Hashtable ht = new Hashtable();
                ht.Add("id", item.HospId);
                ht.Add("value", item.HospId);
                ht.Add("text", item.Hname);
                htlist.Add(ht);
            }
            return Json(htlist, JsonRequestBehavior.AllowGet);
        }
	}
}