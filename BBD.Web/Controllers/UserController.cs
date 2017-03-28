using BBD.Models;
using BBD.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBD.Web.Controllers
{
    public class UserController : Controller
    {
        OperContext oc = OperContext.CurrentContext;
        [AuthenticationAttribute]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 新增编辑的对话框页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Dialog(int Id)
        {
            if (Id > 0)
            {
                tb_Sys_UserInfo user = oc.iBllSession.Itb_Sys_UserInfo_Bo_BLL.GetAllAppUserById(Id);
                if (user == null)
                {
                    user = new tb_Sys_UserInfo();
                }
                return View(user);
            }

            return View(new tb_Sys_UserInfo());
        }

        public ActionResult ChangePasswordDlg(int Id)
        {
            if (Id > 0)
            {
                tb_Sys_UserInfo user = oc.iBllSession.Itb_Sys_UserInfo_Bo_BLL.GetAllAppUserById(Id);
                if (user == null)
                {
                    user = new tb_Sys_UserInfo();
                }
                return View(user);
            }

            return View(new tb_Sys_UserInfo());
        }

        /// <summary>
        /// 获取分页的用户数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAppUserList()
        {
            int pageIndex = int.Parse(Request["page"]);  //当前页  
            int pageSize = int.Parse(Request["rows"]);  //页面行数
            string name = Request["name"];
            int count = 0;
            List<tb_Sys_UserInfo> appUserList = oc.iBllSession.Itb_Sys_UserInfo_Bo_BLL.GetAppUserList(pageIndex, pageSize, name, ref count);
            if (appUserList != null)
            {

                var result = from appUser in appUserList
                             select new
                             {
                                 Uid = appUser.Uid,
                                 appUser.uName,
                                 appUser.uMobile,
                                 appUser.uGender,
                                 uAddtime = Convert.ToDateTime(appUser.uAddtime).ToString("yyyy-MM-dd HH:mm:ss"),
                                 appUser.uPost,
                                 appUser.uLoginName,
                                 appUser.CityName,
                                 appUser.CityId,
                                 appUser.uDepid,
                                 appUser.uDepName
                             };

                var data = new
                {
                    total = count,
                    rows = result
                };

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 保存用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveUser(tb_Sys_UserInfo user)
        {
            if (user == null)
            {
                return Json(new { result = "error", mesage = "用户数据为空" });
            }
            string errMsg = "";
            if (user.Uid == 0)
            {
                //user.uAddtime = DateTime.Now;
                //add
                //oc.iBllSession.Itb_Sys_UserInfo_Pe_BLL.AddUser(user, ref errMsg);
                //Common.LogHelper.InsertLog(String.Format("新增用户,ID-{0}", user.ID.ToString()), 43, "后台用户");

                List<tb_Sys_UserInfo> userlist = oc.iBllSession.Itb_Sys_UserInfo_Bo_BLL.GetListBy(o => o.uLoginName == user.uLoginName);
                if (userlist != null && userlist.Count > 0)
                {
                    errMsg = "用户名已经存在";
                }
                else
                {
                    user.uPwd = BBD.Common.MD5Helper.MD5Encrypt32bit(user.uPwd);
                    int identity = oc.iBllSession.Itb_Sys_UserInfo_Bo_BLL.Add(user);
                    if (identity == 0) errMsg = "系统插入数据失败！";
                    else BBD.Common.LogHelper.Info(String.Format("新增用户,ID-{0}", user.Uid.ToString()) + "后台用户添加");
                }

            }
            else
            {
                //edit
                oc.iBllSession.Itb_Sys_UserInfo_Bo_BLL.EditUser(user, ref errMsg);
                BBD.Common.LogHelper.Info(String.Format("编辑用户,ID-{0}", user.Uid.ToString() + " 后台用户"));
                //Expression<Func<tb_Sys_UserInfo, object>>[] ignoreProperties = new Expression<Func<tb_Sys_UserInfo, object>>[]{
                // p=>p.Uid,p=>p.uIsDel,p=>p.uAddtime,p=>p.uDepName,p=>p.uDepNum,p=>p.tb_Sys_Department,p=>p.tb_Sys_UserRole,p=>p.tb_Sys_UserVipPermission
                //};
                //oc.iBllSession.Itb_Sys_UserInfo_Pe_BLL.Modify(user,ignoreProperties);
                //PMM.Common.LogHelper.Info(String.Format("修改用户，ID-{0}", user.Uid.ToString()) + "后台用户修改");
            }
            var result = new { result = "ok", message = "操作成功" };

            if (!string.IsNullOrEmpty(errMsg))
            {
                result = new { result = "error", message = errMsg };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public JsonResult DeleteUser(int userId)
        {
            if (userId == 0)
            {
                return Json(new { result = "error", mesage = "用户编号为空" });
            }

            string errMsg = string.Empty;
            oc.iBllSession.Itb_Sys_UserInfo_Bo_BLL.DeleteUser(userId, ref errMsg);

            var result = new { result = "ok", message = "操作成功" };
            if (!string.IsNullOrEmpty(errMsg))
            {
                result = new { result = "error", message = errMsg };
            }

            //Common.LogHelper.InsertLog(String.Format("删除用户,ID-{0}", userId.ToString()), 43, "后台用户");
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ChangePassword(int userId, string password, string localpwd)
        {
            if (userId == 0)
            {
                return Json(new { result = "error", mesage = "用户编号为空" });
            }
            var lpwd = oc.iBllSession.Itb_Sys_UserInfo_Bo_BLL.GetObjet(o => o.Uid == userId).uPwd;
            var localMD5 = BBD.Common.MD5Helper.MD5Encrypt32bit(localpwd);
            if (lpwd != localMD5)
            {
                return Json(new { result = "error", message = "原密码不正确！" }, "text/html", JsonRequestBehavior.AllowGet);
            }

            string errMsg = string.Empty;
            oc.iBllSession.Itb_Sys_UserInfo_Bo_BLL.ChangePassword(userId, password, ref errMsg);

            var result = new { result = "ok", message = "修改密码成功" };
            if (!string.IsNullOrEmpty(errMsg))
            {
                result = new { result = "error", message = errMsg };
            }


            //Common.LogHelper.InsertLog(String.Format("修改用户密码,ID-{0}", userId.ToString()), 43, "后台用户");
            return Json(result, "text/html", JsonRequestBehavior.AllowGet);
        }
    }
}