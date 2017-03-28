using BBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBD.IBLL
{
    public partial interface Itb_Sys_MenuInfo_Bo_BLL
    {
        /// <summary>
        /// 根据ID获取菜单编号
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        tb_Sys_MenuInfo GetAppMenuByID(int Id, ref string errMsg);
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menuItem"></param>
        /// <param name="errMsg"></param>
        void AddMenu(tb_Sys_MenuInfo menuItem, ref string errMsg);
        /// <summary>
        /// 获取ComboTree Grid数据
        /// </summary>
        /// <param name="count"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        object GetMenuComboTree(ref int count, ref string errMsg);
        /// <summary>
        /// 获取GridTree
        /// </summary>
        /// <param name="count"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        object GetAppMenuGridTree(ref int count, ref string errMsg);
        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <param name="menuItem"></param>
        /// <param name="errMsg"></param>
        void EditMenu(tb_Sys_MenuInfo menuItem, ref string errMsg);
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="errMsg"></param>
        void DeleteMenu(int menuId, ref string errMsg);
    }
}
