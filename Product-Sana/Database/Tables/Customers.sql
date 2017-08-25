CREATE TABLE [dbo].[Customers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[IdNumber] VARCHAR(20) NOT NULL , 
    [FirstName] VARCHAR(50) NOT NULL, 
    [MiddleName] VARCHAR(50) NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [PhoneNumber] VARCHAR(50) NULL, 
    [Address] VARCHAR(250) NULL, 
    [Email] VARCHAR(250) NULL, 
    CONSTRAINT [AK_Customers_IdNumber] UNIQUE (IdNumber)
)
