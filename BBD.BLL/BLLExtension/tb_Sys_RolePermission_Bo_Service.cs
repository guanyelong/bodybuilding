using BBD.IBLL;
using BBD.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBD.BLL
{
    public partial class tb_Sys_RolePermission_Bo_Service : Itb_Sys_RolePermission_Bo_BLL
    {
        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="name">查询字段</param>
        /// <param name="roleid">角色名称</param>
        /// <param name="errMsg">发生错误返回结果</param>
        /// <param name="count">查询数据总记录数</param>
        /// <returns></returns>
        public List<Hashtable> GetAppRolePermissionTreeGrid(int roleid, ref string errMsg, ref int count)
        {
            try
            {
                List<tb_Sys_Permission> queryList = null;
                using (BXUUEntities appEntities = new BXUUEntities())
                {
                    //查询跟该角色相关的所有权限数据
                    var query = (from a in appEntities.tb_Sys_RolePermissions
                                 join b in appEntities.tb_Sys_Permissions on a.rpPId equals b.pId
                                 where a.rpRId == roleid && b.pState == 1
                                 select new { b });

                    queryList = new List<tb_Sys_Permission>();
                    foreach (var item in query)
                    {
                        tb_Sys_Permission action = item.b;
                        queryList.Add(action);
                    }
                }

                List<Hashtable> list = new List<Hashtable>();
                if (queryList.Count < 1)
                {
                    return list;
                }

                //生成所有数据的集合
                var actionList = queryList.Where(o => o.pParentId == 0);
                foreach (tb_Sys_Permission actionItem in actionList)
                {
                    Hashtable ht = CreateRolePermissionTreeGrid(queryList, actionItem);
                    list.Add(ht);
                }
                count = queryList.Count;
                return list;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return new List<Hashtable>();
            }
        }
        /// <summary>
        /// 获取权限的Tree数据源，如果Role不为空，则根据roleID自动选中
        /// </summary>
        /// <param name="p"></param>
        /// <param name="errMsg"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<Hashtable> GetRolePermissionRoleTreeList(int roleId, ref string errMsg)
        {
            try
            {
                List<tb_Sys_Permission> queryList = null;
                List<tb_Sys_RolePermission> roleMapping = null;

                //获取权限列表和当前的权限列表
                using (BXUUEntities appEntities = new BXUUEntities())
                {
                    queryList = (from a in appEntities.tb_Sys_Permissions where a.pState == 1 select a).ToList();
                    roleMapping = (from a in appEntities.tb_Sys_RolePermissions where a.rpRId == roleId select a).ToList();
                    if (queryList.Count() < 1)
                    {
                        return new List<Hashtable>();
                    }
                }

                List<Hashtable> htList = new List<Hashtable>();
                var findList = queryList.Where(o => o.pParentId == 0 && o.pState == 1);

                foreach (tb_Sys_Permission parentItem in findList)
                {
                    Hashtable ht = InitPermissionTreeChildren(queryList, parentItem, roleMapping);
                    htList.Add(ht);
                }

                return htList;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return new List<Hashtable>();
            }
        }
        /// <summary>
        /// 设置角色的权限
        /// </summary>
        /// <param name="roleid">角色Id</param>
        /// <param name="p">权限Id</param>
        /// <param name="errMsg"></param>
        public void SaveRolePermission(int roleid, string[] actionIds, ref string errMsg)
        {
            try
            {
                List<tb_Sys_RolePermission> existList = null;
                using (BXUUEntities appEntities = new BXUUEntities())
                {

                    existList = appEntities.tb_Sys_RolePermissions.Where(o => o.rpRId == roleid).ToList();
                    //检查是否存在，存在则忽略，不存在则插入
                    foreach (string actionId in actionIds)
                    {

                        int intActionId = Convert.ToInt32(actionId);

                        var actionItem = existList.Where(o => o.rpPId == intActionId).ToList();
                        if (actionItem != null && actionItem.Count() > 0)
                        {
                            continue;
                        }

                        //不存在的插入进数据库
                        tb_Sys_RolePermission newMapping = new tb_Sys_RolePermission();
                        newMapping.rpRId = roleid;
                        newMapping.rpPId = intActionId;
                        newMapping.rpAddtime = DateTime.Now;
                        appEntities.tb_Sys_RolePermissions.Add(newMapping);
                    }

                    //遍历数据库中的数据，数据库存在但是参数中没有的，需要删除
                    foreach (var item in existList)
                    {
                        if (!actionIds.Contains(item.rpPId.ToString()))
                        {
                            var deleteItem = appEntities.tb_Sys_RolePermissions.Where(o => o.rpId == item.rpId).FirstOrDefault();
                            appEntities.tb_Sys_RolePermissions.Remove(deleteItem);
                        }
                    }

                    //提交所做的更改
                    appEntities.SaveChanges();
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
        }
        /// <summary>
        /// 生成查询到的数据权限
        /// </summary>
        /// <param name="queryList"></param>
        /// <param name="list"></param>
        private Hashtable CreateRolePermissionTreeGrid(List<tb_Sys_Permission> queryList, tb_Sys_Permission actionItem)
        {
            Hashtable ht = CreateHashtable(actionItem);
            ht.Remove("children");

            var childrenList = queryList.Where(o => o.pParentId == actionItem.pId);
            if (childrenList.Count() > 0)
            {
                List<Hashtable> htList = new List<Hashtable>();
                foreach (tb_Sys_Permission item in childrenList)
                {
                    Hashtable childrenHt = CreateRolePermissionTreeGrid(queryList, item);
                    htList.Add(childrenHt);
                }
                ht.Add("children", htList);
            }
            return ht;
        }
        private Hashtable CreateHashtable(tb_Sys_Permission actionItem)
        {
            Hashtable htItem = new Hashtable();
            htItem.Add("pId", actionItem.pId);
            htItem.Add("pParentId", actionItem.pParentId);
            htItem.Add("pName", actionItem.pName);
            htItem.Add("pActionMenu", actionItem.pActionMenu);
            htItem.Add("pActionNum", actionItem.pActionNum);
            htItem.Add("pOperationType", actionItem.pOperationType);
            htItem.Add("pOperationTypeName", actionItem.pOperationType == 1 ? "菜单权限" : "按钮权限");
            htItem.Add("pRemark", actionItem.pRemark);
            htItem.Add("pOperTime", actionItem.pOperTime);
            htItem.Add("children", new List<Hashtable>());
            return htItem;
        }

        private Hashtable InitPermissionTreeChildren(List<tb_Sys_Permission> queryList, tb_Sys_Permission parentItem, List<tb_Sys_RolePermission> roleMapping)
        {

            Hashtable ht = CreateTreeHashTable(parentItem);
            var childrenList = queryList.Where(o => o.pParentId == parentItem.pId);
            if (childrenList.Count() > 0)
            {
                List<Hashtable> htList = new List<Hashtable>();
                foreach (tb_Sys_Permission item in childrenList)
                {
                    Hashtable childrenHt = InitPermissionTreeChildren(queryList, item, roleMapping);
                    htList.Add(childrenHt);
                }
                ht.Add("children", htList);
            }
            else
            {
                //如果没有子权限证明是底层权限，底层权限判定是否选中
                if (roleMapping.Where(o => o.rpPId == parentItem.pId).FirstOrDefault() != null)
                {
                    ht.Add("checked", true);
                }
                else
                {
                    ht.Add("checked", false);
                }
            }

            return ht;
        }
        private Hashtable CreateTreeHashTable(tb_Sys_Permission actionItem)
        {
            Hashtable htItem = new Hashtable();
            htItem.Add("id", actionItem.pId);
            htItem.Add("text", actionItem.pName);
            return htItem;
        }
    }
}
