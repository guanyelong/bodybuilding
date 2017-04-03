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
    public partial class tb_User_Dislike_Food_Bo_Service : Itb_User_Dislike_Food_Bo_BLL
    {
        public IList<tb_User_Dislike_Food> GetAppFoodList(int pageIndex, int pageSize, ref int count, tb_User_Dislike_Food udf)
        {
            try
            {
                List<tb_User_Dislike_Food> querylist = new List<tb_User_Dislike_Food>();

                SqlParameter[] param = new SqlParameter[]{
                        new SqlParameter("@uid",udf.uId)
                    };
                DataTable dt = BBD.Common.SQLHelp.ExecuteDataTable("Pro_Select_Food", System.Data.CommandType.StoredProcedure, param);
                if (dt == null) return null;
                IList<tb_User_Dislike_Food> list = ModelConvertHelper<tb_User_Dislike_Food>.ConvertToModel(dt);

                count = list.Count;
                querylist = list.OrderByDescending(p => p.C_time).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return querylist;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
