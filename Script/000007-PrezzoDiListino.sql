CREATE TABLE [dbo].tabPrezzoProdottoDiListino
(
    [IDListino] UNIQUEIDENTIFIER NOT NULL, 
    [IDProdottoDiListino] UNIQUEIDENTIFIER NOT NULL, 
    [Prezzo] DECIMAL(20, 4) NULL, 
    [Maggiorazione] DECIMAL(20, 4) NULL, 
    [isPredefinito] BIT NOT NULL, 
    CONSTRAINT [PK_Table] PRIMARY KEY ([IDListino],[IDProdottoDiListino]), 
    CONSTRAINT [FK_tabPrezzoProdottoDiListino_ToListino] FOREIGN KEY (IDListino) REFERENCES tabListini(ID),
	CONSTRAINT [FK_tabPrezzoProdottoDiListino_ToProdottoDiListino] FOREIGN KEY (IDProdottoDiListino) REFERENCES tabProdottidiListino(ID),
)
