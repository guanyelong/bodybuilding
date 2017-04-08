using BBD.Models;
using BBD.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBD.Web.Controllers
{
    public class ConsumeLogController : Controller
    {
        OperContext oc = OperContext.CurrentContext;
        //
        // GET: /ConsumeLog/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ConsumeDialog(int Id)
        {
            tb_Consume_Log cl = new tb_Consume_Log();
            if (Id != 0)
            {
                cl = oc.iBllSession.Itb_Consume_Log_Bo_BLL.GetObjet(p => p.Id==Id);
            }
            return View(cl);
        }

        [HttpPost]
        public JsonResult SaveRecLog(tb_Consume_Log cl)
        {
            if (cl == null)
            {
                return Json(new { result = "error", mesage = "数据为空" });
            }
            string errMsg = "";
            if (cl.Id != 0)
            {
                string[] prop = { "finish_time" };
                var info = oc.iBllSession.Itb_Consume_Log_Bo_BLL.GetObjet(p => p.Id == cl.Id);
                if (info!=null)
                {
                    info.finish_time = cl.finish_time;
                    int num = oc.iBllSession.Itb_Consume_Log_Bo_BLL.Modify(info, prop);
                    if (num < 1) errMsg = "修改失败";
                }
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