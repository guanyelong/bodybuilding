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
    public partial class tb_Sys_Department_Bo_Service : Itb_Sys_Department_Bo_BLL
    {
        /// <summary>
        /// 根据id 获取department
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public tb_Sys_Department GetDepartmentByID(int id, ref string errMsg)
        {
            try
            {
                using (BXUUEntities appEntitys = new BXUUEntities())
                {
                    tb_Sys_Department department = appEntitys.tb_Sys_Departments.Where(o => o.DepId == id).FirstOrDefault();
                    if (department != null)
                    {
                        return department;
                    }
                    errMsg = "查无数据";
                    return null;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
        }
        /// <summary>
        /// 获取部门Tree
        /// </summary>
        /// <param name="count"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<Hashtable> GetDepartmentTree(ref int count, ref string errMsg)
        {
            try
            {
                using (BXUUEntities appEntitys = new BXUUEntities())
                {

                    IQueryable<tb_Sys_Department> departmentList = appEntitys.tb_Sys_Departments.Where(o => o.DepIsDel == 1 && o.DepPId == 0);
                    count = departmentList.Count();
                    List<Hashtable> tableList = new List<Hashtable>();
                    Hashtable noneTable = new Hashtable();
                    noneTable.Add("id", 0);
                    noneTable.Add("text", "无部门");
                    tableList.Add(noneTable);
                    if (count < 1)
                    {
                        return tableList;
                    }
                    foreach (var item in departmentList)
                    {
                        Hashtable ht = new Hashtable();
                        ht.Add("id", item.DepId);
                        ht.Add("text", item.DepName);
                        InitDepartmentChildren(appEntitys, ht, item.DepId);
                        tableList.Add(ht);
                    }
                    return tableList;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return new List<Hashtable>();
            }
        }
        /// <summary>
        /// 递归构造部门关联
        /// </summary>
        /// <param name="appEntitys"></param>
        /// <param name="ht"></param>
        /// <param name="parentId"></param>
        private void InitDepartmentChildren(BXUUEntities appEntitys, Hashtable ht, int parentId)
        {
            IQueryable<tb_Sys_Department> departmentList = appEntitys.tb_Sys_Departments.Where(o => o.DepIsDel == 1 && o.DepPId == parentId);
            int childrenCount = departmentList.Count();
            if (childrenCount > 0)
            {
                List<Hashtable> cList = new List<Hashtable>();
                foreach (var item in departmentList)
                {
                    Hashtable cht = new Hashtable();
                    cht.Add("id", item.DepId);
                    cht.Add("text", item.DepName);
                    InitDepartmentChildren(appEntitys, cht, item.DepId);
                    cList.Add(cht);
                }
                ht.Add("children", cList);
            }
        }
        /// <summary>
        /// 获取TreeGrid 格式数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="name"></param>
        /// <param name="count"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<Hashtable> GetDepartmentTreeGridList(ref int count, ref string errMsg)
        {
            try
            {
                List<tb_Sys_Department> queryList = null;
                using (BXUUEntities appEntities = new BXUUEntities())
                {
                    queryList = appEntities.tb_Sys_Departments.Where(o => o.DepIsDel == 1).ToList();
                    count = queryList.Count();
                    if (count < 1)
                    {
                        return null;
                    }
                }

                List<Hashtable> htList = new List<Hashtable>();
                var parentList = queryList.Where(o => o.DepPId == 0);
                new tb_Dict_Bo_Service().DbName="hhm";
                var dict = new tb_Dict_Bo_Service().GetListBy(p => p.KeyName == "city" && p.state == 1);

                foreach (tb_Sys_Department actionItem in parentList)
                {
                    actionItem.CityName = dict.Where(p => p.KeyValue == actionItem.CityId).Select(p => p.KeyWords).FirstOrDefault();
                    Hashtable ht = InitDepartmentGridChildren(queryList, actionItem);
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
        /// 添加部门
        /// </summary>
        /// <param name="department"></param>
        /// <param name="errMsg"></param>
        public void AddDepartment(tb_Sys_Department department, ref string errMsg)
        {
            try
            {
                department.DepIsDel = 1;
                using (BXUUEntities appEntitys = new BXUUEntities())
                {
                    if (department.DepPId == null) department.DepPId = 0;
                    //检查部门编号和部门名称不能重复
                    var queryList = appEntitys.tb_Sys_Departments.Where(o => o.DepName == department.DepName || o.DepNum == department.DepNum);
                    if (queryList.Count() > 0)
                    {
                        errMsg = "部门名称或部门编号重复";
                        return;
                    }
                    department.DepNum = CreateDeptNum(department);
                    appEntitys.tb_Sys_Departments.Add(department);
                    appEntitys.SaveChanges();
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

        }

        /// <summary>
        /// 编辑部门
        /// </summary>
        /// <param name="department"></param>
        /// <param name="errMsg"></param>
        public void EditDepartment(tb_Sys_Department department, ref string errMsg)
        {
            try
            {
                using (BXUUEntities appEntitys = new BXUUEntities())
                {
                    tb_Sys_Department editItem = appEntitys.tb_Sys_Departments.Where(o => o.DepId == department.DepId).FirstOrDefault();
                    if (editItem == null)
                    {
                        errMsg = "查无数据";
                        return;
                    }

                    editItem.DepNum = department.DepNum;
                    editItem.DepName = department.DepName;
                    editItem.CityId = department.CityId;
                    editItem.Depremark = department.Depremark;
                    editItem.DepPId = department.DepPId;
                    editItem.DepIsDel = department.DepIsDel;

                    appEntitys.SaveChanges();
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="departmentID"></param>
        /// <param name="errMsg"></param>
        public void DeleteDepartment(int departmentID, ref string errMsg)
        {
            try
            {
                using (BXUUEntities appEntitys = new BXUUEntities())
                {
                    tb_Sys_Department editItem = appEntitys.tb_Sys_Departments.Where(o => o.DepId == departmentID).FirstOrDefault();
                    if (editItem == null)
                    {
                        errMsg = "查无数据";
                        return;
                    }
                    //标记删除
                    editItem.DepIsDel = 0;
                    appEntitys.SaveChanges();
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
        }
        private Hashtable InitDepartmentGridChildren(List<tb_Sys_Department> departmentList, tb_Sys_Department item)
        {
            Hashtable ht = new Hashtable();
            ht.Add("DepId", item.DepId);
            ht.Add("DepName", item.DepName);
            ht.Add("DepNum", item.DepNum);
            ht.Add("Depremark", item.Depremark);
            ht.Add("DepIsDel", item.DepIsDel);
            ht.Add("CityName", item.CityName);
            ht.Add("DepAddtime", Convert.ToDateTime(item.DepAddtime).ToString("yyyy-MM-dd HH:mm:ss"));
            ht.Add("DepPId", item.DepPId);

            var childrenList = departmentList.Where(o => o.DepPId == item.DepId);

            if (childrenList.Count() > 0)
            {
                new tb_Dict_Bo_Service().DbName = "hhm";
                var dict = new tb_Dict_Bo_Service().GetListBy(p => p.KeyName == "city" && p.state == 1);
                List<Hashtable> htList = new List<Hashtable>();
                foreach (tb_Sys_Department childrenItem in childrenList)
                {
                    childrenItem.CityName = dict.Where(p => p.KeyValue == childrenItem.CityId).Select(p => p.KeyWords).FirstOrDefault();
                    Hashtable childrenHt = InitDepartmentGridChildren(departmentList, childrenItem);
                    htList.Add(childrenHt);
                }
                ht.Add("children", htList);
            }
            return ht;
        }
        private string CreateDeptNum(tb_Sys_Department dept)
        {
            string actionCount = "01";
            string deptNum = "DT";
            tb_Sys_Department parentMenu = null;
            tb_Sys_Department lastMenu = null;
            using (BXUUEntities hisEntities = new BXUUEntities())
            {
                //查找上级菜单
                parentMenu = hisEntities.tb_Sys_Departments.Where(o => o.DepId == dept.DepPId).FirstOrDefault();
                //查询菜单下最大编号不包括自己和已经删除的
                lastMenu = hisEntities.tb_Sys_Departments.Where(o => o.DepPId == dept.DepPId).OrderByDescending(o => o.DepNum).FirstOrDefault();
            }
            if (lastMenu != null)
            {
                var str = lastMenu.DepNum.Substring(lastMenu.DepNum.Length - 2, 2);
                int number = Convert.ToInt32(str) + 1;
                actionCount = number.ToString().PadLeft(2, '0');
            }

            if (parentMenu != null)
            {
                deptNum = parentMenu.DepNum + actionCount;
                return deptNum;
            }
            return deptNum + actionCount;
        }
    }
}
