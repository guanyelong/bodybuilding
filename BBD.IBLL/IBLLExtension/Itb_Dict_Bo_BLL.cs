using BBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBD.IBLL
{
    public partial interface Itb_Dict_Bo_BLL
    {
        ///// <summary>
        ///// 获取所有用户
        ///// </summary>
        ///// <param name="Id"></param>
        ///// <returns></returns>
        //tb_Dict GetAllAppDictById(int Id);
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        List<tb_Dict> GetAppDictList(int pageIndex, int pageSize, ref int count, string name, string word);
        /// <summary>
        /// 添加字典
        /// </summary>
        /// <param name="dictInfo"></param>
        /// <param name="errMsg"></param>
        void AddDict(tb_Dict dictInfo, ref string errMsg);
        /// <summary>
        /// 编辑字典
        /// </summary>
        /// <param name="dictInfo"></param>
        /// <param name="errMsg"></param>
        void EditDict(tb_Dict dictInfo, ref string errMsg);
        ///// <summary>
        ///// 删除字典
        ///// </summary>
        ///// <param name="roleId"></param>
        ///// <param name="errMsg"></param>
        //void DeleteDict(int roleId, ref string errMsg);

        /// <summary>
        /// 获取Combobox数据
        /// </summary>
        /// <param name="count"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        object GetDictComboData(ref int count, ref string errMsg);

    }
}
