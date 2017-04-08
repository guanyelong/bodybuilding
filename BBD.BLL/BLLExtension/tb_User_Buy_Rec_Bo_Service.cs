using BBD.Common;
using BBD.IBLL;
using BBD.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BBD.BLL
{
    public partial class tb_User_Buy_Rec_Bo_Service : Itb_User_Buy_Rec_Bo_BLL
    {
        public IList<tb_User_Buy_Rec> GetAppBuyRecList(int pageIndex, int pageSize, ref int count, tb_User_Buy_Rec info) 
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@uid",info.uId)
                };
                DataTable dt = BBD.Common.SQLHelp.ExecuteDataTable("Pro_Select_BuyRec", System.Data.CommandType.StoredProcedure, param);
                if (dt == null) return null;
                IList<tb_User_Buy_Rec> list = ModelConvertHelper<tb_User_Buy_Rec>.ConvertToModel(dt);
                count = list.Count;
                list = list.OrderByDescending(o => o.C_time).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return list;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }

        public bool BuyProductor(tb_User_Buy_Rec br) 
        {
            try
            {
                using (BXUUEntities appEntitys = new BXUUEntities())
                {
                    var cont = false;
                    var weight = false;
                    using (TransactionScope tran = new TransactionScope()) 
                    {
                        appEntitys.tb_User_Buy_Recs.Add(br);
                        appEntitys.SaveChanges();
                        var servInfo=appEntitys.tb_Serv_Infos.Where(p=>p.ID==br.ProductId).FirstOrDefault();
                        if (servInfo != null)
                        {
                           
                            var pofInfo = appEntitys.tb_Serv_POFs.Where(p => p.ServId == servInfo.ServId);
                            #region
                            if (pofInfo!=null)
                            {
                                var account = appEntitys.tb_User_Accounts.Where(p => p.uId == br.uId).ToList();
                                if (account==null || account.Count == 0)
                                {
                                    foreach (var item in pofInfo)
                                    {
                                        tb_User_Account ua = new tb_User_Account();
                                        ua.uId = Convert.ToInt32(br.uId);
                                        ua.TouchFlag = item.TouchFlag;
                                        ua.total = item.Times * br.BuyNum;
                                        ua.delay = item.Times * br.BuyNum;
                                        ua.creator = br.Creator;
                                        ua.creatorId = br.CreatorId;
                                        appEntitys.tb_User_Accounts.Add(ua);
                                        appEntitys.SaveChanges();
                                        cont = true;
                                    }
                                }
                                else
                                {
                                    if (account.Count==1)
                                    {
                                        if (account[0].TouchFlag == 0)//仪器更新 点穴新增
                                        {
                                            var one = false;
                                            var two = false;
                                            foreach (var item in pofInfo) 
                                            {
                                                if (item.TouchFlag==0)
                                                {

                                                    account[0].total = account[0].total + item.Times * br.BuyNum;
                                                    account[0].delay = account[0].delay + item.Times * br.BuyNum;
                                                    appEntitys.SaveChanges();
                                                    one = true;
                                                }
                                                else if (item.TouchFlag==1)
                                                {
                                                    tb_User_Account ua = new tb_User_Account();
                                                    ua.uId = Convert.ToInt32(br.uId);
                                                    ua.TouchFlag = item.TouchFlag;
                                                    ua.total = item.Times * br.BuyNum;
                                                    ua.delay = item.Times * br.BuyNum;
                                                    appEntitys.tb_User_Accounts.Add(ua);
                                                    appEntitys.SaveChanges();
                                                    two = true;
                                                }
                                            }
                                            if (one && two) cont = true;
                                        }
                                        else//仪器新增 点穴更新
                                        {
                                            var one = false;
                                            var two = false;
                                            foreach (var item in pofInfo)
                                            {
                                                if (item.TouchFlag == 0)
                                                {
                                                    tb_User_Account ua = new tb_User_Account();
                                                    ua.uId = Convert.ToInt32(br.uId);
                                                    ua.TouchFlag = item.TouchFlag;
                                                    ua.total = item.Times * br.BuyNum;
                                                    ua.delay = item.Times * br.BuyNum;
                                                    appEntitys.tb_User_Accounts.Add(ua);
                                                    appEntitys.SaveChanges();
                                                    one = true;
                                                }
                                                else if (item.TouchFlag == 1)
                                                {
                                                    account[0].total = account[0].total + item.Times * br.BuyNum;
                                                    account[0].delay = account[0].delay + item.Times * br.BuyNum;
                                                    appEntitys.SaveChanges();
                                                    two = true;
                                                }
                                            }
                                            if (one && two) cont = true;
                                        }
                                    }
                                    else
                                    {
                                        foreach (var item in pofInfo)
                                        {
                                            for (int i = 0; i < account.Count; i++)
                                            {
                                                if (item.TouchFlag==account[i].TouchFlag)
                                                {
                                                    account[i].total = account[i].total + item.Times;
                                                    account[i].delay = account[i].delay + item.Times;
                                                    appEntitys.SaveChanges();
                                                    cont = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                /////插入疗程
                                //int? maxCount = pofInfo.Max(p => p.Times);
                                //int? maxBatch = appEntitys.tb_Weight_Chgs.Where(p => p.uId == br.uId).Max(p => p.BatchId);
                                //if (maxBatch == null) maxBatch = 1;
                                //else maxBatch += 1;
                                //for (int k = 0; k < br.BuyNum; k++)
                                //{
                                //    for (int z = 0; z < maxCount; z++)
                                //    {
                                //        tb_Weight_Chg wc = new tb_Weight_Chg();
                                //        wc.uId = Convert.ToInt32(br.uId);
                                //        wc.BatchId = Convert.ToInt32(maxBatch) + k;
                                //        //wc.TouchFlag = pof.TouchFlag;
                                //        wc.CreatorId = br.CreatorId;
                                //        wc.Creator = br.Creator;
                                //        appEntitys.tb_Weight_Chgs.Add(wc);
                                //        appEntitys.SaveChanges();
                                //        weight = true;
                                //    }
                                //}

                                //foreach (var pof in pofInfo)
                                //{
                                //    int? maxBatch = appEntitys.tb_Weight_Chgs.Where(p => p.uId == br.uId && p.TouchFlag == pof.TouchFlag).Max(p => p.BatchId);
                                //    if (maxBatch == null) maxBatch = 1;
                                //    else maxBatch += 1;
                                //    for (int k = 0; k < br.BuyNum; k++)
                                //    {
                                //        for (int z = 0; z < pof.Times; z++)
                                //        {
                                //            tb_Weight_Chg wc = new tb_Weight_Chg();
                                //            wc.uId = Convert.ToInt32(br.uId);
                                //            wc.BatchId = Convert.ToInt32(maxBatch) + k;
                                //            wc.TouchFlag = pof.TouchFlag;
                                //            wc.CreatorId = br.CreatorId;
                                //            wc.Creator = br.Creator;
                                //            appEntitys.tb_Weight_Chgs.Add(wc);
                                //            appEntitys.SaveChanges();
                                //            weight = true;
                                //        }
                                //    }
                                //}


                            }
                            #endregion
                        }

                        if (cont) tran.Complete();
                        return cont;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return false;
            }
        }
    }
}
