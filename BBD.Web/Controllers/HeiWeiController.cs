using BBD.Models;
using BBD.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBD.Web.Controllers
{
    public class HeiWeiController : Controller
    {
        OperContext oc = OperContext.CurrentContext;
        //
        // GET: /HeiWei/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dialog(int Id)
        {
            tb_Hei_Wei hw = new tb_Hei_Wei();
            if (Id != 0)
            {
                hw = oc.iBllSession.Itb_Hei_Wei_Bo_BLL.GetObjet(p => p.Id == Id);
            }
            return View(hw);
        }


        public ActionResult GetAppHeiWeiList() 
        {
            int pageIndex = int.Parse(Request["page"]);  //当前页  
            int pageSize = int.Parse(Request["rows"]);  //页面行数
            string Height = Request["Height"];
            string Female = Request["Female"];
            int count = 0;
            List<tb_Hei_Wei> query = new List<tb_Hei_Wei>();
            if (string.IsNullOrWhiteSpace(Height) && string.IsNullOrWhiteSpace(Female))
            {
                query = oc.iBllSession.Itb_Hei_Wei_Bo_BLL.GetPageList(pageIndex, pageSize, ref count, p => p.Id > 0, p => p.Height);
            }
            else if (!string.IsNullOrWhiteSpace(Height) && !string.IsNullOrWhiteSpace(Female))
            {
                double h = double.Parse(Height);
                query = oc.iBllSession.Itb_Hei_Wei_Bo_BLL.GetPageList(pageIndex, pageSize, ref count, p => p.Female == Female && p.Height == h, p => p.Height);
            }
            else if (!string.IsNullOrWhiteSpace(Height))
            {
                double h = double.Parse(Height);
                query = oc.iBllSession.Itb_Hei_Wei_Bo_BLL.GetPageList(pageIndex, pageSize, ref count, p => p.Height==h, p => p.Height);
            }
            else if (!string.IsNullOrWhiteSpace(Female))
            {
                query = oc.iBllSession.Itb_Hei_Wei_Bo_BLL.GetPageList(pageIndex, pageSize, ref count, p => p.Female == Female, p => p.Height);
            }
            var data = new
            {
                total = count,
                rows = query
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteHeiWei(int Id)
        {
            string errMsg = "";
            if (Id!=0)
            {
                var info = oc.iBllSession.Itb_Hei_Wei_Bo_BLL.GetObjet(p => p.Id == Id);
                if (info != null) { 
                   int num =oc.iBllSession.Itb_Hei_Wei_Bo_BLL.Del(info);
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
        public JsonResult SaveHeiWei(tb_Hei_Wei hw) 
        {
            if (hw == null)
            {
                return Json(new { result = "error", mesage = "数据为空" });
            }
            string errMsg = "";
            hw.Female = string.IsNullOrWhiteSpace(hw.Female) ? "男" : hw.Female;
            if (hw.Id==0)
            {
                int num = oc.iBllSession.Itb_Hei_Wei_Bo_BLL.Add(hw);
                if (num < 1) errMsg = "添加失败";
            }
            else
            {
                string[] prop = { "Id", "C_time" };
                int num = oc.iBllSession.Itb_Hei_Wei_Bo_BLL.Modifyed(hw, prop);
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