using BBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBD.IBLL
{
    public partial interface Itb_Sys_UserRole_Bo_BLL
    {
        /// <summary>
        /// 根据权限获取该用户的菜单列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<tb_Sys_MenuInfo> GetAppUserMenuList(int userId);
        /// <summary>
        /// 根据用户Id获取所有角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        List<tb_Sys_Role> GetAppUserRoleList(int userId, ref string errMsg);
        /// <summary>
        /// 获取分页用户角色
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="name">用于检索的记录数</param>
        /// <param name="userId">用户Id</param>
        /// <param name="count"></param>
        /// <returns>返回角色列表页</returns>
        List<tb_Sys_Role> GetAppUserRoleList(int pageIndex, int pageSize, string name, int userId, ref int count);
        /// <summary>
        /// 删除用户的角色
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="roleid"></param>
        /// <param name="errMsg"></param>
        void DeleteUserRole(int userid, int roleid, ref string errMsg);
        /// <summary>
        /// 根据用户id，获取用户下的权限列表
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="actionType">权限类型</param>
        /// <returns></returns>
        List<tb_Sys_Permission> GetAppUserActionList(int userid, BBD.Common.AppActionType actionType);
        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="intRoleIds"></param>
        /// <param name="errMsg"></param>
        /// <param name="existRoleNames"></param>
        void SetUserRole(int userid, int[] intRoleIds, ref string errMsg, out string existRoleNames);
    }
}
