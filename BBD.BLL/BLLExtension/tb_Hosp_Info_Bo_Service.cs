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
    public partial class tb_Hosp_Info_Bo_Service : Itb_Hosp_Info_Bo_BLL
    {
        public IList<tb_Hosp_Info> GetAppHospList(int pageIndex, int pageSize, ref int count,tb_Hosp_Info info)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@Hname",info.Hname),
                    new SqlParameter("@tel",info.tel)
                };
                DataTable dt = BBD.Common.SQLHelp.ExecuteDataTable("Pro_Select_HospInfo", System.Data.CommandType.StoredProcedure, param);
                if (dt == null) return null;
                IList<tb_Hosp_Info> list = ModelConvertHelper<tb_Hosp_Info>.ConvertToModel(dt);
                count = list.Count;
                list = list.OrderBy(o => o.CityId).ThenByDescending(o=>o.C_Time).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
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
