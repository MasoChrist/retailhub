CREATE TABLE [dbo].[tabProdotti]
(
	[ID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Descrizione] NVARCHAR(MAX) NOT NULL, 
    [DescrizioneBreve] NVARCHAR(MAX) NOT NULL, 
    [CreationDate] DATETIME NULL, 
    [LastModifiedDate] DATETIME NULL, 
    [LastModifiedBy] NVARCHAR(50) NULL,

)
