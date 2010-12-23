USE [test]
GO

/****** Object:  Table [dbo].[Calls]    Script Date: 12/22/2010 19:24:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Calls](
	[CallID] [uniqueidentifier] NOT NULL,
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


USE [test]
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

USE [test]
GO

/****** Object:  Table [dbo].[People]    Script Date: 12/22/2010 19:28:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[People](
	[ID] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Administrator] [bit] NOT NULL,
	[Active] [bit] NOT NULL
) ON [PRIMARY]

GO


USE [test]
GO

/****** Object:  Table [dbo].[CallPersonAssociation]    Script Date: 12/23/2010 11:13:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CallPersonAssociation](
	[ID] [uniqueidentifier] NOT NULL,
	[PersonID] [uniqueidentifier] NOT NULL,
	[CallID] [uniqueidentifier] NOT NULL
) ON [PRIMARY]

GO


