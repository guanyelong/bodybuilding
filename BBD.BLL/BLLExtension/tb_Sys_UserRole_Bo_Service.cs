using BBD.IBLL;
using BBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBD.BLL
{
    public partial class tb_Sys_UserRole_Bo_Service : Itb_Sys_UserRole_Bo_BLL
    {
        /// <summary>
        /// 根据权限获取该用户的菜单列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<tb_Sys_MenuInfo> GetAppUserMenuList(int userId)
        {
            try
            {
                using (BXUUEntities appEntities = new BXUUEntities())
                {

                    //级联角色，如果角色删除，排除删除的角色
                    //将菜单存到数据库
                    var quertList = from a in appEntities.tb_Sys_MenuInfos
                                    join b in appEntities.tb_Sys_Permissions on a.mPerNum equals b.pActionNum
                                    join c in appEntities.tb_Sys_RolePermissions on b.pId equals c.rpPId
                                    join d in appEntities.tb_Sys_UserRoles on c.rpRId equals d.urRid
                                    join e in appEntities.tb_Sys_Roles on d.urRid equals e.rId
                                    where d.urUid == userId && b.pIsDel == 1 && e.rIsDel == 1
                                    orderby a.mOrderindex ascending
                                    select a;

                    return quertList.Distinct().ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据用户Id获取所有角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<tb_Sys_Role> GetAppUserRoleList(int userId, ref string errMsg)
        {
            try
            {
                using (BXUUEntities appEntities = new BXUUEntities())
                {
                    var query = (from a in appEntities.tb_Sys_Roles
                                 join b in appEntities.tb_Sys_UserRoles on a.rId equals b.urRid
                                 where b.urUid == userId
                                 select a);

                    if (query == null)
                    {
                        return null;
                    }

                    return query.ToList();
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
        }
        /// <summary>
        /// 获取分页用户角色
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="name">用于检索的记录数</param>
        /// <param name="userId">用户Id</param>
        /// <param name="count"></param>
        /// <returns>返回角色列表页</returns>
        public List<tb_Sys_Role> GetAppUserRoleList(int pageIndex, int pageSize, string name, int userId, ref int count)
        {
            try
            {
                using (BXUUEntities appEntities = new BXUUEntities())
                {


                    var query = (from a in appEntities.tb_Sys_Roles
                                 join b in appEntities.tb_Sys_UserRoles on a.rId equals b.urRid
                                 join c in appEntities.tb_Sys_UserInfos on b.urUid equals c.Uid
                                 where b.urUid == userId && a.rIsDel == 1
                                 select new { a });

                    if (query == null)
                    {
                        count = 0;
                        return null;
                    }
                    //筛选
                    query = query.Where(o => o.a.rName.Contains(name));
                    count = query.Count();
                    //分页
                    query = query.OrderByDescending(o => o.a.rAddTime).Skip((pageIndex - 1) * pageSize).Take(pageSize);

                    List<tb_Sys_Role> roleList = new List<tb_Sys_Role>();
                    foreach (var item in query)
                    {
                        tb_Sys_Role model = new tb_Sys_Role();
                        model.rId = item.a.rId;
                        model.rName = item.a.rName;
                        model.rNum = item.a.rNum;
                        model.rAddTime = item.a.rAddTime;
                        model.rIsDel = item.a.rIsDel;
                        //model.Disc = item.a.Disc;
                        roleList.Add(model);
                    }

                    return roleList;
                }
            }
            catch (Exception e)
            {
                count = 0;
                return null;
            }
        }
        /// <summary>
        /// 删除user的role
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="intRoleIds"></param>
        /// <param name="errMsg"></param>
        public void DeleteUserRole(int userid, int roleid, ref string errMsg)
        {
            try
            {
                using (BXUUEntities appEntitys = new BXUUEntities())
                {

                    var item = appEntitys.tb_Sys_UserRoles.Where(o => o.urUid == userid && o.urRid == roleid).FirstOrDefault();
                    if (item == null)
                    {
                        errMsg = "用户不存在该角色";
                        return;
                    }
                    appEntitys.tb_Sys_UserRoles.Remove(item);

                    appEntitys.SaveChanges();
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
        }
        /// <summary>
        /// 根据用户id，获取用户下的权限列表
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="actionType">权限类型</param>
        /// <returns></returns>
        public List<tb_Sys_Permission> GetAppUserActionList(int userid, BBD.Common.AppActionType actionType)
        {
            try
            {
                using (BXUUEntities appEntities = new BXUUEntities())
                {

                    //查询
                    var queryList = (from a in appEntities.tb_Sys_Permissions
                                     join b in appEntities.tb_Sys_RolePermissions on a.pId equals b.rpPId
                                     join c in appEntities.tb_Sys_UserRoles on b.rpRId equals c.urRid
                                     where c.urUid == userid
                                     select a).Distinct();

                    if (queryList.Count() < 1)
                    {
                        return null;
                    }

                    //过滤菜单权限还是按钮权限
                    if (actionType == BBD.Common.AppActionType.AllAction)
                    {
                        return queryList.ToList();
                    }
                    int typeInt = Convert.ToInt32(actionType);
                    queryList = queryList.Where(o => o.pOperationType == typeInt);
                    return queryList.ToList();
                }
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="p"></param>
        /// <param name="intRoleIds"></param>
        /// <param name="errMsg"></param>
        public void SetUserRole(int userid, int[] intRoleIds, ref string errMsg, out string existRoleNames)
        {
            try
            {
                using (BXUUEntities appEntitys = new BXUUEntities())
                {
                    existRoleNames = string.Empty;
                    var items = appEntitys.tb_Sys_UserRoles.Where(o => o.urUid == userid);
                    foreach (int roleId in intRoleIds)
                    {
                        //存在则不添加
                        if (items.Where(o => o.urRid == roleId).Count() > 0)
                        {

                            var appRole = appEntitys.tb_Sys_Roles.Where(o => o.rId == roleId).FirstOrDefault();
                            if (appRole != null)
                            {
                                existRoleNames += "," + appRole.rName;
                            }
                            continue;
                        }
                        tb_Sys_UserRole mapping = new tb_Sys_UserRole();
                        mapping.urUid = userid;
                        mapping.urRid = roleId;
                        mapping.urAddTime = DateTime.Now;
                        mapping.urIsDel = 0;
                        appEntitys.tb_Sys_UserRoles.Add(mapping);
                    }
                    appEntitys.SaveChanges();
                }
            }
            catch (Exception e)
            {
                existRoleNames = string.Empty;
                errMsg = e.Message;
            }
        }

    }
}
