
using System;
using System.Collections.Generic; 
using System.Linq;   
using System.Text; 
using BBD.IDAL; 

namespace BBD.DAL
{
	public partial class DBSession:IDBSession
    {
		#region 01 数据接口 Itb_Consume_Log_Bo_DAL
		Itb_Consume_Log_Bo_DAL itb_Consume_Log_Bo_DAL;
		public Itb_Consume_Log_Bo_DAL Itb_Consume_Log_Bo_DAL
		{
			get
			{
				if(itb_Consume_Log_Bo_DAL==null)
					itb_Consume_Log_Bo_DAL= new tb_Consume_Log_Bo_DAL();
				return itb_Consume_Log_Bo_DAL;
			}
			set
			{
				itb_Consume_Log_Bo_DAL= value;
			}
		}
		#endregion

		#region 02 数据接口 Itb_Dict_Bo_DAL
		Itb_Dict_Bo_DAL itb_Dict_Bo_DAL;
		public Itb_Dict_Bo_DAL Itb_Dict_Bo_DAL
		{
			get
			{
				if(itb_Dict_Bo_DAL==null)
					itb_Dict_Bo_DAL= new tb_Dict_Bo_DAL();
				return itb_Dict_Bo_DAL;
			}
			set
			{
				itb_Dict_Bo_DAL= value;
			}
		}
		#endregion

		#region 03 数据接口 Itb_Emp_Hos_Bo_DAL
		Itb_Emp_Hos_Bo_DAL itb_Emp_Hos_Bo_DAL;
		public Itb_Emp_Hos_Bo_DAL Itb_Emp_Hos_Bo_DAL
		{
			get
			{
				if(itb_Emp_Hos_Bo_DAL==null)
					itb_Emp_Hos_Bo_DAL= new tb_Emp_Hos_Bo_DAL();
				return itb_Emp_Hos_Bo_DAL;
			}
			set
			{
				itb_Emp_Hos_Bo_DAL= value;
			}
		}
		#endregion

		#region 04 数据接口 Itb_Hei_Wei_Bo_DAL
		Itb_Hei_Wei_Bo_DAL itb_Hei_Wei_Bo_DAL;
		public Itb_Hei_Wei_Bo_DAL Itb_Hei_Wei_Bo_DAL
		{
			get
			{
				if(itb_Hei_Wei_Bo_DAL==null)
					itb_Hei_Wei_Bo_DAL= new tb_Hei_Wei_Bo_DAL();
				return itb_Hei_Wei_Bo_DAL;
			}
			set
			{
				itb_Hei_Wei_Bo_DAL= value;
			}
		}
		#endregion

		#region 05 数据接口 Itb_Hosp_Info_Bo_DAL
		Itb_Hosp_Info_Bo_DAL itb_Hosp_Info_Bo_DAL;
		public Itb_Hosp_Info_Bo_DAL Itb_Hosp_Info_Bo_DAL
		{
			get
			{
				if(itb_Hosp_Info_Bo_DAL==null)
					itb_Hosp_Info_Bo_DAL= new tb_Hosp_Info_Bo_DAL();
				return itb_Hosp_Info_Bo_DAL;
			}
			set
			{
				itb_Hosp_Info_Bo_DAL= value;
			}
		}
		#endregion

		#region 06 数据接口 Itb_Serv_Info_Bo_DAL
		Itb_Serv_Info_Bo_DAL itb_Serv_Info_Bo_DAL;
		public Itb_Serv_Info_Bo_DAL Itb_Serv_Info_Bo_DAL
		{
			get
			{
				if(itb_Serv_Info_Bo_DAL==null)
					itb_Serv_Info_Bo_DAL= new tb_Serv_Info_Bo_DAL();
				return itb_Serv_Info_Bo_DAL;
			}
			set
			{
				itb_Serv_Info_Bo_DAL= value;
			}
		}
		#endregion

		#region 07 数据接口 Itb_Serv_POF_Bo_DAL
		Itb_Serv_POF_Bo_DAL itb_Serv_POF_Bo_DAL;
		public Itb_Serv_POF_Bo_DAL Itb_Serv_POF_Bo_DAL
		{
			get
			{
				if(itb_Serv_POF_Bo_DAL==null)
					itb_Serv_POF_Bo_DAL= new tb_Serv_POF_Bo_DAL();
				return itb_Serv_POF_Bo_DAL;
			}
			set
			{
				itb_Serv_POF_Bo_DAL= value;
			}
		}
		#endregion

		#region 08 数据接口 Itb_Sms_Down_Bo_DAL
		Itb_Sms_Down_Bo_DAL itb_Sms_Down_Bo_DAL;
		public Itb_Sms_Down_Bo_DAL Itb_Sms_Down_Bo_DAL
		{
			get
			{
				if(itb_Sms_Down_Bo_DAL==null)
					itb_Sms_Down_Bo_DAL= new tb_Sms_Down_Bo_DAL();
				return itb_Sms_Down_Bo_DAL;
			}
			set
			{
				itb_Sms_Down_Bo_DAL= value;
			}
		}
		#endregion

		#region 09 数据接口 Itb_Sms_report_Bo_DAL
		Itb_Sms_report_Bo_DAL itb_Sms_report_Bo_DAL;
		public Itb_Sms_report_Bo_DAL Itb_Sms_report_Bo_DAL
		{
			get
			{
				if(itb_Sms_report_Bo_DAL==null)
					itb_Sms_report_Bo_DAL= new tb_Sms_report_Bo_DAL();
				return itb_Sms_report_Bo_DAL;
			}
			set
			{
				itb_Sms_report_Bo_DAL= value;
			}
		}
		#endregion

		#region 10 数据接口 Itb_Sms_Tpl_Bo_DAL
		Itb_Sms_Tpl_Bo_DAL itb_Sms_Tpl_Bo_DAL;
		public Itb_Sms_Tpl_Bo_DAL Itb_Sms_Tpl_Bo_DAL
		{
			get
			{
				if(itb_Sms_Tpl_Bo_DAL==null)
					itb_Sms_Tpl_Bo_DAL= new tb_Sms_Tpl_Bo_DAL();
				return itb_Sms_Tpl_Bo_DAL;
			}
			set
			{
				itb_Sms_Tpl_Bo_DAL= value;
			}
		}
		#endregion

		#region 11 数据接口 Itb_Sms_up_Bo_DAL
		Itb_Sms_up_Bo_DAL itb_Sms_up_Bo_DAL;
		public Itb_Sms_up_Bo_DAL Itb_Sms_up_Bo_DAL
		{
			get
			{
				if(itb_Sms_up_Bo_DAL==null)
					itb_Sms_up_Bo_DAL= new tb_Sms_up_Bo_DAL();
				return itb_Sms_up_Bo_DAL;
			}
			set
			{
				itb_Sms_up_Bo_DAL= value;
			}
		}
		#endregion

		#region 12 数据接口 Itb_Sys_Department_Bo_DAL
		Itb_Sys_Department_Bo_DAL itb_Sys_Department_Bo_DAL;
		public Itb_Sys_Department_Bo_DAL Itb_Sys_Department_Bo_DAL
		{
			get
			{
				if(itb_Sys_Department_Bo_DAL==null)
					itb_Sys_Department_Bo_DAL= new tb_Sys_Department_Bo_DAL();
				return itb_Sys_Department_Bo_DAL;
			}
			set
			{
				itb_Sys_Department_Bo_DAL= value;
			}
		}
		#endregion

		#region 13 数据接口 Itb_Sys_log_Bo_DAL
		Itb_Sys_log_Bo_DAL itb_Sys_log_Bo_DAL;
		public Itb_Sys_log_Bo_DAL Itb_Sys_log_Bo_DAL
		{
			get
			{
				if(itb_Sys_log_Bo_DAL==null)
					itb_Sys_log_Bo_DAL= new tb_Sys_log_Bo_DAL();
				return itb_Sys_log_Bo_DAL;
			}
			set
			{
				itb_Sys_log_Bo_DAL= value;
			}
		}
		#endregion

		#region 14 数据接口 Itb_Sys_LoginLog_Bo_DAL
		Itb_Sys_LoginLog_Bo_DAL itb_Sys_LoginLog_Bo_DAL;
		public Itb_Sys_LoginLog_Bo_DAL Itb_Sys_LoginLog_Bo_DAL
		{
			get
			{
				if(itb_Sys_LoginLog_Bo_DAL==null)
					itb_Sys_LoginLog_Bo_DAL= new tb_Sys_LoginLog_Bo_DAL();
				return itb_Sys_LoginLog_Bo_DAL;
			}
			set
			{
				itb_Sys_LoginLog_Bo_DAL= value;
			}
		}
		#endregion

		#region 15 数据接口 Itb_Sys_MenuInfo_Bo_DAL
		Itb_Sys_MenuInfo_Bo_DAL itb_Sys_MenuInfo_Bo_DAL;
		public Itb_Sys_MenuInfo_Bo_DAL Itb_Sys_MenuInfo_Bo_DAL
		{
			get
			{
				if(itb_Sys_MenuInfo_Bo_DAL==null)
					itb_Sys_MenuInfo_Bo_DAL= new tb_Sys_MenuInfo_Bo_DAL();
				return itb_Sys_MenuInfo_Bo_DAL;
			}
			set
			{
				itb_Sys_MenuInfo_Bo_DAL= value;
			}
		}
		#endregion

		#region 16 数据接口 Itb_Sys_Notice_Bo_DAL
		Itb_Sys_Notice_Bo_DAL itb_Sys_Notice_Bo_DAL;
		public Itb_Sys_Notice_Bo_DAL Itb_Sys_Notice_Bo_DAL
		{
			get
			{
				if(itb_Sys_Notice_Bo_DAL==null)
					itb_Sys_Notice_Bo_DAL= new tb_Sys_Notice_Bo_DAL();
				return itb_Sys_Notice_Bo_DAL;
			}
			set
			{
				itb_Sys_Notice_Bo_DAL= value;
			}
		}
		#endregion

		#region 17 数据接口 Itb_Sys_Permission_Bo_DAL
		Itb_Sys_Permission_Bo_DAL itb_Sys_Permission_Bo_DAL;
		public Itb_Sys_Permission_Bo_DAL Itb_Sys_Permission_Bo_DAL
		{
			get
			{
				if(itb_Sys_Permission_Bo_DAL==null)
					itb_Sys_Permission_Bo_DAL= new tb_Sys_Permission_Bo_DAL();
				return itb_Sys_Permission_Bo_DAL;
			}
			set
			{
				itb_Sys_Permission_Bo_DAL= value;
			}
		}
		#endregion

		#region 18 数据接口 Itb_Sys_Role_Bo_DAL
		Itb_Sys_Role_Bo_DAL itb_Sys_Role_Bo_DAL;
		public Itb_Sys_Role_Bo_DAL Itb_Sys_Role_Bo_DAL
		{
			get
			{
				if(itb_Sys_Role_Bo_DAL==null)
					itb_Sys_Role_Bo_DAL= new tb_Sys_Role_Bo_DAL();
				return itb_Sys_Role_Bo_DAL;
			}
			set
			{
				itb_Sys_Role_Bo_DAL= value;
			}
		}
		#endregion

		#region 19 数据接口 Itb_Sys_RolePermission_Bo_DAL
		Itb_Sys_RolePermission_Bo_DAL itb_Sys_RolePermission_Bo_DAL;
		public Itb_Sys_RolePermission_Bo_DAL Itb_Sys_RolePermission_Bo_DAL
		{
			get
			{
				if(itb_Sys_RolePermission_Bo_DAL==null)
					itb_Sys_RolePermission_Bo_DAL= new tb_Sys_RolePermission_Bo_DAL();
				return itb_Sys_RolePermission_Bo_DAL;
			}
			set
			{
				itb_Sys_RolePermission_Bo_DAL= value;
			}
		}
		#endregion

		#region 20 数据接口 Itb_Sys_UserInfo_Bo_DAL
		Itb_Sys_UserInfo_Bo_DAL itb_Sys_UserInfo_Bo_DAL;
		public Itb_Sys_UserInfo_Bo_DAL Itb_Sys_UserInfo_Bo_DAL
		{
			get
			{
				if(itb_Sys_UserInfo_Bo_DAL==null)
					itb_Sys_UserInfo_Bo_DAL= new tb_Sys_UserInfo_Bo_DAL();
				return itb_Sys_UserInfo_Bo_DAL;
			}
			set
			{
				itb_Sys_UserInfo_Bo_DAL= value;
			}
		}
		#endregion

		#region 21 数据接口 Itb_Sys_UserRole_Bo_DAL
		Itb_Sys_UserRole_Bo_DAL itb_Sys_UserRole_Bo_DAL;
		public Itb_Sys_UserRole_Bo_DAL Itb_Sys_UserRole_Bo_DAL
		{
			get
			{
				if(itb_Sys_UserRole_Bo_DAL==null)
					itb_Sys_UserRole_Bo_DAL= new tb_Sys_UserRole_Bo_DAL();
				return itb_Sys_UserRole_Bo_DAL;
			}
			set
			{
				itb_Sys_UserRole_Bo_DAL= value;
			}
		}
		#endregion

		#region 22 数据接口 Itb_Sys_UserVipPermission_Bo_DAL
		Itb_Sys_UserVipPermission_Bo_DAL itb_Sys_UserVipPermission_Bo_DAL;
		public Itb_Sys_UserVipPermission_Bo_DAL Itb_Sys_UserVipPermission_Bo_DAL
		{
			get
			{
				if(itb_Sys_UserVipPermission_Bo_DAL==null)
					itb_Sys_UserVipPermission_Bo_DAL= new tb_Sys_UserVipPermission_Bo_DAL();
				return itb_Sys_UserVipPermission_Bo_DAL;
			}
			set
			{
				itb_Sys_UserVipPermission_Bo_DAL= value;
			}
		}
		#endregion

		#region 23 数据接口 Itb_User_Account_Bo_DAL
		Itb_User_Account_Bo_DAL itb_User_Account_Bo_DAL;
		public Itb_User_Account_Bo_DAL Itb_User_Account_Bo_DAL
		{
			get
			{
				if(itb_User_Account_Bo_DAL==null)
					itb_User_Account_Bo_DAL= new tb_User_Account_Bo_DAL();
				return itb_User_Account_Bo_DAL;
			}
			set
			{
				itb_User_Account_Bo_DAL= value;
			}
		}
		#endregion

		#region 24 数据接口 Itb_User_Buy_Rec_Bo_DAL
		Itb_User_Buy_Rec_Bo_DAL itb_User_Buy_Rec_Bo_DAL;
		public Itb_User_Buy_Rec_Bo_DAL Itb_User_Buy_Rec_Bo_DAL
		{
			get
			{
				if(itb_User_Buy_Rec_Bo_DAL==null)
					itb_User_Buy_Rec_Bo_DAL= new tb_User_Buy_Rec_Bo_DAL();
				return itb_User_Buy_Rec_Bo_DAL;
			}
			set
			{
				itb_User_Buy_Rec_Bo_DAL= value;
			}
		}
		#endregion

		#region 25 数据接口 Itb_User_Dislike_Food_Bo_DAL
		Itb_User_Dislike_Food_Bo_DAL itb_User_Dislike_Food_Bo_DAL;
		public Itb_User_Dislike_Food_Bo_DAL Itb_User_Dislike_Food_Bo_DAL
		{
			get
			{
				if(itb_User_Dislike_Food_Bo_DAL==null)
					itb_User_Dislike_Food_Bo_DAL= new tb_User_Dislike_Food_Bo_DAL();
				return itb_User_Dislike_Food_Bo_DAL;
			}
			set
			{
				itb_User_Dislike_Food_Bo_DAL= value;
			}
		}
		#endregion

		#region 26 数据接口 Itb_User_Info_Bo_DAL
		Itb_User_Info_Bo_DAL itb_User_Info_Bo_DAL;
		public Itb_User_Info_Bo_DAL Itb_User_Info_Bo_DAL
		{
			get
			{
				if(itb_User_Info_Bo_DAL==null)
					itb_User_Info_Bo_DAL= new tb_User_Info_Bo_DAL();
				return itb_User_Info_Bo_DAL;
			}
			set
			{
				itb_User_Info_Bo_DAL= value;
			}
		}
		#endregion

		#region 27 数据接口 Itb_Weight_Chg_Bo_DAL
		Itb_Weight_Chg_Bo_DAL itb_Weight_Chg_Bo_DAL;
		public Itb_Weight_Chg_Bo_DAL Itb_Weight_Chg_Bo_DAL
		{
			get
			{
				if(itb_Weight_Chg_Bo_DAL==null)
					itb_Weight_Chg_Bo_DAL= new tb_Weight_Chg_Bo_DAL();
				return itb_Weight_Chg_Bo_DAL;
			}
			set
			{
				itb_Weight_Chg_Bo_DAL= value;
			}
		}
		#endregion

		#region 28 数据接口 Itb_Weight_Chg_Self_Bo_DAL
		Itb_Weight_Chg_Self_Bo_DAL itb_Weight_Chg_Self_Bo_DAL;
		public Itb_Weight_Chg_Self_Bo_DAL Itb_Weight_Chg_Self_Bo_DAL
		{
			get
			{
				if(itb_Weight_Chg_Self_Bo_DAL==null)
					itb_Weight_Chg_Self_Bo_DAL= new tb_Weight_Chg_Self_Bo_DAL();
				return itb_Weight_Chg_Self_Bo_DAL;
			}
			set
			{
				itb_Weight_Chg_Self_Bo_DAL= value;
			}
		}
		#endregion

    }

}