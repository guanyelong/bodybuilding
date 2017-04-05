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
    public class DislikeFoodController : Controller
    {
        OperContext oc = OperContext.CurrentContext;
        //
        // GET: /DislikeFood/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FoodDialog(int Id, int uId)
        {
            tb_User_Dislike_Food udf = new tb_User_Dislike_Food();
            if (uId != 0)
            {
                udf = oc.iBllSession.Itb_User_Dislike_Food_Bo_BLL.GetObjet(p => p.uId == uId && p.id == Id);
            }
            if (udf == null) {
                udf = new tb_User_Dislike_Food();
                udf.uId = uId; 
            }
            return View(udf);
        }

        public ActionResult GetAppFoodList()
        {
            int pageIndex = int.Parse(Request["page"]);  //当前页  
            int pageSize = int.Parse(Request["rows"]);  //页面行数
            string uid = Request["uid"];
            tb_User_Dislike_Food info = new tb_User_Dislike_Food();
            if (!string.IsNullOrWhiteSpace(uid) && uid != "undefined")
                info.uId = int.Parse(uid);
            else
                info.uId = 0;
            int count = 0;
            IList<tb_User_Dislike_Food> query = oc.iBllSession.Itb_User_Dislike_Food_Bo_BLL.GetAppFoodList(pageIndex, pageSize, ref count, info);
            var data = new
            {
                total = count,
                rows = query
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAppFoodContentForType(int uid,string type) 
        {
            var query = oc.iBllSession.Itb_User_Dislike_Food_Bo_BLL.GetObjet(p => p.uId == uid && p.FoodType == type);
            string content = string.Empty;
            var result = new { result = "ok", message = content };
            if (query!=null)
            {
                result = new { result = "ok", message = query.FoodName };
            }
            else if (string.IsNullOrEmpty(content))
            {
                result = new { result = "error", message = "" };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAppFoodTypeTree() 
        {
            var query = oc.iBllSession.Itb_Dict_Bo_BLL.GetListBy(p => p.KeyName == "FoodType", p => p.Seq);
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

        public JsonResult DeleteFood(int Id)
        {
            string errMsg = "";
            if (Id != 0)
            {
                var info = oc.iBllSession.Itb_User_Dislike_Food_Bo_BLL.GetObjet(p => p.id == Id);
                if (info != null)
                {
                    int num = oc.iBllSession.Itb_User_Dislike_Food_Bo_BLL.Del(info);
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
        public JsonResult SaveFood(tb_User_Dislike_Food hw)
        {
            if (hw == null)
            {
                return Json(new { result = "error", mesage = "数据为空" });
            }
            string errMsg = "";
            var info = oc.iBllSession.Itb_User_Dislike_Food_Bo_BLL.GetObjet(p => p.uId == hw.uId && p.FoodType == hw.FoodType);
            if (info==null)//添加
            {
                info.Creator = AdminSystemInfo.CurrentUser.uName;
                info.CreatorId = AdminSystemInfo.CurrentUser.Uid;
                int num = oc.iBllSession.Itb_User_Dislike_Food_Bo_BLL.Add(hw);
                if (num < 1) errMsg = "添加失败";
            }
            else
            {
                string[] prop = { "FoodName" };
                info.FoodName = hw.FoodName;
                int num = oc.iBllSession.Itb_User_Dislike_Food_Bo_BLL.Modify(info, prop);
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