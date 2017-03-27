 
 
using System;
using System.Collections.Generic;  
using System.Linq;  
using System.Text;   

namespace BBD.IDAL
{
	public partial interface IDBSession  
    {
		Itb_Consume_Log_Bo_DAL Itb_Consume_Log_Bo_DAL{get;set;} 
		Itb_Dict_Bo_DAL Itb_Dict_Bo_DAL{get;set;} 
		Itb_Emp_Hos_Bo_DAL Itb_Emp_Hos_Bo_DAL{get;set;} 
		Itb_Hei_Wei_Bo_DAL Itb_Hei_Wei_Bo_DAL{get;set;} 
		Itb_Hosp_Info_Bo_DAL Itb_Hosp_Info_Bo_DAL{get;set;} 
		Itb_Serv_Info_Bo_DAL Itb_Serv_Info_Bo_DAL{get;set;} 
		Itb_Serv_POF_Bo_DAL Itb_Serv_POF_Bo_DAL{get;set;} 
		Itb_Sms_Down_Bo_DAL Itb_Sms_Down_Bo_DAL{get;set;} 
		Itb_Sms_report_Bo_DAL Itb_Sms_report_Bo_DAL{get;set;} 
		Itb_Sms_Tpl_Bo_DAL Itb_Sms_Tpl_Bo_DAL{get;set;} 
		Itb_Sms_up_Bo_DAL Itb_Sms_up_Bo_DAL{get;set;} 
		Itb_Sys_Department_Bo_DAL Itb_Sys_Department_Bo_DAL{get;set;} 
		Itb_Sys_log_Bo_DAL Itb_Sys_log_Bo_DAL{get;set;} 
		Itb_Sys_LoginLog_Bo_DAL Itb_Sys_LoginLog_Bo_DAL{get;set;} 
		Itb_Sys_MenuInfo_Bo_DAL Itb_Sys_MenuInfo_Bo_DAL{get;set;} 
		Itb_Sys_Notice_Bo_DAL Itb_Sys_Notice_Bo_DAL{get;set;} 
		Itb_Sys_Permission_Bo_DAL Itb_Sys_Permission_Bo_DAL{get;set;} 
		Itb_Sys_Role_Bo_DAL Itb_Sys_Role_Bo_DAL{get;set;} 
		Itb_Sys_RolePermission_Bo_DAL Itb_Sys_RolePermission_Bo_DAL{get;set;} 
		Itb_Sys_UserInfo_Bo_DAL Itb_Sys_UserInfo_Bo_DAL{get;set;} 
		Itb_Sys_UserRole_Bo_DAL Itb_Sys_UserRole_Bo_DAL{get;set;} 
		Itb_Sys_UserVipPermission_Bo_DAL Itb_Sys_UserVipPermission_Bo_DAL{get;set;} 
		Itb_User_Account_Bo_DAL Itb_User_Account_Bo_DAL{get;set;} 
		Itb_User_Buy_Rec_Bo_DAL Itb_User_Buy_Rec_Bo_DAL{get;set;} 
		Itb_User_Dislike_Food_Bo_DAL Itb_User_Dislike_Food_Bo_DAL{get;set;} 
		Itb_User_Info_Bo_DAL Itb_User_Info_Bo_DAL{get;set;} 
		Itb_Weight_Chg_Bo_DAL Itb_Weight_Chg_Bo_DAL{get;set;} 
		Itb_Weight_Chg_Self_Bo_DAL Itb_Weight_Chg_Self_Bo_DAL{get;set;} 
    }

}