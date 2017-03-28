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
    public partial class tb_Sys_Permission_Bo_Service : Itb_Sys_Permission_Bo_BLL
    {
        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="actionItem"></param>
        /// <param name="errMsg"></param>
        public void AddPermission(tb_Sys_Permission actionItem, ref string errMsg)
        {
            try
            {
                string actionNum = CreatePermissionNum(actionItem);
                //创建权限
                actionItem.pActionNum = actionNum;
                if (actionItem.pParentId == null) actionItem.pParentId = 0;
                using (BXUUEntities appEntities = new BXUUEntities())
                {
                    actionItem.pOperTime = DateTime.Now;
                    actionItem.pState = 1;
                    actionItem.pIsDel = 1;
                    //新增权限名称不能重复
                    var findList = appEntities.tb_Sys_Permissions.Where(o => o.pState == 1 && o.pName == actionItem.pName && o.pParentId == actionItem.pParentId);
                    if (findList.Count() > 0)
                    {
                        errMsg = "权限名称已经存在";
                        return;
                    }

                    if (actionItem.pOperationType == 1)
                    {
                        //给菜单编写权限编码
                        var appMenu = appEntities.tb_Sys_MenuInfos.Where(o => o.mId == actionItem.pActionMenu).FirstOrDefault();
                        //如果菜单已被其他权限关联，则不能继续被关联
                        if (!string.IsNullOrEmpty(appMenu.mPerNum))
                        {
                            errMsg = "菜单已被权限" + appMenu.mPerNum + "关联,请选择其他菜单";
                            return;
                        }
                        appMenu.mPerNum = actionItem.pActionNum;
                    }

                    appEntities.tb_Sys_Permissions.Add(actionItem);
                    appEntities.SaveChanges();
                }


            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
        }


        /// <summary>
        /// 根据name获取菜单列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<Hashtable> GetPermissionTreeGridList(ref int count)
        {
            try
            {
                List<Models.tb_Sys_Permission> queryList = null;
                using (BXUUEntities appEntities = new BXUUEntities())
                {

                    queryList = appEntities.tb_Sys_Permissions.Where(o => o.pState == 1).ToList();
                    count = queryList.Count();
                    if (count < 1)
                    {
                        return null;
                    }
                }
                List<Hashtable> htList = new List<Hashtable>();
                var parentList = queryList.Where(o => o.pParentId == 0);
                foreach (tb_Sys_Permission actionItem in parentList)
                {
                    Hashtable ht = CreateTreeGridList(queryList, actionItem);
                    htList.Add(ht);
                }
                return htList;
            }
            catch (Exception e)
            {
                count = 0;
                return null;
            }
        }
        /// <summary>
        /// 返回查询的数据
        /// </summary>
        /// <param name="queryList"></param>
        /// <param name="resultList"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public Hashtable CreateTreeGridList(List<tb_Sys_Permission> queryList, tb_Sys_Permission actionItem)
        {
            Hashtable ht = new Hashtable();
            ht.Add("pId", actionItem.pId);
            ht.Add("pName", actionItem.pName);
            ht.Add("pActionNum", actionItem.pActionNum);
            ht.Add("pRemark", actionItem.pRemark);
            ht.Add("pOperationType", actionItem.pOperationType);
            ht.Add("pOperationTypeName", actionItem.pOperationType == 1 ? "菜单权限" : "按钮权限");
            ht.Add("pParentId", actionItem.pParentId);
            ht.Add("pOperTime", Convert.ToDateTime(actionItem.pOperTime).ToString("yyyy-MM-dd HH:mm:ss"));

            var childrenList = queryList.Where(o => o.pParentId == actionItem.pId);
            if (childrenList.Count() > 0)
            {
                List<Hashtable> htList = new List<Hashtable>();
                foreach (tb_Sys_Permission item in childrenList)
                {
                    Hashtable childrenHt = CreateTreeGridList(queryList, item);
                    htList.Add(childrenHt);
                }
                ht.Add("children", htList);
            }
            return ht;
        }

        /// <summary>
        /// 根据ID获取权限Model
        /// </summary>
        /// <param name="p"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public tb_Sys_Permission GetPermissionByID(int actionId, ref string errMsg)
        {
            try
            {
                using (BXUUEntities appEntities = new BXUUEntities())
                {
                    tb_Sys_Permission actionItem = appEntities.tb_Sys_Permissions.Where(o => o.pId == actionId && o.pState == 1).FirstOrDefault();
                    if (actionItem == null)
                    {
                        errMsg = "查无数据";
                        return null;
                    }
                    //根据action num 获取菜单id
                    var appMenu = appEntities.tb_Sys_MenuInfos.Where(o => o.mPerNum == actionItem.pActionNum).FirstOrDefault();
                    if (appMenu != null)
                    {

                        actionItem.pActionMenu = appMenu.mId;
                    }
                    return actionItem;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
        }
        /// <summary>
        /// 编辑权限
        /// </summary>
        /// <param name="actionItem">最新的权限数据</param>
        /// <param name="errMsg">错误提示</param>
        public void EditPermission(tb_Sys_Permission actionItem, ref string errMsg)
        {
            try
            {
                using (BXUUEntities appEntities = new BXUUEntities())
                {
                    //原来的数据
                    var findItem = (from a in appEntities.tb_Sys_Permissions
                                    join b in appEntities.tb_Sys_MenuInfos on a.pActionNum equals b.mPerNum into temp
                                    from b in temp.DefaultIfEmpty()
                                    where a.pId == actionItem.pId
                                    select new { a, b }).FirstOrDefault();

                    tb_Sys_Permission updateItem = findItem.a;
                    if (findItem.b != null)
                    {

                        updateItem.pActionMenu = findItem.b.mId;
                    }

                    if (updateItem == null)
                    {
                        errMsg = "查无数据";
                        return;
                    }

                    //关联菜单发生变化
                    //1：菜单权限下关联的菜单发生改变
                    //2: 权限类型按钮权限和控制权限之间互相转换
                    if ((actionItem.pActionMenu != updateItem.pActionMenu) || (actionItem.pOperationType != updateItem.pOperationType))
                    {

                        //如果需要关联的菜单已经被关联了，则限制不能被关联
                        var updateMenu = appEntities.tb_Sys_MenuInfos.Where(o => o.mId == actionItem.pActionMenu).FirstOrDefault();
                        if (!string.IsNullOrEmpty(updateMenu.mPerNum) && updateMenu.mPerNum != actionItem.pActionNum)
                        {
                            errMsg = "菜单已被权限" + updateMenu.mPerNum + "关联，请选择其他菜单";
                            return;
                        }

                        if (updateItem.pOperationType == 1)
                        {
                            //将原来的关联菜单取消
                            var appMenu = appEntities.tb_Sys_MenuInfos.Where(o => o.mPerNum == updateItem.pActionNum).FirstOrDefault();
                            if (appMenu != null)
                            {
                                appMenu.mPerNum = string.Empty;
                            }
                        }
                        if (actionItem.pOperationType == 1)
                        {
                            //关联最新的权限Num
                            updateMenu.mPerNum = actionItem.pActionNum;
                        }
                    }

                    updateItem.pActionNum = actionItem.pActionNum;
                    updateItem.pName = actionItem.pName;
                    updateItem.pActionMenu = actionItem.pActionMenu;
                    updateItem.pRemark = actionItem.pRemark;
                    updateItem.pParentId = actionItem.pParentId;
                    updateItem.pOperationType = actionItem.pOperationType;

                    appEntities.SaveChanges();
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        public void DeletePermission(int actionId, ref string errMsg)
        {
            try
            {
                List<tb_Sys_Permission> deleteList = null;
                using (BXUUEntities appEntities = new BXUUEntities())
                {

                    tb_Sys_Permission actionItem = appEntities.tb_Sys_Permissions.Where(o => o.pId == actionId).FirstOrDefault();
                    if (actionItem == null)
                    {
                        errMsg = "查无数据";
                        return;
                    }
                    actionItem.pState = 0;
                    deleteList = DeletePermissionChildren(appEntities, actionId);
                    deleteList.Add(actionItem);
                    //更新菜单中的ActionNum
                    tb_Sys_MenuInfo findMenu = appEntities.tb_Sys_MenuInfos.Where(o => o.mPerNum == actionItem.pActionNum).FirstOrDefault();
                    if (findMenu != null)
                    {
                        findMenu.mPerNum = string.Empty;
                    }
                    // 保存
                    appEntities.SaveChanges();
                }

            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
        }
        //递归获取所有的子权限

        public List<Hashtable> GetPermissionComboTreeList(ref int count)
        {
            try
            {
                List<tb_Sys_Permission> queryList = null;
                using (BXUUEntities appEntities = new BXUUEntities())
                {
                    queryList = appEntities.tb_Sys_Permissions.Where(o => o.pState == 1).ToList();
                    count = queryList.Count();
                }

                List<Hashtable> htList = new List<Hashtable>();
                Hashtable firstItem = new Hashtable();
                firstItem.Add("id", 0);
                firstItem.Add("text", "无上级");
                htList.Add(firstItem);

                if (count < 1)
                {
                    return htList;
                }

                var parentList = queryList.Where(o => o.pParentId == 0);
                foreach (tb_Sys_Permission actionItem in parentList)
                {
                    Hashtable ht = CreateComboTreeList(queryList, actionItem);
                    htList.Add(ht);
                }
                return htList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 自动生成权限编码
        /// </summary>
        /// <param name="actionItem"></param>
        /// <returns></returns>
        private string CreatePermissionNum(tb_Sys_Permission actionItem)
        {
            tb_Sys_Permission parentAction = null;
            tb_Sys_Permission lastAction = null;
            string actionCount = "01";
            string actionNum = "RT";

            using (BXUUEntities appEntities = new BXUUEntities())
            {
                //查找上级菜单
                parentAction = appEntities.tb_Sys_Permissions.Where(o => o.pId == actionItem.pParentId).FirstOrDefault();
                //查询菜单下最大编号不包括自己和已经删除的
                lastAction = appEntities.tb_Sys_Permissions.Where(o => o.pParentId == actionItem.pParentId).OrderByDescending(o => o.pActionNum).FirstOrDefault();
            }

            if (lastAction != null)
            {
                var str = lastAction.pActionNum.Substring(lastAction.pActionNum.Length - 2, 2);
                int number = Convert.ToInt32(str) + 1;
                actionCount = number.ToString().PadLeft(2, '0');
            }

            if (parentAction != null)
            {
                actionNum = parentAction.pActionNum + actionCount;
                return actionNum;
            }
            return actionNum + actionCount;
        }
        private List<tb_Sys_Permission> DeletePermissionChildren(BXUUEntities appEntities, int actionId)
        {
            List<tb_Sys_Permission> listAction = new List<tb_Sys_Permission>();
            var childrenAction = appEntities.tb_Sys_Permissions.Where(o => o.pParentId == actionId);
            if (childrenAction.Count() > 0)
            {
                foreach (tb_Sys_Permission actionItem in childrenAction)
                {
                    listAction.Add(actionItem);
                    listAction.AddRange(DeletePermissionChildren(appEntities, actionItem.pId));
                    actionItem.pState = 0;
                    actionItem.pActionNum = string.Empty;
                }
            }
            return listAction;
        }
        private Hashtable CreateComboTreeList(List<tb_Sys_Permission> queryList, tb_Sys_Permission actionItem)
        {
            Hashtable ht = new Hashtable();
            ht.Add("id", actionItem.pId);
            ht.Add("text", actionItem.pName);

            var childrenList = queryList.Where(o => o.pParentId == actionItem.pId);
            if (childrenList.Count() > 0)
            {
                List<Hashtable> htList = new List<Hashtable>();
                foreach (tb_Sys_Permission item in childrenList)
                {
                    Hashtable childrenHt = CreateComboTreeList(queryList, item);
                    htList.Add(childrenHt);
                }
                ht.Add("children", htList);
            }
            return ht;
        }

    }
}
