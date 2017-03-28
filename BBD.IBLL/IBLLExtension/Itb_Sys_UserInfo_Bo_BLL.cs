using BBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBD.IBLL
{
    public partial interface Itb_Sys_UserInfo_Bo_BLL
    {
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        tb_Sys_UserInfo GetAllAppUserById(int id);
        /// <summary>
        /// 分页获取AppUserList
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="name"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<tb_Sys_UserInfo> GetAppUserList(int pageIndex, int pageSize, string name, ref int count);
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="errMsg"></param>
        void AddUser(tb_Sys_UserInfo user, ref string errMsg);
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="errMsg"></param>
        void EditUser(tb_Sys_UserInfo user, ref string errMsg);
        /// <summary>
        /// 删除用户数据
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="errMsg"></param>
        void DeleteUser(int userId, ref string errMsg);
        /// <summary>
        /// 想修改用户密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="errMsg"></param>
        void ChangePassword(int userId, string password, ref string errMsg);
        /// <summary>
        /// 用户验证
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        tb_Sys_UserInfo ValidateLogin(tb_Sys_UserInfo info);

        tb_Sys_UserInfo GetAppUserByLoginName(string userName);
    }
}
