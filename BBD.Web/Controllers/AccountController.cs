using BBD.Models;
using BBD.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBD.Web.Controllers
{
    public class AccountController : Controller
    {
        OperContext oc = OperContext.CurrentContext;
        //
        // GET: /Account/ 
        public ActionResult Index(int uid)
        {
            tb_User_Info info = new tb_User_Info();
            if (uid != 0)
            {
                var list = oc.iBllSession.Itb_User_Account_Bo_BLL.GetListBy(p => p.uId == uid);
                if (list != null)
                {
                    //仪器 0 点穴1
                    foreach (var item in list)
                    {
                        if (item.TouchFlag == 0)
                        {
                            ViewBag.instrumentCount = item.delay;
                        }
                        if (item.TouchFlag == 1)
                        {
                            ViewBag.TouchCount = item.delay;
                        }
                    }
                }
                info = oc.iBllSession.Itb_User_Info_Bo_BLL.GetObjet(p => p.uId == uid);
            }
            return View(info);
        }

        public ActionResult ConsumeDialog(int uId)
        {
            tb_Consume_Log hw = new tb_Consume_Log();
            if (uId != 0)
            {
                hw.uId = uId;
                var query = oc.iBllSession.Itb_User_Account_Bo_BLL.GetListBy(p => p.uId == uId,p=>p.TouchFlag);
                foreach (var item in query)
                {
                    if (item.TouchFlag==0)
                    {
                        ViewBag.Enum = item.delay;
                    }
                    if (item.TouchFlag==1)
                    {
                        ViewBag.Tnum = item.delay;
                    }
                }
                var info = oc.iBllSession.Itb_User_Info_Bo_BLL.GetObjet(p => p.uId == uId);
                //检查剩余次数
                if (info != null) hw.uName = info.Name;
            }
            return View(hw);
        }

        public ActionResult GetAppConsumeList()
        {
            int pageIndex = int.Parse(Request["page"]);  //当前页  
            int pageSize = int.Parse(Request["rows"]);  //页面行数
            string uid = Request["uid"];
            int uuid=-1;
            if (!string.IsNullOrWhiteSpace(uid)) uuid = int.Parse(uid);
            int count = 0;
            List<tb_Consume_Log> query = oc.iBllSession.Itb_Consume_Log_Bo_BLL.GetPageList(pageIndex,pageSize,ref count,p=>p.uId==uuid,p=>p.c_time,false);
            var data = new
            {
                total = count,
                rows = query
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveCunsume(tb_Consume_Log cl)
        {
            if (cl == null)
            {
                return Json(new { result = "error", mesage = "数据为空" });
            }
            if (cl.Tnum==0 && cl.Enum==0)
            {
                return Json(new { result = "error", mesage = "消费次数不足" });
            }
            string errMsg = "";
            //cl.creator = AdminSystemInfo.CurrentUser.uName;
            //cl.creatorid = AdminSystemInfo.CurrentUser.Uid;
            bool num = oc.iBllSession.Itb_Consume_Log_Bo_BLL.UpdConsumeLog(cl);
            if (!num)
            {
                errMsg = "操作失败";
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