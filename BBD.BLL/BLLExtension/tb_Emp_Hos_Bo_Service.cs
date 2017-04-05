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
    public partial class tb_Emp_Hos_Bo_Service : Itb_Emp_Hos_Bo_BLL
    {
        public List<tb_Sys_UserInfo> GetAppEmpHosList(int pageIndex, int pageSize, ref int count, int hospid, tb_Sys_UserInfo info)
        {
            try
            {
                // List<tb_Sys_UserInfo> list = new List<tb_Sys_UserInfo>();

                List<tb_Sys_UserInfo> querylist = new List<tb_Sys_UserInfo>();
                using (BXUUEntities appEntities = new BXUUEntities())
                {

                    SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@HospId",hospid),
                    new SqlParameter("@Name",info.uName)
                };
                    DataTable dt = BBD.Common.SQLHelp.ExecuteDataTable("Pro_Select_EmpHos", System.Data.CommandType.StoredProcedure, param);
                    if (dt == null) return null;
                    IList<tb_Sys_UserInfo> list = ModelConvertHelper<tb_Sys_UserInfo>.ConvertToModel(dt);

                    count = list.Count;
                    querylist = list.OrderByDescending(p => p.uAddtime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    return querylist;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }

        public List<tb_Sys_UserInfo> GetAppEmpList(int pageIndex, int pageSize, ref int count, int hospid, tb_Sys_UserInfo info)
        {
            try
            {
                List<tb_Sys_UserInfo> querylist = new List<tb_Sys_UserInfo>();
                using (BXUUEntities appEntities = new BXUUEntities())
                {
                    SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@Name",info.uName)
                };
                    var empids = appEntities.tb_Emp_Hoss.Where(p => p.hospid == hospid).Select(p => p.emp_id).ToList();//部分门店
                    DataTable dt = BBD.Common.SQLHelp.ExecuteDataTable("Pro_Select_SysUserInfo", System.Data.CommandType.StoredProcedure, param);
                    if (dt == null) return null;
                    IList<tb_Sys_UserInfo> list = ModelConvertHelper<tb_Sys_UserInfo>.ConvertToModel(dt);

                    count = list.Count;
                    foreach (var item in list)
                    {
                        tb_Sys_UserInfo su = new tb_Sys_UserInfo();
                        su.Uid = item.Uid;
                        su.uLoginName = item.uLoginName;
                        su.uName = item.uName;
                        su.uMobile = item.uMobile;
                        su.CityName = item.CityName;
                        su.uPost = item.uPost;
                        su.uDepName = item.uDepName;
                        var check = "nocheck";
                        if (empids != null && empids.Contains(item.Uid)) check = "checked";
                        su.Checked = check;
                        querylist.Add(su);
                    }
                    querylist = querylist.OrderByDescending(p => p.uAddtime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    return querylist;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }
    }
}
