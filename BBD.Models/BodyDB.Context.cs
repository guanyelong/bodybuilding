﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------


namespace BBD.Models
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using System.Data.Entity.Core.Objects;
using System.Linq;


public partial class BXUUEntities : DbContext
{
    public BXUUEntities()
        : base("name=BXUUEntities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<tb_Dict> tb_Dicts { get; set; }

    public virtual DbSet<tb_Emp_Hos> tb_Emp_Hoss { get; set; }

    public virtual DbSet<tb_Hei_Wei> tb_Hei_Weis { get; set; }

    public virtual DbSet<tb_Hosp_Info> tb_Hosp_Infos { get; set; }

    public virtual DbSet<tb_Serv_POF> tb_Serv_POFs { get; set; }

    public virtual DbSet<tb_Sms_Down> tb_Sms_Downs { get; set; }

    public virtual DbSet<tb_Sms_report> tb_Sms_reports { get; set; }

    public virtual DbSet<tb_Sms_Tpl> tb_Sms_Tpls { get; set; }

    public virtual DbSet<tb_Sms_up> tb_Sms_ups { get; set; }

    public virtual DbSet<tb_Sys_Department> tb_Sys_Departments { get; set; }

    public virtual DbSet<tb_Sys_MenuInfo> tb_Sys_MenuInfos { get; set; }

    public virtual DbSet<tb_Sys_Permission> tb_Sys_Permissions { get; set; }

    public virtual DbSet<tb_Sys_Role> tb_Sys_Roles { get; set; }

    public virtual DbSet<tb_Sys_RolePermission> tb_Sys_RolePermissions { get; set; }

    public virtual DbSet<tb_Sys_UserInfo> tb_Sys_UserInfos { get; set; }

    public virtual DbSet<tb_Sys_UserRole> tb_Sys_UserRoles { get; set; }

    public virtual DbSet<tb_Sys_UserVipPermission> tb_Sys_UserVipPermissions { get; set; }

    public virtual DbSet<tb_User_Account> tb_User_Accounts { get; set; }

    public virtual DbSet<tb_User_Dislike_Food> tb_User_Dislike_Foods { get; set; }

    public virtual DbSet<tb_Weight_Chg_Self> tb_Weight_Chg_Selfs { get; set; }

    public virtual DbSet<tb_Sys_log> tb_Sys_logs { get; set; }

    public virtual DbSet<tb_Sys_Notice> tb_Sys_Notices { get; set; }

    public virtual DbSet<tb_Sys_LoginLog> tb_Sys_LoginLogs { get; set; }

    public virtual DbSet<tb_User_Info> tb_User_Infos { get; set; }

    public virtual DbSet<tb_Serv_Info> tb_Serv_Infos { get; set; }

    public virtual DbSet<tb_User_Buy_Rec> tb_User_Buy_Recs { get; set; }

    public virtual DbSet<tb_Consume_Log> tb_Consume_Logs { get; set; }

    public virtual DbSet<tb_Weight_Chg> tb_Weight_Chgs { get; set; }


    public virtual int Pro_Build_Recommend_Str(string uname, string umobile, string para, ObjectParameter outCode)
    {

        var unameParameter = uname != null ?
            new ObjectParameter("Uname", uname) :
            new ObjectParameter("Uname", typeof(string));


        var umobileParameter = umobile != null ?
            new ObjectParameter("Umobile", umobile) :
            new ObjectParameter("Umobile", typeof(string));


        var paraParameter = para != null ?
            new ObjectParameter("Para", para) :
            new ObjectParameter("Para", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Pro_Build_Recommend_Str", unameParameter, umobileParameter, paraParameter, outCode);
    }

}

}

