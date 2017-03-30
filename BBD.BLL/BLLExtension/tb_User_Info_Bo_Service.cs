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
    public partial class tb_User_Info_Bo_Service : Itb_User_Info_Bo_BLL
    {
        public IList<tb_User_Info> GetAppUserList(int pageIndex, int pageSize, ref int count, tb_User_Info info)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@Name",info.Name),
                    new SqlParameter("@Mobile",info.Mobile),
                    new SqlParameter("@Female",info.Female),
                    new SqlParameter("@ComeFrom",info.ComeFrom),
                    new SqlParameter("@HospId",info.HospId)
                };
                DataTable dt = BBD.Common.SQLHelp.ExecuteDataTable("Pro_Select_Customer", System.Data.CommandType.StoredProcedure, param);
                if (dt == null) return null;
                IList<tb_User_Info> list = ModelConvertHelper<tb_User_Info>.ConvertToModel(dt);
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
    }
}
