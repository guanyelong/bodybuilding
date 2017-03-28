using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBD.IBLL
{
    public partial interface Itb_Sys_RolePermission_Bo_BLL
    {
        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="name">查询字段</param>
        /// <param name="roleid">角色名称</param>
        /// <param name="errMsg">发生错误返回结果</param>
        /// <param name="count">查询数据总记录数</param>
        /// <returns></returns>
        List<Hashtable> GetAppRolePermissionTreeGrid(int roleid, ref string errMsg, ref int count);
        /// <summary>
        /// 获取权限的Tree数据源，如果Role不为空，则根据roleID自动选中
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        List<Hashtable> GetRolePermissionRoleTreeList(int roleId, ref string errMsg);
        /// <summary>
        /// 设置角色的权限
        /// </summary>
        /// <param name="roleid">角色Id</param>
        /// <param name="actionIds">权限Id</param>
        /// <param name="errMsg"></param>
        void SaveRolePermission(int roleid, string[] actionIds, ref string errMsg);

    }
}
