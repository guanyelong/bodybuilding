using BBD.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBD.Web.Models
{
    public class JTreeCommon
    {
        private OperContext oc = OperContext.CurrentContext;

        /// <summary>
        ///  根据权限获取MenuTree
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Hashtable> GetUserTreeList(string id)
        {
            //var menus = xDoc.GetTreeData();
            //登录失效
            if (AdminSystemInfo.CurrentUser == null)
            {
                return new List<Hashtable>();
            }

            id = string.IsNullOrEmpty(id) ? "0" : id;

            int userId = AdminSystemInfo.CurrentUser.Uid;
            //根据权限获取xml所有数据
            var menus = oc.iBllSession.Itb_Sys_UserRole_Bo_BLL.GetAppUserMenuList(userId);
            //获取顶级菜单
            var parentMenus = menus.Where(o => o.mPId == Convert.ToInt32(id)).OrderBy(o => o.mOrderindex);

            List<Hashtable> treeList = new List<Hashtable>();
            foreach (tb_Sys_MenuInfo item in parentMenus)
            {
                Hashtable ht = InitMenuTreeChildren(menus, item);
                treeList.Add(ht);
                //Hashtable ht = new Hashtable();
                //ht.Add("id", item.mId);
                //ht.Add("text", item.mText);
                //ht.Add("checked", item.mChecked);

                //Dictionary<string, string> attributes = new Dictionary<string, string>();
                //attributes.Add("url", item.mUrl);
                //ht.Add("attributes", attributes);
                //ht.Add("state", item.mState);
                //InitMenuTreeChildren(menus, ht, item.mId);
                //treeList.Add(ht);
            }
            return treeList;
        }
        //加载子级节点var
        private void InitMenuTreeChildren(List<tb_Sys_MenuInfo> menuList, Hashtable ht, int parentId)
        {
            var childrenList = menuList.Where(o => o.mPId == parentId).OrderBy(o => o.mOrderindex);
            int childrenCount = childrenList.Count();
            if (childrenCount > 0)
            {
                List<Hashtable> cList = new List<Hashtable>();
                foreach (var item in childrenList)
                {
                    Hashtable cht = new Hashtable();
                    cht.Add("id", item.mId);
                    cht.Add("text", item.mText);
                    cht.Add("checked", item.mChecked);
                    Dictionary<string, string> attributes = new Dictionary<string, string>();
                    attributes.Add("url", item.mUrl);
                    cht.Add("attributes", attributes);
                    cht.Add("state", item.mState);
                    InitMenuTreeChildren(menuList, ht, item.mId);
                    cList.Add(cht);
                }

                ht.Add("children", cList);
            }
        }

        private Hashtable InitMenuTreeChildren(List<tb_Sys_MenuInfo> menuList, tb_Sys_MenuInfo item)
        {
            Hashtable cht = new Hashtable();
            cht.Add("id", item.mId);
            cht.Add("text", item.mText);
            cht.Add("checked", item.mChecked);
            Dictionary<string, string> attributes = new Dictionary<string, string>();
            attributes.Add("url", item.mUrl);
            cht.Add("attributes", attributes);
            cht.Add("state", item.mState);

            var childrenList = menuList.Where(o => o.mPId == item.mId).OrderBy(o => o.mOrderindex);
            if (childrenList.Count() > 0)
            {
                List<Hashtable> htList = new List<Hashtable>();
                foreach (tb_Sys_MenuInfo childrenItem in childrenList)
                {
                    Hashtable childrenHt = InitMenuTreeChildren(menuList, childrenItem);
                    htList.Add(childrenHt);
                }
                cht.Add("children", htList);
            }
            return cht;
        }

    }
}