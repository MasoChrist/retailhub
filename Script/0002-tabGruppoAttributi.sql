CREATE TABLE [dbo].[tabGruppoAttributi]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Descrizione] NVARCHAR(MAX) NOT NULL, 
    [DescrizioneBreve] NVARCHAR(50) NOT NULL,
	[CreationDate] DATETIME NULL, 
    [LastModifiedDate] DATETIME NULL, 
    [LastModifiedBy] NVARCHAR(50) NULL,
)
