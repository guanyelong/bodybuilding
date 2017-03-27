 using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text; 
using BBD.Models; 
using BBD.IBLL;   
namespace BBD.BLL  
{
	public partial class tb_Consume_Log_Bo_Service : BaseBLL<tb_Consume_Log>,Itb_Consume_Log_Bo_BLL
    {
		public override void SetiDal()
		{
			idal = DbSession.Itb_Consume_Log_Bo_DAL;
		}
    }
	public partial class tb_Dict_Bo_Service : BaseBLL<tb_Dict>,Itb_Dict_Bo_BLL
    {
		public override void SetiDal()
		{
			idal = DbSession.Itb_Dict_Bo_DAL;
		}
    }
	public partial class tb_Emp_Hos_Bo_Service : BaseBLL<tb_Emp_Hos>,Itb_Emp_Hos_Bo_BLL
    {
		public override void SetiDal()
		{
			idal = DbSession.Itb_Emp_Hos_Bo_DAL;
		}
    }
	public partial class tb_Hei_Wei_Bo_Service : BaseBLL<tb_Hei_Wei>,Itb_Hei_Wei_Bo_BLL
    {
		public override void SetiDal()
		{
			idal = DbSession.Itb_Hei_Wei_Bo_DAL;
		}
    }
	public partial class tb_Hosp_Info_Bo_Service : BaseBLL<tb_Hosp_Info>,Itb_Hosp_Info_Bo_BLL
    {
		public override void SetiDal()
		{
			idal = DbSession.Itb_Hosp_Info_Bo_DAL;
		}
    }
	public partial class tb_Serv_Info_Bo_Service : BaseBLL<tb_Serv_Info>,Itb_Serv_Info_Bo_BLL
    {
		public override void SetiDal()
		{
			idal = DbSession.Itb_Serv_Info_Bo_DAL;
		}
    }
	public partial class tb_Serv_POF_Bo_Service : BaseBLL<tb_Serv_POF>,Itb_Serv_POF_Bo_BLL
    {
		public override void SetiDal()
		{
			idal = DbSession.Itb_Serv_POF_Bo_DAL;
		}
    }
	public partial class tb_Sms_Down_Bo_Service : BaseBLL<tb_Sms_Down>,Itb_Sms_Down_Bo_BLL
    {
		public override void SetiDal()
		{
			idal = DbSession.Itb_Sms_Down_Bo_DAL;
		}
    }
	public partial class tb_Sms_report_Bo_Service : BaseBLL<tb_Sms_report>,Itb_Sms_report_Bo_BLL
    {
		public override void SetiDal()
		{
			idal = DbSession.Itb_Sms_report_Bo_DAL;
		}
    }
	public partial class tb_Sms_Tpl_Bo_Service : BaseBLL<tb_Sms_Tpl>,Itb_Sms_Tpl_Bo_BLL
    {
		public override void SetiDal()
		{
			idal = DbSession.Itb_Sms_Tpl_Bo_DAL;
		}
    }
	public partial class tb_Sms_up_Bo_Service : BaseBLL<tb_Sms_up>,Itb_Sms_up_Bo_BLL
    {
		public override void SetiDal()
		{
			idal = DbSession.Itb_Sms_up_Bo_DAL;
		}
    }
	public partial class tb_Sys_Department_Bo_Service : BaseBLL<tb_Sys_Department>,Itb_Sys_Department_Bo_BLL
    {
		public override void SetiDal()
		{
			idal = DbSession.Itb_Sys_Department_Bo_DAL;
		}
    }
	public partial class tb_Sys_log_Bo_Service : BaseBLL<tb_Sys_log>,Itb_Sys_log_Bo_BLL
    {
		public override void SetiDal()
		{
			idal = DbSession.Itb_Sys_log_Bo_DAL;
		}
    }
	public partial class tb_Sys_LoginLog_Bo_Service : BaseBLL<tb_Sys_LoginLog>,Itb_Sys_LoginLog_Bo_BLL
    {
		public override void SetiDal()
		{
			idal = DbSession.Itb_Sys_LoginLog_Bo_DAL;
		}
    }
	public partial class tb_Sys_MenuInfo_Bo_Service : BaseBLL<tb_Sys_MenuInfo>,Itb_Sys_MenuInfo_Bo_BLL
    {
		public override void SetiDal()
		{
			idal = DbSession.Itb_Sys_MenuInfo_Bo_DAL;
		}
    }
	public partial class tb_Sys_Notice_Bo_Service : BaseBLL<tb_Sys_Notice>,Itb_Sys_Notice_Bo_BLL
    {
		public override void SetiDal()
		{
			idal = DbSession.Itb_Sys_Notice_Bo_DAL;
		}
    }
	public partial class tb_Sys_Permission_Bo_Service : BaseBLL<tb_Sys_Permission>,Itb_Sys_Permission_Bo_BLL
    {
		public override void SetiDal()
		{
			idal = DbSession.Itb_Sys_Permission_Bo_DAL;
		}
    }
	public partial class tb_Sys_Role_Bo_Service : BaseBLL<tb_Sys_Role>,Itb_Sys_Role_Bo_BLL
    {
		public override void SetiDal()
		{
			idal = DbSession.Itb_Sys_Role_Bo_DAL;
		}
    }
	public partial class tb_Sys_RolePermission_Bo_Service : BaseBLL<tb_Sys_RolePermission>,Itb_Sys_RolePermission_Bo_BLL
    {
		public override void SetiDal()
		{
			idal = DbSession.Itb_Sys_RolePermission_Bo_DAL;
		}
    }
	public partial class tb_Sys_UserInfo_Bo_Service : BaseBLL<tb_Sys_UserInfo>,Itb_Sys_UserInfo_Bo_BLL
    {
		public override void SetiDal()
		{
			idal = DbSession.Itb_Sys_UserInfo_Bo_DAL;
		}
    }
	public partial class tb_Sys_UserRole_Bo_Service : BaseBLL<tb_Sys_UserRole>,Itb_Sys_UserRole_Bo_BLL
    {
		public override void SetiDal()
		{
			idal = DbSession.Itb_Sys_UserRole_Bo_DAL;
		}
    }
	public partial class tb_Sys_UserVipPermission_Bo_Service : BaseBLL<tb_Sys_UserVipPermission>,Itb_Sys_UserVipPermission_Bo_BLL
    {
		public override void SetiDal()
		{
			idal = DbSession.Itb_Sys_UserVipPermission_Bo_DAL;
		}
    }
	public partial class tb_User_Account_Bo_Service : BaseBLL<tb_User_Account>,Itb_User_Account_Bo_BLL
    {
		public override void SetiDal()
		{
			idal = DbSession.Itb_User_Account_Bo_DAL;
		}
    }
	public partial class tb_User_Buy_Rec_Bo_Service : BaseBLL<tb_User_Buy_Rec>,Itb_User_Buy_Rec_Bo_BLL
    {
		public override void SetiDal()
		{
			idal = DbSession.Itb_User_Buy_Rec_Bo_DAL;
		}
    }
	public partial class tb_User_Dislike_Food_Bo_Service : BaseBLL<tb_User_Dislike_Food>,Itb_User_Dislike_Food_Bo_BLL
    {
		public override void SetiDal()
		{
			idal = DbSession.Itb_User_Dislike_Food_Bo_DAL;
		}
    }
	public partial class tb_User_Info_Bo_Service : BaseBLL<tb_User_Info>,Itb_User_Info_Bo_BLL
    {
		public override void SetiDal()
		{
			idal = DbSession.Itb_User_Info_Bo_DAL;
		}
    }
	public partial class tb_Weight_Chg_Bo_Service : BaseBLL<tb_Weight_Chg>,Itb_Weight_Chg_Bo_BLL
    {
		public override void SetiDal()
		{
			idal = DbSession.Itb_Weight_Chg_Bo_DAL;
		}
    }
	public partial class tb_Weight_Chg_Self_Bo_Service : BaseBLL<tb_Weight_Chg_Self>,Itb_Weight_Chg_Self_Bo_BLL
    {
		public override void SetiDal()
		{
			idal = DbSession.Itb_Weight_Chg_Self_Bo_DAL;
		}
    }
}