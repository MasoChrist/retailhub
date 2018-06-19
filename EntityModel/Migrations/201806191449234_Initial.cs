namespace EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.baseEntityTables",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CreatorIDentifier = c.Guid(),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 50),
                        isDeleted = c.Boolean(nullable: false),
                        Nome = c.String(),
                        IdCategoriaPadre = c.Guid(),
                        Type = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.baseEntityTables", t => t.IdCategoriaPadre)
                .Index(t => t.IdCategoriaPadre);
            
            CreateTable(
                "dbo.tabProperties",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropertyName = c.String(),
                        PropertyValue = c.String(),
                        EntityID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.baseEntityTables", t => t.EntityID)
                .Index(t => t.EntityID);
            
            CreateTable(
                "dbo.tabGruppoAttributitabProdottis",
                c => new
                    {
                        tabGruppoAttributi_ID = c.Guid(nullable: false),
                        tabProdotti_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.tabGruppoAttributi_ID, t.tabProdotti_ID })
                .ForeignKey("dbo.tabGruppoAttributi", t => t.tabGruppoAttributi_ID)
                .ForeignKey("dbo.tabProdotti", t => t.tabProdotti_ID)
                .Index(t => t.tabGruppoAttributi_ID)
                .Index(t => t.tabProdotti_ID);
            
            CreateTable(
                "dbo.tabGruppoAttributi",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Descrizione = c.String(nullable: false),
                        DescrizioneBreve = c.String(nullable: false, maxLength: 50),
                        Type = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.baseEntityTables", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.tabListini",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 50),
                        DataInizioValidita = c.DateTime(),
                        DataFineValidita = c.DateTime(),
                        TipoListino = c.Int(nullable: false),
                        Type = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.baseEntityTables", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.tabPrezzoProdottoDiListino",
                c => new
                    {
                        IDListino = c.Guid(nullable: false),
                        IDProdottoDiListino = c.Guid(nullable: false),
                        ID = c.Guid(nullable: false),
                        Prezzo = c.Decimal(precision: 20, scale: 4),
                        Maggiorazione = c.Decimal(precision: 20, scale: 4),
                        isPredefinito = c.Boolean(nullable: false),
                        Type = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.baseEntityTables", t => t.ID)
                .ForeignKey("dbo.tabListini", t => t.IDListino)
                .ForeignKey("dbo.tabProdottiDiListino", t => t.IDProdottoDiListino)
                .Index(t => t.IDListino)
                .Index(t => t.IDProdottoDiListino)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.tabProdotti",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Descrizione = c.String(nullable: false),
                        DescrizioneBreve = c.String(nullable: false),
                        CodiceArticolo = c.String(nullable: false),
                        IdCategoria = c.Guid(),
                        Type = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.baseEntityTables", t => t.ID)
                .ForeignKey("dbo.baseEntityTables", t => t.IdCategoria)
                .Index(t => t.ID)
                .Index(t => t.IdCategoria);
            
            CreateTable(
                "dbo.tabProdottiDiListino",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        SKU = c.String(nullable: false, maxLength: 50),
                        IDProdotto = c.Guid(nullable: false),
                        Type = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.baseEntityTables", t => t.ID)
                .ForeignKey("dbo.tabProdotti", t => t.IDProdotto)
                .Index(t => t.ID)
                .Index(t => t.IDProdotto);
            
            CreateTable(
                "dbo.tabProdottiToGruppoAttributi",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        tabGruppoAttributi_ID = c.Guid(),
                        tabProdotti_ID = c.Guid(),
                        IDProdotto = c.Guid(nullable: false),
                        IDGruppoAttributi = c.Guid(nullable: false),
                        Valore = c.String(),
                        Type = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.baseEntityTables", t => t.ID)
                .ForeignKey("dbo.tabGruppoAttributi", t => t.tabGruppoAttributi_ID)
                .ForeignKey("dbo.tabProdotti", t => t.tabProdotti_ID)
                .Index(t => t.ID)
                .Index(t => t.tabGruppoAttributi_ID)
                .Index(t => t.tabProdotti_ID);
            
            CreateTable(
                "dbo.tabSetAttributiProdottoListino",
                c => new
                    {
                        IDProdottoListino = c.Guid(nullable: false),
                        IDGruppoAttributi = c.Guid(nullable: false),
                        ID = c.Guid(nullable: false),
                        tabGruppoAttributi_ID = c.Guid(),
                        Valore = c.String(),
                        Type = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.baseEntityTables", t => t.ID)
                .ForeignKey("dbo.tabGruppoAttributi", t => t.tabGruppoAttributi_ID)
                .ForeignKey("dbo.tabProdottiDiListino", t => t.IDProdottoListino)
                .Index(t => t.IDProdottoListino)
                .Index(t => t.ID)
                .Index(t => t.tabGruppoAttributi_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tabSetAttributiProdottoListino", "IDProdottoListino", "dbo.tabProdottiDiListino");
            DropForeignKey("dbo.tabSetAttributiProdottoListino", "tabGruppoAttributi_ID", "dbo.tabGruppoAttributi");
            DropForeignKey("dbo.tabSetAttributiProdottoListino", "ID", "dbo.baseEntityTables");
            DropForeignKey("dbo.tabProdottiToGruppoAttributi", "tabProdotti_ID", "dbo.tabProdotti");
            DropForeignKey("dbo.tabProdottiToGruppoAttributi", "tabGruppoAttributi_ID", "dbo.tabGruppoAttributi");
            DropForeignKey("dbo.tabProdottiToGruppoAttributi", "ID", "dbo.baseEntityTables");
            DropForeignKey("dbo.tabProdottiDiListino", "IDProdotto", "dbo.tabProdotti");
            DropForeignKey("dbo.tabProdottiDiListino", "ID", "dbo.baseEntityTables");
            DropForeignKey("dbo.tabProdotti", "IdCategoria", "dbo.baseEntityTables");
            DropForeignKey("dbo.tabProdotti", "ID", "dbo.baseEntityTables");
            DropForeignKey("dbo.tabPrezzoProdottoDiListino", "IDProdottoDiListino", "dbo.tabProdottiDiListino");
            DropForeignKey("dbo.tabPrezzoProdottoDiListino", "IDListino", "dbo.tabListini");
            DropForeignKey("dbo.tabPrezzoProdottoDiListino", "ID", "dbo.baseEntityTables");
            DropForeignKey("dbo.tabListini", "ID", "dbo.baseEntityTables");
            DropForeignKey("dbo.tabGruppoAttributi", "ID", "dbo.baseEntityTables");
            DropForeignKey("dbo.tabProperties", "EntityID", "dbo.baseEntityTables");
            DropForeignKey("dbo.tabGruppoAttributitabProdottis", "tabProdotti_ID", "dbo.tabProdotti");
            DropForeignKey("dbo.tabGruppoAttributitabProdottis", "tabGruppoAttributi_ID", "dbo.tabGruppoAttributi");
            DropForeignKey("dbo.baseEntityTables", "IdCategoriaPadre", "dbo.baseEntityTables");
            DropIndex("dbo.tabSetAttributiProdottoListino", new[] { "tabGruppoAttributi_ID" });
            DropIndex("dbo.tabSetAttributiProdottoListino", new[] { "ID" });
            DropIndex("dbo.tabSetAttributiProdottoListino", new[] { "IDProdottoListino" });
            DropIndex("dbo.tabProdottiToGruppoAttributi", new[] { "tabProdotti_ID" });
            DropIndex("dbo.tabProdottiToGruppoAttributi", new[] { "tabGruppoAttributi_ID" });
            DropIndex("dbo.tabProdottiToGruppoAttributi", new[] { "ID" });
            DropIndex("dbo.tabProdottiDiListino", new[] { "IDProdotto" });
            DropIndex("dbo.tabProdottiDiListino", new[] { "ID" });
            DropIndex("dbo.tabProdotti", new[] { "IdCategoria" });
            DropIndex("dbo.tabProdotti", new[] { "ID" });
            DropIndex("dbo.tabPrezzoProdottoDiListino", new[] { "ID" });
            DropIndex("dbo.tabPrezzoProdottoDiListino", new[] { "IDProdottoDiListino" });
            DropIndex("dbo.tabPrezzoProdottoDiListino", new[] { "IDListino" });
            DropIndex("dbo.tabListini", new[] { "ID" });
            DropIndex("dbo.tabGruppoAttributi", new[] { "ID" });
            DropIndex("dbo.tabGruppoAttributitabProdottis", new[] { "tabProdotti_ID" });
            DropIndex("dbo.tabGruppoAttributitabProdottis", new[] { "tabGruppoAttributi_ID" });
            DropIndex("dbo.tabProperties", new[] { "EntityID" });
            DropIndex("dbo.baseEntityTables", new[] { "IdCategoriaPadre" });
            DropTable("dbo.tabSetAttributiProdottoListino");
            DropTable("dbo.tabProdottiToGruppoAttributi");
            DropTable("dbo.tabProdottiDiListino");
            DropTable("dbo.tabProdotti");
            DropTable("dbo.tabPrezzoProdottoDiListino");
            DropTable("dbo.tabListini");
            DropTable("dbo.tabGruppoAttributi");
            DropTable("dbo.tabGruppoAttributitabProdottis");
            DropTable("dbo.tabProperties");
            DropTable("dbo.baseEntityTables");
        }
    }
}
