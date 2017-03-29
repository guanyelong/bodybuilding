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

        public JsonResult GetAppHosp() {
            return View();
        }
	}
}