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
    public partial class tb_Sys_MenuInfo_Bo_Service : Itb_Sys_MenuInfo_Bo_BLL
    {
        //根据ID获取菜单编号
        public tb_Sys_MenuInfo GetAppMenuByID(int Id, ref string errMsg)
        {
            try
            {
                using (BXUUEntities appEntities = new BXUUEntities())
                {
                    var menuItem = appEntities.tb_Sys_MenuInfos.Where(o => o.mId == Id).FirstOrDefault();
                    if (menuItem == null)
                    {
                        errMsg = "查无数据";
                        return null;
                    }
                    return menuItem;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
        }
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menuItem"></param>
        /// <param name="errMsg"></param>
        public void AddMenu(tb_Sys_MenuInfo menuItem, ref string errMsg)
        {
            try
            {
                using (BXUUEntities hisEntities = new BXUUEntities())
                {
                    menuItem.mNum = CreateMenuNum(menuItem);
                    menuItem.mAddtime = DateTime.Now;
                    menuItem.mState = "open";

                    //更新上级菜单state 为 closeed
                    var findItem = hisEntities.tb_Sys_MenuInfos.Where(o => o.mId == menuItem.mPId).FirstOrDefault();
                    if (findItem != null)
                    {
                        findItem.mState = "closed";
                    }

                    menuItem.mChecked = true;
                    menuItem.mSeq = 1;//排序

                    hisEntities.tb_Sys_MenuInfos.Add(menuItem);
                    hisEntities.SaveChanges();
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
        }
        /// <summary>
        /// 获取ComboTree Grid数据
        /// </summary>
        /// <param name="count"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public object GetMenuComboTree(ref int count, ref string errMsg)
        {
            try
            {
                List<tb_Sys_MenuInfo> menuList = new List<tb_Sys_MenuInfo>();
                List<tb_Sys_MenuInfo> parentList = new List<tb_Sys_MenuInfo>();
                using (BXUUEntities appEntitys = new BXUUEntities())
                {
                    menuList = appEntitys.tb_Sys_MenuInfos.ToList();

                }
                parentList = menuList.Where(o => o.mPId == 0).ToList();
                count = parentList.Count();

                List<Hashtable> tableList = new List<Hashtable>();
                Hashtable noneTable = new Hashtable();
                noneTable.Add("id", 0);
                noneTable.Add("text", "无菜单");
                tableList.Add(noneTable);

                if (count < 1)
                {
                    return tableList;
                }

                foreach (var item in parentList)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("id", item.mId);
                    ht.Add("text", item.mText);
                    InitMenuComboChildren(menuList, ht, item.mId);
                    tableList.Add(ht);
                }
                return tableList;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return new List<Hashtable>();
            }
        }

        /// <summary>
        /// 获取GridTree
        /// </summary>
        /// <param name="count"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public object GetAppMenuGridTree(ref int count, ref string errMsg)
        {
            try
            {
                List<tb_Sys_MenuInfo> queryList = null;
                using (BXUUEntities appEntities = new BXUUEntities())
                {
                    queryList = appEntities.tb_Sys_MenuInfos.OrderBy(o => o.mOrderindex).ToList();
                    count = queryList.Count();
                    if (count < 1)
                    {
                        return null;
                    }
                }

                List<Hashtable> htList = new List<Hashtable>();
                var parentList = queryList.Where(o => o.mPId == 0).OrderBy(o => o.mOrderindex);
                foreach (tb_Sys_MenuInfo actionItem in parentList)
                {
                    Hashtable ht = InitMenuGridTreeChildren(queryList, actionItem);
                    htList.Add(ht);
                }
                return htList;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                count = 0;
                return null;
            }
        }
        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <param name="menuItem"></param>
        /// <param name="errMsg"></param>
        public void EditMenu(tb_Sys_MenuInfo menuItem, ref string errMsg)
        {
            try
            {
                using (BXUUEntities appEntities = new BXUUEntities())
                {
                    var findItem = appEntities.tb_Sys_MenuInfos.Where(o => o.mId == menuItem.mId).FirstOrDefault();

                    findItem.mText = menuItem.mText;
                    findItem.mPId = menuItem.mPId;
                    findItem.mNum = menuItem.mNum;
                    //findItem.Disc = menuItem.Disc;
                    findItem.mIcon = menuItem.mIcon;
                    findItem.mUrl = menuItem.mUrl;
                    findItem.mOrderindex = menuItem.mOrderindex;

                    appEntities.SaveChanges();
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="errMsg"></param>
        public void DeleteMenu(int menuId, ref string errMsg)
        {
            try
            {
                using (BXUUEntities appEntities = new BXUUEntities())
                {
                    var menuItem = appEntities.tb_Sys_MenuInfos.Where(o => o.mId == menuId).FirstOrDefault();
                    if (menuId == null)
                    {
                        errMsg = "查无菜单";
                        return;
                    }

                    appEntities.tb_Sys_MenuInfos.Remove(menuItem);
                    appEntities.SaveChanges();
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
        }
        private string CreateMenuNum(tb_Sys_MenuInfo menu)
        {
            string actionCount = "01";
            string menuNum = "MN";
            tb_Sys_MenuInfo parentMenu = null;
            tb_Sys_MenuInfo lastMenu = null;
            using (BXUUEntities hisEntities = new BXUUEntities())
            {
                //查找上级菜单
                parentMenu = hisEntities.tb_Sys_MenuInfos.Where(o => o.mId == menu.mPId).FirstOrDefault();
                //查询菜单下最大编号不包括自己和已经删除的
                lastMenu = hisEntities.tb_Sys_MenuInfos.Where(o => o.mPId == menu.mPId).OrderByDescending(o => o.mNum).FirstOrDefault();
            }
            if (lastMenu != null)
            {
                var str = lastMenu.mNum.Substring(lastMenu.mNum.Length - 2, 2);
                int number = Convert.ToInt32(str) + 1;
                actionCount = number.ToString().PadLeft(2, '0');
            }

            if (parentMenu != null)
            {
                menuNum = parentMenu.mNum + actionCount;
                return menuNum;
            }
            return menuNum + actionCount;
        }
        private Hashtable InitMenuGridTreeChildren(List<tb_Sys_MenuInfo> menuList, tb_Sys_MenuInfo item)
        {
            Hashtable ht = new Hashtable();
            ht.Add("mId", item.mId);
            ht.Add("mText", item.mText);
            ht.Add("mNum", item.mNum);
            ht.Add("mUrl", item.mUrl);
            ht.Add("mIcon", item.mIcon);
            //ht.Add("Disc", item.Disc);
            ht.Add("mState", item.mState);
            ht.Add("mAddtime", Convert.ToDateTime(item.mAddtime).ToString("yyyy-MM-dd HH:mm:ss"));
            ht.Add("mPId", item.mPId);
            ht.Add("mOrderindex", item.mOrderindex);

            var childrenList = menuList.Where(o => o.mPId == item.mId).OrderBy(o => o.mOrderindex);
            if (childrenList.Count() > 0)
            {
                List<Hashtable> htList = new List<Hashtable>();
                foreach (tb_Sys_MenuInfo childrenItem in childrenList)
                {
                    Hashtable childrenHt = InitMenuGridTreeChildren(menuList, childrenItem);
                    htList.Add(childrenHt);
                }
                ht.Add("children", htList);
            }
            return ht;

        }
        private void InitMenuComboChildren(List<tb_Sys_MenuInfo> appMenus, Hashtable ht, int parentId)
        {
            List<tb_Sys_MenuInfo> menuList = appMenus.Where(o => o.mPId == parentId).ToList();
            int childrenCount = menuList.Count();
            if (childrenCount > 0)
            {
                List<Hashtable> cList = new List<Hashtable>();
                foreach (var item in menuList)
                {
                    Hashtable cht = new Hashtable();
                    cht.Add("id", item.mId);
                    cht.Add("text", item.mText);
                    InitMenuComboChildren(appMenus, cht, item.mId);
                    cList.Add(cht);
                }
                ht.Add("children", cList);
            }
        }
    }
}
