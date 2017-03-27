
using System;
using System.Collections.Generic;
using System.Linq;  
using System.Text; 
using BBD.IBLL; 


namespace BBD.BLL    
{
	public partial class BLLSession:IBLLSession
    {
		#region 01 业务接口 Itb_Consume_Log_Bo_DAL
		Itb_Consume_Log_Bo_BLL itb_Consume_Log_Bo_BLL;
		public Itb_Consume_Log_Bo_BLL Itb_Consume_Log_Bo_BLL
		{
			get
			{
				if(itb_Consume_Log_Bo_BLL==null)
					itb_Consume_Log_Bo_BLL= new tb_Consume_Log_Bo_Service();
				return itb_Consume_Log_Bo_BLL;
			}
			set
			{
				itb_Consume_Log_Bo_BLL= value;
			}
		}
		#endregion

		#region 02 业务接口 Itb_Dict_Bo_DAL
		Itb_Dict_Bo_BLL itb_Dict_Bo_BLL;
		public Itb_Dict_Bo_BLL Itb_Dict_Bo_BLL
		{
			get
			{
				if(itb_Dict_Bo_BLL==null)
					itb_Dict_Bo_BLL= new tb_Dict_Bo_Service();
				return itb_Dict_Bo_BLL;
			}
			set
			{
				itb_Dict_Bo_BLL= value;
			}
		}
		#endregion

		#region 03 业务接口 Itb_Emp_Hos_Bo_DAL
		Itb_Emp_Hos_Bo_BLL itb_Emp_Hos_Bo_BLL;
		public Itb_Emp_Hos_Bo_BLL Itb_Emp_Hos_Bo_BLL
		{
			get
			{
				if(itb_Emp_Hos_Bo_BLL==null)
					itb_Emp_Hos_Bo_BLL= new tb_Emp_Hos_Bo_Service();
				return itb_Emp_Hos_Bo_BLL;
			}
			set
			{
				itb_Emp_Hos_Bo_BLL= value;
			}
		}
		#endregion

		#region 04 业务接口 Itb_Hei_Wei_Bo_DAL
		Itb_Hei_Wei_Bo_BLL itb_Hei_Wei_Bo_BLL;
		public Itb_Hei_Wei_Bo_BLL Itb_Hei_Wei_Bo_BLL
		{
			get
			{
				if(itb_Hei_Wei_Bo_BLL==null)
					itb_Hei_Wei_Bo_BLL= new tb_Hei_Wei_Bo_Service();
				return itb_Hei_Wei_Bo_BLL;
			}
			set
			{
				itb_Hei_Wei_Bo_BLL= value;
			}
		}
		#endregion

		#region 05 业务接口 Itb_Hosp_Info_Bo_DAL
		Itb_Hosp_Info_Bo_BLL itb_Hosp_Info_Bo_BLL;
		public Itb_Hosp_Info_Bo_BLL Itb_Hosp_Info_Bo_BLL
		{
			get
			{
				if(itb_Hosp_Info_Bo_BLL==null)
					itb_Hosp_Info_Bo_BLL= new tb_Hosp_Info_Bo_Service();
				return itb_Hosp_Info_Bo_BLL;
			}
			set
			{
				itb_Hosp_Info_Bo_BLL= value;
			}
		}
		#endregion

		#region 06 业务接口 Itb_Serv_Info_Bo_DAL
		Itb_Serv_Info_Bo_BLL itb_Serv_Info_Bo_BLL;
		public Itb_Serv_Info_Bo_BLL Itb_Serv_Info_Bo_BLL
		{
			get
			{
				if(itb_Serv_Info_Bo_BLL==null)
					itb_Serv_Info_Bo_BLL= new tb_Serv_Info_Bo_Service();
				return itb_Serv_Info_Bo_BLL;
			}
			set
			{
				itb_Serv_Info_Bo_BLL= value;
			}
		}
		#endregion

		#region 07 业务接口 Itb_Serv_POF_Bo_DAL
		Itb_Serv_POF_Bo_BLL itb_Serv_POF_Bo_BLL;
		public Itb_Serv_POF_Bo_BLL Itb_Serv_POF_Bo_BLL
		{
			get
			{
				if(itb_Serv_POF_Bo_BLL==null)
					itb_Serv_POF_Bo_BLL= new tb_Serv_POF_Bo_Service();
				return itb_Serv_POF_Bo_BLL;
			}
			set
			{
				itb_Serv_POF_Bo_BLL= value;
			}
		}
		#endregion

		#region 08 业务接口 Itb_Sms_Down_Bo_DAL
		Itb_Sms_Down_Bo_BLL itb_Sms_Down_Bo_BLL;
		public Itb_Sms_Down_Bo_BLL Itb_Sms_Down_Bo_BLL
		{
			get
			{
				if(itb_Sms_Down_Bo_BLL==null)
					itb_Sms_Down_Bo_BLL= new tb_Sms_Down_Bo_Service();
				return itb_Sms_Down_Bo_BLL;
			}
			set
			{
				itb_Sms_Down_Bo_BLL= value;
			}
		}
		#endregion

		#region 09 业务接口 Itb_Sms_report_Bo_DAL
		Itb_Sms_report_Bo_BLL itb_Sms_report_Bo_BLL;
		public Itb_Sms_report_Bo_BLL Itb_Sms_report_Bo_BLL
		{
			get
			{
				if(itb_Sms_report_Bo_BLL==null)
					itb_Sms_report_Bo_BLL= new tb_Sms_report_Bo_Service();
				return itb_Sms_report_Bo_BLL;
			}
			set
			{
				itb_Sms_report_Bo_BLL= value;
			}
		}
		#endregion

		#region 10 业务接口 Itb_Sms_Tpl_Bo_DAL
		Itb_Sms_Tpl_Bo_BLL itb_Sms_Tpl_Bo_BLL;
		public Itb_Sms_Tpl_Bo_BLL Itb_Sms_Tpl_Bo_BLL
		{
			get
			{
				if(itb_Sms_Tpl_Bo_BLL==null)
					itb_Sms_Tpl_Bo_BLL= new tb_Sms_Tpl_Bo_Service();
				return itb_Sms_Tpl_Bo_BLL;
			}
			set
			{
				itb_Sms_Tpl_Bo_BLL= value;
			}
		}
		#endregion

		#region 11 业务接口 Itb_Sms_up_Bo_DAL
		Itb_Sms_up_Bo_BLL itb_Sms_up_Bo_BLL;
		public Itb_Sms_up_Bo_BLL Itb_Sms_up_Bo_BLL
		{
			get
			{
				if(itb_Sms_up_Bo_BLL==null)
					itb_Sms_up_Bo_BLL= new tb_Sms_up_Bo_Service();
				return itb_Sms_up_Bo_BLL;
			}
			set
			{
				itb_Sms_up_Bo_BLL= value;
			}
		}
		#endregion

		#region 12 业务接口 Itb_Sys_Department_Bo_DAL
		Itb_Sys_Department_Bo_BLL itb_Sys_Department_Bo_BLL;
		public Itb_Sys_Department_Bo_BLL Itb_Sys_Department_Bo_BLL
		{
			get
			{
				if(itb_Sys_Department_Bo_BLL==null)
					itb_Sys_Department_Bo_BLL= new tb_Sys_Department_Bo_Service();
				return itb_Sys_Department_Bo_BLL;
			}
			set
			{
				itb_Sys_Department_Bo_BLL= value;
			}
		}
		#endregion

		#region 13 业务接口 Itb_Sys_log_Bo_DAL
		Itb_Sys_log_Bo_BLL itb_Sys_log_Bo_BLL;
		public Itb_Sys_log_Bo_BLL Itb_Sys_log_Bo_BLL
		{
			get
			{
				if(itb_Sys_log_Bo_BLL==null)
					itb_Sys_log_Bo_BLL= new tb_Sys_log_Bo_Service();
				return itb_Sys_log_Bo_BLL;
			}
			set
			{
				itb_Sys_log_Bo_BLL= value;
			}
		}
		#endregion

		#region 14 业务接口 Itb_Sys_LoginLog_Bo_DAL
		Itb_Sys_LoginLog_Bo_BLL itb_Sys_LoginLog_Bo_BLL;
		public Itb_Sys_LoginLog_Bo_BLL Itb_Sys_LoginLog_Bo_BLL
		{
			get
			{
				if(itb_Sys_LoginLog_Bo_BLL==null)
					itb_Sys_LoginLog_Bo_BLL= new tb_Sys_LoginLog_Bo_Service();
				return itb_Sys_LoginLog_Bo_BLL;
			}
			set
			{
				itb_Sys_LoginLog_Bo_BLL= value;
			}
		}
		#endregion

		#region 15 业务接口 Itb_Sys_MenuInfo_Bo_DAL
		Itb_Sys_MenuInfo_Bo_BLL itb_Sys_MenuInfo_Bo_BLL;
		public Itb_Sys_MenuInfo_Bo_BLL Itb_Sys_MenuInfo_Bo_BLL
		{
			get
			{
				if(itb_Sys_MenuInfo_Bo_BLL==null)
					itb_Sys_MenuInfo_Bo_BLL= new tb_Sys_MenuInfo_Bo_Service();
				return itb_Sys_MenuInfo_Bo_BLL;
			}
			set
			{
				itb_Sys_MenuInfo_Bo_BLL= value;
			}
		}
		#endregion

		#region 16 业务接口 Itb_Sys_Notice_Bo_DAL
		Itb_Sys_Notice_Bo_BLL itb_Sys_Notice_Bo_BLL;
		public Itb_Sys_Notice_Bo_BLL Itb_Sys_Notice_Bo_BLL
		{
			get
			{
				if(itb_Sys_Notice_Bo_BLL==null)
					itb_Sys_Notice_Bo_BLL= new tb_Sys_Notice_Bo_Service();
				return itb_Sys_Notice_Bo_BLL;
			}
			set
			{
				itb_Sys_Notice_Bo_BLL= value;
			}
		}
		#endregion

		#region 17 业务接口 Itb_Sys_Permission_Bo_DAL
		Itb_Sys_Permission_Bo_BLL itb_Sys_Permission_Bo_BLL;
		public Itb_Sys_Permission_Bo_BLL Itb_Sys_Permission_Bo_BLL
		{
			get
			{
				if(itb_Sys_Permission_Bo_BLL==null)
					itb_Sys_Permission_Bo_BLL= new tb_Sys_Permission_Bo_Service();
				return itb_Sys_Permission_Bo_BLL;
			}
			set
			{
				itb_Sys_Permission_Bo_BLL= value;
			}
		}
		#endregion

		#region 18 业务接口 Itb_Sys_Role_Bo_DAL
		Itb_Sys_Role_Bo_BLL itb_Sys_Role_Bo_BLL;
		public Itb_Sys_Role_Bo_BLL Itb_Sys_Role_Bo_BLL
		{
			get
			{
				if(itb_Sys_Role_Bo_BLL==null)
					itb_Sys_Role_Bo_BLL= new tb_Sys_Role_Bo_Service();
				return itb_Sys_Role_Bo_BLL;
			}
			set
			{
				itb_Sys_Role_Bo_BLL= value;
			}
		}
		#endregion

		#region 19 业务接口 Itb_Sys_RolePermission_Bo_DAL
		Itb_Sys_RolePermission_Bo_BLL itb_Sys_RolePermission_Bo_BLL;
		public Itb_Sys_RolePermission_Bo_BLL Itb_Sys_RolePermission_Bo_BLL
		{
			get
			{
				if(itb_Sys_RolePermission_Bo_BLL==null)
					itb_Sys_RolePermission_Bo_BLL= new tb_Sys_RolePermission_Bo_Service();
				return itb_Sys_RolePermission_Bo_BLL;
			}
			set
			{
				itb_Sys_RolePermission_Bo_BLL= value;
			}
		}
		#endregion

		#region 20 业务接口 Itb_Sys_UserInfo_Bo_DAL
		Itb_Sys_UserInfo_Bo_BLL itb_Sys_UserInfo_Bo_BLL;
		public Itb_Sys_UserInfo_Bo_BLL Itb_Sys_UserInfo_Bo_BLL
		{
			get
			{
				if(itb_Sys_UserInfo_Bo_BLL==null)
					itb_Sys_UserInfo_Bo_BLL= new tb_Sys_UserInfo_Bo_Service();
				return itb_Sys_UserInfo_Bo_BLL;
			}
			set
			{
				itb_Sys_UserInfo_Bo_BLL= value;
			}
		}
		#endregion

		#region 21 业务接口 Itb_Sys_UserRole_Bo_DAL
		Itb_Sys_UserRole_Bo_BLL itb_Sys_UserRole_Bo_BLL;
		public Itb_Sys_UserRole_Bo_BLL Itb_Sys_UserRole_Bo_BLL
		{
			get
			{
				if(itb_Sys_UserRole_Bo_BLL==null)
					itb_Sys_UserRole_Bo_BLL= new tb_Sys_UserRole_Bo_Service();
				return itb_Sys_UserRole_Bo_BLL;
			}
			set
			{
				itb_Sys_UserRole_Bo_BLL= value;
			}
		}
		#endregion

		#region 22 业务接口 Itb_Sys_UserVipPermission_Bo_DAL
		Itb_Sys_UserVipPermission_Bo_BLL itb_Sys_UserVipPermission_Bo_BLL;
		public Itb_Sys_UserVipPermission_Bo_BLL Itb_Sys_UserVipPermission_Bo_BLL
		{
			get
			{
				if(itb_Sys_UserVipPermission_Bo_BLL==null)
					itb_Sys_UserVipPermission_Bo_BLL= new tb_Sys_UserVipPermission_Bo_Service();
				return itb_Sys_UserVipPermission_Bo_BLL;
			}
			set
			{
				itb_Sys_UserVipPermission_Bo_BLL= value;
			}
		}
		#endregion

		#region 23 业务接口 Itb_User_Account_Bo_DAL
		Itb_User_Account_Bo_BLL itb_User_Account_Bo_BLL;
		public Itb_User_Account_Bo_BLL Itb_User_Account_Bo_BLL
		{
			get
			{
				if(itb_User_Account_Bo_BLL==null)
					itb_User_Account_Bo_BLL= new tb_User_Account_Bo_Service();
				return itb_User_Account_Bo_BLL;
			}
			set
			{
				itb_User_Account_Bo_BLL= value;
			}
		}
		#endregion

		#region 24 业务接口 Itb_User_Buy_Rec_Bo_DAL
		Itb_User_Buy_Rec_Bo_BLL itb_User_Buy_Rec_Bo_BLL;
		public Itb_User_Buy_Rec_Bo_BLL Itb_User_Buy_Rec_Bo_BLL
		{
			get
			{
				if(itb_User_Buy_Rec_Bo_BLL==null)
					itb_User_Buy_Rec_Bo_BLL= new tb_User_Buy_Rec_Bo_Service();
				return itb_User_Buy_Rec_Bo_BLL;
			}
			set
			{
				itb_User_Buy_Rec_Bo_BLL= value;
			}
		}
		#endregion

		#region 25 业务接口 Itb_User_Dislike_Food_Bo_DAL
		Itb_User_Dislike_Food_Bo_BLL itb_User_Dislike_Food_Bo_BLL;
		public Itb_User_Dislike_Food_Bo_BLL Itb_User_Dislike_Food_Bo_BLL
		{
			get
			{
				if(itb_User_Dislike_Food_Bo_BLL==null)
					itb_User_Dislike_Food_Bo_BLL= new tb_User_Dislike_Food_Bo_Service();
				return itb_User_Dislike_Food_Bo_BLL;
			}
			set
			{
				itb_User_Dislike_Food_Bo_BLL= value;
			}
		}
		#endregion

		#region 26 业务接口 Itb_User_Info_Bo_DAL
		Itb_User_Info_Bo_BLL itb_User_Info_Bo_BLL;
		public Itb_User_Info_Bo_BLL Itb_User_Info_Bo_BLL
		{
			get
			{
				if(itb_User_Info_Bo_BLL==null)
					itb_User_Info_Bo_BLL= new tb_User_Info_Bo_Service();
				return itb_User_Info_Bo_BLL;
			}
			set
			{
				itb_User_Info_Bo_BLL= value;
			}
		}
		#endregion

		#region 27 业务接口 Itb_Weight_Chg_Bo_DAL
		Itb_Weight_Chg_Bo_BLL itb_Weight_Chg_Bo_BLL;
		public Itb_Weight_Chg_Bo_BLL Itb_Weight_Chg_Bo_BLL
		{
			get
			{
				if(itb_Weight_Chg_Bo_BLL==null)
					itb_Weight_Chg_Bo_BLL= new tb_Weight_Chg_Bo_Service();
				return itb_Weight_Chg_Bo_BLL;
			}
			set
			{
				itb_Weight_Chg_Bo_BLL= value;
			}
		}
		#endregion

		#region 28 业务接口 Itb_Weight_Chg_Self_Bo_DAL
		Itb_Weight_Chg_Self_Bo_BLL itb_Weight_Chg_Self_Bo_BLL;
		public Itb_Weight_Chg_Self_Bo_BLL Itb_Weight_Chg_Self_Bo_BLL
		{
			get
			{
				if(itb_Weight_Chg_Self_Bo_BLL==null)
					itb_Weight_Chg_Self_Bo_BLL= new tb_Weight_Chg_Self_Bo_Service();
				return itb_Weight_Chg_Self_Bo_BLL;
			}
			set
			{
				itb_Weight_Chg_Self_Bo_BLL= value;
			}
		}
		#endregion

    }

}