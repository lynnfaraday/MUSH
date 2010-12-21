USE [test]
GO

/****** Object:  Table [dbo].[Calls]    Script Date: 12/20/2010 23:12:52 ******/
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
	[ALS] [bit] NULL,
	[StateNumber] [nvarchar](50) NULL,
	[IncidentNumber] [int] NULL
) ON [PRIMARY]

GO


