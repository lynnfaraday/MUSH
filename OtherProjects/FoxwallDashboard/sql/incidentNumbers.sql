USE [test]
GO

/****** Object:  Table [dbo].[IncidentNumbers]    Script Date: 12/21/2010 11:31:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[IncidentNumbers](
	[Year] [int] NOT NULL,
	[LastIncident] [int] NOT NULL,
 CONSTRAINT [PK_IncidentNumbers] PRIMARY KEY CLUSTERED 
(
	[Year] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


