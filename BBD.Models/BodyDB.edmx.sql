
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/28/2017 14:58:39
-- Generated from EDMX file: D:\project\Bodybuilding\BBD.Models\BodyDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BXUU];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[tb_Consume_Log]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_Consume_Log];
GO
IF OBJECT_ID(N'[dbo].[tb_Dict]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_Dict];
GO
IF OBJECT_ID(N'[dbo].[tb_Emp_Hos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_Emp_Hos];
GO
IF OBJECT_ID(N'[dbo].[tb_Hei_Wei]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_Hei_Wei];
GO
IF OBJECT_ID(N'[dbo].[tb_Hosp_Info]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_Hosp_Info];
GO
IF OBJECT_ID(N'[dbo].[tb_Serv_Info]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_Serv_Info];
GO
IF OBJECT_ID(N'[dbo].[tb_Serv_POF]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_Serv_POF];
GO
IF OBJECT_ID(N'[dbo].[tb_Sms_Down]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_Sms_Down];
GO
IF OBJECT_ID(N'[dbo].[tb_Sms_report]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_Sms_report];
GO
IF OBJECT_ID(N'[dbo].[tb_Sms_Tpl]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_Sms_Tpl];
GO
IF OBJECT_ID(N'[dbo].[tb_Sms_up]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_Sms_up];
GO
IF OBJECT_ID(N'[dbo].[tb_Sys_Department]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_Sys_Department];
GO
IF OBJECT_ID(N'[dbo].[tb_Sys_MenuInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_Sys_MenuInfo];
GO
IF OBJECT_ID(N'[dbo].[tb_Sys_Permission]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_Sys_Permission];
GO
IF OBJECT_ID(N'[dbo].[tb_Sys_Role]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_Sys_Role];
GO
IF OBJECT_ID(N'[dbo].[tb_Sys_RolePermission]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_Sys_RolePermission];
GO
IF OBJECT_ID(N'[dbo].[tb_Sys_UserInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_Sys_UserInfo];
GO
IF OBJECT_ID(N'[dbo].[tb_Sys_UserRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_Sys_UserRole];
GO
IF OBJECT_ID(N'[dbo].[tb_Sys_UserVipPermission]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_Sys_UserVipPermission];
GO
IF OBJECT_ID(N'[dbo].[tb_User_Account]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_User_Account];
GO
IF OBJECT_ID(N'[dbo].[tb_User_Buy_Rec]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_User_Buy_Rec];
GO
IF OBJECT_ID(N'[dbo].[tb_User_Dislike_Food]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_User_Dislike_Food];
GO
IF OBJECT_ID(N'[dbo].[tb_User_Info]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_User_Info];
GO
IF OBJECT_ID(N'[dbo].[tb_Weight_Chg]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_Weight_Chg];
GO
IF OBJECT_ID(N'[dbo].[tb_Weight_Chg_Self]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_Weight_Chg_Self];
GO
IF OBJECT_ID(N'[BXUUModelStoreContainer].[tb_Sys_log]', 'U') IS NOT NULL
    DROP TABLE [BXUUModelStoreContainer].[tb_Sys_log];
GO
IF OBJECT_ID(N'[BXUUModelStoreContainer].[tb_Sys_LoginLog]', 'U') IS NOT NULL
    DROP TABLE [BXUUModelStoreContainer].[tb_Sys_LoginLog];
GO
IF OBJECT_ID(N'[BXUUModelStoreContainer].[tb_Sys_Notice]', 'U') IS NOT NULL
    DROP TABLE [BXUUModelStoreContainer].[tb_Sys_Notice];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'tb_Consume_Logs'
CREATE TABLE [dbo].[tb_Consume_Logs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [uId] int  NOT NULL,
    [rec_date] datetime  NULL,
    [num] int  NULL,
    [flag] int  NULL,
    [creatorid] int  NULL,
    [creator] varchar(50)  NULL,
    [c_time] datetime  NULL
);
GO

-- Creating table 'tb_Dicts'
CREATE TABLE [dbo].[tb_Dicts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [KeyName] varchar(50)  NOT NULL,
    [KeyWords] varchar(50)  NOT NULL,
    [KeyValue] varchar(10)  NOT NULL,
    [Seq] float  NULL,
    [C_Time] datetime  NULL,
    [state] int  NULL,
    [mark] varchar(50)  NULL
);
GO

-- Creating table 'tb_Emp_Hoss'
CREATE TABLE [dbo].[tb_Emp_Hoss] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [emp_id] int  NULL,
    [hospid] int  NULL,
    [creatorid] int  NULL,
    [creator] varchar(50)  NULL,
    [c_time] datetime  NULL
);
GO

-- Creating table 'tb_Hei_Weis'
CREATE TABLE [dbo].[tb_Hei_Weis] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Female] varchar(20)  NULL,
    [Height] float  NULL,
    [St_weight] float  NULL,
    [Pt_weight] float  NULL,
    [WC] float  NULL,
    [Hipline] float  NULL,
    [Thigh_Cir] float  NULL,
    [C_time] datetime  NULL
);
GO

-- Creating table 'tb_Hosp_Infos'
CREATE TABLE [dbo].[tb_Hosp_Infos] (
    [HospId] int IDENTITY(1,1) NOT NULL,
    [Hname] varchar(100)  NULL,
    [address] varchar(200)  NULL,
    [MapUrl] varchar(200)  NULL,
    [tel] varchar(50)  NULL,
    [sType] int  NULL,
    [traffic] varchar(500)  NULL,
    [grade] int  NULL,
    [state] int  NULL,
    [CityId] varchar(10)  NULL,
    [C_Time] datetime  NULL,
    [CreatorId] int  NULL,
    [ModLastTime] datetime  NULL,
    [intro] nvarchar(max)  NULL,
    [ModfyId] int  NULL,
    [IsDel] int  NULL,
    [PyShort] varchar(20)  NULL,
    [PyLong] varchar(100)  NULL
);
GO

-- Creating table 'tb_Serv_Infos'
CREATE TABLE [dbo].[tb_Serv_Infos] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ServId] int  NOT NULL,
    [ServName] varchar(200)  NULL,
    [ServMemo] varchar(1000)  NULL,
    [ServType] varchar(10)  NULL,
    [state] int  NULL,
    [price] decimal(19,4)  NULL,
    [CTime] datetime  NULL,
    [CreatorId] int  NULL,
    [Creator] varchar(50)  NULL,
    [IsDel] int  NULL,
    [LastModTime] datetime  NULL,
    [ModifyId] int  NULL
);
GO

-- Creating table 'tb_Serv_POFs'
CREATE TABLE [dbo].[tb_Serv_POFs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ServId] int  NULL,
    [TouchFlag] int  NULL,
    [Times] int  NULL,
    [CreatorId] int  NULL,
    [Creator] varchar(50)  NULL,
    [C_time] datetime  NULL
);
GO

-- Creating table 'tb_Sms_Downs'
CREATE TABLE [dbo].[tb_Sms_Downs] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [mobile] varchar(50)  NOT NULL,
    [msg] varchar(500)  NOT NULL,
    [creatorId] int  NULL,
    [creator] varchar(50)  NULL,
    [c_time] datetime  NULL,
    [sendFlag] int  NULL,
    [errMsg] nvarchar(100)  NULL,
    [sendTime] datetime  NULL,
    [stype] int  NULL,
    [pipeId] int  NULL,
    [levelNum] int  NULL,
    [exId] varchar(50)  NULL,
    [sysPlatform] int  NULL,
    [M_NO] nvarchar(50)  NULL
);
GO

-- Creating table 'tb_Sms_reports'
CREATE TABLE [dbo].[tb_Sms_reports] (
    [id] bigint IDENTITY(1,1) NOT NULL,
    [mobile] varchar(20)  NULL,
    [C_time] datetime  NULL,
    [R_time] datetime  NULL,
    [pipeId] int  NULL,
    [errMsg] nvarchar(50)  NULL,
    [M_no] nvarchar(50)  NULL,
    [S_time] datetime  NULL
);
GO

-- Creating table 'tb_Sms_Tpls'
CREATE TABLE [dbo].[tb_Sms_Tpls] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [sms] varchar(500)  NULL,
    [state] int  NULL,
    [creatorid] int  NULL,
    [creator] varchar(50)  NULL,
    [c_time] datetime  NULL
);
GO

-- Creating table 'tb_Sms_ups'
CREATE TABLE [dbo].[tb_Sms_ups] (
    [id] bigint IDENTITY(1,1) NOT NULL,
    [mobile] varchar(20)  NULL,
    [msg] nvarchar(500)  NULL,
    [C_time] datetime  NULL,
    [S_time] datetime  NULL,
    [PipeId] int  NULL,
    [CreatorId] int  NULL,
    [M_no] nvarchar(50)  NULL
);
GO

-- Creating table 'tb_Sys_Departments'
CREATE TABLE [dbo].[tb_Sys_Departments] (
    [DepId] int IDENTITY(1,1) NOT NULL,
    [DepPId] int  NULL,
    [DepName] varchar(50)  NOT NULL,
    [Depremark] varchar(100)  NULL,
    [DepIsDel] int  NULL,
    [DepAddtime] datetime  NULL,
    [CityId] varchar(10)  NOT NULL,
    [DepNum] varchar(50)  NULL
);
GO

-- Creating table 'tb_Sys_MenuInfos'
CREATE TABLE [dbo].[tb_Sys_MenuInfos] (
    [mId] int IDENTITY(1,1) NOT NULL,
    [mNum] varchar(50)  NULL,
    [mPId] int  NULL,
    [mText] varchar(200)  NULL,
    [mUrl] varchar(200)  NULL,
    [mIcon] varchar(200)  NULL,
    [mAddtime] datetime  NULL,
    [mState] varchar(20)  NULL,
    [mChecked] bit  NULL,
    [mSeq] float  NULL,
    [mPerNum] varchar(50)  NULL,
    [mOrderindex] float  NULL
);
GO

-- Creating table 'tb_Sys_Permissions'
CREATE TABLE [dbo].[tb_Sys_Permissions] (
    [pId] int IDENTITY(1,1) NOT NULL,
    [pParentId] int  NOT NULL,
    [pName] varchar(50)  NOT NULL,
    [pAreaName] varchar(50)  NULL,
    [pControllerName] varchar(150)  NULL,
    [pActionMenu] int  NULL,
    [pActionNum] varchar(50)  NULL,
    [PActionName] varchar(50)  NULL,
    [pFormMethod] varchar(50)  NULL,
    [pOperationType] int  NULL,
    [pIco] varchar(100)  NULL,
    [pOrder] int  NULL,
    [pIsLink] int  NULL,
    [pLinkUrl] varchar(200)  NULL,
    [pIsShow] int  NULL,
    [pRemark] varchar(200)  NULL,
    [pIsDel] int  NULL,
    [pState] int  NULL,
    [pOperTime] datetime  NULL
);
GO

-- Creating table 'tb_Sys_Roles'
CREATE TABLE [dbo].[tb_Sys_Roles] (
    [rId] int IDENTITY(1,1) NOT NULL,
    [rName] varchar(50)  NOT NULL,
    [rRemark] varchar(100)  NULL,
    [rIsShow] int  NULL,
    [rIsDel] int  NULL,
    [rDepId] int  NULL,
    [rAddTime] datetime  NULL,
    [rNum] varchar(50)  NULL
);
GO

-- Creating table 'tb_Sys_RolePermissions'
CREATE TABLE [dbo].[tb_Sys_RolePermissions] (
    [rpId] int IDENTITY(1,1) NOT NULL,
    [rpRId] int  NOT NULL,
    [rpPId] int  NOT NULL,
    [rpIsDel] int  NULL,
    [rpAddtime] datetime  NULL
);
GO

-- Creating table 'tb_Sys_UserInfos'
CREATE TABLE [dbo].[tb_Sys_UserInfos] (
    [Uid] int IDENTITY(1,1) NOT NULL,
    [uDepid] int  NULL,
    [uLoginName] varchar(50)  NOT NULL,
    [uPwd] varchar(50)  NOT NULL,
    [uGender] varchar(10)  NULL,
    [uIsDel] int  NULL,
    [uPost] varchar(100)  NULL,
    [uRemark] varchar(100)  NULL,
    [uAddtime] datetime  NULL,
    [uMobile] varchar(20)  NULL,
    [CityId] varchar(10)  NULL,
    [uName] varchar(50)  NULL
);
GO

-- Creating table 'tb_Sys_UserRoles'
CREATE TABLE [dbo].[tb_Sys_UserRoles] (
    [urId] int IDENTITY(1,1) NOT NULL,
    [urUid] int  NOT NULL,
    [urRid] int  NOT NULL,
    [urIsDel] int  NULL,
    [urAddTime] datetime  NULL
);
GO

-- Creating table 'tb_Sys_UserVipPermissions'
CREATE TABLE [dbo].[tb_Sys_UserVipPermissions] (
    [vipId] int IDENTITY(1,1) NOT NULL,
    [vipUserId] int  NOT NULL,
    [VipPermission] int  NOT NULL,
    [vipRemark] int  NULL,
    [vipIsdel] int  NULL,
    [vipAddtime] datetime  NULL
);
GO

-- Creating table 'tb_User_Accounts'
CREATE TABLE [dbo].[tb_User_Accounts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [uId] int  NOT NULL,
    [TouchFlag] int  NULL,
    [total] int  NULL,
    [delay] int  NULL,
    [creatorId] int  NULL,
    [creator] varchar(50)  NULL,
    [c_time] datetime  NULL
);
GO

-- Creating table 'tb_User_Buy_Recs'
CREATE TABLE [dbo].[tb_User_Buy_Recs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [uId] int  NULL,
    [RecDate] datetime  NULL,
    [Buy_Hospid] int  NULL,
    [BuyNum] int  NULL,
    [PayMoney] decimal(19,4)  NULL,
    [ProductId] int  NULL,
    [ProductPrice] decimal(19,4)  NULL,
    [CreatorId] int  NULL,
    [Creator] varchar(50)  NULL,
    [C_time] datetime  NULL
);
GO

-- Creating table 'tb_User_Dislike_Foods'
CREATE TABLE [dbo].[tb_User_Dislike_Foods] (
    [id] int IDENTITY(1,1) NOT NULL,
    [uId] int  NOT NULL,
    [FoodType] varchar(10)  NULL,
    [FoodName] varchar(500)  NULL,
    [CreatorId] int  NULL,
    [Creator] varchar(50)  NULL,
    [C_time] datetime  NULL
);
GO

-- Creating table 'tb_User_Infos'
CREATE TABLE [dbo].[tb_User_Infos] (
    [uId] int IDENTITY(1,1) NOT NULL,
    [HospId] int  NULL,
    [Name] varchar(50)  NOT NULL,
    [Female] varchar(4)  NOT NULL,
    [Birth] datetime  NULL,
    [Mobile] varchar(20)  NOT NULL,
    [Email] varchar(50)  NULL,
    [ComeFrom] varchar(10)  NULL,
    [IndustryType] varchar(10)  NULL,
    [Height] float  NULL,
    [Weight] float  NULL,
    [WC] float  NULL,
    [Hipline] float  NULL,
    [Thigh_Cir] float  NULL,
    [Qq] varchar(20)  NULL,
    [WeiXin] varchar(50)  NULL,
    [BMI] float  NULL,
    [Enter_Code] varchar(20)  NULL,
    [Self_code] varchar(20)  NULL,
    [Cityid] varchar(10)  NULL,
    [PicPath] varchar(200)  NULL,
    [Myhope] varchar(200)  NULL,
    [CreatorId] int  NULL,
    [Creator] varchar(50)  NULL,
    [C_time] datetime  NULL
);
GO

-- Creating table 'tb_Weight_Chgs'
CREATE TABLE [dbo].[tb_Weight_Chgs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [uId] int  NOT NULL,
    [RecDate] datetime  NULL,
    [Hid] int  NULL,
    [Weight] float  NULL,
    [BatchId] int  NULL,
    [TouchFlag] int  NULL,
    [PicPath] varchar(200)  NULL,
    [CreatorId] int  NULL,
    [Creator] varchar(50)  NULL,
    [C_time] datetime  NULL
);
GO

-- Creating table 'tb_Weight_Chg_Selfs'
CREATE TABLE [dbo].[tb_Weight_Chg_Selfs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [uId] int  NOT NULL,
    [RecDate] datetime  NULL,
    [Hid] int  NULL,
    [Weight] float  NULL,
    [CreatorId] int  NULL,
    [Creator] varchar(50)  NULL,
    [C_time] datetime  NULL
);
GO

-- Creating table 'tb_Sys_logs'
CREATE TABLE [dbo].[tb_Sys_logs] (
    [id] int  NOT NULL,
    [Date] datetime  NULL,
    [Thread] varchar(100)  NULL,
    [Level] varchar(100)  NULL,
    [Logger] varchar(100)  NULL,
    [Message] varchar(100)  NULL,
    [Exception] varchar(100)  NULL
);
GO

-- Creating table 'tb_Sys_LoginLogs'
CREATE TABLE [dbo].[tb_Sys_LoginLogs] (
    [id] int  NOT NULL,
    [loginName] varchar(50)  NULL,
    [loginCity] varchar(50)  NULL,
    [loginTime] datetime  NULL,
    [LoginIp] varchar(50)  NULL
);
GO

-- Creating table 'tb_Sys_Notices'
CREATE TABLE [dbo].[tb_Sys_Notices] (
    [Id] int  NOT NULL,
    [Title] nvarchar(50)  NOT NULL,
    [Contents] nvarchar(max)  NULL,
    [Hot] int  NULL,
    [Seq] float  NULL,
    [ClickNum] int  NULL,
    [IsDel] int  NULL,
    [Creator] nvarchar(50)  NULL,
    [C_Time] datetime  NULL,
    [ShowImgUrl] nvarchar(300)  NULL,
    [FilePath] nvarchar(450)  NULL,
    [FileName] nvarchar(50)  NULL,
    [IsPush] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'tb_Consume_Logs'
ALTER TABLE [dbo].[tb_Consume_Logs]
ADD CONSTRAINT [PK_tb_Consume_Logs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'tb_Dicts'
ALTER TABLE [dbo].[tb_Dicts]
ADD CONSTRAINT [PK_tb_Dicts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'tb_Emp_Hoss'
ALTER TABLE [dbo].[tb_Emp_Hoss]
ADD CONSTRAINT [PK_tb_Emp_Hoss]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'tb_Hei_Weis'
ALTER TABLE [dbo].[tb_Hei_Weis]
ADD CONSTRAINT [PK_tb_Hei_Weis]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [HospId] in table 'tb_Hosp_Infos'
ALTER TABLE [dbo].[tb_Hosp_Infos]
ADD CONSTRAINT [PK_tb_Hosp_Infos]
    PRIMARY KEY CLUSTERED ([HospId] ASC);
GO

-- Creating primary key on [ID] in table 'tb_Serv_Infos'
ALTER TABLE [dbo].[tb_Serv_Infos]
ADD CONSTRAINT [PK_tb_Serv_Infos]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'tb_Serv_POFs'
ALTER TABLE [dbo].[tb_Serv_POFs]
ADD CONSTRAINT [PK_tb_Serv_POFs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'tb_Sms_Downs'
ALTER TABLE [dbo].[tb_Sms_Downs]
ADD CONSTRAINT [PK_tb_Sms_Downs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [id] in table 'tb_Sms_reports'
ALTER TABLE [dbo].[tb_Sms_reports]
ADD CONSTRAINT [PK_tb_Sms_reports]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [Id] in table 'tb_Sms_Tpls'
ALTER TABLE [dbo].[tb_Sms_Tpls]
ADD CONSTRAINT [PK_tb_Sms_Tpls]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [id] in table 'tb_Sms_ups'
ALTER TABLE [dbo].[tb_Sms_ups]
ADD CONSTRAINT [PK_tb_Sms_ups]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [DepId] in table 'tb_Sys_Departments'
ALTER TABLE [dbo].[tb_Sys_Departments]
ADD CONSTRAINT [PK_tb_Sys_Departments]
    PRIMARY KEY CLUSTERED ([DepId] ASC);
GO

-- Creating primary key on [mId] in table 'tb_Sys_MenuInfos'
ALTER TABLE [dbo].[tb_Sys_MenuInfos]
ADD CONSTRAINT [PK_tb_Sys_MenuInfos]
    PRIMARY KEY CLUSTERED ([mId] ASC);
GO

-- Creating primary key on [pId] in table 'tb_Sys_Permissions'
ALTER TABLE [dbo].[tb_Sys_Permissions]
ADD CONSTRAINT [PK_tb_Sys_Permissions]
    PRIMARY KEY CLUSTERED ([pId] ASC);
GO

-- Creating primary key on [rId] in table 'tb_Sys_Roles'
ALTER TABLE [dbo].[tb_Sys_Roles]
ADD CONSTRAINT [PK_tb_Sys_Roles]
    PRIMARY KEY CLUSTERED ([rId] ASC);
GO

-- Creating primary key on [rpId] in table 'tb_Sys_RolePermissions'
ALTER TABLE [dbo].[tb_Sys_RolePermissions]
ADD CONSTRAINT [PK_tb_Sys_RolePermissions]
    PRIMARY KEY CLUSTERED ([rpId] ASC);
GO

-- Creating primary key on [Uid] in table 'tb_Sys_UserInfos'
ALTER TABLE [dbo].[tb_Sys_UserInfos]
ADD CONSTRAINT [PK_tb_Sys_UserInfos]
    PRIMARY KEY CLUSTERED ([Uid] ASC);
GO

-- Creating primary key on [urId] in table 'tb_Sys_UserRoles'
ALTER TABLE [dbo].[tb_Sys_UserRoles]
ADD CONSTRAINT [PK_tb_Sys_UserRoles]
    PRIMARY KEY CLUSTERED ([urId] ASC);
GO

-- Creating primary key on [vipId] in table 'tb_Sys_UserVipPermissions'
ALTER TABLE [dbo].[tb_Sys_UserVipPermissions]
ADD CONSTRAINT [PK_tb_Sys_UserVipPermissions]
    PRIMARY KEY CLUSTERED ([vipId] ASC);
GO

-- Creating primary key on [Id] in table 'tb_User_Accounts'
ALTER TABLE [dbo].[tb_User_Accounts]
ADD CONSTRAINT [PK_tb_User_Accounts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'tb_User_Buy_Recs'
ALTER TABLE [dbo].[tb_User_Buy_Recs]
ADD CONSTRAINT [PK_tb_User_Buy_Recs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [id] in table 'tb_User_Dislike_Foods'
ALTER TABLE [dbo].[tb_User_Dislike_Foods]
ADD CONSTRAINT [PK_tb_User_Dislike_Foods]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [uId] in table 'tb_User_Infos'
ALTER TABLE [dbo].[tb_User_Infos]
ADD CONSTRAINT [PK_tb_User_Infos]
    PRIMARY KEY CLUSTERED ([uId] ASC);
GO

-- Creating primary key on [Id] in table 'tb_Weight_Chgs'
ALTER TABLE [dbo].[tb_Weight_Chgs]
ADD CONSTRAINT [PK_tb_Weight_Chgs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'tb_Weight_Chg_Selfs'
ALTER TABLE [dbo].[tb_Weight_Chg_Selfs]
ADD CONSTRAINT [PK_tb_Weight_Chg_Selfs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [id] in table 'tb_Sys_logs'
ALTER TABLE [dbo].[tb_Sys_logs]
ADD CONSTRAINT [PK_tb_Sys_logs]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'tb_Sys_LoginLogs'
ALTER TABLE [dbo].[tb_Sys_LoginLogs]
ADD CONSTRAINT [PK_tb_Sys_LoginLogs]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [Id], [Title] in table 'tb_Sys_Notices'
ALTER TABLE [dbo].[tb_Sys_Notices]
ADD CONSTRAINT [PK_tb_Sys_Notices]
    PRIMARY KEY CLUSTERED ([Id], [Title] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------