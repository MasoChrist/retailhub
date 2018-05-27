CREATE TABLE [dbo].tabSetAttributiProdottoListino
(
    [IDProdottoListino] UNIQUEIDENTIFIER NOT NULL, 
    [IDGruppoAttributi] UNIQUEIDENTIFIER NOT NULL, 
    [Valore] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_tabSetAttributiProdottoListino] PRIMARY KEY ([IDProdottoListino],[IDGruppoAttributi]), 
    CONSTRAINT [FK_tabSetAttributiProdottoListino_tabProdottiListino] FOREIGN KEY (IdProdottolistino) REFERENCES tabprodottidilistino(id) ,
	CONSTRAINT [FK_tabSetAttributiProdottoListino_tabGruppiAttributi] FOREIGN KEY (IDgruppoAttributi) REFERENCES tabGruppoAttributi(id) 

)
