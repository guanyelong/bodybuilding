using BBD.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBD.IBLL
{
    public partial interface Itb_Sys_Permission_Bo_BLL
    {
        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="actionItem"></param>
        /// <param name="errMsg"></param>
        void AddPermission(tb_Sys_Permission actionItem, ref string errMsg);
        /// <summary>
        /// 根据name获取菜单列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<Hashtable> GetPermissionTreeGridList(ref int count);
        /// <summary>
        /// 返回查询的数据
        /// </summary>
        /// <param name="queryList"></param>
        /// <param name="resultList"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        Hashtable CreateTreeGridList(List<tb_Sys_Permission> queryList, tb_Sys_Permission actionItem);
        /// <summary>
        /// 根据ID获取权限Model
        /// </summary>
        /// <param name="p"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        tb_Sys_Permission GetPermissionByID(int actionId, ref string errMsg);
        /// <summary>
        /// 编辑权限
        /// </summary>
        /// <param name="actionItem">最新的权限数据</param>
        /// <param name="errMsg">错误提示</param>
        void EditPermission(tb_Sys_Permission actionItem, ref string errMsg);
        /// <summary>
        /// 删除权限
        /// </summary>
        void DeletePermission(int actionId, ref string errMsg);
        /// <summary>
        /// 递归获取所有的子权限
        /// </summary>
        /// <param name="appEntities"></param>
        /// <param name="actionId"></param>
        /// <returns></returns>

        List<Hashtable> GetPermissionComboTreeList(ref int count);
    }
}
