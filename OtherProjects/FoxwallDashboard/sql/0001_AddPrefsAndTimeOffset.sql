
USE [foxwall]
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
             '?,?JID??#???!??$Jk', 'true', 'true', '-1347497617')
GO

INSERT INTO [foxwall].[dbo].[Preferences]
           ([ID]
           ,[Name]
           ,[Value])
     VALUES ('8A9100EA-E80B-4D26-84A5-220D902654F0', 'TimeOffset', '0')
GO

ALTER TABLE [foxwall].[dbo].[Calls] ADD TimeOffset int

GO

UPDATE [foxwall].[dbo].[Calls] 
SET [TimeOffset] = 0

GO