using BBD.Models;
using BBD.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBD.Web.Controllers
{
    public class WeightChgSelfController : Controller
    {
        OperContext oc = OperContext.CurrentContext;
        //
        // GET: /WeightChgSelf/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dialog(int Id, int uid)
        {
            tb_Weight_Chg_Self hw = new tb_Weight_Chg_Self();
            if (Id != 0)
            {
                hw = oc.iBllSession.Itb_Weight_Chg_Self_Bo_BLL.GetObjet(p => p.Id == Id);
            }
            if (uid!=0)
            {
                hw.uName = oc.iBllSession.Itb_User_Info_Bo_BLL.GetObjet(p => p.uId == uid).Name;
                hw.uId = uid;
            }
            return View(hw);
        }


        public ActionResult GetAppWeightChgSelfList()
        {
            int pageIndex = int.Parse(Request["page"]);  //当前页  
            int pageSize = int.Parse(Request["rows"]);  //页面行数
            string uid = Request["uid"];
            string uName = Request["uName"];
            string Mobile = Request["Mobile"];
            int count = 0;
            tb_Weight_Chg_Self info = new tb_Weight_Chg_Self();
            if (!string.IsNullOrWhiteSpace(uid) && uid != "undefined")
            {
                info.uId = int.Parse(uid);
            }
            else
            {
                info.uId = 0;
            }
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
            var query = oc.iBllSession.Itb_Weight_Chg_Self_Bo_BLL.GetAppWeightSelfList(pageIndex, pageSize, ref count, info);
            var data = new
            {
                total = count,
                rows = query
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveWeightChgSelf(tb_Weight_Chg_Self wcs)
        {
            if (wcs == null)
            {
                return Json(new { result = "error", mesage = "数据为空" });
            }
            string errMsg = "";
            if (wcs.Id == 0)
            {
                int num = oc.iBllSession.Itb_Weight_Chg_Self_Bo_BLL.Add(wcs);
                if (num < 1) errMsg = "添加失败";
            }
            else
            {
                string[] prop = { "RecDate", "Hid", "Weight",};
                int num = oc.iBllSession.Itb_Weight_Chg_Self_Bo_BLL.Modify(wcs, prop);
                if (num < 1) errMsg = "修改失败";
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