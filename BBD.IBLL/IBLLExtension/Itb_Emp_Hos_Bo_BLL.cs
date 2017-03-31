using BBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBD.IBLL
{
    public partial interface Itb_Emp_Hos_Bo_BLL
    {
        /// <summary>
        /// 根据门店查门店下的店员
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <param name="hospid"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        List<tb_Sys_UserInfo> GetAppEmpHosList(int pageIndex, int pageSize, ref int count, int hospid, tb_Sys_UserInfo info);
        /// <summary>
        /// 遍历所有用户若有门店店员权限则选中
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <param name="hospid"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        List<tb_Sys_UserInfo> GetAppEmpList(int pageIndex, int pageSize, ref int count, int hospid, tb_Sys_UserInfo info);
    }
}
