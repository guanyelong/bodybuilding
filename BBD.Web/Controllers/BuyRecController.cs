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
    public class BuyRecController : Controller
    {
        OperContext oc = OperContext.CurrentContext;
        //
        // GET: /BuyRec/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dialog(int Id, string uid)
        {
            tb_Serv_Buy_temp si = new tb_Serv_Buy_temp();
            if (uid == "undefined") uid = string.Empty;
            int uuid = string.IsNullOrWhiteSpace(uid) ? 0 : int.Parse(uid);
            if (Id != 0)
            {
                var info = oc.iBllSession.Itb_Serv_Info_Bo_BLL.GetObjet(p => p.ID == Id);
                if (info != null)
                {
                    si.ID = info.ID;
                    si.uId = uuid;
                    si.ServName = info.ServName;
                    si.ServMemo = info.ServMemo;
                    si.ServPrice = Convert.ToDecimal(info.price);
                    si.PayMoney = si.ServPrice;
                    si.ServNum = 1;
                }
                var hinfo = oc.iBllSession.Itb_User_Info_Bo_BLL.GetObjet(o => o.uId == uuid);
                if (hinfo != null)
                {
                    ViewBag.currentHospId = hinfo.HospId;
                }
            }
            return View(si);
        }


        public ActionResult GetAppBuyRecList()
        {
            int pageIndex = int.Parse(Request["page"]);  //当前页  
            int pageSize = int.Parse(Request["rows"]);  //页面行数
            string uid = Request["uid"];
            int count = 0;
            tb_User_Buy_Rec info = new tb_User_Buy_Rec();
            if (!string.IsNullOrWhiteSpace(uid))
            {
                info.uId = int.Parse(uid);

                IList<tb_User_Buy_Rec> query = oc.iBllSession.Itb_User_Buy_Rec_Bo_BLL.GetAppBuyRecList(pageIndex, pageSize, ref count, info);

                var data = new
                {
                    total = count,
                    rows = query
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
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
            else
            {
                info.ServName = "";
            }
            info.IsDel = 0;
            info.state = 0;
            IList<tb_Serv_Info> query = oc.iBllSession.Itb_Serv_POF_Bo_BLL.GetAppServList(pageIndex, pageSize, ref count, info);

            var data = new
            {
                total = count,
                rows = query
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveBuyRec(tb_Serv_Buy_temp si)
        {
            if (si == null)
            {
                return Json(new { result = "error", mesage = "数据为空" });
            }
            string errMsg = "";
            if (si.ID != 0)
            {
                tb_User_Buy_Rec br = new tb_User_Buy_Rec();
                br.uId = si.uId;
                br.RecDate = DateTime.Now;
                br.Buy_Hospid = si.HospId;
                br.BuyNum = si.ServNum;
                br.PayMoney = si.PayMoney;
                br.ProductId = si.ID;
                br.ProductPrice = si.ServPrice;
                //br.Creator = AdminSystemInfo.CurrentUser.uName;
                //br.CreatorId = AdminSystemInfo.CurrentUser.Uid;
                bool num = oc.iBllSession.Itb_User_Buy_Rec_Bo_BLL.BuyProductor(br);
                if (!num) errMsg = "添加失败";
            }
            var result = new { result = "ok", message = "操作成功" };

            if (!string.IsNullOrEmpty(errMsg))
            {
                result = new { result = "error", message = errMsg };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAppUserTree(string hospId)
        {
            int hid = 0;
            if (hospId == "undefined" || string.IsNullOrWhiteSpace(hospId)) hid = -1;
            else hid = int.Parse(hospId);
            List<Hashtable> htlist = new List<Hashtable>();
            var query = oc.iBllSession.Itb_User_Info_Bo_BLL.GetListBy(p => p.isDel == 0 && p.HospId == hid, p => p.C_time);
            //Hashtable htq = new Hashtable();
            //htq.Add("id", "");
            //htq.Add("value", "");
            //htq.Add("text", "请选择");
            //htlist.Add(htq);
            foreach (var item in query)
            {
                Hashtable ht = new Hashtable();
                ht.Add("id", item.uId);
                ht.Add("value", item.uId);
                ht.Add("text", item.Name);
                htlist.Add(ht);
            }
            return Json(htlist, JsonRequestBehavior.AllowGet);
        }
    }
}