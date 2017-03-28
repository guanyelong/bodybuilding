using BBD.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBD.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        OperContext oc = OperContext.CurrentContext;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Main()
        {
            if (AdminSystemInfo.CurrentUser != null)
                return View(AdminSystemInfo.CurrentUser);
            else
                return RedirectToAction("Index", "Login");
        }
        public ActionResult Left()
        {
            if (AdminSystemInfo.CurrentUser != null)
            {
                var queryList = oc.iBllSession.Itb_Sys_UserRole_Bo_BLL.GetAppUserMenuList(AdminSystemInfo.CurrentUser.Uid);
                if (queryList == null)
                {
                    return View();
                }
                if (queryList != null)
                {
                    ViewBag.MainPMenu = queryList.Where(o => o.mPId == 0).OrderBy(o => o.mOrderindex).ToList();
                    ViewBag.MainCMenu = queryList.Where(o => o.mPId != 0).OrderBy(o => o.mOrderindex).ToList();
                }
            }
            return View();
        }
        public ActionResult Center()
        {
            return View();
        }

        public ActionResult Portal()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult LoadUserTree(string id)
        {
            JTreeCommon jtree = new JTreeCommon();
            #region XML方法
            /*
             1.获取XML中菜单数据 转化为EasyuiTreeNode
             2.返回给json
             */
            List<Hashtable> list = jtree.GetUserTreeList(id);
            return Json(list, JsonRequestBehavior.AllowGet);
            #endregion
        }

        public ActionResult Logout()
        {
            AdminSystemInfo.CurrentUser = null;
            Response.Cookies["HHYS_TokenId"].Value = null;
            Response.Cookies["HHYS_TokenId"].Expires = DateTime.Now.AddDays(-1);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
	}
}