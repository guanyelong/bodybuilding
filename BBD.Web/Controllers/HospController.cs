using BBD.Common;
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
    public class HospController : Controller
    {
        OperContext oc = OperContext.CurrentContext;
        //
        // GET: /Hosp/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dialog(int Id)
        {
            tb_Hosp_Info hi = new tb_Hosp_Info();
            if (Id != 0)
            {
                hi = oc.iBllSession.Itb_Hosp_Info_Bo_BLL.GetObjet(p => p.HospId == Id);
            }
            return View(hi);
        }


        public ActionResult GetAppHospList()
        {
            int pageIndex = int.Parse(Request["page"]);  //当前页  
            int pageSize = int.Parse(Request["rows"]);  //页面行数
            string Hname = Request["Hname"];
            string tel = Request["tel"];
            int count = 0;
            tb_Hosp_Info info=new tb_Hosp_Info();
            if (string.IsNullOrWhiteSpace(Hname))
            {
                Hname = "";
            }
            if (string.IsNullOrWhiteSpace(tel))
            {
                tel = "";
            }
            info.Hname=Hname;
            info.tel=tel;

            List<tb_Emp_Hos> hospList = AdminSystemInfo.EmpHospList;
            if (hospList != null && hospList.Count > 0)
            {

                var hids = hospList.Select(p => p.hospid).ToList();
                var strHids = "";
                for (int i = 0; i < hids.Count; i++)
                {
                    strHids += hids[i] + ",";
                }
                if (strHids.Length > 0)
                {
                    strHids = "," + strHids;
                }
                info.HospStrIds = strHids;
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            IList<tb_Hosp_Info> query = oc.iBllSession.Itb_Hosp_Info_Bo_BLL.GetAppHospList(pageIndex, pageSize, ref count, info);
            
            var data = new
            {
                total = count,
                rows = query
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAppHospListForEmp()
        {
            int pageIndex = int.Parse(Request["page"]);  //当前页  
            int pageSize = int.Parse(Request["rows"]);  //页面行数
            string Hname = Request["Hname"];
            string tel = Request["tel"];
            int count = 0;
            tb_Hosp_Info info = new tb_Hosp_Info();
            if (string.IsNullOrWhiteSpace(Hname))
            {
                Hname = "";
            }
            if (string.IsNullOrWhiteSpace(tel))
            {
                tel = "";
            }
            info.Hname = Hname;
            info.tel = tel;
            info.HospStrIds = "-1";
            

            IList<tb_Hosp_Info> query = oc.iBllSession.Itb_Hosp_Info_Bo_BLL.GetAppHospList(pageIndex, pageSize, ref count, info);

            var data = new
            {
                total = count,
                rows = query
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteHosp(int Id)
        {
            string errMsg = "";
            if (Id != 0)
            {
                var info = oc.iBllSession.Itb_Hosp_Info_Bo_BLL.GetObjet(p => p.HospId == Id);
                if (info != null)
                {
                    info.IsDel = 1;
                    string[] prop = { "IsDel" };
                    int num = oc.iBllSession.Itb_Hosp_Info_Bo_BLL.Modify(info, prop);
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
        public JsonResult SaveHosp(tb_Hosp_Info hi)
        {
            if (hi == null)
            {
                return Json(new { result = "error", mesage = "数据为空" });
            }
            string errMsg = "";
            if (hi.HospId == 0)
            {
                hi.IsDel = 0;
                hi.state = 0;
                hi.PyLong = new ChineseToPinyin().GetPinyin(hi.Hname);
                hi.PyShort = ChineseToPinyin.GetCodstring(hi.Hname);
                hi.CreatorId = AdminSystemInfo.CurrentUser.Uid;
                int num = oc.iBllSession.Itb_Hosp_Info_Bo_BLL.Add(hi);
                if (num < 1) errMsg = "添加失败";
            }
            else
            {
                string[] prop = { "HospId", "C_time", "CreatorId", "IsDel", "state", "sTypeName", "CityName", "HospStrIds" };
                hi.PyLong = new ChineseToPinyin().GetPinyin(hi.Hname);
                hi.PyShort = ChineseToPinyin.GetCodstring(hi.Hname);
                hi.ModfyId = AdminSystemInfo.CurrentUser.Uid;
                hi.ModLastTime = DateTime.Now;
                int num = oc.iBllSession.Itb_Hosp_Info_Bo_BLL.Modifyed(hi, prop);
                if (num < 1) errMsg = "修改失败";
            }
            var result = new { result = "ok", message = "操作成功" };

            if (!string.IsNullOrEmpty(errMsg))
            {
                result = new { result = "error", message = errMsg };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAppCityTree() {
            var query = oc.iBllSession.Itb_Dict_Bo_BLL.GetListBy(p=>p.KeyName=="city",p=>p.Seq);
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

        public JsonResult GetAppsTypeTree() {
            var query = oc.iBllSession.Itb_Dict_Bo_BLL.GetListBy(p => p.KeyName == "Hosptype", p => p.Seq);
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

        public JsonResult QueryHospKeyWord()
        {
            string query = Request["q"].ToUpper();
            List<string> list = new List<string>();
            List<tb_Hosp_Info> items = oc.iBllSession.Itb_Hosp_Info_Bo_BLL.GetListBy(p => (p.Hname.Contains(query) || p.PyLong.Contains(query) || p.PyShort.Contains(query)) && p.state == 0, p => p.C_Time, false);
            if (items != null)
            {
                var hnamelist = items.Where(p => p.Hname.Contains(query));
                if (hnamelist == null || hnamelist.Count() == 0)
                {
                    var shortlist = items.Where(p => p.PyShort.Contains(query));
                    if (shortlist == null || shortlist.Count() == 0)
                    {
                        list = items.Where(p => p.PyLong.Contains(query)).OrderByDescending(p => p.PyLong).Select(p => p.PyLong).ToList();
                    }
                    else
                    {
                        list = shortlist.OrderByDescending(p => p.PyShort).Select(p => p.PyShort).ToList();
                    }
                }
                else
                {
                    list = hnamelist.OrderByDescending(p => p.Hname).Select(p => p.Hname).ToList();
                }
            }

            List<Hashtable> tableList = new List<Hashtable>();
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("id", item);
                    ht.Add("value", item);
                    ht.Add("text", item);
                    tableList.Add(ht);
                }
            }
            return Json(tableList, JsonRequestBehavior.AllowGet);
        }

	}
}