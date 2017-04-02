using BBD.Models;
using BBD.Web.Models;
using System;
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

        public ActionResult Dialog(int Id)
        {
            tb_Serv_Info si = new tb_Serv_Info();
            if (Id != 0)
            {
                si = oc.iBllSession.Itb_Serv_Info_Bo_BLL.GetObjet(p => p.ID == Id);
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
                info.uId = int.Parse(uid);
            else
                info.uId = 0;
            IList<tb_User_Buy_Rec> query = oc.iBllSession.Itb_User_Buy_Rec_Bo_BLL.GetAppBuyRecList(pageIndex, pageSize, ref count, info);

            var data = new
            {
                total = count,
                rows = query
            };
            return Json(data, JsonRequestBehavior.AllowGet);
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
	}
}