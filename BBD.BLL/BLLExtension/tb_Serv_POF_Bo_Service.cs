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

namespace BBD.BLL
{
    public partial class tb_Serv_POF_Bo_Service : Itb_Serv_POF_Bo_BLL
    {
        public IList<tb_Serv_Info> GetAppServList(int pageIndex, int pageSize, ref int count, tb_Serv_Info info) 
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@ServName",info.ServName),
                    new SqlParameter("@state",info.state),
                    new SqlParameter("@IsDel",info.IsDel),
                    new SqlParameter("@HospIds",SqlDbType.VarChar)
                };
                param[3].Value = info.HospStrIds;
                DataTable dt = BBD.Common.SQLHelp.ExecuteDataTable("Pro_Select_ServInfo", System.Data.CommandType.StoredProcedure, param);
                if (dt == null) return null;
                IList<tb_Serv_Info> list = ModelConvertHelper<tb_Serv_Info>.ConvertToModel(dt);
                count = list.Count;
                list = list.OrderByDescending(p=>p.CTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return list;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }
    }
}
