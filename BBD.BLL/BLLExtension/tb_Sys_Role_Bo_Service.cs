using BBD.IBLL;
using BBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBD.BLL
{
    public partial class tb_Sys_Role_Bo_Service : Itb_Sys_Role_Bo_BLL
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
                                    where d.urUid == userId && b.pState == 1 && e.rIsDel == 1
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
        /// 根据ID获取系统角色
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public tb_Sys_Role GetAppRoleByID(int Id, ref string errMsg)
        {
            try
            {
                using (BXUUEntities appEntities = new BXUUEntities())
                {

                    tb_Sys_Role role = appEntities.tb_Sys_Roles.Where(o => o.rId == Id).FirstOrDefault();
                    if (role == null)
                    {
                        errMsg = "查无数据";
                        return null;
                    }

                    return role;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<tb_Sys_Role> GetAppRoleList(int pageIndex, int pageSize, ref int count, string name)
        {
            try
            {
                using (BXUUEntities appEntites = new BXUUEntities())
                {
                    var roleList = appEntites.tb_Sys_Roles.Where(o => o.rIsDel == 1);
                    if (!string.IsNullOrEmpty(name))
                    {
                        roleList = roleList.Where(o => o.rName.Contains(name));
                    }

                    count = roleList.Count();
                    if (count < 1)
                    {
                        return new List<tb_Sys_Role>();
                    }
                    roleList = roleList.OrderByDescending(o => o.rId).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    return roleList.ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <param name="errMsg"></param>
        public void AddRole(tb_Sys_Role roleInfo, ref string errMsg)
        {
            try
            {
                using (BXUUEntities appEntities = new BXUUEntities())
                {
                    roleInfo.rIsDel = 1;
                    roleInfo.rAddTime = DateTime.Now;

                    //判断角色编码不能重复
                    var existRole = appEntities.tb_Sys_Roles.Where(o => (o.rNum == roleInfo.rNum || o.rName == roleInfo.rName) && o.rIsDel != -1).FirstOrDefault();
                    if (existRole != null)
                    {
                        errMsg = "角色编码/名称不能重复";
                        return;
                    }
                    roleInfo.rNum = CreateRoleNum(roleInfo);
                    appEntities.tb_Sys_Roles.Add(roleInfo);
                    appEntities.SaveChanges();
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
        }

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <param name="errMsg"></param>
        public void EditRole(tb_Sys_Role roleInfo, ref string errMsg)
        {
            try
            {
                using (BXUUEntities appEntities = new BXUUEntities())
                {
                    tb_Sys_Role roleItem = appEntities.tb_Sys_Roles.Where(o => o.rId == roleInfo.rId).FirstOrDefault();
                    if (roleItem == null)
                    {
                        errMsg = "查无数据";
                        return;
                    }

                    //检查用户编码不能重复
                    if (appEntities.tb_Sys_Roles.Where(o => o.rNum == roleInfo.rNum && o.rId != roleInfo.rId).Count() > 0)
                    {
                        errMsg = "角色编码不能重复";
                        return;
                    }

                    roleItem.rName = roleInfo.rName;
                    roleItem.rNum = roleInfo.rNum;
                    roleItem.rRemark = roleInfo.rRemark;

                    appEntities.SaveChanges();
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="errMsg"></param>
        public void DeleteRole(int roleId, ref string errMsg)
        {
            try
            {
                using (BXUUEntities appEntities = new BXUUEntities())
                {
                    var roleItem = appEntities.tb_Sys_Roles.Where(o => o.rId == roleId).FirstOrDefault();
                    if (roleItem == null)
                    {
                        errMsg = "查无数据";
                        return;
                    }
                    //标记删除角色
                    roleItem.rIsDel = -1;
                    //删除角色关联的权限
                    var roleMapping = appEntities.tb_Sys_RolePermissions.Where(o => o.rpRId == roleId);
                    if (roleMapping.Count() > 0)
                    {
                        foreach (var item in roleMapping)
                        {
                            appEntities.tb_Sys_RolePermissions.Remove(item);
                        }
                    }
                    //删除角色关联的用户

                    var userMapping = appEntities.tb_Sys_UserRoles.Where(o => o.urRid == roleId);
                    if (userMapping.Count() > 0)
                    {
                        foreach (var item in userMapping)
                        {
                            appEntities.tb_Sys_UserRoles.Remove(item);
                        }
                    }

                    appEntities.SaveChanges();
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
        }
        private string CreateRoleNum(tb_Sys_Role role)
        {
            string actionCount = "01";
            string menuNum = "RE";
            tb_Sys_Role lastMenu = null;
            using (BXUUEntities hisEntities = new BXUUEntities())
            {
                //查询菜单下最大编号不包括自己和已经删除的
                lastMenu = hisEntities.tb_Sys_Roles.OrderByDescending(o => o.rNum).FirstOrDefault();
            }
            if (lastMenu != null)
            {
                var str = lastMenu.rNum.Substring(lastMenu.rNum.Length - 2, 2);
                int number = Convert.ToInt32(str) + 1;
                actionCount = number.ToString().PadLeft(2, '0');
            }
            return menuNum + actionCount;
        }
    }
}
