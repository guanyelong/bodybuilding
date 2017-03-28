using BBD.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBD.IBLL
{
    public partial interface Itb_Sys_Department_Bo_BLL
    {
        /// <summary>
        /// 根据id 获取department
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        tb_Sys_Department GetDepartmentByID(int id, ref string errMsg);
        /// <summary>
        /// 获取部门Tree
        /// </summary>
        /// <param name="count"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        List<Hashtable> GetDepartmentTree(ref int count, ref string errMsg);
        /// <summary>
        /// 获取TreeGrid 格式数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="name"></param>
        /// <param name="count"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        List<Hashtable> GetDepartmentTreeGridList(ref int count, ref string errMsg);
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="department"></param>
        /// <param name="errMsg"></param>
        void AddDepartment(tb_Sys_Department department, ref string errMsg);
        /// <summary>
        /// 编辑部门
        /// </summary>
        /// <param name="department"></param>
        /// <param name="errMsg"></param>
        void EditDepartment(tb_Sys_Department department, ref string errMsg);
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="departmentID"></param>
        /// <param name="errMsg"></param>
        void DeleteDepartment(int departmentID, ref string errMsg);

    }
}
