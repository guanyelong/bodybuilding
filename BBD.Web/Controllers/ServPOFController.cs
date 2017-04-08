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
    public class ServPOFController : Controller
    {
        OperContext oc = OperContext.CurrentContext;
        //
        // GET: /Serv/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Dialog(int Id)
        {
            tb_Serv_POF hw = new tb_Serv_POF();
            if (Id != 0)
            {
                hw = oc.iBllSession.Itb_Serv_POF_Bo_BLL.GetObjet(p => p.Id == Id);
                if (hw!=null)
                {
                    var info = oc.iBllSession.Itb_Serv_Info_Bo_BLL.GetObjet(p => p.ServId == hw.ServId);
                    if (info!=null)
                    {
                        hw.ServName = info.ServName;
                    }
                }
            }
            return View(hw);
        }


        public ActionResult GetAppServPOFList()
        {
            int pageIndex = int.Parse(Request["page"]);  //当前页  
            int pageSize = int.Parse(Request["rows"]);  //页面行数
            string ServName = Request["ServName"];
            string HospId = Request["HospId"];
            int count = 0;
            tb_Serv_POF info = new tb_Serv_POF();
            if (!string.IsNullOrWhiteSpace(ServName))
            {
                info.ServName = ServName;
            }
            else
            {
                info.ServName = "";
            }

            if (string.IsNullOrWhiteSpace(HospId))
            {

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
                    info.HospIds = strHids;
                }
                else
                {

                    info.HospIds = "0";
                }
            }
            else
            {
                info.HospIds = "," + HospId + ",";
            }
            var query = oc.iBllSession.Itb_Serv_POF_Bo_BLL.GetAppServPOFList(pageIndex, pageSize, ref count, info);

            var data = new
            {
                total = count,
                rows = query
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteServPOF(int Id)
        {
            string errMsg = "";
            if (Id != 0)
            {
                var info = oc.iBllSession.Itb_Serv_POF_Bo_BLL.GetObjet(p => p.Id == Id);
                if (info != null)
                {
                    int num = oc.iBllSession.Itb_Serv_POF_Bo_BLL.Del(info);
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
        public JsonResult SaveServPOF(tb_Serv_POF hw)
        {
            var result = new { result = "ok", message = "操作成功" };
            if (hw == null)
            {
                return Json(new { result = "error", mesage = "数据为空" });
            }
            string errMsg = "";
            if (hw.Id == 0)
            {
                hw.Creator = AdminSystemInfo.CurrentUser.uName;
                hw.CreatorId = AdminSystemInfo.CurrentUser.Uid;
                if (hw.TouchFlag==2)
                {
                    var list = oc.iBllSession.Itb_Serv_POF_Bo_BLL.GetListBy(p => p.ServId == hw.ServId);
                    if (list == null || list.Count == 0)
                    {
                        hw.TouchFlag = 0;
                        int num = oc.iBllSession.Itb_Serv_POF_Bo_BLL.Add(hw);

                        hw.TouchFlag = 1;
                        int numtwo = oc.iBllSession.Itb_Serv_POF_Bo_BLL.Add(hw);
                        if (num < 1) errMsg = "添加失败";
                        if (numtwo < 1) errMsg = "添加失败";
                    }
                    else
                    {
                        result = new { result = "error", message = "服务下已存在相同配置" };
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (hw.TouchFlag != null)
                {
                    var list = oc.iBllSession.Itb_Serv_POF_Bo_BLL.GetListBy(p => p.ServId == hw.ServId);
                    if (list != null && list.Count == 2)
                    {
                        result = new { result = "error", message = "服务下已存在相同配置" };
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    if (list == null || list.Count == 0)
                    {
                        if (hw.TouchFlag==0)
                        {
                            hw.TouchFlag = 0;
                            int num = oc.iBllSession.Itb_Serv_POF_Bo_BLL.Add(hw);
                            if (num < 1) errMsg = "添加失败";
                        }
                        if (hw.TouchFlag==1)
                        {
                            hw.TouchFlag = 1;
                            int numtwo = oc.iBllSession.Itb_Serv_POF_Bo_BLL.Add(hw);
                            if (numtwo < 1) errMsg = "添加失败";
                        }
                       
                    }
                    else if (list != null && list.Count == 1)
                    {
                        var queryinfo = list.FirstOrDefault();
                        if (queryinfo.TouchFlag == hw.TouchFlag)
                        {
                            result = new { result = "error", message = "服务下已存在相同配置" };
                            return Json(result, JsonRequestBehavior.AllowGet);
                        }
                        int num = oc.iBllSession.Itb_Serv_POF_Bo_BLL.Add(hw);
                        if (num < 1) errMsg = "添加失败";
                    }
                }
                else 
                {
                    errMsg = "类型不能为空";
                }
            }
            else
            {
                string[] prop = { "Times" };
                var info = oc.iBllSession.Itb_Serv_POF_Bo_BLL.GetObjet(p => p.Id == hw.Id);
                int num = oc.iBllSession.Itb_Serv_POF_Bo_BLL.Modify(info, prop);
                if (num < 1) errMsg = "修改失败";
            }
            if (!string.IsNullOrEmpty(errMsg))
            {
                result = new { result = "error", message = errMsg };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAppHospTree()
        {
            List<Hashtable> htlist = new List<Hashtable>();
            var query = oc.iBllSession.Itb_Hosp_Info_Bo_BLL.GetListBy(p => p.IsDel == 0, p => p.HospId);
            List<tb_Emp_Hos> hospList = AdminSystemInfo.EmpHospList;
            if (hospList != null && hospList.Count > 0)
            {
                Hashtable htq = new Hashtable();
                htq.Add("id", "");
                htq.Add("value", "");
                htq.Add("text", "请选择");
                htlist.Add(htq);
                List<int?> hospIds = hospList.OrderBy(p => p.hospid).Select(p => p.hospid).ToList();
                foreach (var item in query)
                {
                    if (hospIds.Contains(item.HospId))
                    {
                        Hashtable ht = new Hashtable();
                        ht.Add("id", item.HospId);
                        ht.Add("value", item.HospId);
                        ht.Add("text", item.Hname);
                        htlist.Add(ht);
                    }
                }
            }
            else//没有权限
            {
                Hashtable htq = new Hashtable();
                htq.Add("id", "-1");
                htq.Add("value", "");
                htq.Add("text", "请选择");
                htlist.Add(htq);
            }
            return Json(htlist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAppServNameTree()
        {
            List<Hashtable> htlist = new List<Hashtable>();
            var query = oc.iBllSession.Itb_Serv_Info_Bo_BLL.GetListBy(p => p.IsDel == 0 && p.state==0, p => p.ServId);
            List<tb_Emp_Hos> hospList = AdminSystemInfo.EmpHospList;
            if (hospList != null && hospList.Count > 0)
            {
                Hashtable htq = new Hashtable();
                htq.Add("id", "");
                htq.Add("value", "");
                htq.Add("text", "请选择");
                htlist.Add(htq);
                List<int?> hospIds = hospList.OrderBy(p => p.hospid).Select(p => p.hospid).ToList();
                foreach (var item in query)
                {
                    if (hospIds.Contains(item.HospId))
                    {
                        Hashtable ht = new Hashtable();
                        ht.Add("id", item.ServId);
                        ht.Add("value", item.ServId);
                        ht.Add("text", item.ServName);
                        htlist.Add(ht);
                    }
                }
            }
            else//没有权限
            {
                Hashtable htq = new Hashtable();
                htq.Add("id", "-1");
                htq.Add("value", "");
                htq.Add("text", "请选择");
                htlist.Add(htq);
            }
            return Json(htlist, JsonRequestBehavior.AllowGet);
        }


    }
}