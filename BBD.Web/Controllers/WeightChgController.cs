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
    public class WeightChgController : Controller
    {
        OperContext oc = OperContext.CurrentContext;
        //
        // GET: /WeightChg/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dialog(int Id,int uid)
        {
            tb_Weight_Chg wc = new tb_Weight_Chg();
            if (Id != 0)
            {
                wc = oc.iBllSession.Itb_Weight_Chg_Bo_BLL.GetObjet(p => p.Id == Id);
                if (wc.uId!=0)
                {
                    wc.uName = oc.iBllSession.Itb_User_Info_Bo_BLL.GetObjet(p => p.uId == wc.uId).Name;
                }
            }
            if (uid != 0)
            {
                var info = oc.iBllSession.Itb_User_Info_Bo_BLL.GetObjet(p => p.uId == uid);
                if (info != null)
                {
                    wc.uId = info.uId;
                    wc.uName = info.Name;
                }
            }
            return View(wc);
        }


        public ActionResult GetAppHeightChList()
        {
            int pageIndex = int.Parse(Request["page"]);  //当前页  
            int pageSize = int.Parse(Request["rows"]);  //页面行数
            string uName = Request["uName"];
            string Mobile = Request["Mobile"];
            string uid = Request["uid"];
            int count = 0;
            tb_Weight_Chg info = new tb_Weight_Chg();
            if (!string.IsNullOrWhiteSpace(uName))
            {
                info.uName = uName;
            }
            else
            {
                info.uName = "";
            }
            if (!string.IsNullOrWhiteSpace(Mobile))
            {
                info.Mobile = Mobile;
            }
            else
            {
                info.Mobile = "";
            }
            if (!string.IsNullOrWhiteSpace(uid) && uid!="undefined")
            {
                info.uId = int.Parse(uid);
            }
            else
            {
                info.uId = 0;
            }
            var query = oc.iBllSession.Itb_Weight_Chg_Bo_BLL.GetAppWeightChgList(pageIndex, pageSize, ref count,info);
            var data = new
            {
                total = count,
                rows = query
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveWeightChg(tb_Weight_Chg wc)
        {
            if (wc == null || wc.uId==0)
            {
                return Json(new { result = "error", mesage = "数据为空或不正确" });
            }
            string errMsg = "";
            //if(wc.TouchFlag==null)
            if (wc.Id != 0)
            {
                string[] prop = { "RecDate", "Hid", "Weight", "PicPath", "TouchFlag" };
                var info = oc.iBllSession.Itb_Weight_Chg_Bo_BLL.GetObjet(p => p.Id == wc.Id);
                if (info!=null)
                {
                    info.RecDate = wc.RecDate;
                    info.Hid = wc.Hid;
                    info.Weight = wc.Weight;
                    info.PicPath = wc.PicPath;
                    info.TouchFlag = wc.TouchFlag;
                    int num = oc.iBllSession.Itb_Weight_Chg_Bo_BLL.Modify(info, prop);
                    if (num < 1) errMsg = "修改失败";
                }
            }
            else
            {
                wc.Creator = AdminSystemInfo.CurrentUser.uName;
                wc.CreatorId = AdminSystemInfo.CurrentUser.Uid;
                int num = oc.iBllSession.Itb_Weight_Chg_Bo_BLL.Add(wc);
                if (num < 1) errMsg = "新增失败";
            }
            var result = new { result = "ok", message = "操作成功" };

            if (!string.IsNullOrEmpty(errMsg))
            {
                result = new { result = "error", message = errMsg };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAppHospTree()
        {
            var query = oc.iBllSession.Itb_Hosp_Info_Bo_BLL.GetListBy(p => p.IsDel==0, p => p.HospId);
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
                ht.Add("id", item.HospId);
                ht.Add("value", item.HospId);
                ht.Add("text", item.Hname);
                htlist.Add(ht);
            }
            return Json(htlist, JsonRequestBehavior.AllowGet);
        }

	}
}