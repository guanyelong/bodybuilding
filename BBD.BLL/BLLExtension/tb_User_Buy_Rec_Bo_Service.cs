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
    }
}
