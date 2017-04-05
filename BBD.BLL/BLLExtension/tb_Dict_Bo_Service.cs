using BBD.Common;
using BBD.Common.Redis;
using BBD.IBLL;
using BBD.Models;
using ServiceStack.Redis;
using ServiceStack.Redis.Support;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BBD.BLL
{
    public partial class tb_Dict_Bo_Service : Itb_Dict_Bo_BLL
    {
        ///// <summary>
        ///// 获取所有用户
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public tb_Dict GetAllAppDictById(int id)
        //{
        //    using (BXUUEntities AppStoreEntity = new BXUUEntities())
        //    {
        //        tb_Dict model = null;
        //        var query = AppStoreEntity.tb_Dicts.Where(p => p.Id == id).FirstOrDefault();
        //        if (query == null)
        //        {
        //            return new tb_Dict();
        //        }

        //        model = new tb_Dict();
        //        model.Id = query.Id;
        //        model.KeyName = query.KeyName;
        //        model.KeyWords = query.KeyWords;
        //        model.KeyValue = query.KeyValue;
        //        model.Seq = query.Seq;
        //        model.C_Time = query.C_Time;
        //        model.state = query.state;
        //        model.mark = query.mark;

        //        return model;
        //    }
        //}

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<tb_Dict> GetAppDictList(int pageIndex, int pageSize, ref int count, string name, string word)
        {
            try
            {
                using (BXUUEntities appEntites = new BXUUEntities())
                {
                    var DictList = appEntites.tb_Dicts.Where(o => o.state == 1||o.state==0);
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        DictList = DictList.Where(o => o.KeyName.Contains(name) || (o.mark != null && o.mark.Contains(name)));
                        //DictList = DictList.Where(o => o.KeyName.Contains(name));
                    }
                    if (!string.IsNullOrWhiteSpace(word))
                    {
                        DictList = DictList.Where(o => o.KeyName.Contains(word) || o.mark.Contains(word) || o.KeyValue.Contains(word) || o.KeyWords.Contains(word));
                    }

                    count = DictList.Count();
                    if (count < 1)
                    {
                        return new List<tb_Dict>();
                    }
                    DictList = DictList.OrderByDescending(o => o.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    return DictList.ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 添加字典
        /// </summary>
        /// <param name="DictInfo"></param>
        /// <param name="errMsg"></param>
        public void AddDict(tb_Dict DictInfo, ref string errMsg)
        {
            try
            {
                 using (BXUUEntities appEntities = new BXUUEntities())
                 {
                     //var existDict = appEntities.tb_Dicts.Where(o=>o.state==1).FirstOrDefault();
                     var existDict = this.GetListBy(o => (o.KeyName == DictInfo.KeyName && o.KeyWords == DictInfo.KeyWords) || (o.KeyName == DictInfo.KeyName && o.KeyValue == DictInfo.KeyValue));
                     if (existDict != null && existDict.Count > 0)
                     {
                         errMsg = "相同类型下，字典名称/值不能重复";
                         return;
                     }
                     DictInfo.state = 1;
                     appEntities.tb_Dicts.Add(DictInfo);
                     appEntities.SaveChanges();
                 }
                
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
        }

        /// <summary>
        /// 编辑字典
        /// </summary>
        /// <param name="DictInfo"></param>
        /// <param name="errMsg"></param>
        public void EditDict(tb_Dict DictInfo, ref string errMsg)
        {
            try
            {
                using (BXUUEntities appEntities = new BXUUEntities())
                {
                    tb_Dict DictItem = appEntities.tb_Dicts.Where(o => o.Id == DictInfo.Id).FirstOrDefault();
                    if (DictItem == null)
                    {
                        errMsg = "查无数据";
                        return;
                    }

                    //检查字典值/名称不能重复
                    var existDict = this.GetListBy(o =>((o.KeyName==DictInfo.KeyName && o.KeyWords == DictInfo.KeyWords)||(o.KeyName==DictInfo.KeyName&& o.KeyValue == DictInfo.KeyValue))&&o.Id!=DictInfo.Id);
                    if (existDict.Count() > 0)
                    {
                        errMsg = "相同类型下，字典名称/值不能重复";
                        return;
                    }
                    DictItem.KeyWords = DictInfo.KeyWords;
                    //DictItem.KeyName = DictInfo.KeyName;
                    DictItem.KeyValue = DictInfo.KeyValue;
                    DictItem.Seq = DictInfo.Seq;
                    DictItem.mark = DictInfo.mark;

                    appEntities.SaveChanges();
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
        }
        
        /// <summary>
        /// 获取Combobox数据
        /// </summary>
        /// <param name="count"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public object GetDictComboData(ref int count, ref string errMsg)
        {
            try
            {
                List<tb_Dict> dictList = new List<tb_Dict>();

                count = dictList.Count();

                List<Hashtable> tableList = new List<Hashtable>();
                Hashtable noneTable = new Hashtable();
                noneTable.Add("id", "");
                noneTable.Add("text", "请选择");
                tableList.Add(noneTable);


                using (BXUUEntities appEntitys = new BXUUEntities())
                {
                    var query = from items in appEntitys.tb_Dicts group items by items.KeyName into g select new { g.Key, info = g.FirstOrDefault() };
                    foreach (var item in query)
                    {
                        Hashtable ht = new Hashtable();
                        ht.Add("id", item.info.KeyName);
                        ht.Add("text", item.info.KeyName);

                        tableList.Add(ht);
                    }
                }
                //foreach (var item in dictList)
                //{
                //    Hashtable ht = new Hashtable();
                //    ht.Add("id", item.Id);
                //    ht.Add("text", item.KeyName);
                //    tableList.Add(ht);
                //}
                return tableList;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return new List<Hashtable>();
            }
        }
    }
}
