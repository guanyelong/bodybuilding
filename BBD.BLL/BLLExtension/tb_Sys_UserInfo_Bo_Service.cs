using BBD.IBLL;
using BBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBD.BLL
{
    public partial class tb_Sys_UserInfo_Bo_Service : Itb_Sys_UserInfo_Bo_BLL
    {
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public tb_Sys_UserInfo GetAllAppUserById(int id)
        {
            using (BXUUEntities AppStoreEntity = new BXUUEntities())
            {
                tb_Sys_UserInfo model = null;
                var query = (from a in AppStoreEntity.tb_Sys_UserInfos
                             join d in AppStoreEntity.tb_Sys_Departments on a.uDepid equals d.DepId into temp
                             from d in temp.DefaultIfEmpty()
                             where a.Uid == id
                             select new { a, d }).FirstOrDefault();
                if (query == null)
                {
                    return new tb_Sys_UserInfo();
                }

                model = new tb_Sys_UserInfo();
                model.uDepid = query.a.uDepid;
                model.uAddtime = query.a.uAddtime;
                model.Uid = query.a.Uid;
                model.uLoginName = query.a.uLoginName;
                model.uPwd = query.a.uPwd;
                model.uName = query.a.uName;
                model.uMobile = query.a.uMobile;
                model.CityId = query.a.CityId;
                model.uGender = query.a.uGender;
                model.uPost = query.a.uPost;
                model.uRemark = query.a.uRemark;
                if (query.d != null)
                {
                    model.uDepName = query.d.DepName;
                    model.uDepNum = query.d.DepNum;
                }
                else
                {
                    model.uDepName = "无部门";
                }

                return model;
            }
        }
        /// <summary>
        /// 分页获取AppUserList
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="name"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<tb_Sys_UserInfo> GetAppUserList(int pageIndex, int pageSize, string name, ref int count)
        {

            try
            {
                //查询数据
                using (BXUUEntities appEntities = new BXUUEntities())
                {
                    var queryResult = (from a in appEntities.tb_Sys_UserInfos
                                       join b in appEntities.tb_Sys_Departments on a.uDepid equals b.DepId into temp
                                       from b in temp.DefaultIfEmpty()
                                       select new { a, b });
                    if (!String.IsNullOrEmpty(name))
                    {
                        queryResult = queryResult.Where(p => p.a.uLoginName.Contains(name) || p.a.uName.Contains(name));
                    }
                    count = queryResult.Count();

                    if (count < 1)
                    {
                        return new List<tb_Sys_UserInfo>();
                    }
                    //分页
                    queryResult = queryResult.OrderByDescending(p => p.a.Uid).Skip((pageIndex - 1) * pageSize).Take(pageSize);

                    //转换
                    List<tb_Sys_UserInfo> result = new List<tb_Sys_UserInfo>();
                    List<tb_Dict> list_dict = new tb_Dict_Bo_Service().GetListBy(p=>p.KeyName=="city",p=>p.Seq);
                    foreach (var item in queryResult)
                    {

                        tb_Sys_UserInfo user = new tb_Sys_UserInfo();
                        user.Uid = item.a.Uid;
                        user.uName = item.a.uName;
                        user.uMobile = item.a.uMobile;
                        user.uAddtime = item.a.uAddtime;
                        user.uLoginName = item.a.uLoginName;
                        user.uPost = item.a.uPost;
                        user.CityId = item.a.CityId;
                        user.CityName = list_dict.Where(p => p.KeyValue == item.a.CityId && p.KeyName.ToLower()=="city").Select(p => p.KeyWords).FirstOrDefault();
                        user.uGender = item.a.uGender;
                        if (item.b == null)
                        {
                            user.uDepid = 0;
                            user.uDepName = "无部门";
                        }
                        else
                        {
                            user.uDepid = item.b.DepId;
                            user.uDepName = item.b.DepName;
                        }

                        result.Add(user);
                    }

                    return result;
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(tb_Sys_UserInfo user, ref string errMsg)
        {
            try
            {
                using (BXUUEntities appEntity = new BXUUEntities())
                {
                    //检查重复用户名
                    IQueryable<tb_Sys_UserInfo> appList = appEntity.tb_Sys_UserInfos.Where(o => o.uLoginName == user.uLoginName);
                    if (appList.Count() > 0)
                    {

                        errMsg = "用户名已经存在";
                        return;
                    }

                    user.uPwd = BBD.Common.MD5Helper.MD5Encrypt32bit(user.uPwd);
                    appEntity.tb_Sys_UserInfos.Add(user);

                    //为用户设置默认角色
                    appEntity.SaveChanges();
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
        }

        /// <summary>
        /// 编辑User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="errMsg"></param>
        public void EditUser(tb_Sys_UserInfo user, ref string errMsg)
        {
            try
            {
                using (BXUUEntities appEntity = new BXUUEntities())
                {
                    var item = appEntity.tb_Sys_UserInfos.Where(o => o.Uid == user.Uid).FirstOrDefault();

                    if (item == null)
                    {
                        errMsg = "无此数据";
                        return;
                    }
                    item.uName = user.uName;
                    item.uLoginName = user.uLoginName;
                    item.CityId = user.CityId;
                    item.uGender = user.uGender;
                    item.uMobile = user.uMobile;
                    item.uPost = user.uPost;
                    item.uDepid = user.uDepid;
                    appEntity.SaveChanges();
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
        }
        /// <summary>
        /// 删除用户数据
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="errMsg"></param>
        public void DeleteUser(int userId, ref string errMsg)
        {
            try
            {
                using (BXUUEntities appEntitys = new BXUUEntities())
                {
                    var appUser = appEntitys.tb_Sys_UserInfos.Where(o => o.Uid == userId).FirstOrDefault();
                    if (appUser == null)
                    {
                        errMsg = "查找不到数据数据";
                        return;
                    }
                    //删除用户
                    appEntitys.tb_Sys_UserInfos.Remove(appUser);
                    //删除用户级联的角色
                    var roleMapping = appEntitys.tb_Sys_UserRoles.Where(o => o.urUid == userId);
                    if (roleMapping.Count() > 0)
                    {
                        foreach (var item in roleMapping)
                        {
                            appEntitys.tb_Sys_UserRoles.Remove(item);
                        }
                    }

                    appEntitys.SaveChanges();

                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
        }
        /// <summary>
        /// 想修改用户密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="errMsg"></param>
        public void ChangePassword(int userId, string password, ref string errMsg)
        {
            try
            {
                using (BXUUEntities appEntities = new BXUUEntities())
                {
                    var findUser = appEntities.tb_Sys_UserInfos.Where(o => o.Uid == userId).FirstOrDefault();
                    if (findUser == null)
                    {
                        errMsg = "查无用户";
                        return;
                    }
                    // 密码使用md5 加密
                    findUser.uPwd = Common.MD5Helper.MD5Encrypt32bit(password);
                    appEntities.SaveChanges();
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
        }

        public tb_Sys_UserInfo ValidateLogin(tb_Sys_UserInfo info)
        {
            using (BXUUEntities appEntitys = new BXUUEntities())
            {
                var pwd = BBD.Common.MD5Helper.MD5Encrypt32bit(info.uPwd);
                var item = appEntitys.tb_Sys_UserInfos.Where(p => p.uLoginName == info.uLoginName && p.uPwd == pwd).FirstOrDefault();
                return item;
            }
        }

        public tb_Sys_UserInfo GetAppUserByLoginName(string userName)
        {
            using (BXUUEntities appEntities = new BXUUEntities())
            {
                var userItem = (from a in appEntities.tb_Sys_UserInfos where a.uLoginName == userName select a).FirstOrDefault();
                return userItem;
            }
        }
    }
}
