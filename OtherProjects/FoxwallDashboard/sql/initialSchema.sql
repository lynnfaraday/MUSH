USE [foxwall2]
GO

/****** Object:  Table [dbo].[Calls]    Script Date: 12/22/2010 19:24:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Calls](
	[CallID] [uniqueidentifier] NOT NULL PRIMARY KEY,
	[Dispatched] [datetime] NULL,
	[Location] [nvarchar](200) NULL,
	[Borough] [nvarchar](50) NULL,
	[ChiefComplaint] [nvarchar](50) NULL,
	[Age] [int] NULL,
	[AgeUnits] [nvarchar](10) NULL,
	[Disposition] [nvarchar](50) NULL,
	[StateNumber] [nvarchar](50) NULL,
	[IncidentNumber] [int] NULL
) ON [PRIMARY]


GO


USE [foxwall2]
GO

/****** Object:  Table [dbo].[YearlyIncidents]    Script Date: 12/22/2010 19:25:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[YearlyIncidents](
	[ID] [uniqueidentifier] NOT NULL,
	[Year] [int] NOT NULL,
	[LastIncident] [int] NOT NULL,
 CONSTRAINT [PK_YearlyIncidents] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
USE [foxwall2]
GO

/****** Object:  Table [dbo].[People]    Script Date: 12/28/2010 00:38:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[People](
	[ID] [uniqueidentifier] NOT NULL PRIMARY KEY,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Administrator] [bit] NOT NULL,
	[Active] [bit] NOT NULL,
	[Salt] [int] NULL
) ON [PRIMARY]

GO



USE [foxwall2]
GO

/****** Object:  Table [dbo].[CallPersonAssociation]    Script Date: 12/23/2010 11:13:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CallPersonAssociation](
	[ID] [uniqueidentifier] NOT NULL PRIMARY KEY,
	[PersonID] [uniqueidentifier] NOT NULL,
	[CallID] [uniqueidentifier] NOT NULL
) ON [PRIMARY]

GO



USE [foxwall2]
GO
INSERT INTO [dbo].[People]
           ([ID]
           ,[FirstName]
           ,[LastName]
           ,[Username]
           ,[Password]
           ,[Administrator]
           ,[Active]
           ,[Salt])
     VALUES ('8A9100EA-E80B-4D26-84A5-220D902654F0', 'Default', 'Administrator', 'admin', 
             '?,?JID??#???!??$Jk', 'true', 'true', '-1347497617',)
GO
