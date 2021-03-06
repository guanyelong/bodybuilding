USE [master]
GO
/****** Object:  Database [BXUU]    Script Date: 2017/4/7 17:32:14 ******/
CREATE DATABASE [BXUU]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BXUU', FILENAME = N'D:\SQL Server2012\MSSQL11.MSSQLSERVER\MSSQL\DATA\BXUU.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'BXUU_log', FILENAME = N'D:\SQL Server2012\MSSQL11.MSSQLSERVER\MSSQL\DATA\BXUU_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [BXUU] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BXUU].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BXUU] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BXUU] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BXUU] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BXUU] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BXUU] SET ARITHABORT OFF 
GO
ALTER DATABASE [BXUU] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BXUU] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [BXUU] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BXUU] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BXUU] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BXUU] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BXUU] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BXUU] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BXUU] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BXUU] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BXUU] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BXUU] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BXUU] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BXUU] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BXUU] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BXUU] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BXUU] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BXUU] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BXUU] SET RECOVERY FULL 
GO
ALTER DATABASE [BXUU] SET  MULTI_USER 
GO
ALTER DATABASE [BXUU] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BXUU] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BXUU] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BXUU] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [BXUU]
GO
/****** Object:  StoredProcedure [dbo].[Pro_Build_Recommend_Str]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<tang>
-- Create date: <2017-03>
-- Description:	<生成客户专属推荐码>
-- =============================================
CREATE PROCEDURE [dbo].[Pro_Build_Recommend_Str] 
	@Uname varchar(20),
	@Umobile varchar(20),
	@Para  varchar(100),
	@OutCode varchar(20) output
AS
BEGIN
	
	SELECT @OutCode='';
END








GO
/****** Object:  StoredProcedure [dbo].[Pro_Select_BuyRec]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Pro_Select_BuyRec]
	-- Add the parameters for the stored procedure here
	@uId int
AS
BEGIN
	SELECT R.[Id]
      ,R.[uId]
	  ,U.Name uName
      ,R.[RecDate]
      ,[Buy_Hospid]
	  ,H.Hname
      ,[BuyNum]
      ,[PayMoney]
      ,[ProductId]
	  ,(SELECT Times FROM [dbo].[tb_Serv_POF] WHERE  ServId=(SELECT ServId FROM [dbo].[tb_Serv_Info] WHERE ID=R.ProductId) AND TouchFlag=0) TouchTimes
	  ,(SELECT Times FROM [dbo].[tb_Serv_POF] WHERE  ServId=(SELECT ServId FROM [dbo].[tb_Serv_Info] WHERE ID=R.ProductId) AND TouchFlag=1) InstrumentTimes
	  ,S.ServName ProductName
      ,[ProductPrice]
      ,R.[CreatorId]
      ,R.[Creator]
      ,R.[C_time] FROM [dbo].[tb_User_Buy_Rec] R
	LEFT JOIN [dbo].[tb_Hosp_Info] H ON R.Buy_Hospid=H.HospId
	LEFT JOIN [dbo].[tb_Serv_Info] S ON R.ProductId=S.ID
	LEFT JOIN [dbo].[tb_User_Info] U ON R.[uId]=U.[uId]
	where (@uid=0 or r.[uId]=@uId)
END



GO
/****** Object:  StoredProcedure [dbo].[Pro_Select_Customer]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Pro_Select_Customer]
	-- Add the parameters for the stored procedure here
	@Name varchar(50),
	@Mobile varchar(20),
	@Female varchar(4),
	@ComeFrom varchar(10),
	@HospId int,
	@uid int
AS
BEGIN
	SELECT [uId]
      ,[HospId]
	  ,(SELECT Hname FROM [dbo].[tb_Hosp_Info] WHERE [HospId]=U.HospId) HospName
      ,[Name]
      ,[Female]
      ,[Birth]
      ,[Mobile]
      ,[Email]
      ,[ComeFrom]
	  ,(SELECT KeyWords FROM [dbo].[tb_Dict] WHERE KeyValue=U.[IndustryType] AND KeyName='comfrom') ComeFromName
      ,[IndustryType]
	  ,(SELECT KeyWords FROM [dbo].[tb_Dict] WHERE KeyValue=U.[IndustryType] AND KeyName='IndustryType') IndustryTypeName
      ,[Height]
      ,[Weight]
      ,[WC]
      ,[Hipline]
      ,[Thigh_Cir]
      ,[Qq]
      ,[WeiXin]
      ,[BMI]
      ,[Enter_Code]
      ,[Self_code]
      ,[Cityid]
	  ,(SELECT KeyWords FROM [dbo].[tb_Dict] WHERE KeyValue=U.Cityid AND KeyName='city') CityName
      ,[PicPath]
      ,[Myhope]
      ,[CreatorId]
      ,[Creator]
      ,[C_time]
	  ,[isDel] FROM [dbo].[tb_User_Info] U WHERE (@Name='' OR U.Name LIKE '%'+@Name+'%')
	  AND (@Mobile='' OR U.Mobile LIKE '%'+@Mobile+'%') AND (@Female='' OR U.Female=@Female)
	  AND (@ComeFrom='' OR U.ComeFrom=@ComeFrom) AND (@HospId=0 OR U.HospId=@HospId) AND (@uid=0 OR U.[uId]=@uid)
END






GO
/****** Object:  StoredProcedure [dbo].[Pro_Select_EmpHos]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Pro_Select_EmpHos]
	-- Add the parameters for the stored procedure here
	 @HospId int,
	 @Name varchar(50)
AS
BEGIN
	SELECT [Uid]
      ,[uDepid]
	  ,D.DepName uDepName
      ,[uLoginName]
      ,[uGender]
      ,[uIsDel]
      ,[uPost]
      ,[uAddtime]
      ,[uMobile]
      ,U.[CityId] 
	  ,T.KeyWords CityName
      ,[uName] FROM [dbo].[tb_Sys_UserInfo] U 
	  LEFT  JOIN [dbo].[tb_Sys_Department] D ON U.uDepid=D.DepId
	  LEFT  JOIN [dbo].[tb_Dict] T ON U.CityId=T.KeyValue 
	  LEFT  JOIN [dbo].[tb_Emp_Hos] E ON E.emp_id=U.[Uid]
	  WHERE T.KeyName='city' and (@HospId=0 or E.hospid=@HospId) AND (@Name='' OR U.uName LIKE '%'+@Name+'%')
END




GO
/****** Object:  StoredProcedure [dbo].[Pro_Select_Food]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Pro_Select_Food]
	-- Add the parameters for the stored procedure here
	@uid int
AS
BEGIN
	SELECT F.[id]
      ,F.[uId]
      ,F.[FoodType]
	  ,D.[KeyWords] FoodTypeName
      ,F.[FoodName]
      ,F.[CreatorId]
      ,F.[Creator]
      ,F.[C_time] FROM [dbo].[tb_User_Dislike_Food] F
	LEFT JOIN [dbo].[tb_Dict] D ON F.FoodType=D.KeyValue
	WHERE D.KeyName='FoodType' AND (@uid=0 OR f.[uId]=@uid)
END


GO
/****** Object:  StoredProcedure [dbo].[Pro_Select_HospInfo]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Pro_Select_HospInfo]
	-- Add the parameters for the stored procedure here
	@Hname varchar(100),
	@tel varchar(50),
	@HospIds varchar(200)
AS
BEGIN
	SELECT [HospId]
      ,[Hname]
      ,[address]
      ,[MapUrl]
      ,[tel]
	  ,[sType]
      ,(SELECT KeyWords FROM [dbo].[tb_Dict] WHERE KeyValue=HI.[sType] AND KeyName='Hosptype') sTypeName
      ,[traffic]
      ,[grade]
      ,[state]
      ,[CityId]
	  ,(SELECT KeyWords FROM [dbo].[tb_Dict] WHERE KeyValue=HI.[CityId] AND KeyName='city') CityName
      ,[C_Time]
      ,[CreatorId]
      ,[ModLastTime]
      ,[intro]
      ,[ModfyId]
      ,[IsDel]
      ,[PyShort]
      ,[PyLong] FROM [dbo].[tb_Hosp_Info] HI 
	  where (@Hname='' or (([Hname] like '%'+@Hname+'%') or (PyShort like '%'+@Hname+'%') or ([PyLong] like '%'+@Hname+'%'))) 
	  and (@tel='' or ([tel] like '%'+@tel+'%'))
	  AND (@HospIds='-1' OR (CAST(@HospIds as varchar(50)) like ('%,'+CAST(HI.HospId as varchar(50))+',%')))
END







GO
/****** Object:  StoredProcedure [dbo].[Pro_Select_ServInfo]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Pro_Select_ServInfo]
	@ServName varchar(200),
	@state int,
	@IsDel int,
	@HospIds varchar(200)
AS
BEGIN
	SELECT SI.[ID]
      ,SI.[ServId]
      ,SI.[ServName]
      ,SI.[ServMemo]
      ,SI.[ServType]
	  ,D.KeyWords ServTypeName
      ,SI.[state]
      ,SI.[price]
      ,SI.[CTime]
      ,SI.[CreatorId]
      ,SI.[Creator]
      ,SI.[IsDel]
      ,SI.[LastModTime]
      ,SI.[ModifyId]
	  ,SI.[HospId]
	  ,H.Hname HospName
	  ,(SELECT Times FROM [dbo].[tb_Serv_POF] WHERE  ServId=SI.ServId AND TouchFlag=0) TouchTimes
	  ,(SELECT Times FROM [dbo].[tb_Serv_POF] WHERE  ServId=SI.ServId AND TouchFlag=1) InstrumentTimes
	   FROM [dbo].[tb_Serv_Info] SI
	  LEFT JOIN [dbo].[tb_Dict] D ON SI.ServType=D.KeyValue
	  LEFT JOIN [dbo].[tb_Hosp_Info] H ON SI.HospId=H.HospId
	  WHERE D.KeyName='ServType' and (@ServName='' OR (SI.ServName LIKE '%'+@ServName+'%')) and (@state=-1 or SI.[state]=@state) AND (@IsDel=-1 OR SI.IsDel=@IsDel)
	  AND (@HospIds='-1' OR (CAST(@HospIds as varchar(50)) like ('%,'+CAST(SI.HospId as varchar(50))+',%')))
END



GO
/****** Object:  StoredProcedure [dbo].[Pro_Select_ServPOF]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Pro_Select_ServPOF] 
	-- Add the parameters for the stored procedure here
	@ServName varchar(50),
	@HospIds varchar(200)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SELECT P.[Id]
      ,P.[ServId]
      ,P.[TouchFlag]
      ,P.[Times]
      ,P.[CreatorId]
      ,P.[Creator]
      ,P.[C_time] 
	  ,S.price
	  ,S.ServName
	  ,H.Hname
	  FROM [dbo].[tb_Serv_POF] P
	  LEFT JOIN [dbo].[tb_Serv_Info] S ON P.ServId=S.ServId
	  LEFT JOIN [dbo].[tb_Hosp_Info] H ON S.HospId=H.HospId
	  WHERE S.IsDel=0 AND S.[state]=0 AND
	  (@ServName='' OR S.ServName LIKE '%'+@ServName+'%') AND (@HospIds='-1' OR (CAST(@HospIds as varchar(50)) like ('%,'+CAST(S.HospId as varchar(50))+',%')))
END

GO
/****** Object:  StoredProcedure [dbo].[Pro_Select_SysUserInfo]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Pro_Select_SysUserInfo]
	-- Add the parameters for the stored procedure here
	@Name varchar(50)
AS
BEGIN
	SELECT U.[Uid]
      ,U.[uDepid]
	  ,M.DepName uDepName
      ,U.[uLoginName]
      ,U.[uPost]
      ,U.[uMobile]
      ,U.[CityId]
	  ,D.KeyWords CityName
      ,U.[uName] FROM [dbo].[tb_Sys_UserInfo] U
	  LEFT JOIN [dbo].[tb_Dict] D ON U.CityId =D.KeyValue
	  LEFT JOIN [dbo].[tb_Sys_Department] M ON U.uDepid =M.DepId
	  WHERE D.KeyName='city' AND U.uIsDel=0 AND (@Name='' OR U.uName LIKE '%'+@Name+'%') 
END

GO
/****** Object:  StoredProcedure [dbo].[Pro_Select_Weight]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Pro_Select_Weight]
	-- Add the parameters for the stored procedure here
	@uid int,
	@uName varchar(50),
	@Mobile varchar(20)
AS
BEGIN
	SELECT 
	 WC.[Id]
      ,WC.[uId]
      ,WC.[RecDate]
      ,WC.[Hid]
	  ,H.Hname
      ,WC.[Weight]
      ,WC.[BatchId]
      ,WC.[TouchFlag]
      ,WC.[PicPath]
      ,WC.[CreatorId]
      ,WC.[Creator]
      ,WC.[C_time]
	  ,U.Name uName FROM [dbo].[tb_Weight_Chg] WC
	LEFT JOIN [dbo].[tb_User_Info] U ON WC.[uId]=U.[uId]
	LEFT JOIN [dbo].[tb_Hosp_Info] H ON WC.Hid=H.HospId
	WHERE (@uid=0 OR WC.[uId]=@uid) AND (@uName='' OR U.Name=@uName) AND (@Mobile='' OR U.Mobile=@Mobile)
END


GO
/****** Object:  StoredProcedure [dbo].[Pro_Select_WeightSelf]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Pro_Select_WeightSelf]
	-- Add the parameters for the stored procedure here
	@uid int,
	@uName varchar(50),
	@Mobile varchar(20)
AS
BEGIN
	SELECT 
	 WCS.[Id]
      ,WCS.[uId]
      ,WCS.[RecDate]
      ,WCS.[Hid]
      ,WCS.[Weight]
      ,WCS.[CreatorId]
      ,WCS.[Creator]
      ,WCS.[C_time]
	  ,U.Name uName FROM [dbo].[tb_Weight_Chg_Self] WCS
	LEFT JOIN [dbo].[tb_User_Info] U ON WCS.[uId]=U.[uId]
	WHERE (@uid=0 OR WCS.[uId]=@uid) AND (@uName='' OR U.Name=@uName) AND (@Mobile='' OR U.Mobile=@Mobile)
END


GO
/****** Object:  Table [dbo].[tb_Consume_Log]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Consume_Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[uId] [int] NOT NULL,
	[rec_date] [date] NULL,
	[num] [int] NULL,
	[flag] [int] NULL,
	[creatorid] [int] NULL,
	[creator] [varchar](50) NULL,
	[c_time] [datetime] NULL,
 CONSTRAINT [PK_tb_Consume_Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_Dict]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Dict](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[KeyName] [varchar](50) NOT NULL,
	[KeyWords] [varchar](50) NOT NULL,
	[KeyValue] [varchar](10) NOT NULL,
	[Seq] [float] NULL,
	[C_Time] [datetime] NULL,
	[state] [int] NULL,
	[mark] [varchar](50) NULL,
 CONSTRAINT [PK_tb_Dict] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_Emp_Hos]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Emp_Hos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[emp_id] [int] NULL,
	[hospid] [int] NULL,
	[creatorid] [int] NULL,
	[creator] [varchar](50) NULL,
	[c_time] [datetime] NULL,
 CONSTRAINT [PK_T_emp_hos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_Hei_Wei]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Hei_Wei](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Female] [varchar](20) NULL,
	[Height] [float] NULL,
	[St_weight] [float] NULL,
	[Pt_weight] [float] NULL,
	[WC] [float] NULL,
	[Hipline] [float] NULL,
	[Thigh_Cir] [float] NULL,
	[C_time] [datetime] NULL,
 CONSTRAINT [PK_tb_Hei_Wei] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_Hosp_Info]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Hosp_Info](
	[HospId] [int] IDENTITY(1,1) NOT NULL,
	[Hname] [varchar](100) NULL,
	[address] [varchar](200) NULL,
	[MapUrl] [varchar](200) NULL,
	[tel] [varchar](50) NULL,
	[sType] [int] NULL,
	[traffic] [varchar](500) NULL,
	[grade] [int] NULL,
	[state] [int] NULL,
	[CityId] [varchar](10) NULL,
	[C_Time] [datetime] NULL,
	[CreatorId] [int] NULL,
	[ModLastTime] [datetime] NULL,
	[intro] [ntext] NULL,
	[ModfyId] [int] NULL,
	[IsDel] [int] NULL,
	[PyShort] [varchar](20) NULL,
	[PyLong] [varchar](100) NULL,
 CONSTRAINT [PK_tb_Hosp_Info] PRIMARY KEY CLUSTERED 
(
	[HospId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_Serv_Info]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Serv_Info](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ServId] [int] NOT NULL,
	[ServName] [varchar](200) NULL,
	[ServMemo] [varchar](1000) NULL,
	[ServType] [varchar](10) NULL,
	[state] [int] NULL,
	[price] [money] NULL,
	[CTime] [datetime] NULL,
	[CreatorId] [int] NULL,
	[Creator] [varchar](50) NULL,
	[IsDel] [int] NULL,
	[LastModTime] [datetime] NULL,
	[ModifyId] [int] NULL,
	[HospId] [int] NULL,
 CONSTRAINT [PK_tb_Serv_Info] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_Serv_POF]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Serv_POF](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ServId] [int] NULL,
	[TouchFlag] [int] NULL,
	[Times] [int] NULL,
	[CreatorId] [int] NULL,
	[Creator] [varchar](50) NULL,
	[C_time] [datetime] NULL,
 CONSTRAINT [PK_T_Serv_POF] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_Sms_Down]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Sms_Down](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[mobile] [varchar](50) NOT NULL,
	[msg] [varchar](500) NOT NULL,
	[creatorId] [int] NULL,
	[creator] [varchar](50) NULL,
	[c_time] [datetime] NULL,
	[sendFlag] [int] NULL,
	[errMsg] [nvarchar](100) NULL,
	[sendTime] [datetime] NULL,
	[stype] [int] NULL,
	[pipeId] [int] NULL,
	[levelNum] [int] NULL,
	[exId] [varchar](50) NULL,
	[sysPlatform] [int] NULL,
	[M_NO] [nvarchar](50) NULL,
 CONSTRAINT [PK_tb_Sms_Down] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_Sms_report]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Sms_report](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[mobile] [varchar](20) NULL,
	[C_time] [datetime] NULL,
	[R_time] [datetime] NULL,
	[pipeId] [int] NULL,
	[errMsg] [nvarchar](50) NULL,
	[M_no] [nvarchar](50) NULL,
	[S_time] [datetime] NULL,
 CONSTRAINT [PK_tb_Sms_report] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_Sms_Tpl]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Sms_Tpl](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[sms] [varchar](500) NULL,
	[state] [int] NULL,
	[creatorid] [int] NULL,
	[creator] [varchar](50) NULL,
	[c_time] [date] NULL,
 CONSTRAINT [PK_T_smstpl] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_Sms_up]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Sms_up](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[mobile] [varchar](20) NULL,
	[msg] [nvarchar](500) NULL,
	[C_time] [datetime] NULL,
	[S_time] [datetime] NULL,
	[PipeId] [int] NULL,
	[CreatorId] [int] NULL,
	[M_no] [nvarchar](50) NULL,
 CONSTRAINT [PK_tb_Sms_up] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_Sys_Department]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Sys_Department](
	[DepId] [int] IDENTITY(1,1) NOT NULL,
	[DepPId] [int] NULL,
	[DepName] [varchar](50) NOT NULL,
	[Depremark] [varchar](100) NULL,
	[DepIsDel] [int] NULL,
	[DepAddtime] [datetime] NULL,
	[CityId] [varchar](10) NOT NULL,
	[DepNum] [varchar](50) NULL,
 CONSTRAINT [PK_tb_Sys_Department] PRIMARY KEY CLUSTERED 
(
	[DepId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_Sys_log]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Sys_log](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[Thread] [varchar](100) NULL,
	[Level] [varchar](100) NULL,
	[Logger] [varchar](100) NULL,
	[Message] [varchar](100) NULL,
	[Exception] [varchar](100) NULL,
 CONSTRAINT [PK_tb_Sys_log] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_Sys_LoginLog]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Sys_LoginLog](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[loginName] [varchar](50) NULL,
	[loginCity] [varchar](50) NULL,
	[loginTime] [datetime] NULL,
	[LoginIp] [varchar](50) NULL,
 CONSTRAINT [PK_tb_Sys_LoginLog] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_Sys_MenuInfo]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Sys_MenuInfo](
	[mId] [int] IDENTITY(1,1) NOT NULL,
	[mNum] [varchar](50) NULL,
	[mPId] [int] NULL,
	[mText] [varchar](200) NULL,
	[mUrl] [varchar](200) NULL,
	[mIcon] [varchar](200) NULL,
	[mAddtime] [datetime] NULL,
	[mState] [varchar](20) NULL,
	[mChecked] [bit] NULL,
	[mSeq] [float] NULL,
	[mPerNum] [varchar](50) NULL,
	[mOrderindex] [float] NULL,
 CONSTRAINT [PK_tb_Sys_MenuInfo] PRIMARY KEY CLUSTERED 
(
	[mId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_Sys_Notice]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Sys_Notice](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Contents] [nvarchar](max) NULL,
	[Hot] [int] NULL,
	[Seq] [float] NULL,
	[ClickNum] [int] NULL,
	[IsDel] [int] NULL,
	[Creator] [nvarchar](50) NULL,
	[C_Time] [smalldatetime] NULL,
	[ShowImgUrl] [nvarchar](300) NULL,
	[FilePath] [nvarchar](450) NULL,
	[FileName] [nvarchar](50) NULL,
	[IsPush] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tb_Sys_Permission]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Sys_Permission](
	[pId] [int] IDENTITY(1,1) NOT NULL,
	[pParentId] [int] NOT NULL,
	[pName] [varchar](50) NOT NULL,
	[pAreaName] [varchar](50) NULL,
	[pControllerName] [varchar](150) NULL,
	[pActionMenu] [int] NULL,
	[pActionNum] [varchar](50) NULL,
	[PActionName] [varchar](50) NULL,
	[pFormMethod] [varchar](50) NULL,
	[pOperationType] [int] NULL,
	[pIco] [varchar](100) NULL,
	[pOrder] [int] NULL,
	[pIsLink] [int] NULL,
	[pLinkUrl] [varchar](200) NULL,
	[pIsShow] [int] NULL,
	[pRemark] [varchar](200) NULL,
	[pIsDel] [int] NULL,
	[pState] [int] NULL,
	[pOperTime] [datetime] NULL,
 CONSTRAINT [PK_tb_Sys_Permission] PRIMARY KEY CLUSTERED 
(
	[pId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_Sys_Role]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Sys_Role](
	[rId] [int] IDENTITY(1,1) NOT NULL,
	[rName] [varchar](50) NOT NULL,
	[rRemark] [varchar](100) NULL,
	[rIsShow] [int] NULL,
	[rIsDel] [int] NULL,
	[rDepId] [int] NULL,
	[rAddTime] [datetime] NULL,
	[rNum] [varchar](50) NULL,
 CONSTRAINT [PK_tb_Sys_Role] PRIMARY KEY CLUSTERED 
(
	[rId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_Sys_RolePermission]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Sys_RolePermission](
	[rpId] [int] IDENTITY(1,1) NOT NULL,
	[rpRId] [int] NOT NULL,
	[rpPId] [int] NOT NULL,
	[rpIsDel] [int] NULL,
	[rpAddtime] [datetime] NULL,
 CONSTRAINT [PK_tb_Sys_RolePermission] PRIMARY KEY CLUSTERED 
(
	[rpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tb_Sys_UserInfo]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Sys_UserInfo](
	[Uid] [int] IDENTITY(1,1) NOT NULL,
	[uDepid] [int] NULL,
	[uLoginName] [varchar](50) NOT NULL,
	[uPwd] [varchar](50) NOT NULL,
	[uGender] [varchar](10) NULL,
	[uIsDel] [int] NULL,
	[uPost] [varchar](100) NULL,
	[uRemark] [varchar](100) NULL,
	[uAddtime] [datetime] NULL,
	[uMobile] [varchar](20) NULL,
	[CityId] [varchar](10) NULL,
	[uName] [varchar](50) NULL,
 CONSTRAINT [PK_tb_Sys_UserInfo] PRIMARY KEY CLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_Sys_UserRole]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Sys_UserRole](
	[urId] [int] IDENTITY(1,1) NOT NULL,
	[urUid] [int] NOT NULL,
	[urRid] [int] NOT NULL,
	[urIsDel] [int] NULL,
	[urAddTime] [datetime] NULL,
 CONSTRAINT [PK_tb_Sys_UserRole] PRIMARY KEY CLUSTERED 
(
	[urId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tb_Sys_UserVipPermission]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Sys_UserVipPermission](
	[vipId] [int] IDENTITY(1,1) NOT NULL,
	[vipUserId] [int] NOT NULL,
	[VipPermission] [int] NOT NULL,
	[vipRemark] [int] NULL,
	[vipIsdel] [int] NULL,
	[vipAddtime] [datetime] NULL,
 CONSTRAINT [PK_tb_Sys_UserVipPermission] PRIMARY KEY CLUSTERED 
(
	[vipId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tb_User_Account]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_User_Account](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[uId] [int] NOT NULL,
	[TouchFlag] [int] NULL,
	[total] [int] NULL,
	[delay] [int] NULL,
	[creatorId] [int] NULL,
	[creator] [varchar](50) NULL,
	[c_time] [datetime] NULL,
 CONSTRAINT [PK_tb_User_Account] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_User_Buy_Rec]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_User_Buy_Rec](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[uId] [int] NULL,
	[RecDate] [date] NULL,
	[Buy_Hospid] [int] NULL,
	[BuyNum] [int] NULL,
	[PayMoney] [money] NULL,
	[ProductId] [int] NULL,
	[ProductPrice] [money] NULL,
	[CreatorId] [int] NULL,
	[Creator] [varchar](50) NULL,
	[C_time] [datetime] NULL,
 CONSTRAINT [PK_tb_User_Buy_Rec] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_User_Dislike_Food]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_User_Dislike_Food](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[uId] [int] NOT NULL,
	[FoodType] [varchar](10) NULL,
	[FoodName] [varchar](500) NULL,
	[CreatorId] [int] NULL,
	[Creator] [varchar](50) NULL,
	[C_time] [datetime] NULL,
 CONSTRAINT [PK_tb_user_dislike_food] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_User_Info]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_User_Info](
	[uId] [int] IDENTITY(1,1) NOT NULL,
	[HospId] [int] NULL,
	[Name] [varchar](50) NOT NULL,
	[Female] [varchar](4) NOT NULL,
	[Birth] [date] NULL,
	[Mobile] [varchar](20) NOT NULL,
	[Email] [varchar](50) NULL,
	[ComeFrom] [varchar](10) NULL,
	[IndustryType] [varchar](10) NULL,
	[Height] [float] NULL,
	[Weight] [float] NULL,
	[WC] [float] NULL,
	[Hipline] [float] NULL,
	[Thigh_Cir] [float] NULL,
	[Qq] [varchar](20) NULL,
	[WeiXin] [varchar](50) NULL,
	[BMI] [float] NULL,
	[Enter_Code] [varchar](20) NULL,
	[Self_code] [varchar](20) SPARSE  NULL,
	[Cityid] [varchar](10) NULL,
	[PicPath] [varchar](200) NULL,
	[Myhope] [varchar](200) NULL,
	[CreatorId] [int] NULL,
	[Creator] [varchar](50) NULL,
	[C_time] [datetime] NULL,
	[isDel] [int] NULL,
 CONSTRAINT [PK_T_Userinfo] PRIMARY KEY CLUSTERED 
(
	[uId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_Weight_Chg]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Weight_Chg](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[uId] [int] NOT NULL,
	[RecDate] [date] NULL,
	[Hid] [int] NULL,
	[Weight] [float] NULL,
	[BatchId] [int] NULL,
	[TouchFlag] [int] NULL,
	[PicPath] [varchar](200) NULL,
	[CreatorId] [int] NULL,
	[Creator] [varchar](50) NULL,
	[C_time] [datetime] NULL,
	[self] [varchar](5) NULL,
 CONSTRAINT [PK_T_weight_chg] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_Weight_Chg_Self]    Script Date: 2017/4/7 17:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Weight_Chg_Self](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[uId] [int] NOT NULL,
	[RecDate] [date] NULL,
	[Hid] [int] NULL,
	[Weight] [float] NULL,
	[CreatorId] [int] NULL,
	[Creator] [varchar](50) NULL,
	[C_time] [datetime] NULL,
 CONSTRAINT [PK_T_Weight_Chg_Self] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tb_Dict] ON 

INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (1, N'MemberGrade', N'普通', N'1', 1, CAST(0x0000A6EE00000000 AS DateTime), 1, N'会员等级')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (2, N'MemberGrade', N'VIP', N'2', 2, CAST(0x0000A6EE00000000 AS DateTime), 1, N'会员等级')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (3, N'MemberGrade', N'VVIP', N'3', 3, CAST(0x0000A6EE00000000 AS DateTime), 1, N'会员等级')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (5, N'HospGrade', N'其他', N'1', 1, CAST(0x0000A6EE00000000 AS DateTime), 1, N'医院等级')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (6, N'city', N'北京', N'100', 1, CAST(0x0000A6EE00000000 AS DateTime), 1, N'城市')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (9, N'city', N'上海', N'101', 2, CAST(0x0000A6EE00000000 AS DateTime), 1, N'城市')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (10, N'city', N'广州', N'102', 3, CAST(0x0000A6EE00000000 AS DateTime), 1, N'城市')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (11, N'city', N'深圳', N'103', 4, CAST(0x0000A6EE00000000 AS DateTime), 1, N'城市')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (12, N'city', N'成都', N'104', 5, CAST(0x0000A6EE00000000 AS DateTime), 1, N'城市')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (14, N'ServType', N'医疗服务', N'10', 1, CAST(0x0000A6EE00000000 AS DateTime), 1, N'产品分类')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (15, N'IndustryType', N'IT及信息服务', N'1', 1, CAST(0x0000A6EE00000000 AS DateTime), 1, N'行业分类')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (17, N'Pipeid', N'pica', N'1', 1, CAST(0x0000A6EE00000000 AS DateTime), 1, N'短信通道')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (18, N'Relation', N'夫妻', N'1', 1, CAST(0x0000A6EE00000000 AS DateTime), 1, N'亲属关系')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (19, N'Relation', N'父', N'2', 2, CAST(0x0000A6EE00000000 AS DateTime), 1, N'亲属关系')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (20, N'Relation', N'母', N'3', 3, CAST(0x0000A6EE00000000 AS DateTime), 1, N'亲属关系')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (23, N'Relation', N'夫妻', N'6', 6, CAST(0x0000A6EE00000000 AS DateTime), 1, N'亲属关系')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (27, N'SpecTitle', N'主任医师', N'1', 1, CAST(0x0000A6EE00000000 AS DateTime), 1, N'医疗专业职务')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (28, N'SpecTitle', N'副主任医师', N'2', 2, CAST(0x0000A6EE00000000 AS DateTime), 1, N'医疗专业职务')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (29, N'SpecTitle', N'主治医师', N'3', 3, CAST(0x0000A6EE00000000 AS DateTime), 1, N'医疗专业职务')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (31, N'EduTitle', N'教授', N'1', 1, CAST(0x0000A6EE00000000 AS DateTime), 1, N'教学职务')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (32, N'EduTitle', N'副教授', N'2', 2, CAST(0x0000A6EE00000000 AS DateTime), 1, N'教学职务')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (33, N'EduTitle', N'教授，博士生导师', N'3', 3, CAST(0x0000A6EE00000000 AS DateTime), 1, N'教学职务')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (34, N'ProjectType', N'一般销售', N'1', 1, CAST(0x0000A6EE00000000 AS DateTime), 1, N'会员类别')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (35, N'ProjectType', N'公关', N'2', 2, CAST(0x0000A6EE00000000 AS DateTime), 1, N'项目类别')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (36, N'ProjectType', N'交换', N'3', 3, CAST(0x0000A6EE00000000 AS DateTime), 1, N'项目类别')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (37, N'HospGrade', N'自营店', N'2', 2, CAST(0x0000A70000EB88F3 AS DateTime), 1, N'医院等级')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (38, N'HospGrade', N'加盟店', N'3', 3, CAST(0x0000A70000EBA823 AS DateTime), 1, N'医院等级')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (40, N'comfrom', N'朋友介绍', N'1', 1, CAST(0x0000A705009B4AD7 AS DateTime), 1, N'投诉来源')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (41, N'comfrom', N'网络', N'2', 2, CAST(0x0000A705009B53D9 AS DateTime), 1, N'投诉来源')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (42, N'comfrom', N'微信', N'3', 3, CAST(0x0000A705009B610C AS DateTime), 1, N'投诉来源')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (43, N'comfrom', N'门店', N'4', 4, CAST(0x0000A705009B7143 AS DateTime), 1, N'投诉来源')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (44, N'MemberType', N'个人会员', N'1', 1, CAST(0x0000A705009CB943 AS DateTime), 1, N'会员类别')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (45, N'MemberType', N'企业会员', N'2', 2, CAST(0x0000A705009CC8E9 AS DateTime), 1, N'会员类别')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (46, N'Hosptype', N'自营店', N'1', 1, CAST(0x0000A705009D52E8 AS DateTime), 1, N'医院类别')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (47, N'Hosptype', N'民营', N'2', 2, CAST(0x0000A705009D62F8 AS DateTime), 1, N'医院类别')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (48, N'city', N'哈尔滨', N'105', 5, CAST(0x0000A7220119B701 AS DateTime), 1, N'城市')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (49, N'city', N'天津', N'106', 7, CAST(0x0000A72E010EC0B6 AS DateTime), 1, N'城市')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (50, N'city', N'武汉', N'107', 6, CAST(0x0000A72E011362CC AS DateTime), 1, N'城市')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (51, N'IndustryType', N'金融保险', N'2', 2, CAST(0x0000A73700F05DE8 AS DateTime), 1, N'行业分类')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (52, N'IndustryType', N'医疗服务', N'3', 3, CAST(0x0000A73700F080C1 AS DateTime), 1, N'行业分类')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (53, N'IndustryType', N'交通运输', N'4', 4, CAST(0x0000A73700F097BB AS DateTime), 1, N'行业分类')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (54, N'IndustryType', N'教育', N'5', 5, CAST(0x0000A73700F0EC79 AS DateTime), 1, N'行业分类')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (55, N'IndustryType', N'政府机构', N'6', 6, CAST(0x0000A73700F112A5 AS DateTime), 1, N'行业分类')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (56, N'IndustryType', N'房地产', N'7', 7, CAST(0x0000A73700F1315E AS DateTime), 1, N'行业分类')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (57, N'IndustryType', N'能源', N'8', 8, CAST(0x0000A73700F17CB5 AS DateTime), 1, N'行业分类')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (58, N'IndustryType', N'零售', N'9', 9, CAST(0x0000A73700F1B3D6 AS DateTime), 1, N'行业分类')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (59, N'IndustryType', N'建筑', N'10', 10, CAST(0x0000A73700F1D877 AS DateTime), 1, N'行业分类')
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (60, N'FoodType', N'蔬菜', N'2', 2, NULL, NULL, NULL)
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (61, N'FoodType', N'水果', N'3', 3, NULL, NULL, NULL)
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (63, N'FoodType', N'主食', N'1', 1, NULL, NULL, NULL)
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (64, N'FoodType', N'饮料', N'4', 4, NULL, NULL, NULL)
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (66, N'hope', N'大学时代的牛仔裤', N'1', 1, CAST(0x0000A74000F06A16 AS DateTime), NULL, NULL)
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (67, N'hope', N'腰围减少10厘米', N'2', 2, CAST(0x0000A74000F07872 AS DateTime), NULL, NULL)
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (68, N'hope', N'臀围减少15厘米 ', N'3', 3, CAST(0x0000A74000F0D4FC AS DateTime), NULL, NULL)
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (70, N'FoodType
', N'豆制品', N'5', 5, CAST(0x0000A74000F64521 AS DateTime), NULL, NULL)
INSERT [dbo].[tb_Dict] ([Id], [KeyName], [KeyWords], [KeyValue], [Seq], [C_Time], [state], [mark]) VALUES (71, N'FoodType
', N'其他', N'6', 6, CAST(0x0000A74000F67729 AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[tb_Dict] OFF
SET IDENTITY_INSERT [dbo].[tb_Emp_Hos] ON 

INSERT [dbo].[tb_Emp_Hos] ([Id], [emp_id], [hospid], [creatorid], [creator], [c_time]) VALUES (11, 1, 15, 1, N'admin', CAST(0x0000A74D01198783 AS DateTime))
INSERT [dbo].[tb_Emp_Hos] ([Id], [emp_id], [hospid], [creatorid], [creator], [c_time]) VALUES (12, 1, 14, 1, N'admin', CAST(0x0000A74D011A74AD AS DateTime))
INSERT [dbo].[tb_Emp_Hos] ([Id], [emp_id], [hospid], [creatorid], [creator], [c_time]) VALUES (13, 1, 16, 1, N'admin', CAST(0x0000A74D011BAA16 AS DateTime))
SET IDENTITY_INSERT [dbo].[tb_Emp_Hos] OFF
SET IDENTITY_INSERT [dbo].[tb_Hei_Wei] ON 

INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (1, N'男', 145, 45, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (2, N'男', 146, 46, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (3, N'男', 147, 47, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (4, N'男', 148, 48, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (5, N'男', 149, 49, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (6, N'男', 150, 50, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (7, N'男', 151, 51, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (8, N'男', 152, 52, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (9, N'男', 153, 53, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (10, N'男', 154, 54, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (11, N'男', 155, 55, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (15, N'男', 157, 57, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (16, N'男', 158, 58, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (17, N'男', 159, 59, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (18, N'男', 160, 60, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (19, N'男', 161, 61, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (20, N'男', 162, 62, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (21, N'男', 163, 63, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (22, N'男', 164, 64, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (23, N'男', 165, 65, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (25, N'男', 156, 56, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (26, N'男', 166, 66, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (27, N'男', 167, 67, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (28, N'男', 168, 68, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (29, N'男', 169, 69, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (30, N'男', 170, 70, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (31, N'男', 171, 71, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (32, N'男', 172, 72, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (33, N'男', 173, 73, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (34, N'男', 174, 74, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (35, N'男', 175, 75, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (36, N'男', 176, 76, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (37, N'男', 177, 77, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (38, N'男', 178, 78, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (39, N'男', 179, 79, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (40, N'男', 180, 80, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (41, N'男', 181, 81, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (42, N'男', 182, 82, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (43, N'男', 183, 83, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (44, N'男', 184, 84, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (45, N'男', 185, 85, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (46, N'男', 186, 86, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (47, N'男', 187, 87, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (48, N'男', 188, 88, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (49, N'男', 189, 89, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (50, N'男', 190, 90, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (51, N'男', 191, 91, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (52, N'男', 192, 92, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (53, N'男', 193, 93, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (54, N'男', 194, 94, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (55, N'男', 195, 95, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (56, N'男', 196, 96, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (57, N'男', 197, 97, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (58, N'男', 198, 98, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (59, N'男', 199, 99, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (60, N'男', 200, 100, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (61, N'女', 145, 45, 41, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (62, N'女', 146, 45.6, 41.4, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (63, N'女', 147, 46.2, 41.8, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (64, N'女', 148, 46.8, 42.1, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (65, N'女', 149, 47.4, 42.6, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (66, N'女', 150, 48, 43.2, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (67, N'女', 151, 48.6, 43.7, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (68, N'女', 152, 49.2, 44.3, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (69, N'女', 153, 49.8, 44.9, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (70, N'女', 154, 50.4, 45.4, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (71, N'女', 155, 51, 46, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (72, N'女', 156, 51.6, 46.5, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (73, N'女', 157, 52.2, 47, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (74, N'女', 158, 52.8, 47.6, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (75, N'女', 159, 53.4, 47.8, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (76, N'女', 160, 54, 48, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (77, N'女', 161, 54.6, 48.3, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (78, N'女', 162, 55.2, 48.6, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (79, N'女', 163, 55.8, 50, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (80, N'女', 164, 56.4, 50.4, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (81, N'女', 165, 57, 51, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (82, N'女', 166, 57.6, 51.6, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (83, N'女', 167, 58.2, 52.2, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (84, N'女', 168, 58.8, 52.8, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (85, N'女', 169, 59.4, 53.3, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (86, N'女', 170, 60, 53.9, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (87, N'女', 171, 60.6, 54.3, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (88, N'女', 172, 61.2, 54.8, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (89, N'女', 173, 61.8, 55, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (90, N'女', 174, 62.4, 55.3, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (91, N'女', 175, 63, 55.6, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (92, N'女', 176, 63.6, 55.8, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (93, N'女', 177, 64.2, 56, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (94, N'女', 178, 64.8, 56.4, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (95, N'女', 179, 65.4, 56.6, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (96, N'女', 180, 66, 56.8, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (97, N'女', 181, 66.6, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (98, N'女', 182, 67.2, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (99, N'女', 183, 67.8, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (100, N'女', 184, 68.4, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (101, N'女', 185, 69, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (102, N'女', 186, 69.6, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (103, N'女', 187, 70.2, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (104, N'女', 188, 70.8, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (105, N'女', 189, 71.4, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (106, N'女', 190, 72, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (107, N'女', 191, 72.6, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (108, N'女', 192, 73.2, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (109, N'女', 193, 73.8, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (110, N'女', 194, 74.4, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (111, N'女', 195, 75, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (112, N'女', 196, 75.6, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (113, N'女', 197, 76.2, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (114, N'女', 198, 76.8, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (115, N'女', 199, 77.4, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tb_Hei_Wei] ([Id], [Female], [Height], [St_weight], [Pt_weight], [WC], [Hipline], [Thigh_Cir], [C_time]) VALUES (116, N'女', 200, 78, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[tb_Hei_Wei] OFF
SET IDENTITY_INSERT [dbo].[tb_Hosp_Info] ON 

INSERT [dbo].[tb_Hosp_Info] ([HospId], [Hname], [address], [MapUrl], [tel], [sType], [traffic], [grade], [state], [CityId], [C_Time], [CreatorId], [ModLastTime], [intro], [ModfyId], [IsDel], [PyShort], [PyLong]) VALUES (14, N'门店1', NULL, NULL, N'1304947384938', 1, NULL, NULL, 0, N'101', CAST(0x0000A74D01153754 AS DateTime), 1, NULL, NULL, NULL, 0, N'MD1', N'MENDIAN1')
INSERT [dbo].[tb_Hosp_Info] ([HospId], [Hname], [address], [MapUrl], [tel], [sType], [traffic], [grade], [state], [CityId], [C_Time], [CreatorId], [ModLastTime], [intro], [ModfyId], [IsDel], [PyShort], [PyLong]) VALUES (15, N'门店2', NULL, NULL, N'13049485833', 2, NULL, NULL, 0, N'100', CAST(0x0000A74D01154FFC AS DateTime), 1, NULL, NULL, NULL, 0, N'MD2', N'MENDIAN2')
INSERT [dbo].[tb_Hosp_Info] ([HospId], [Hname], [address], [MapUrl], [tel], [sType], [traffic], [grade], [state], [CityId], [C_Time], [CreatorId], [ModLastTime], [intro], [ModfyId], [IsDel], [PyShort], [PyLong]) VALUES (16, N'门店3', NULL, NULL, N'13000948309', 1, NULL, NULL, 0, N'102', CAST(0x0000A74D01156253 AS DateTime), 1, NULL, NULL, NULL, 0, N'MD3', N'MENDIAN3')
INSERT [dbo].[tb_Hosp_Info] ([HospId], [Hname], [address], [MapUrl], [tel], [sType], [traffic], [grade], [state], [CityId], [C_Time], [CreatorId], [ModLastTime], [intro], [ModfyId], [IsDel], [PyShort], [PyLong]) VALUES (17, N'门店4', NULL, NULL, N'1345534343', 2, NULL, NULL, 0, N'105', CAST(0x0000A74D01157657 AS DateTime), 1, NULL, NULL, NULL, 0, N'MD4', N'MENDIAN4')
SET IDENTITY_INSERT [dbo].[tb_Hosp_Info] OFF
SET IDENTITY_INSERT [dbo].[tb_Serv_Info] ON 

INSERT [dbo].[tb_Serv_Info] ([ID], [ServId], [ServName], [ServMemo], [ServType], [state], [price], [CTime], [CreatorId], [Creator], [IsDel], [LastModTime], [ModifyId], [HospId]) VALUES (11, 21, N'服务1', N'服务1', N'10', 0, 11.0000, CAST(0x0000A74D011BD531 AS DateTime), 1, N'admin', 0, NULL, NULL, 14)
INSERT [dbo].[tb_Serv_Info] ([ID], [ServId], [ServName], [ServMemo], [ServType], [state], [price], [CTime], [CreatorId], [Creator], [IsDel], [LastModTime], [ModifyId], [HospId]) VALUES (12, 22, N'服务2', N'服务2', N'10', 0, 22.0000, CAST(0x0000A74D011BE158 AS DateTime), 1, N'admin', 0, NULL, NULL, 15)
SET IDENTITY_INSERT [dbo].[tb_Serv_Info] OFF
SET IDENTITY_INSERT [dbo].[tb_Serv_POF] ON 

INSERT [dbo].[tb_Serv_POF] ([Id], [ServId], [TouchFlag], [Times], [CreatorId], [Creator], [C_time]) VALUES (20, 21, 0, 5, 1, N'admin', CAST(0x0000A74D011E0E2F AS DateTime))
INSERT [dbo].[tb_Serv_POF] ([Id], [ServId], [TouchFlag], [Times], [CreatorId], [Creator], [C_time]) VALUES (21, 21, 1, 5, 1, N'admin', CAST(0x0000A74D011E0E38 AS DateTime))
INSERT [dbo].[tb_Serv_POF] ([Id], [ServId], [TouchFlag], [Times], [CreatorId], [Creator], [C_time]) VALUES (22, 22, 0, 6, 1, N'admin', CAST(0x0000A74D011F1ED0 AS DateTime))
INSERT [dbo].[tb_Serv_POF] ([Id], [ServId], [TouchFlag], [Times], [CreatorId], [Creator], [C_time]) VALUES (23, 22, 1, 4, 1, N'admin', CAST(0x0000A74D011F3400 AS DateTime))
SET IDENTITY_INSERT [dbo].[tb_Serv_POF] OFF
SET IDENTITY_INSERT [dbo].[tb_Sms_Down] ON 

INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (73, N'13031190350', N'【和合益生】测试一下数据！', 0, N'0', CAST(0x0000A72F00A1D9FC AS DateTime), 1, NULL, CAST(0x0000A72F00A1DA54 AS DateTime), 0, 0, 0, N'308094923127217887', 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (74, N'13716455813', N'【和合益生】测试一下数据！', 0, N'0', CAST(0x0000A72F00A1D9FC AS DateTime), 1, NULL, CAST(0x0000A72F00A1DA54 AS DateTime), 0, 0, 0, N'308094932514107525', 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (75, N'18731575597', N'【和合益生】测试一下数据！', 0, N'0', CAST(0x0000A72F00A1D9FC AS DateTime), 1, NULL, CAST(0x0000A72F00A1DA54 AS DateTime), 0, 0, 0, N'308094923361217890', 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (76, N'18731575597', N'【和合益生】换了个数据库，测试一下，是否能正常发送！', 0, N'0', CAST(0x0000A73000A69938 AS DateTime), 1, NULL, CAST(0x0000A73000A699B3 AS DateTime), 0, 0, 0, N'308095229293217919', 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (77, N'13716455813', N'【和合益生】换了个数据库，测试一下，是否能正常发送！', 0, N'0', CAST(0x0000A73000A69938 AS DateTime), 1, NULL, CAST(0x0000A73000A699B3 AS DateTime), 0, 0, 0, N'308095239028337549', 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (78, N'13701234567', N'【和合益生】您于2017/3/15 0:00:0014:00-14:30在安康医院药物科预约医生张三', 0, N'0', CAST(0x0000A73500AC0968 AS DateTime), 0, NULL, CAST(0x0000A73500AC09D0 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (79, N'13701233367', N'【和合益生】您于2017/3/15 0:00:0014:30-15:00在安康医院药物科预约医生李四', 0, N'0', CAST(0x0000A73500AC4E8C AS DateTime), 0, NULL, CAST(0x0000A73500AC4F24 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (80, N'13031190350', N'【和合益生】您于2017/3/15 0:00:0014:00-14:30在安康医院药物科预约医生张三', 0, N'0', CAST(0x0000A73500B1C498 AS DateTime), 0, NULL, CAST(0x0000A73500B1C56D AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (81, N'13601234567', N'【和合益生】您于2017/3/15 0:00:0014:00-14:30在安康医院药物科预约医生张三', 0, N'0', CAST(0x0000A73500B956E0 AS DateTime), 0, NULL, CAST(0x0000A73500B956E5 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (82, N'13031190350', N'【和合益生】您于2017/3/15 0:00:0014:30-15:00在安康医院药物科预约医生李四', 0, N'0', CAST(0x0000A73500F187A4 AS DateTime), 0, NULL, CAST(0x0000A73500F188B6 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (83, N'3333333333', N'【和合益生】您于2017/3/15 0:00:0008:00-08:30在安康医院药物科预约医生李四', 0, N'0', CAST(0x0000A735011E2A98 AS DateTime), 0, NULL, CAST(0x0000A735011E2AB1 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (84, N'3333333333333', N'【和合益生】您于2017/3/15 0:00:0014:00-14:30在安康医院药物科预约医生张三', 0, N'0', CAST(0x0000A735011EBD14 AS DateTime), 0, NULL, CAST(0x0000A735011EBD8F AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (85, N'66666666666666', N'【和合益生】您于2017/3/15 0:00:0014:00-14:30在安康医院药物科预约医生张三', 0, N'0', CAST(0x0000A7350120D824 AS DateTime), 0, NULL, CAST(0x0000A7350120D85C AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (86, N'444444444', N'【和合益生】您于2017/3/15 0:00:0014:00-14:30在安康医院药物科预约医生张三', 0, N'0', CAST(0x0000A7350122A000 AS DateTime), 0, NULL, CAST(0x0000A7350122A0C6 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (87, N'222222222', N'【和合益生】您于在预约医生', 0, N'0', CAST(0x0000A73700A5C9CC AS DateTime), 0, NULL, CAST(0x0000A73700A5CAEC AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (88, N'6666666666', N'【和合益生】您于在预约医生', 0, N'0', CAST(0x0000A73700A5D908 AS DateTime), 0, NULL, CAST(0x0000A73700A5D9F2 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (89, N'13701234567', N'【和合益生】您于2017/3/15 0:00:0008:00-08:15在请选择请选择预约医生请选择', 0, N'0', CAST(0x0000A73700E25F90 AS DateTime), 0, NULL, CAST(0x0000A73700E26068 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (90, N'13701234567', N'【和合益生】您于2017/3/15 0:00:0008:00-08:15在请选择请选择预约医生请选择', 0, N'0', CAST(0x0000A73700E65668 AS DateTime), 0, NULL, CAST(0x0000A73700E65773 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (91, N'13701231111', N'【和合益生】您于2017/3/15 0:00:0008:00-08:15在请选择请选择预约医生请选择', 0, N'0', CAST(0x0000A73700E88438 AS DateTime), 0, NULL, CAST(0x0000A73700E88487 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (92, N'13701233367', N'【和合益生】您于2017/3/15 0:00:0008:30-09:00在请选择请选择预约医生请选择', 0, N'0', CAST(0x0000A73700E8DAF0 AS DateTime), 0, NULL, CAST(0x0000A73700E8DB35 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (93, N'13661019024', N'【和合益生】你好，短信测试', 0, N'0', CAST(0x0000A73700EF9214 AS DateTime), 0, NULL, CAST(0x0000A73700EF92F1 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (94, N'13701233367', N'【和合益生】您于2017/3/9 0:00:0008:00-08:15在请选择请选择预约医生请选择', 0, N'0', CAST(0x0000A73700EFCA54 AS DateTime), 0, NULL, CAST(0x0000A73700EFCAD2 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (95, N'13701231111', N'【和合益生】您于2017/3/15 0:00:0014:30-15:00在安康医院药物科预约医生李四', 0, N'0', CAST(0x0000A7370110ED88 AS DateTime), 0, NULL, CAST(0x0000A7370110EDEE AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (96, N'010-84210088', N'【和合益生】您于在预约医生', 0, N'0', CAST(0x0000A73701117DAC AS DateTime), 0, NULL, CAST(0x0000A73701117DE1 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (97, N'010-84210088', N'【和合益生】您于在预约医生', 0, N'0', CAST(0x0000A73701118964 AS DateTime), 0, NULL, CAST(0x0000A7370111898B AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (98, N'010-84210088', N'【和合益生】您于在预约医生', 0, N'0', CAST(0x0000A7370111E97C AS DateTime), 0, NULL, CAST(0x0000A7370111E9D1 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (99, N'010-84210088', N'【和合益生】您于在预约医生', 0, N'0', CAST(0x0000A7370112F380 AS DateTime), 0, NULL, CAST(0x0000A7370112F406 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (100, N'010-84210088', N'【和合益生】您于在预约医生', 0, N'0', CAST(0x0000A737011B14C0 AS DateTime), 0, NULL, CAST(0x0000A737011B15C3 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (101, N'010-84210088', N'【和合益生】您于在预约医生', 0, N'0', CAST(0x0000A737011E5018 AS DateTime), 0, NULL, CAST(0x0000A737011E5025 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (102, N'010-84210088', N'【和合益生】您于在预约医生', 0, N'0', CAST(0x0000A73701206C54 AS DateTime), 0, NULL, CAST(0x0000A73701206CD1 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (103, N'13701234567', N'【和合益生】您于2017/3/17 0:00:0013:00-13:30在请选择请选择预约医生请选择', 0, N'0', CAST(0x0000A73800A2A968 AS DateTime), 0, NULL, CAST(0x0000A73800A2A9B6 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (104, N'13701234567', N'【和合益生】您于2017/3/18 0:00:0013:00-13:30在请选择请选择预约医生请选择', 0, N'0', CAST(0x0000A73800A2C6B4 AS DateTime), 0, NULL, CAST(0x0000A73800A2C7D2 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (105, N'13701231111', N'【和合益生】您于2017/3/18 0:00:0013:30-14:00在安康医院药物科预约医生李四', 0, N'0', CAST(0x0000A73800AD89C8 AS DateTime), 0, NULL, CAST(0x0000A73800AD8A96 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (106, N'10395737593', N'【和合益生】您于2017/3/2 0:00:0013:00-13:30在请选择请选择预约医生请选择', 0, N'0', CAST(0x0000A73800ADAA98 AS DateTime), 0, NULL, CAST(0x0000A73800ADAB8B AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (107, N'13601234567', N'【和合益生】您于2017/3/18 0:00:0013:30-14:00在安康医院药物科预约医生李四', 0, N'0', CAST(0x0000A73800AE027C AS DateTime), 0, NULL, CAST(0x0000A73800AE028D AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (108, N'13759367493', N'【和合益生】您于2017/3/9 0:00:0013:30-14:00在请选择请选择预约医生请选择', 0, N'0', CAST(0x0000A73800AE1FC8 AS DateTime), 0, NULL, CAST(0x0000A73800AE2016 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (109, N'13701231111', N'【和合益生】您于2017/3/11 0:00:0015:00-15:30在请选择请选择预约医生请选择', 0, N'0', CAST(0x0000A73800AE5100 AS DateTime), 0, NULL, CAST(0x0000A73800AE512D AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (110, N'777745477', N'【和合益生】您于在预约医生', 0, N'0', CAST(0x0000A73800E03094 AS DateTime), 0, NULL, CAST(0x0000A73800E030F6 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (111, N'777745477', N'【和合益生】您于在预约医生', 0, N'0', CAST(0x0000A73800E03C4C AS DateTime), 0, NULL, CAST(0x0000A73800E03D44 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (112, N'13701234567', N'【和合益生】您于2017/3/24 0:00:0011:30-12:00在请选择请选择预约医生请选择', 0, N'0', CAST(0x0000A738010928DC AS DateTime), 0, NULL, CAST(0x0000A738010928E9 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (113, N'010-84217565', N'【和合益生】您于在预约医生', 0, N'0', CAST(0x0000A738010935C0 AS DateTime), 0, NULL, CAST(0x0000A7380109367B AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (114, N'13601234567', N'【和合益生】您于2017/3/17 0:00:0013:00-13:30在请选择请选择预约医生请选择', 0, N'0', CAST(0x0000A7380110E428 AS DateTime), 0, NULL, CAST(0x0000A7380110E4AA AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (115, N'13601234567', N'【和合益生】您于2017/3/17 0:00:0013:00-13:30在请选择请选择预约医生请选择', 0, N'0', CAST(0x0000A73900E32470 AS DateTime), 0, NULL, CAST(0x0000A73900E324F6 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (116, N'13601234567', N'【和合益生】您于2017/3/17 0:00:008:00-8:15在请选择请选择预约医生请选择', 0, N'0', CAST(0x0000A739010F0158 AS DateTime), 0, NULL, CAST(0x0000A739010F0162 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (117, N'010-84210099', N'【和合益生】您于在预约医生', 0, N'0', CAST(0x0000A739012296A0 AS DateTime), 0, NULL, CAST(0x0000A73901229785 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (118, N'010-84210099', N'【和合益生】您于在预约医生', 0, N'0', CAST(0x0000A73D009BC10C AS DateTime), 0, NULL, CAST(0x0000A73D009BC141 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (119, N'13701231111', N'【和合益生】您于2017/3/21 0:00:0013:00-13:30在请选择请选择预约医生请选择', 0, N'0', CAST(0x0000A73D00DCFD70 AS DateTime), 0, NULL, CAST(0x0000A73D00DCFDFD AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (120, N'13601234567', N'【和合益生】您于2017/3/21 0:00:0008:30-09:00在请选择请选择预约医生请选择', 0, N'0', CAST(0x0000A73D00F5CF58 AS DateTime), 0, NULL, CAST(0x0000A73D00F5CFB6 AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (121, N'13601234567', N'【和合益生】您于2017/3/24 0:00:0013:00-13:30在请选择请选择预约医生请选择', 0, N'0', CAST(0x0000A73D00F5DB10 AS DateTime), 0, NULL, CAST(0x0000A73D00F5DB6E AS DateTime), 0, 0, 0, NULL, 1, NULL)
INSERT [dbo].[tb_Sms_Down] ([Id], [mobile], [msg], [creatorId], [creator], [c_time], [sendFlag], [errMsg], [sendTime], [stype], [pipeId], [levelNum], [exId], [sysPlatform], [M_NO]) VALUES (122, N'13601234567', N'【和合益生】您于2017/3/28 0:00:0014:00-14:30在安康医院药物科预约医生张三', 0, N'0', CAST(0x0000A73D00F631C8 AS DateTime), 0, NULL, CAST(0x0000A73D00F632D0 AS DateTime), 0, 0, 0, NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[tb_Sms_Down] OFF
SET IDENTITY_INSERT [dbo].[tb_Sms_report] ON 

INSERT [dbo].[tb_Sms_report] ([id], [mobile], [C_time], [R_time], [pipeId], [errMsg], [M_no], [S_time]) VALUES (18, N'15033280877', CAST(0x0000A72A00EAB0DC AS DateTime), CAST(0x0000A72A00EAFE34 AS DateTime), 0, N'Y', N'302140043075185775', CAST(0x0000A72A00EC34C0 AS DateTime))
INSERT [dbo].[tb_Sms_report] ([id], [mobile], [C_time], [R_time], [pipeId], [errMsg], [M_no], [S_time]) VALUES (19, N'13031190350', CAST(0x0000A72A00EAB0DC AS DateTime), CAST(0x0000A72A00EABEEC AS DateTime), 0, N'Y', N'302140043362185778', CAST(0x0000A72A00EC34C0 AS DateTime))
INSERT [dbo].[tb_Sms_report] ([id], [mobile], [C_time], [R_time], [pipeId], [errMsg], [M_no], [S_time]) VALUES (20, N'13716455813', CAST(0x0000A72A00EAB0DC AS DateTime), CAST(0x0000A72A00EADB0C AS DateTime), 0, N'N', N'302140052463217947', CAST(0x0000A72A00EC34C0 AS DateTime))
INSERT [dbo].[tb_Sms_report] ([id], [mobile], [C_time], [R_time], [pipeId], [errMsg], [M_no], [S_time]) VALUES (21, N'15101095274', CAST(0x0000A72A00EAB0DC AS DateTime), CAST(0x0000A72A00EAFE34 AS DateTime), 0, N'Y', N'302140052710217950', CAST(0x0000A72A00EC34C0 AS DateTime))
INSERT [dbo].[tb_Sms_report] ([id], [mobile], [C_time], [R_time], [pipeId], [errMsg], [M_no], [S_time]) VALUES (22, N'13784207189', CAST(0x0000A72A00F1B1D4 AS DateTime), CAST(0x0000A72B008C1360 AS DateTime), 0, N'Y', N'302142613251185955', CAST(0x0000A730009F1C08 AS DateTime))
INSERT [dbo].[tb_Sms_report] ([id], [mobile], [C_time], [R_time], [pipeId], [errMsg], [M_no], [S_time]) VALUES (23, N'13784207189', CAST(0x0000A72A00F1D4FC AS DateTime), CAST(0x0000A72B008C1360 AS DateTime), 0, N'Y', N'302142652420248122', CAST(0x0000A730009F1C08 AS DateTime))
INSERT [dbo].[tb_Sms_report] ([id], [mobile], [C_time], [R_time], [pipeId], [errMsg], [M_no], [S_time]) VALUES (24, N'13784207189', CAST(0x0000A72A00F12534 AS DateTime), CAST(0x0000A72B008C1360 AS DateTime), 0, N'Y', N'302142422429248109', CAST(0x0000A730009F1C08 AS DateTime))
INSERT [dbo].[tb_Sms_report] ([id], [mobile], [C_time], [R_time], [pipeId], [errMsg], [M_no], [S_time]) VALUES (25, N'13784207189', CAST(0x0000A72A00F00BF4 AS DateTime), CAST(0x0000A72B008C1360 AS DateTime), 0, N'Y', N'302142013048215918', CAST(0x0000A730009F1C08 AS DateTime))
INSERT [dbo].[tb_Sms_report] ([id], [mobile], [C_time], [R_time], [pipeId], [errMsg], [M_no], [S_time]) VALUES (26, N'13784207189', CAST(0x0000A72A00F02F1C AS DateTime), CAST(0x0000A72B008C1360 AS DateTime), 0, N'Y', N'302142043042225921', CAST(0x0000A730009F1C08 AS DateTime))
INSERT [dbo].[tb_Sms_report] ([id], [mobile], [C_time], [R_time], [pipeId], [errMsg], [M_no], [S_time]) VALUES (27, N'13784207189', CAST(0x0000A72A00F09894 AS DateTime), CAST(0x0000A72B008C1360 AS DateTime), 0, N'Y', N'302142213044225927', CAST(0x0000A730009F1C08 AS DateTime))
INSERT [dbo].[tb_Sms_report] ([id], [mobile], [C_time], [R_time], [pipeId], [errMsg], [M_no], [S_time]) VALUES (28, N'18731575597', CAST(0x0000A72A00ACCF74 AS DateTime), CAST(0x0000A72B008C1360 AS DateTime), 0, N'Y', N'302101535145275642', CAST(0x0000A730009F1C08 AS DateTime))
INSERT [dbo].[tb_Sms_report] ([id], [mobile], [C_time], [R_time], [pipeId], [errMsg], [M_no], [S_time]) VALUES (29, N'13043522175', CAST(0x0000A72A00E10F3C AS DateTime), CAST(0x0000A72B008C1360 AS DateTime), 0, N'Y', N'302132539529175601', CAST(0x0000A730009F1C08 AS DateTime))
INSERT [dbo].[tb_Sms_report] ([id], [mobile], [C_time], [R_time], [pipeId], [errMsg], [M_no], [S_time]) VALUES (30, N'13784207189', CAST(0x0000A72A00F0BBBC AS DateTime), CAST(0x0000A72B008C1360 AS DateTime), 0, N'Y', N'302142252429248103', CAST(0x0000A730009F1C08 AS DateTime))
INSERT [dbo].[tb_Sms_report] ([id], [mobile], [C_time], [R_time], [pipeId], [errMsg], [M_no], [S_time]) VALUES (31, N'18731575597', CAST(0x0000A72A00E10F3C AS DateTime), CAST(0x0000A72B008C1360 AS DateTime), 0, N'Y', N'302132548917217734', CAST(0x0000A730009F1C08 AS DateTime))
INSERT [dbo].[tb_Sms_report] ([id], [mobile], [C_time], [R_time], [pipeId], [errMsg], [M_no], [S_time]) VALUES (32, N'13784207189', CAST(0x0000A72A00F1485C AS DateTime), CAST(0x0000A72B008C1360 AS DateTime), 0, N'Y', N'302142443038225946', CAST(0x0000A730009F1C08 AS DateTime))
INSERT [dbo].[tb_Sms_report] ([id], [mobile], [C_time], [R_time], [pipeId], [errMsg], [M_no], [S_time]) VALUES (33, N'13031190350', CAST(0x0000A73000A5B838 AS DateTime), CAST(0x0000A73000A5C06C AS DateTime), 0, N'Y', N'308094923127217887', CAST(0x0000A73000AA49C0 AS DateTime))
INSERT [dbo].[tb_Sms_report] ([id], [mobile], [C_time], [R_time], [pipeId], [errMsg], [M_no], [S_time]) VALUES (34, N'13716455813', CAST(0x0000A73000A5B838 AS DateTime), CAST(0x0000A73000A5EF4C AS DateTime), 0, N'Y', N'308094932514107525', CAST(0x0000A73000AA49C0 AS DateTime))
INSERT [dbo].[tb_Sms_report] ([id], [mobile], [C_time], [R_time], [pipeId], [errMsg], [M_no], [S_time]) VALUES (35, N'13716455813', CAST(0x0000A73000A6935C AS DateTime), CAST(0x0000A73000A6C368 AS DateTime), 0, N'Y', N'308095239028337549', CAST(0x0000A73000AA49C0 AS DateTime))
SET IDENTITY_INSERT [dbo].[tb_Sms_report] OFF
SET IDENTITY_INSERT [dbo].[tb_Sms_up] ON 

INSERT [dbo].[tb_Sms_up] ([id], [mobile], [msg], [C_time], [S_time], [PipeId], [CreatorId], [M_no]) VALUES (1, N'13716455813', N'收到', CAST(0x0000A72A00E91588 AS DateTime), CAST(0x0000A72A00E93400 AS DateTime), 0, 1, N'6022161')
INSERT [dbo].[tb_Sms_up] ([id], [mobile], [msg], [C_time], [S_time], [PipeId], [CreatorId], [M_no]) VALUES (2, N'15710335735', N'^(oo)^?', CAST(0x0000A72A00E92398 AS DateTime), CAST(0x0000A72A00E93400 AS DateTime), 0, 1, N'6022162')
INSERT [dbo].[tb_Sms_up] ([id], [mobile], [msg], [C_time], [S_time], [PipeId], [CreatorId], [M_no]) VALUES (3, N'15710335735', N' 哈喽', CAST(0x0000A72A00E86DF4 AS DateTime), CAST(0x0000A72A00E93400 AS DateTime), 0, 1, N'6022158')
INSERT [dbo].[tb_Sms_up] ([id], [mobile], [msg], [C_time], [S_time], [PipeId], [CreatorId], [M_no]) VALUES (4, N'15033280877', N'你', CAST(0x0000A72A00F1FE00 AS DateTime), CAST(0x0000A730009F1C08 AS DateTime), 0, 1, N'6022225')
SET IDENTITY_INSERT [dbo].[tb_Sms_up] OFF
SET IDENTITY_INSERT [dbo].[tb_Sys_Department] ON 

INSERT [dbo].[tb_Sys_Department] ([DepId], [DepPId], [DepName], [Depremark], [DepIsDel], [DepAddtime], [CityId], [DepNum]) VALUES (1, 0, N'技术部', N'开发', 1, CAST(0x0000A6F300EC5984 AS DateTime), N'100', N'DT01')
INSERT [dbo].[tb_Sys_Department] ([DepId], [DepPId], [DepName], [Depremark], [DepIsDel], [DepAddtime], [CityId], [DepNum]) VALUES (2, 0, N'客服部', N'客服部', 1, CAST(0x0000A6F300ED4EA7 AS DateTime), N'102', N'DT07')
INSERT [dbo].[tb_Sys_Department] ([DepId], [DepPId], [DepName], [Depremark], [DepIsDel], [DepAddtime], [CityId], [DepNum]) VALUES (4, 0, N'财务部', N'财务部', 1, CAST(0x0000A73700E1CC31 AS DateTime), N'100', N'DT02')
INSERT [dbo].[tb_Sys_Department] ([DepId], [DepPId], [DepName], [Depremark], [DepIsDel], [DepAddtime], [CityId], [DepNum]) VALUES (6, 0, N'市场部', N'市场部', 1, CAST(0x0000A73700E2675A AS DateTime), N'100', N'DT03')
INSERT [dbo].[tb_Sys_Department] ([DepId], [DepPId], [DepName], [Depremark], [DepIsDel], [DepAddtime], [CityId], [DepNum]) VALUES (7, 0, N'销售部', N'销售部', 1, CAST(0x0000A73700E29727 AS DateTime), N'100', N'DT04')
INSERT [dbo].[tb_Sys_Department] ([DepId], [DepPId], [DepName], [Depremark], [DepIsDel], [DepAddtime], [CityId], [DepNum]) VALUES (8, 0, N'医疗资源部', N'医疗资源部', 1, CAST(0x0000A73700E2D1B0 AS DateTime), N'100', N'DT05')
INSERT [dbo].[tb_Sys_Department] ([DepId], [DepPId], [DepName], [Depremark], [DepIsDel], [DepAddtime], [CityId], [DepNum]) VALUES (9, 0, N'人事部', N'人事部', 1, CAST(0x0000A73700E2EC90 AS DateTime), N'100', N'DT06')
SET IDENTITY_INSERT [dbo].[tb_Sys_Department] OFF
SET IDENTITY_INSERT [dbo].[tb_Sys_LoginLog] ON 

INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (1, N'test', N'111', CAST(0x0000A744016F26A3 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (2, N'admin', N'303', CAST(0x0000A744016FE130 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (3, N'admin', N'303', CAST(0x0000A7440171091D AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (4, N'admin', N'303', CAST(0x0000A7440171758F AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (5, N'admin', N'100', CAST(0x0000A74C009FF624 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (6, N'admin', N'100', CAST(0x0000A74C009FF661 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (7, N'admin', N'100', CAST(0x0000A74C00A18706 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (8, N'admin', N'100', CAST(0x0000A74C00A97B50 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (9, N'admin', N'100', CAST(0x0000A74C00AB3512 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (10, N'admin', N'100', CAST(0x0000A74C00ABD089 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (11, N'admin', N'100', CAST(0x0000A74C00ADAC30 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (12, N'admin', N'100', CAST(0x0000A74C00B1FD96 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (13, N'admin', N'100', CAST(0x0000A74C00B2DDCE AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (14, N'admin', N'100', CAST(0x0000A74C00B32C4B AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (15, N'test', N'100', CAST(0x0000A74C00B42B81 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (16, N'admin', N'100', CAST(0x0000A74C00B87CE4 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (17, N'admin', N'100', CAST(0x0000A74C00BA07B5 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (18, N'admin', N'100', CAST(0x0000A74C00BA8A01 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (19, N'admin', N'100', CAST(0x0000A74C00BB4F76 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (20, N'admin', N'100', CAST(0x0000A74C00E1132B AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (21, N'admin', N'100', CAST(0x0000A74C00E1C4D8 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (22, N'admin', N'100', CAST(0x0000A74C00E4A7DD AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (23, N'admin', N'100', CAST(0x0000A74C00E891D1 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (24, N'admin', N'100', CAST(0x0000A74C00E8FD4F AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (25, N'admin', N'100', CAST(0x0000A74C00E9A9B3 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (26, N'admin', N'100', CAST(0x0000A74C00EA94B2 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (27, N'admin', N'100', CAST(0x0000A74C00EE23F9 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (28, N'admin', N'100', CAST(0x0000A74C00EECD6E AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (29, N'admin', N'100', CAST(0x0000A74C00F0CDDA AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (30, N'admin', N'100', CAST(0x0000A74C00F2EC18 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (31, N'admin', N'100', CAST(0x0000A74C00F706C2 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (32, N'admin', N'100', CAST(0x0000A74C00F7A198 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (33, N'admin', N'100', CAST(0x0000A74C00F931E2 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (34, N'admin', N'100', CAST(0x0000A74C00F9A7BF AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (35, N'admin', N'100', CAST(0x0000A74C00FE0390 AS DateTime), N'192.168.5.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (36, N'admin', N'100', CAST(0x0000A74C00FE0390 AS DateTime), N'192.168.5.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (37, N'admin', N'100', CAST(0x0000A74C00FE9606 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (38, N'admin', N'100', CAST(0x0000A74C01013221 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (39, N'admin', N'100', CAST(0x0000A74C01086003 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (40, N'admin', N'100', CAST(0x0000A74C010BB7B5 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (41, N'admin', N'100', CAST(0x0000A74C01195DA3 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (42, N'admin', N'100', CAST(0x0000A74D00A311C2 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (43, N'admin', N'100', CAST(0x0000A74D00A51DD3 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (44, N'admin', N'100', CAST(0x0000A74D00A51E6B AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (45, N'admin', N'100', CAST(0x0000A74D00A5C6BC AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (46, N'admin', N'100', CAST(0x0000A74D00A5C6C3 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (47, N'admin', N'100', CAST(0x0000A74D00A84FFD AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (48, N'admin', N'100', CAST(0x0000A74D00A85010 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (49, N'admin', N'100', CAST(0x0000A74D00AB07E9 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (50, N'admin', N'100', CAST(0x0000A74D00BBA9B8 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (51, N'admin', N'100', CAST(0x0000A74D00BBA9B8 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (52, N'admin', N'100', CAST(0x0000A74D00BE03FB AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (53, N'admin', N'100', CAST(0x0000A74D00D89D51 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (54, N'admin', N'100', CAST(0x0000A74D00DAEA0D AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (55, N'admin', N'100', CAST(0x0000A74D00DAEA0D AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (56, N'admin', N'100', CAST(0x0000A74D00DBD526 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (57, N'admin', N'100', CAST(0x0000A74D00F84848 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (58, N'admin', N'100', CAST(0x0000A74D00F987D6 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (59, N'admin', N'100', CAST(0x0000A74D00FB4449 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (60, N'admin', N'100', CAST(0x0000A74D00FE1510 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (61, N'admin', N'100', CAST(0x0000A74D010904C5 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (62, N'admin', N'100', CAST(0x0000A74D010E10FA AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (63, N'admin', N'100', CAST(0x0000A74D010F4A70 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (64, N'admin', N'100', CAST(0x0000A74D010F4A7C AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (65, N'liucong', N'100', CAST(0x0000A74D01102D4A AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (66, N'admin', N'100', CAST(0x0000A74D01104C5B AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (67, N'liucong', N'100', CAST(0x0000A74D0110A3C5 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (68, N'admin', N'100', CAST(0x0000A74D0110DB41 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (69, N'liucong', N'100', CAST(0x0000A74D0111087D AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (70, N'admin', N'100', CAST(0x0000A74D011501ED AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (71, N'admin', N'100', CAST(0x0000A74D011872C7 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (72, N'admin', N'100', CAST(0x0000A74D01197833 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (73, N'admin', N'100', CAST(0x0000A74D011A52A3 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (74, N'admin', N'100', CAST(0x0000A74D011B9630 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (75, N'admin', N'100', CAST(0x0000A74E00A20586 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (76, N'admin', N'100', CAST(0x0000A74E00A2454A AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (77, N'admin', N'100', CAST(0x0000A74E011E7F82 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (78, N'admin', N'100', CAST(0x0000A74E011E7F82 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (79, N'xindi', N'100', CAST(0x0000A74E011FA86F AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (80, N'liucong', N'100', CAST(0x0000A74E011FB7D9 AS DateTime), N'127.0.0.1')
INSERT [dbo].[tb_Sys_LoginLog] ([id], [loginName], [loginCity], [loginTime], [LoginIp]) VALUES (81, N'admin', N'100', CAST(0x0000A74E011FD48E AS DateTime), N'127.0.0.1')
SET IDENTITY_INSERT [dbo].[tb_Sys_LoginLog] OFF
SET IDENTITY_INSERT [dbo].[tb_Sys_MenuInfo] ON 

INSERT [dbo].[tb_Sys_MenuInfo] ([mId], [mNum], [mPId], [mText], [mUrl], [mIcon], [mAddtime], [mState], [mChecked], [mSeq], [mPerNum], [mOrderindex]) VALUES (1051, N'MN01', 0, N'系统配置', NULL, NULL, CAST(0x0000A74C00A29EAD AS DateTime), N'closed', 1, 1, N'RT01', 1)
INSERT [dbo].[tb_Sys_MenuInfo] ([mId], [mNum], [mPId], [mText], [mUrl], [mIcon], [mAddtime], [mState], [mChecked], [mSeq], [mPerNum], [mOrderindex]) VALUES (1052, N'MN0101', 1051, N'部门管理', N'/Department/Index', NULL, CAST(0x0000A74C00A30D43 AS DateTime), N'open', 1, 1, N'RT0101', 1)
INSERT [dbo].[tb_Sys_MenuInfo] ([mId], [mNum], [mPId], [mText], [mUrl], [mIcon], [mAddtime], [mState], [mChecked], [mSeq], [mPerNum], [mOrderindex]) VALUES (1053, N'MN0102', 1051, N'添加角色', N'/Role/Index', NULL, CAST(0x0000A74C00A33291 AS DateTime), N'open', 1, 1, N'RT0102', 2)
INSERT [dbo].[tb_Sys_MenuInfo] ([mId], [mNum], [mPId], [mText], [mUrl], [mIcon], [mAddtime], [mState], [mChecked], [mSeq], [mPerNum], [mOrderindex]) VALUES (1054, N'MN0103', 1051, N'菜单管理', N'/Menu/Index', NULL, CAST(0x0000A74C00A3562A AS DateTime), N'open', 1, 1, N'RT0103', 3)
INSERT [dbo].[tb_Sys_MenuInfo] ([mId], [mNum], [mPId], [mText], [mUrl], [mIcon], [mAddtime], [mState], [mChecked], [mSeq], [mPerNum], [mOrderindex]) VALUES (1055, N'MN0104', 1051, N'添加权限', N'/Permission/Index', NULL, CAST(0x0000A74C00A375CF AS DateTime), N'open', 1, 1, N'RT0104', 4)
INSERT [dbo].[tb_Sys_MenuInfo] ([mId], [mNum], [mPId], [mText], [mUrl], [mIcon], [mAddtime], [mState], [mChecked], [mSeq], [mPerNum], [mOrderindex]) VALUES (1056, N'MN0105', 1051, N'分配权限', N'/Permission/RolePermission', NULL, CAST(0x0000A74C00A3A975 AS DateTime), N'open', 1, 1, N'RT0105', 5)
INSERT [dbo].[tb_Sys_MenuInfo] ([mId], [mNum], [mPId], [mText], [mUrl], [mIcon], [mAddtime], [mState], [mChecked], [mSeq], [mPerNum], [mOrderindex]) VALUES (1057, N'MN02', 0, N'用户管理', NULL, NULL, CAST(0x0000A74C00A3F1CF AS DateTime), N'closed', 1, 1, N'RT02', 2)
INSERT [dbo].[tb_Sys_MenuInfo] ([mId], [mNum], [mPId], [mText], [mUrl], [mIcon], [mAddtime], [mState], [mChecked], [mSeq], [mPerNum], [mOrderindex]) VALUES (1058, N'MN0201', 1057, N'店员列表', N'/User/Index', NULL, CAST(0x0000A74C00A43016 AS DateTime), N'open', 1, 1, N'RT0201', 1)
INSERT [dbo].[tb_Sys_MenuInfo] ([mId], [mNum], [mPId], [mText], [mUrl], [mIcon], [mAddtime], [mState], [mChecked], [mSeq], [mPerNum], [mOrderindex]) VALUES (1059, N'MN0202', 1057, N'分配角色', N'/Role/RoleUser', NULL, CAST(0x0000A74C00A44F03 AS DateTime), N'open', 1, 1, N'RT0202', 2)
INSERT [dbo].[tb_Sys_MenuInfo] ([mId], [mNum], [mPId], [mText], [mUrl], [mIcon], [mAddtime], [mState], [mChecked], [mSeq], [mPerNum], [mOrderindex]) VALUES (1061, N'MN0203', 1057, N'会员列表', N'/EmpCenter/Index', NULL, CAST(0x0000A74C00A51B1A AS DateTime), N'open', 1, 1, N'RT0203', 3)
INSERT [dbo].[tb_Sys_MenuInfo] ([mId], [mNum], [mPId], [mText], [mUrl], [mIcon], [mAddtime], [mState], [mChecked], [mSeq], [mPerNum], [mOrderindex]) VALUES (1062, N'MN03', 0, N'门店管理', NULL, NULL, CAST(0x0000A74C00A56AAA AS DateTime), N'closed', 1, 1, N'RT03', 3)
INSERT [dbo].[tb_Sys_MenuInfo] ([mId], [mNum], [mPId], [mText], [mUrl], [mIcon], [mAddtime], [mState], [mChecked], [mSeq], [mPerNum], [mOrderindex]) VALUES (1063, N'MN0301', 1062, N'门店列表', N'/Hosp/Index', NULL, CAST(0x0000A74C00A5992B AS DateTime), N'open', 1, 1, N'RT0301', 1)
INSERT [dbo].[tb_Sys_MenuInfo] ([mId], [mNum], [mPId], [mText], [mUrl], [mIcon], [mAddtime], [mState], [mChecked], [mSeq], [mPerNum], [mOrderindex]) VALUES (1064, N'MN0106', 1051, N'数据查询权限', N'/EmpHos/Index', NULL, CAST(0x0000A74C00A5DC91 AS DateTime), N'open', 1, 1, N'RT0106', 6)
INSERT [dbo].[tb_Sys_MenuInfo] ([mId], [mNum], [mPId], [mText], [mUrl], [mIcon], [mAddtime], [mState], [mChecked], [mSeq], [mPerNum], [mOrderindex]) VALUES (1065, N'MN04', 0, N'产品管理', NULL, NULL, CAST(0x0000A74C00A60EC0 AS DateTime), N'closed', 1, 1, N'RT04', 4)
INSERT [dbo].[tb_Sys_MenuInfo] ([mId], [mNum], [mPId], [mText], [mUrl], [mIcon], [mAddtime], [mState], [mChecked], [mSeq], [mPerNum], [mOrderindex]) VALUES (1066, N'MN0401', 1065, N'产品列表', N'/Service/Index', NULL, CAST(0x0000A74C00A63336 AS DateTime), N'open', 1, 1, N'RT0401', 1)
INSERT [dbo].[tb_Sys_MenuInfo] ([mId], [mNum], [mPId], [mText], [mUrl], [mIcon], [mAddtime], [mState], [mChecked], [mSeq], [mPerNum], [mOrderindex]) VALUES (1067, N'MN05', 0, N'短信管理', NULL, NULL, CAST(0x0000A74C00A65D11 AS DateTime), N'closed', 1, 1, N'RT05', 5)
INSERT [dbo].[tb_Sys_MenuInfo] ([mId], [mNum], [mPId], [mText], [mUrl], [mIcon], [mAddtime], [mState], [mChecked], [mSeq], [mPerNum], [mOrderindex]) VALUES (1068, N'MN0501', 1067, N'短信模板', NULL, NULL, CAST(0x0000A74C00A68889 AS DateTime), N'open', 1, 1, N'RT0501', 1)
INSERT [dbo].[tb_Sys_MenuInfo] ([mId], [mNum], [mPId], [mText], [mUrl], [mIcon], [mAddtime], [mState], [mChecked], [mSeq], [mPerNum], [mOrderindex]) VALUES (1069, N'MN0502', 1067, N'短信列表', NULL, NULL, CAST(0x0000A74C00A6AFA4 AS DateTime), N'open', 1, 1, N'RT0502', 2)
INSERT [dbo].[tb_Sys_MenuInfo] ([mId], [mNum], [mPId], [mText], [mUrl], [mIcon], [mAddtime], [mState], [mChecked], [mSeq], [mPerNum], [mOrderindex]) VALUES (1070, N'MN0204', 1057, N'标准体重列表', N'/HeiWei/Index', NULL, CAST(0x0000A74D00AE06B0 AS DateTime), N'open', 1, 1, N'RT0204', 4)
INSERT [dbo].[tb_Sys_MenuInfo] ([mId], [mNum], [mPId], [mText], [mUrl], [mIcon], [mAddtime], [mState], [mChecked], [mSeq], [mPerNum], [mOrderindex]) VALUES (1071, N'MN0402', 1065, N'产品配置', N'/ServPOF/Index', NULL, CAST(0x0000A74D00BBF6DB AS DateTime), N'open', 1, 1, N'RT0402', 2)
INSERT [dbo].[tb_Sys_MenuInfo] ([mId], [mNum], [mPId], [mText], [mUrl], [mIcon], [mAddtime], [mState], [mChecked], [mSeq], [mPerNum], [mOrderindex]) VALUES (1072, N'MN0107', 1051, N'字典管理', N'/Dictionary/Index', NULL, CAST(0x0000A74E011EC8F9 AS DateTime), N'open', 1, 1, N'RT0107', 7)
SET IDENTITY_INSERT [dbo].[tb_Sys_MenuInfo] OFF
INSERT [dbo].[tb_Sys_Notice] ([Id], [Title], [Contents], [Hot], [Seq], [ClickNum], [IsDel], [Creator], [C_Time], [ShowImgUrl], [FilePath], [FileName], [IsPush]) VALUES (2, N'放假通知', N'和合益生（北京）诊所', 1, 1, 10, 0, N'admin', CAST(0xA6FE0304 AS SmallDateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[tb_Sys_Notice] ([Id], [Title], [Contents], [Hot], [Seq], [ClickNum], [IsDel], [Creator], [C_Time], [ShowImgUrl], [FilePath], [FileName], [IsPush]) VALUES (3, N'放假通知', N'和合益生（北京）诊所', 1, 1, 10, 0, N'admin', CAST(0xA6FE0304 AS SmallDateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[tb_Sys_Notice] ([Id], [Title], [Contents], [Hot], [Seq], [ClickNum], [IsDel], [Creator], [C_Time], [ShowImgUrl], [FilePath], [FileName], [IsPush]) VALUES (4, N'放假通知', N'和合益生（北京）诊所', 1, 1, 10, 0, N'admin', CAST(0xA6FE0304 AS SmallDateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[tb_Sys_Notice] ([Id], [Title], [Contents], [Hot], [Seq], [ClickNum], [IsDel], [Creator], [C_Time], [ShowImgUrl], [FilePath], [FileName], [IsPush]) VALUES (5, N'放假通知', N'<p>999</p>', 1, 1, 8, 0, N'admin', CAST(0xA6FE0304 AS SmallDateTime), N'', NULL, NULL, 1)
INSERT [dbo].[tb_Sys_Notice] ([Id], [Title], [Contents], [Hot], [Seq], [ClickNum], [IsDel], [Creator], [C_Time], [ShowImgUrl], [FilePath], [FileName], [IsPush]) VALUES (6, N'222', N'<p>
	反反复复
</p>', 1, 55, 44, 0, N'admin', CAST(0xA7010258 AS SmallDateTime), N'', NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[tb_Sys_Permission] ON 

INSERT [dbo].[tb_Sys_Permission] ([pId], [pParentId], [pName], [pAreaName], [pControllerName], [pActionMenu], [pActionNum], [PActionName], [pFormMethod], [pOperationType], [pIco], [pOrder], [pIsLink], [pLinkUrl], [pIsShow], [pRemark], [pIsDel], [pState], [pOperTime]) VALUES (1062, 0, N'系统配置', NULL, NULL, 1051, N'RT01', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'系统配置', 1, 1, CAST(0x0000A74C00A70BAA AS DateTime))
INSERT [dbo].[tb_Sys_Permission] ([pId], [pParentId], [pName], [pAreaName], [pControllerName], [pActionMenu], [pActionNum], [PActionName], [pFormMethod], [pOperationType], [pIco], [pOrder], [pIsLink], [pLinkUrl], [pIsShow], [pRemark], [pIsDel], [pState], [pOperTime]) VALUES (1063, 1062, N'部门管理', NULL, NULL, 1052, N'RT0101', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'部门管理', 1, 1, CAST(0x0000A74C00A71F09 AS DateTime))
INSERT [dbo].[tb_Sys_Permission] ([pId], [pParentId], [pName], [pAreaName], [pControllerName], [pActionMenu], [pActionNum], [PActionName], [pFormMethod], [pOperationType], [pIco], [pOrder], [pIsLink], [pLinkUrl], [pIsShow], [pRemark], [pIsDel], [pState], [pOperTime]) VALUES (1064, 1062, N'添加角色', NULL, NULL, 1053, N'RT0102', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'添加角色', 1, 1, CAST(0x0000A74C00A739DB AS DateTime))
INSERT [dbo].[tb_Sys_Permission] ([pId], [pParentId], [pName], [pAreaName], [pControllerName], [pActionMenu], [pActionNum], [PActionName], [pFormMethod], [pOperationType], [pIco], [pOrder], [pIsLink], [pLinkUrl], [pIsShow], [pRemark], [pIsDel], [pState], [pOperTime]) VALUES (1065, 1062, N'菜单管理', NULL, NULL, 1054, N'RT0103', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'菜单管理', 1, 1, CAST(0x0000A74C00A74EF0 AS DateTime))
INSERT [dbo].[tb_Sys_Permission] ([pId], [pParentId], [pName], [pAreaName], [pControllerName], [pActionMenu], [pActionNum], [PActionName], [pFormMethod], [pOperationType], [pIco], [pOrder], [pIsLink], [pLinkUrl], [pIsShow], [pRemark], [pIsDel], [pState], [pOperTime]) VALUES (1066, 1062, N'添加权限', NULL, NULL, 1055, N'RT0104', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'添加权限', 1, 1, CAST(0x0000A74C00A76282 AS DateTime))
INSERT [dbo].[tb_Sys_Permission] ([pId], [pParentId], [pName], [pAreaName], [pControllerName], [pActionMenu], [pActionNum], [PActionName], [pFormMethod], [pOperationType], [pIco], [pOrder], [pIsLink], [pLinkUrl], [pIsShow], [pRemark], [pIsDel], [pState], [pOperTime]) VALUES (1067, 1062, N'分配权限', NULL, NULL, 1056, N'RT0105', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'分配权限', 1, 1, CAST(0x0000A74C00A77938 AS DateTime))
INSERT [dbo].[tb_Sys_Permission] ([pId], [pParentId], [pName], [pAreaName], [pControllerName], [pActionMenu], [pActionNum], [PActionName], [pFormMethod], [pOperationType], [pIco], [pOrder], [pIsLink], [pLinkUrl], [pIsShow], [pRemark], [pIsDel], [pState], [pOperTime]) VALUES (1068, 1062, N'数据查询权限', NULL, NULL, 1064, N'RT0106', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'数据查询权限', 1, 1, CAST(0x0000A74C00A78C01 AS DateTime))
INSERT [dbo].[tb_Sys_Permission] ([pId], [pParentId], [pName], [pAreaName], [pControllerName], [pActionMenu], [pActionNum], [PActionName], [pFormMethod], [pOperationType], [pIco], [pOrder], [pIsLink], [pLinkUrl], [pIsShow], [pRemark], [pIsDel], [pState], [pOperTime]) VALUES (1069, 0, N'用户管理', NULL, NULL, 1057, N'RT02', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'用户管理', 1, 1, CAST(0x0000A74C00A79CD9 AS DateTime))
INSERT [dbo].[tb_Sys_Permission] ([pId], [pParentId], [pName], [pAreaName], [pControllerName], [pActionMenu], [pActionNum], [PActionName], [pFormMethod], [pOperationType], [pIco], [pOrder], [pIsLink], [pLinkUrl], [pIsShow], [pRemark], [pIsDel], [pState], [pOperTime]) VALUES (1070, 1069, N'店员列表', NULL, NULL, 1058, N'RT0201', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'店员列表', 1, 1, CAST(0x0000A74C00A7B03F AS DateTime))
INSERT [dbo].[tb_Sys_Permission] ([pId], [pParentId], [pName], [pAreaName], [pControllerName], [pActionMenu], [pActionNum], [PActionName], [pFormMethod], [pOperationType], [pIco], [pOrder], [pIsLink], [pLinkUrl], [pIsShow], [pRemark], [pIsDel], [pState], [pOperTime]) VALUES (1071, 1069, N'分配角色', NULL, NULL, 1059, N'RT0202', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'分配角色', 1, 1, CAST(0x0000A74C00A7CDC4 AS DateTime))
INSERT [dbo].[tb_Sys_Permission] ([pId], [pParentId], [pName], [pAreaName], [pControllerName], [pActionMenu], [pActionNum], [PActionName], [pFormMethod], [pOperationType], [pIco], [pOrder], [pIsLink], [pLinkUrl], [pIsShow], [pRemark], [pIsDel], [pState], [pOperTime]) VALUES (1072, 1069, N'会员列表', NULL, NULL, 1061, N'RT0203', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'会员列表', 1, 1, CAST(0x0000A74C00A7E3A4 AS DateTime))
INSERT [dbo].[tb_Sys_Permission] ([pId], [pParentId], [pName], [pAreaName], [pControllerName], [pActionMenu], [pActionNum], [PActionName], [pFormMethod], [pOperationType], [pIco], [pOrder], [pIsLink], [pLinkUrl], [pIsShow], [pRemark], [pIsDel], [pState], [pOperTime]) VALUES (1073, 0, N'门店管理', NULL, NULL, 1062, N'RT03', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'门店管理', 1, 1, CAST(0x0000A74C00A7F90C AS DateTime))
INSERT [dbo].[tb_Sys_Permission] ([pId], [pParentId], [pName], [pAreaName], [pControllerName], [pActionMenu], [pActionNum], [PActionName], [pFormMethod], [pOperationType], [pIco], [pOrder], [pIsLink], [pLinkUrl], [pIsShow], [pRemark], [pIsDel], [pState], [pOperTime]) VALUES (1074, 1073, N'门店列表', NULL, NULL, 1063, N'RT0301', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'门店列表', 1, 1, CAST(0x0000A74C00A80FF0 AS DateTime))
INSERT [dbo].[tb_Sys_Permission] ([pId], [pParentId], [pName], [pAreaName], [pControllerName], [pActionMenu], [pActionNum], [PActionName], [pFormMethod], [pOperationType], [pIco], [pOrder], [pIsLink], [pLinkUrl], [pIsShow], [pRemark], [pIsDel], [pState], [pOperTime]) VALUES (1075, 0, N'产品管理', NULL, NULL, 1065, N'RT04', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'产品管理', 1, 1, CAST(0x0000A74C00A82229 AS DateTime))
INSERT [dbo].[tb_Sys_Permission] ([pId], [pParentId], [pName], [pAreaName], [pControllerName], [pActionMenu], [pActionNum], [PActionName], [pFormMethod], [pOperationType], [pIco], [pOrder], [pIsLink], [pLinkUrl], [pIsShow], [pRemark], [pIsDel], [pState], [pOperTime]) VALUES (1076, 1075, N'产品列表', NULL, NULL, 1066, N'RT0401', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'产品列表', 1, 1, CAST(0x0000A74C00A83884 AS DateTime))
INSERT [dbo].[tb_Sys_Permission] ([pId], [pParentId], [pName], [pAreaName], [pControllerName], [pActionMenu], [pActionNum], [PActionName], [pFormMethod], [pOperationType], [pIco], [pOrder], [pIsLink], [pLinkUrl], [pIsShow], [pRemark], [pIsDel], [pState], [pOperTime]) VALUES (1077, 0, N'短信管理', NULL, NULL, 1067, N'RT05', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'短信管理', 1, 1, CAST(0x0000A74C00A84BFF AS DateTime))
INSERT [dbo].[tb_Sys_Permission] ([pId], [pParentId], [pName], [pAreaName], [pControllerName], [pActionMenu], [pActionNum], [PActionName], [pFormMethod], [pOperationType], [pIco], [pOrder], [pIsLink], [pLinkUrl], [pIsShow], [pRemark], [pIsDel], [pState], [pOperTime]) VALUES (1078, 1077, N'短信模板', NULL, NULL, 1068, N'RT0501', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'短信模板', 1, 1, CAST(0x0000A74C00A86031 AS DateTime))
INSERT [dbo].[tb_Sys_Permission] ([pId], [pParentId], [pName], [pAreaName], [pControllerName], [pActionMenu], [pActionNum], [PActionName], [pFormMethod], [pOperationType], [pIco], [pOrder], [pIsLink], [pLinkUrl], [pIsShow], [pRemark], [pIsDel], [pState], [pOperTime]) VALUES (1079, 1077, N'短信列表', NULL, NULL, 1069, N'RT0502', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'短信列表', 1, 1, CAST(0x0000A74C00A87E53 AS DateTime))
INSERT [dbo].[tb_Sys_Permission] ([pId], [pParentId], [pName], [pAreaName], [pControllerName], [pActionMenu], [pActionNum], [PActionName], [pFormMethod], [pOperationType], [pIco], [pOrder], [pIsLink], [pLinkUrl], [pIsShow], [pRemark], [pIsDel], [pState], [pOperTime]) VALUES (1080, 1069, N'标准体重列表', NULL, NULL, 1070, N'RT0204', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'标准体重列表', 1, 1, CAST(0x0000A74D00AE3326 AS DateTime))
INSERT [dbo].[tb_Sys_Permission] ([pId], [pParentId], [pName], [pAreaName], [pControllerName], [pActionMenu], [pActionNum], [PActionName], [pFormMethod], [pOperationType], [pIco], [pOrder], [pIsLink], [pLinkUrl], [pIsShow], [pRemark], [pIsDel], [pState], [pOperTime]) VALUES (1081, 1075, N'产品配置', NULL, NULL, 1071, N'RT0402', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'产品配置', 1, 1, CAST(0x0000A74D00BC10D7 AS DateTime))
INSERT [dbo].[tb_Sys_Permission] ([pId], [pParentId], [pName], [pAreaName], [pControllerName], [pActionMenu], [pActionNum], [PActionName], [pFormMethod], [pOperationType], [pIco], [pOrder], [pIsLink], [pLinkUrl], [pIsShow], [pRemark], [pIsDel], [pState], [pOperTime]) VALUES (1082, 1062, N'字典管理', NULL, NULL, 1072, N'RT0107', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, N'字典管理', 1, 1, CAST(0x0000A74E011EF221 AS DateTime))
SET IDENTITY_INSERT [dbo].[tb_Sys_Permission] OFF
SET IDENTITY_INSERT [dbo].[tb_Sys_Role] ON 

INSERT [dbo].[tb_Sys_Role] ([rId], [rName], [rRemark], [rIsShow], [rIsDel], [rDepId], [rAddTime], [rNum]) VALUES (12, N'管理员', N'管理员', NULL, 1, NULL, CAST(0x0000A74C00A1DF8D AS DateTime), N'RE01')
INSERT [dbo].[tb_Sys_Role] ([rId], [rName], [rRemark], [rIsShow], [rIsDel], [rDepId], [rAddTime], [rNum]) VALUES (13, N'普通员工', N'普通员工', NULL, 1, NULL, CAST(0x0000A74C00A1EBEF AS DateTime), N'RE02')
INSERT [dbo].[tb_Sys_Role] ([rId], [rName], [rRemark], [rIsShow], [rIsDel], [rDepId], [rAddTime], [rNum]) VALUES (14, N'店长', N'店长', NULL, 1, NULL, CAST(0x0000A74C00A1FD0F AS DateTime), N'RE03')
INSERT [dbo].[tb_Sys_Role] ([rId], [rName], [rRemark], [rIsShow], [rIsDel], [rDepId], [rAddTime], [rNum]) VALUES (15, N'总店长', N'总店长', NULL, 1, NULL, CAST(0x0000A74C00A20E2D AS DateTime), N'RE04')
SET IDENTITY_INSERT [dbo].[tb_Sys_Role] OFF
SET IDENTITY_INSERT [dbo].[tb_Sys_RolePermission] ON 

INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1091, 12, 1062, NULL, CAST(0x0000A74C00AA5FFB AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1092, 12, 1063, NULL, CAST(0x0000A74C00AA5FFB AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1093, 12, 1064, NULL, CAST(0x0000A74C00AA5FFB AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1094, 12, 1065, NULL, CAST(0x0000A74C00AA5FFB AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1095, 12, 1066, NULL, CAST(0x0000A74C00AA5FFB AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1096, 12, 1067, NULL, CAST(0x0000A74C00AA5FFB AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1097, 12, 1068, NULL, CAST(0x0000A74C00AA5FFB AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1098, 12, 1069, NULL, CAST(0x0000A74C00AA5FFB AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1099, 12, 1070, NULL, CAST(0x0000A74C00AA5FFB AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1100, 12, 1071, NULL, CAST(0x0000A74C00AA5FFB AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1101, 12, 1072, NULL, CAST(0x0000A74C00AA5FFB AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1102, 12, 1073, NULL, CAST(0x0000A74C00AA5FFB AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1103, 12, 1074, NULL, CAST(0x0000A74C00AA5FFB AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1104, 12, 1075, NULL, CAST(0x0000A74C00AA5FFB AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1105, 12, 1076, NULL, CAST(0x0000A74C00AA5FFB AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1106, 12, 1077, NULL, CAST(0x0000A74C00AA5FFB AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1107, 12, 1078, NULL, CAST(0x0000A74C00AA5FFB AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1108, 12, 1079, NULL, CAST(0x0000A74C00AA5FFB AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1109, 14, 1069, NULL, CAST(0x0000A74C00AAA8B8 AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1110, 14, 1070, NULL, CAST(0x0000A74C00AAA8B8 AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1111, 14, 1072, NULL, CAST(0x0000A74C00AAA8B8 AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1112, 13, 1069, NULL, CAST(0x0000A74C00AAC1CA AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1113, 13, 1072, NULL, CAST(0x0000A74C00AAC1CA AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1114, 15, 1062, NULL, CAST(0x0000A74C00AAEA4A AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1115, 15, 1068, NULL, CAST(0x0000A74C00AAEA4A AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1116, 15, 1069, NULL, CAST(0x0000A74C00AAEA4A AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1117, 15, 1070, NULL, CAST(0x0000A74C00AAEA4A AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1118, 15, 1072, NULL, CAST(0x0000A74C00AAEA4A AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1119, 15, 1073, NULL, CAST(0x0000A74C00AAEA4A AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1120, 15, 1074, NULL, CAST(0x0000A74C00AAEA4A AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1121, 15, 1075, NULL, CAST(0x0000A74C00AAEA4A AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1122, 15, 1076, NULL, CAST(0x0000A74C00AAEA4A AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1123, 15, 1077, NULL, CAST(0x0000A74C00AAEA4A AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1124, 15, 1078, NULL, CAST(0x0000A74C00AAEA4A AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1125, 15, 1079, NULL, CAST(0x0000A74C00AAEA4A AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1126, 14, 1075, NULL, CAST(0x0000A74C00AB021B AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1127, 14, 1076, NULL, CAST(0x0000A74C00AB021B AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1128, 14, 1077, NULL, CAST(0x0000A74C00AB021B AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1129, 14, 1078, NULL, CAST(0x0000A74C00AB021B AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1130, 14, 1079, NULL, CAST(0x0000A74C00AB021B AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1131, 12, 1080, NULL, CAST(0x0000A74D00AE3ED9 AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1132, 13, 1080, NULL, CAST(0x0000A74D00AE4C04 AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1133, 14, 1080, NULL, CAST(0x0000A74D00AE52A6 AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1134, 15, 1080, NULL, CAST(0x0000A74D00AE59F7 AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1135, 12, 1081, NULL, CAST(0x0000A74D00BC1E00 AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1136, 14, 1081, NULL, CAST(0x0000A74D00BC3978 AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1137, 15, 1081, NULL, CAST(0x0000A74D00BC549E AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1138, 14, 1062, NULL, CAST(0x0000A74E011F13A1 AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1139, 14, 1082, NULL, CAST(0x0000A74E011F13A1 AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1140, 15, 1082, NULL, CAST(0x0000A74E011F20E7 AS DateTime))
INSERT [dbo].[tb_Sys_RolePermission] ([rpId], [rpRId], [rpPId], [rpIsDel], [rpAddtime]) VALUES (1141, 12, 1082, NULL, CAST(0x0000A74E011F286E AS DateTime))
SET IDENTITY_INSERT [dbo].[tb_Sys_RolePermission] OFF
SET IDENTITY_INSERT [dbo].[tb_Sys_UserInfo] ON 

INSERT [dbo].[tb_Sys_UserInfo] ([Uid], [uDepid], [uLoginName], [uPwd], [uGender], [uIsDel], [uPost], [uRemark], [uAddtime], [uMobile], [CityId], [uName]) VALUES (1, 2, N'admin', N'4297f44b13955235245b2497399d7a93', N'女', 0, N'管理员', NULL, CAST(0x0000A6F300E4F8E9 AS DateTime), N'13088888888', N'100', N'admin')
INSERT [dbo].[tb_Sys_UserInfo] ([Uid], [uDepid], [uLoginName], [uPwd], [uGender], [uIsDel], [uPost], [uRemark], [uAddtime], [uMobile], [CityId], [uName]) VALUES (2, 1, N'test', N'e10adc3949ba59abbe56e057f20f883e', N'女', 0, N'123', NULL, CAST(0x0000A70001048CDD AS DateTime), N'13088888888', N'103', N'test')
INSERT [dbo].[tb_Sys_UserInfo] ([Uid], [uDepid], [uLoginName], [uPwd], [uGender], [uIsDel], [uPost], [uRemark], [uAddtime], [uMobile], [CityId], [uName]) VALUES (4, 8, N'xindi', N'e10adc3949ba59abbe56e057f20f883e', N'女', 0, N'普通员工', NULL, CAST(0x0000A73701078B92 AS DateTime), N'13712345678', N'100', N'辛迪')
INSERT [dbo].[tb_Sys_UserInfo] ([Uid], [uDepid], [uLoginName], [uPwd], [uGender], [uIsDel], [uPost], [uRemark], [uAddtime], [uMobile], [CityId], [uName]) VALUES (5, 1, N'liucong', N'e10adc3949ba59abbe56e057f20f883e', N'女', 0, N'程序员', NULL, NULL, N'13049484738', N'100', N'刘聪')
SET IDENTITY_INSERT [dbo].[tb_Sys_UserInfo] OFF
SET IDENTITY_INSERT [dbo].[tb_Sys_UserRole] ON 

INSERT [dbo].[tb_Sys_UserRole] ([urId], [urUid], [urRid], [urIsDel], [urAddTime]) VALUES (16, 2, 14, 0, CAST(0x0000A74C00A8FFF3 AS DateTime))
INSERT [dbo].[tb_Sys_UserRole] ([urId], [urUid], [urRid], [urIsDel], [urAddTime]) VALUES (17, 1, 12, 0, CAST(0x0000A74C00ABBAFE AS DateTime))
INSERT [dbo].[tb_Sys_UserRole] ([urId], [urUid], [urRid], [urIsDel], [urAddTime]) VALUES (19, 5, 13, 0, CAST(0x0000A74D01107384 AS DateTime))
SET IDENTITY_INSERT [dbo].[tb_Sys_UserRole] OFF
SET IDENTITY_INSERT [dbo].[tb_User_Info] ON 

INSERT [dbo].[tb_User_Info] ([uId], [HospId], [Name], [Female], [Birth], [Mobile], [Email], [ComeFrom], [IndustryType], [Height], [Weight], [WC], [Hipline], [Thigh_Cir], [Qq], [WeiXin], [BMI], [Enter_Code], [Self_code], [Cityid], [PicPath], [Myhope], [CreatorId], [Creator], [C_time], [isDel]) VALUES (1, 10, N'张飒', N'男', CAST(0xA83C0B00 AS Date), N'13701234567', NULL, N'1', N'1', 160, 70, 88, 85, NULL, N'111', N'222', 0.002734375, N'Ma9024', NULL, N'101', NULL, N'我要穿大学时代的牛仔裤', NULL, NULL, CAST(0x0000A74000A53B2D AS DateTime), 0)
INSERT [dbo].[tb_User_Info] ([uId], [HospId], [Name], [Female], [Birth], [Mobile], [Email], [ComeFrom], [IndustryType], [Height], [Weight], [WC], [Hipline], [Thigh_Cir], [Qq], [WeiXin], [BMI], [Enter_Code], [Self_code], [Cityid], [PicPath], [Myhope], [CreatorId], [Creator], [C_time], [isDel]) VALUES (2, 10, N'刘丽', N'女', CAST(0x00090B00 AS Date), N'13701234599', NULL, N'1', N'1', 160, 60, 70, 66, NULL, N'111', N'222', 23.43, N'Ma9024', NULL, N'100', NULL, N'腰围减少10厘米', NULL, NULL, CAST(0x0000A74000A53B2D AS DateTime), 0)
INSERT [dbo].[tb_User_Info] ([uId], [HospId], [Name], [Female], [Birth], [Mobile], [Email], [ComeFrom], [IndustryType], [Height], [Weight], [WC], [Hipline], [Thigh_Cir], [Qq], [WeiXin], [BMI], [Enter_Code], [Self_code], [Cityid], [PicPath], [Myhope], [CreatorId], [Creator], [C_time], [isDel]) VALUES (3, 11, N'434434', N'男', CAST(0xA73C0B00 AS Date), N'1323123123', N'7777777', N'3', N'3', 170, 62, 343, 33, 33, N'333', N'333333', 0.0021453287197231836, N'T434343', NULL, N'102', NULL, N'666666', NULL, NULL, CAST(0x0000A74601869B7A AS DateTime), 0)
INSERT [dbo].[tb_User_Info] ([uId], [HospId], [Name], [Female], [Birth], [Mobile], [Email], [ComeFrom], [IndustryType], [Height], [Weight], [WC], [Hipline], [Thigh_Cir], [Qq], [WeiXin], [BMI], [Enter_Code], [Self_code], [Cityid], [PicPath], [Myhope], [CreatorId], [Creator], [C_time], [isDel]) VALUES (4, 11, N'6666', N'男', NULL, N'666', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'104', NULL, NULL, NULL, NULL, CAST(0x0000A74601896104 AS DateTime), 0)
INSERT [dbo].[tb_User_Info] ([uId], [HospId], [Name], [Female], [Birth], [Mobile], [Email], [ComeFrom], [IndustryType], [Height], [Weight], [WC], [Hipline], [Thigh_Cir], [Qq], [WeiXin], [BMI], [Enter_Code], [Self_code], [Cityid], [PicPath], [Myhope], [CreatorId], [Creator], [C_time], [isDel]) VALUES (5, 12, N'6666', N'男', NULL, N'666', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'101', NULL, NULL, NULL, NULL, CAST(0x0000A7460189611D AS DateTime), 0)
INSERT [dbo].[tb_User_Info] ([uId], [HospId], [Name], [Female], [Birth], [Mobile], [Email], [ComeFrom], [IndustryType], [Height], [Weight], [WC], [Hipline], [Thigh_Cir], [Qq], [WeiXin], [BMI], [Enter_Code], [Self_code], [Cityid], [PicPath], [Myhope], [CreatorId], [Creator], [C_time], [isDel]) VALUES (6, 12, N'55', N'男', NULL, N'4444', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'103', NULL, NULL, NULL, NULL, CAST(0x0000A746018B48CB AS DateTime), 0)
INSERT [dbo].[tb_User_Info] ([uId], [HospId], [Name], [Female], [Birth], [Mobile], [Email], [ComeFrom], [IndustryType], [Height], [Weight], [WC], [Hipline], [Thigh_Cir], [Qq], [WeiXin], [BMI], [Enter_Code], [Self_code], [Cityid], [PicPath], [Myhope], [CreatorId], [Creator], [C_time], [isDel]) VALUES (7, 14, N'会员1', N'男', NULL, N'138493828749', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'102', NULL, NULL, 1, N'admin', CAST(0x0000A74D011EC2EF AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[tb_User_Info] OFF
ALTER TABLE [dbo].[tb_Consume_Log] ADD  CONSTRAINT [DF_T_consume_log_c_time]  DEFAULT (getdate()) FOR [c_time]
GO
ALTER TABLE [dbo].[tb_Dict] ADD  CONSTRAINT [DF_tb_Dict_C_Time]  DEFAULT (getdate()) FOR [C_Time]
GO
ALTER TABLE [dbo].[tb_Emp_Hos] ADD  CONSTRAINT [DF_T_emp_hos_c_time]  DEFAULT (getdate()) FOR [c_time]
GO
ALTER TABLE [dbo].[tb_Hei_Wei] ADD  CONSTRAINT [DF_tb_Hei_Wei_C_time]  DEFAULT (getdate()) FOR [C_time]
GO
ALTER TABLE [dbo].[tb_Hosp_Info] ADD  CONSTRAINT [DF_tb_Hosp_Info_C_Time]  DEFAULT (getdate()) FOR [C_Time]
GO
ALTER TABLE [dbo].[tb_Serv_Info] ADD  CONSTRAINT [DF_tb_Serv_Info_CTime]  DEFAULT (getdate()) FOR [CTime]
GO
ALTER TABLE [dbo].[tb_Serv_POF] ADD  CONSTRAINT [DF_T_Serv_POF_C_time]  DEFAULT (getdate()) FOR [C_time]
GO
ALTER TABLE [dbo].[tb_Sms_Down] ADD  CONSTRAINT [DF_tb_Sms_Down_c_time]  DEFAULT (getdate()) FOR [c_time]
GO
ALTER TABLE [dbo].[tb_Sms_report] ADD  CONSTRAINT [DF_tb_Sms_report_S_time]  DEFAULT (getdate()) FOR [S_time]
GO
ALTER TABLE [dbo].[tb_Sms_Tpl] ADD  CONSTRAINT [DF_T_smstpl_c_time]  DEFAULT (getdate()) FOR [c_time]
GO
ALTER TABLE [dbo].[tb_Sms_up] ADD  CONSTRAINT [DF_tb_Sms_up_C_time]  DEFAULT (getdate()) FOR [C_time]
GO
ALTER TABLE [dbo].[tb_Sys_Department] ADD  CONSTRAINT [DF_tb_Sys_Department_DepAddtime]  DEFAULT (getdate()) FOR [DepAddtime]
GO
ALTER TABLE [dbo].[tb_Sys_LoginLog] ADD  CONSTRAINT [DF_tb_Sys_LoginLog_loginTime]  DEFAULT (getdate()) FOR [loginTime]
GO
ALTER TABLE [dbo].[tb_Sys_MenuInfo] ADD  CONSTRAINT [DF_tb_Sys_MenuInfo_mAddtime]  DEFAULT (getdate()) FOR [mAddtime]
GO
ALTER TABLE [dbo].[tb_Sys_Permission] ADD  CONSTRAINT [DF_tb_Sys_Permission_pOperTime]  DEFAULT (getdate()) FOR [pOperTime]
GO
ALTER TABLE [dbo].[tb_Sys_Role] ADD  CONSTRAINT [DF_tb_Sys_Role_rAddTime]  DEFAULT (getdate()) FOR [rAddTime]
GO
ALTER TABLE [dbo].[tb_Sys_RolePermission] ADD  CONSTRAINT [DF_tb_Sys_RolePermission_rpAddtime]  DEFAULT (getdate()) FOR [rpAddtime]
GO
ALTER TABLE [dbo].[tb_Sys_UserInfo] ADD  CONSTRAINT [DF_tb_Sys_UserInfo_uAddtime]  DEFAULT (getdate()) FOR [uAddtime]
GO
ALTER TABLE [dbo].[tb_Sys_UserRole] ADD  CONSTRAINT [DF_tb_Sys_UserRole_urAddTime]  DEFAULT (getdate()) FOR [urAddTime]
GO
ALTER TABLE [dbo].[tb_Sys_UserVipPermission] ADD  CONSTRAINT [DF_tb_Sys_UserVipPermission_vipAddtime]  DEFAULT (getdate()) FOR [vipAddtime]
GO
ALTER TABLE [dbo].[tb_User_Account] ADD  CONSTRAINT [DF_T_User_Account_c_time]  DEFAULT (getdate()) FOR [c_time]
GO
ALTER TABLE [dbo].[tb_User_Buy_Rec] ADD  CONSTRAINT [DF_T_user_buy_rec_C_time]  DEFAULT (getdate()) FOR [C_time]
GO
ALTER TABLE [dbo].[tb_User_Dislike_Food] ADD  CONSTRAINT [DF_tb_user_dislike_food_C_time]  DEFAULT (getdate()) FOR [C_time]
GO
ALTER TABLE [dbo].[tb_User_Info] ADD  CONSTRAINT [DF_T_Userinfo_C_time]  DEFAULT (getdate()) FOR [C_time]
GO
ALTER TABLE [dbo].[tb_Weight_Chg] ADD  CONSTRAINT [DF_T_weight_chg_C_time]  DEFAULT (getdate()) FOR [C_time]
GO
ALTER TABLE [dbo].[tb_Weight_Chg_Self] ADD  CONSTRAINT [DF_T_Weight_Chg_Self_C_time]  DEFAULT (getdate()) FOR [C_time]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'仪器 0 点穴1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Consume_Log', @level2type=N'COLUMN',@level2name=N'flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'tb_hosp_info.id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Emp_Hos', @level2type=N'COLUMN',@level2name=N'hospid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'毫米计的升高' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Hei_Wei', @level2type=N'COLUMN',@level2name=N'Height'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标准体重（公斤计）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Hei_Wei', @level2type=N'COLUMN',@level2name=N'St_weight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'美学体重（公斤计）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Hei_Wei', @level2type=N'COLUMN',@level2name=N'Pt_weight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'腰围' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Hei_Wei', @level2type=N'COLUMN',@level2name=N'WC'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'臀围' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Hei_Wei', @level2type=N'COLUMN',@level2name=N'Hipline'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'大腿围' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Hei_Wei', @level2type=N'COLUMN',@level2name=N'Thigh_Cir'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'门店类别  见字典hosptype' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Hosp_Info', @level2type=N'COLUMN',@level2name=N'sType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'等级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Hosp_Info', @level2type=N'COLUMN',@level2name=N'grade'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务分类（见字典表）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Serv_Info', @level2type=N'COLUMN',@level2name=N'ServType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品价格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Serv_Info', @level2type=N'COLUMN',@level2name=N'price'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'仪器 0 点穴1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Serv_POF', @level2type=N'COLUMN',@level2name=N'TouchFlag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'疗程' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Serv_POF', @level2type=N'COLUMN',@level2name=N'Times'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'仪器 0 点穴1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_User_Account', @level2type=N'COLUMN',@level2name=N'TouchFlag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'购买门店id(tb_hosp_info.hospid)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_User_Buy_Rec', @level2type=N'COLUMN',@level2name=N'Buy_Hospid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'c' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_User_Buy_Rec', @level2type=N'COLUMN',@level2name=N'ProductPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户入会门店(tb_hosp_info.hospid)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_User_Info', @level2type=N'COLUMN',@level2name=N'HospId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_User_Info', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'性别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_User_Info', @level2type=N'COLUMN',@level2name=N'Female'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出生日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_User_Info', @level2type=N'COLUMN',@level2name=N'Birth'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_User_Info', @level2type=N'COLUMN',@level2name=N'Mobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'来源（见字典表）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_User_Info', @level2type=N'COLUMN',@level2name=N'ComeFrom'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户所在行业id（字典表）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_User_Info', @level2type=N'COLUMN',@level2name=N'IndustryType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'身高' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_User_Info', @level2type=N'COLUMN',@level2name=N'Height'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'体重' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_User_Info', @level2type=N'COLUMN',@level2name=N'Weight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'腰围' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_User_Info', @level2type=N'COLUMN',@level2name=N'WC'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'腿围' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_User_Info', @level2type=N'COLUMN',@level2name=N'Hipline'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'大腿围' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_User_Info', @level2type=N'COLUMN',@level2name=N'Thigh_Cir'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计算得到（体重(公斤 )/身高*身高） (身高用米计)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_User_Info', @level2type=N'COLUMN',@level2name=N'BMI'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'推荐码,客户是通过这个推荐码入会的' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_User_Info', @level2type=N'COLUMN',@level2name=N'Enter_Code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户自己的推荐码(系统自动计算,外部程序不处理此列)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_User_Info', @level2type=N'COLUMN',@level2name=N'Self_code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户所在城市' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_User_Info', @level2type=N'COLUMN',@level2name=N'Cityid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户减重期望（从字典表中获得的文字）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_User_Info', @level2type=N'COLUMN',@level2name=N'Myhope'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N't_userinfo.UID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Weight_Chg', @level2type=N'COLUMN',@level2name=N'uId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Weight_Chg', @level2type=N'COLUMN',@level2name=N'RecDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'体重' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Weight_Chg', @level2type=N'COLUMN',@level2name=N'Weight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'疗程' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Weight_Chg', @level2type=N'COLUMN',@level2name=N'BatchId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否点穴(1是，0否)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Weight_Chg', @level2type=N'COLUMN',@level2name=N'TouchFlag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1公司 2 自己' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Weight_Chg', @level2type=N'COLUMN',@level2name=N'self'
GO
USE [master]
GO
ALTER DATABASE [BXUU] SET  READ_WRITE 
GO
