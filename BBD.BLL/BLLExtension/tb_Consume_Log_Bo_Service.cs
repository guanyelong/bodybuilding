using BBD.Common;
using BBD.IBLL;
using BBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BBD.BLL
{
    public partial class tb_Consume_Log_Bo_Service : Itb_Consume_Log_Bo_BLL
    {
        public bool UpdConsumeLog(tb_Consume_Log cl) 
        {
            try
            {
                var cont = false;
                using (BXUUEntities appEntitys = new BXUUEntities()) 
                {
                    using (TransactionScope tran = new TransactionScope()) 
                    {
                        if (cl.Id == 0)
                        {
                            if (cl.Tnum != 0)//点穴
                            {
                                var info = appEntitys.tb_User_Accounts.Where(p => p.uId == cl.uId && p.TouchFlag==1).FirstOrDefault();
                                if (info != null)
                                {
                                    cl.flag = 1;
                                    cl.num = cl.Tnum;
                                    appEntitys.tb_Consume_Logs.Add(cl);
                                    appEntitys.SaveChanges();
                                    info.delay = info.delay - cl.Tnum;
                                    appEntitys.SaveChanges();
                                    cont = true;
                                }
                                
                            }
                            if (cl.Enum != 0)//机器
                            {
                                var info = appEntitys.tb_User_Accounts.Where(p => p.uId == cl.uId && p.TouchFlag == 0).FirstOrDefault();
                                if (info!=null)
                                {
                                    cl.flag = 0;
                                    cl.num = cl.Enum;
                                    appEntitys.tb_Consume_Logs.Add(cl);
                                    appEntitys.SaveChanges();
                                    info.delay = info.delay - cl.Enum;
                                    appEntitys.SaveChanges();
                                    cont = true;
                                }
                            }
                            
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
