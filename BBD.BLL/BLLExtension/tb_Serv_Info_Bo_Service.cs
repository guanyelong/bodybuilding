using BBD.IBLL;
using BBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBD.BLL
{
    public partial class tb_Serv_Info_Bo_Service : Itb_Serv_Info_Bo_BLL
    {
        public List<tb_Serv_Info> GetAppServList(int pageIndex, int pageSize, ref int count, tb_Serv_Info info) 
        {
            try
            {
                List<tb_Serv_Info> querylist=new List<tb_Serv_Info>();
                using (BXUUEntities appEntities = new BXUUEntities()) 
                {
                    var query = from s in appEntities.tb_Serv_Infos
                                join t in appEntities.tb_Dicts on s.ServType equals t.KeyValue into temp
                                from tt in temp.DefaultIfEmpty()
                                where tt.KeyName=="ServType"
                                select new { s,tt };
                    foreach (var item in query)
                    {
                        tb_Serv_Info si = new tb_Serv_Info();
                        si.ID = item.s.ID;
                        si.ServId = item.s.ServId;
                        si.ServMemo = item.s.ServMemo;
                        si.ServType = item.s.ServType;
                        si.ServName = item.s.ServName;
                        si.ServTypeName = item.tt.KeyWords;
                        si.state = item.s.state;
                        si.price = item.s.price;
                        si.IsDel = item.s.IsDel;
                        si.CTime = item.s.CTime;
                        querylist.Add(si);
                    }
                    if (!string.IsNullOrWhiteSpace(info.ServName))
                    {
                        querylist = querylist.Where(p => p.ServName.Contains(info.ServName)).ToList();
                    }
                    count = querylist.Count;
                    querylist = querylist.OrderByDescending(p => p.CTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
                return querylist;
            }
            catch (Exception ex)
            {
                return null;
            }
        }  
    }
}
