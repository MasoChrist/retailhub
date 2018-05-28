CREATE TABLE [dbo].tabProdottiDiListino
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [SKU] NVARCHAR(50) NOT NULL, 
    [IDProdotto] UNIQUEIDENTIFIER NOT NULL, 
	
    CONSTRAINT [FK_ProdottiDiListino_tabProdotti] FOREIGN KEY (IDProdotto) REFERENCES tabProdotti(ID)
)
