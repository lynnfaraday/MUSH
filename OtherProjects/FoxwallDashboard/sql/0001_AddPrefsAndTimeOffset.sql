USE [foxwall]
GO

/****** Object:  Table [dbo].[Preferences]    Script Date: 12/29/2010 07:36:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Preferences](
	[ID] [uniqueidentifier] NOT NULL PRIMARY KEY,
	[Name] [nvarchar](50) NOT NULL,
	[Value] [nvarchar](50) NOT NULL
) ON [PRIMARY]

GO

INSERT INTO [foxwall].[dbo].[Preferences]
           ([ID]
           ,[Name]
           ,[Value])
     VALUES ('8A9100EA-E80B-4D26-84A5-220D902654F0', 'ServerToLocalOffsetHours', '0')
GO
