CREATE TABLE [dbo].tabListini
(
	[ID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[CreationDate] DATETIME NULL, 
    [LastModifiedDate] DATETIME NOT NULL, 
    [LastModifiedBy] NVARCHAR(50) NOT NULL, 
    [Nome] NVARCHAR(50) NOT NULL, 
    [DataInizioValidita] DATETIME NULL, 
    [DataFineValidita] DATETIME NULL, 
    [TipoListino] INT NOT NULL, 
)
