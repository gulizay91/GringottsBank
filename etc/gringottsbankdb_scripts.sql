USE [GringottsBankDb]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 2/5/2022 4:43:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [uniqueidentifier] NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[IBAN] [nvarchar](32) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Currency] [nvarchar](max) NOT NULL,
	[Balance] [decimal](18, 2) NOT NULL,
	[AuditInfo_CreatedBy] [uniqueidentifier] NOT NULL,
	[AuditInfo_Created] [datetime2](7) NOT NULL,
	[AuditInfo_LastModifiedBy] [uniqueidentifier] NULL,
	[AuditInfo_LastModified] [datetime2](7) NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 2/5/2022 4:43:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [uniqueidentifier] NOT NULL,
	[IdentityNumber] [nvarchar](11) NOT NULL,
	[FirstName] [nvarchar](200) NOT NULL,
	[FamilyName] [nvarchar](250) NOT NULL,
	[AuditInfo_CreatedBy] [uniqueidentifier] NOT NULL,
	[AuditInfo_Created] [datetime2](7) NOT NULL,
	[AuditInfo_LastModifiedBy] [uniqueidentifier] NULL,
	[AuditInfo_LastModified] [datetime2](7) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
