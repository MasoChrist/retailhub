CREATE TABLE [dbo].[tabProdottiToGruppoAttributi]
(
	[ID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[CreationDate] DATETIME NULL, 
    [LastModifiedDate] DATETIME NOT NULL, 
    [LastModifiedBy] NVARCHAR(50) NOT NULL, 
    [IDProdotto] UNIQUEIDENTIFIER NOT NULL, 
    [IDGruppoAttributi] UNIQUEIDENTIFIER NOT NULL, 
    [Valore] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_tabProdottiToGruppoAttributi_tabProdotti] FOREIGN KEY (IDProdotto) REFERENCES tabProdotti(ID), 
    CONSTRAINT [FK_tabProdottiToGruppoAttributi_tabGruppoAttributi] FOREIGN KEY (IDGruppoAttributi) REFERENCES tabGruppoAttributi(ID),
)
