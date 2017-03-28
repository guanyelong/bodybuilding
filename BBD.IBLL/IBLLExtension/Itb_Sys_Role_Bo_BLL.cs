using BBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBD.IBLL
{
    public partial interface Itb_Sys_Role_Bo_BLL
    {
        /// <summary>
        /// 根据权限获取该用户的菜单列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<tb_Sys_MenuInfo> GetAppUserMenuList(int userId);
        /// <summary>
        /// 根据ID获取系统角色
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        tb_Sys_Role GetAppRoleByID(int Id, ref string errMsg);
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        List<tb_Sys_Role> GetAppRoleList(int pageIndex, int pageSize, ref int count, string name);
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <param name="errMsg"></param>
        void AddRole(tb_Sys_Role roleInfo, ref string errMsg);
        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <param name="errMsg"></param>
        void EditRole(tb_Sys_Role roleInfo, ref string errMsg);
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="errMsg"></param>
        void DeleteRole(int roleId, ref string errMsg);
    }
}
