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
    public partial class tb_Weight_Chg_Bo_Service : Itb_Weight_Chg_Bo_BLL
    {
        public IList<tb_Weight_Chg> GetAppWeightChgList(int pageIndex, int pageSize, ref int count, tb_Weight_Chg info)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@uid",info.uId),
                    new SqlParameter("@uName",info.uName),
                    new SqlParameter("@Mobile",info.Mobile)
                };
                DataTable dt = BBD.Common.SQLHelp.ExecuteDataTable("Pro_Select_Weight", System.Data.CommandType.StoredProcedure, param);
                if (dt == null) return null;
                IList<tb_Weight_Chg> list = ModelConvertHelper<tb_Weight_Chg>.ConvertToModel(dt);
                count = list.Count;
                list = list.OrderBy(o => o.uId).ThenByDescending(o => o.C_time).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
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
