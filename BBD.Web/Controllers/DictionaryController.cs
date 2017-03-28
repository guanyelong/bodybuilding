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
    public class DictionaryController : Controller
    {
        //
        // GET: /Dictionary/
        OperContext oc = OperContext.CurrentContext;
        BXUUEntities appEntities = new BXUUEntities();
        //[AuthenticationAttribute]
        public ActionResult Index()
        {
            //    try
            //    {
            //        using (IRedisClient Redis = RedisManager.GetClient())
            //        {
            //            List<tb_Dict> list = appEntities.tb_Dicts.ToList();
            //            var ser = new ObjectSerializer();
            //            JavaScriptSerializer jss = new JavaScriptSerializer();
            //            HashOperator operators = new HashOperator();
            //            if (operators != null)
            //            {
            //                //内存实例
            //                //if (!operators.Exist<List<tb_Dict>>("hhys_hhm_id", "vkey"))
            //                //{
            //                //    bool reuslt = operators.Set<byte[]>("hhys_hhm_id", "vkey", ser.Serialize(list));
            //                //}
            //                //else
            //                //{
            //                //    byte[] b = operators.Get<byte[]>("hhys_hhm_id", "vkey");
            //                //    List<PMM.Models.tb_Dict> dict = ser.Deserialize(b) as List<PMM.Models.tb_Dict>;
            //                //}
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {

            //        throw;
            //    }

            return View();
        }

        /// <summary>
        /// 新增编辑的对话框页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Dialog(int Id)
        {
            if (Id > 0)
            {
                oc.iBllSession.Itb_Dict_Bo_BLL.DbName = "hhm";
                tb_Dict user = oc.iBllSession.Itb_Dict_Bo_BLL.GetObjet(o => o.Id == Id);
                if (user == null)
                {
                    user = new tb_Dict();
                }
                return View(user);
            }

            return View(new tb_Dict());
        }

        /// <summary>
        /// 获取分页的用户数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAppDictList()
        {
            oc.iBllSession.Itb_Dict_Bo_BLL.DbName = "hhm";
            int pageIndex = int.Parse(Request["page"]);  //当前页  
            int pageSize = int.Parse(Request["rows"]);  //页面行数
            string name = Request["name"];
            string word = Request["word"];
            int count = 0;

            //var DictList = appEntities.tb_Dicts.Where(o => o.state == 1);
            //if (!string.IsNullOrWhiteSpace(name))
            //{
            //    DictList = DictList.Where(o => o.KeyName.Contains(name) || (o.mark != null && o.mark.Contains(name)));

            //}
            //if (!string.IsNullOrWhiteSpace(word))
            //{
            //    DictList = DictList.Where(o => o.KeyName.Contains(word) || o.mark.Contains(word) || o.KeyValue.Contains(word) || o.KeyWords.Contains(word));
            //}

            List<tb_Dict> appDictList = oc.iBllSession.Itb_Dict_Bo_BLL.GetAppDictList(pageIndex, pageSize, ref count, name, word);

            if (appDictList != null)
            {

                var result = from appDict in appDictList
                             select new
                             {
                                 Id = appDict.Id,
                                 appDict.KeyName,
                                 appDict.KeyWords,
                                 appDict.KeyValue,
                                 C_Time = Convert.ToDateTime(appDict.C_Time).ToString("yyyy-MM-dd HH:mm:ss"),
                                 appDict.Seq,
                                 appDict.state,
                                 appDict.mark
                             };

                var data = new
                {
                    total = count,
                    rows = result
                };

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="dictInfo"></param>
        /// <returns></returns>
        public JsonResult SaveDict(tb_Dict dictInfo)
        {
            oc.iBllSession.Itb_Dict_Bo_BLL.DbName = "hhm";
            if (dictInfo == null)
            {
                return Json(new { result = "error", message = "字典数据为空" });
            }
            if (dictInfo.KeyName == null)
            {
                return Json(new { result = "error", message = "字典类型不能为空！" });
            }
            string errMsg = "";
            if (dictInfo.Id == 0)
            {
                //检查字典值/名称不能重复

                //var existDict = oc.iBllSession.Itb_Dict_Bo_BLL.GetListBy(o => o.KeyWords == dictInfo.KeyWords || o.KeyValue == dictInfo.KeyValue);
                //var exist = oc.iBllSession.Itb_Dict_Bo_BLL.GetListBy(o => o.KeyWords == dictInfo.KeyWords);
                //var existDict = exist.Where(o => o.KeyWords == dictInfo.KeyWords || o.KeyValue == dictInfo.KeyValue);
                //if (existDict != null && existDict.ToList().Count > 0)
                //{
                //    return Json(new { result = "error", message = "相同类型下，字典名称/值不能重复" });
                //}
                dictInfo.C_Time = DateTime.Now;
                dictInfo.state = 1;
                //add
                oc.iBllSession.Itb_Dict_Bo_BLL.AddDict(dictInfo, ref errMsg);

                //Common.LogHelper.InsertLog(String.Format("新增角色,ID-{0}", roleInfo.ID.ToString()), 52, "角色列表");
            }
            else
            {
                //检查字典值/名称不能重复
                //var exist = oc.iBllSession.Itb_Dict_Bo_BLL.GetListBy(o => o.KeyWords == dictInfo.KeyWords);
                //var existDict = exist.Where(o => o.KeyWords == dictInfo.KeyWords || o.KeyValue == dictInfo.KeyValue);
                //if (existDict.ToList().Count > 0)
                //{
                //    return Json(new { result = "error", message = "相同类型下，字典名称/值不能重复" });
                //}
                //edit
                dictInfo.C_Time = DateTime.Now;
                dictInfo.state = 1;
                oc.iBllSession.Itb_Dict_Bo_BLL.EditDict(dictInfo, ref errMsg);

                //Common.LogHelper.InsertLog(String.Format("编辑角色,ID-{0}", roleInfo.ID.ToString()), 52, "角色列表");
            }

            var result = new { result = "ok", message = "操作成功" };

            if (!string.IsNullOrEmpty(errMsg))
            {
                result = new { result = "error", message = errMsg };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除字典数据
        /// </summary>
        /// <param name="dictId"></param>
        /// <returns></returns>
        public JsonResult DeleteDict(int dictId)
        {
            oc.iBllSession.Itb_Dict_Bo_BLL.DbName = "hhm";
            if (dictId == 0)
            {
                return Json(new { result = "error", message = "字典编号为空" });
            }

            string errMsg = string.Empty;
            var dicp = oc.iBllSession.Itb_Dict_Bo_BLL.GetObjet(o => o.Id == dictId);
            if (dicp != null)
            {
                dicp.state = 0;
                int num = oc.iBllSession.Itb_Dict_Bo_BLL.Modify(dicp, new string[1] { "state" });
                if (num == 0) errMsg = "删除失败！";
            }
            var result = new { result = "ok", message = "操作成功" };
            if (!string.IsNullOrEmpty(errMsg))
            {
                result = new { result = "error", message = errMsg };
            }
            //Common.LogHelper.InsertLog(String.Format("删除角色,ID-{0}", roleId.ToString()), 52, "角色列表");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDictCombobox()
        {

            string errMsg = string.Empty;
            int count = 0;
            //构造combobox tree 需要的数据
            var dict = oc.iBllSession.Itb_Dict_Bo_BLL.GetDictComboData(ref count, ref errMsg);

            if (!string.IsNullOrEmpty(errMsg))
            {
                dict = new List<Hashtable>();
            }
            return Json(dict, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetAppCityTree()
        {
            oc.iBllSession.Itb_Dict_Bo_BLL.DbName = "hhm";
            List<tb_Dict> list = oc.iBllSession.Itb_Dict_Bo_BLL.GetListBy(o => o.state == 1 && o.KeyName == "CITY", o => o.C_Time, false);
            List<Hashtable> dictTree = new List<Hashtable>();
            foreach (tb_Dict item in list)
            {
                Hashtable ht = new Hashtable();
                ht.Add("id", item.KeyValue);
                ht.Add("text", item.KeyWords);
                dictTree.Add(ht);
            }
            return Json(dictTree, JsonRequestBehavior.AllowGet);
        }


    }
}