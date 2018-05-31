namespace EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inheritance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tabGruppoAttributi",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Descrizione = c.String(nullable: false),
                        DescrizioneBreve = c.String(nullable: false, maxLength: 50),
                        CreatorIDentifier = c.Guid(),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tabProdottiToGruppoAttributi",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        IDProdotto = c.Guid(nullable: false),
                        IDGruppoAttributi = c.Guid(nullable: false),
                        Valore = c.String(),
                        CreatorIDentifier = c.Guid(),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.tabProdotti", t => t.IDProdotto)
                .ForeignKey("dbo.tabGruppoAttributi", t => t.IDGruppoAttributi)
                .Index(t => t.IDProdotto)
                .Index(t => t.IDGruppoAttributi);
            
            CreateTable(
                "dbo.tabProdotti",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Descrizione = c.String(nullable: false),
                        DescrizioneBreve = c.String(nullable: false),
                        CreatorIDentifier = c.Guid(),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.tabProdottiDiListino",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SKU = c.String(nullable: false, maxLength: 50),
                        IDProdotto = c.Guid(nullable: false),
                        CreatorIDentifier = c.Guid(),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tabProdotti", t => t.IDProdotto)
                .Index(t => t.IDProdotto);
            
            CreateTable(
                "dbo.tabPrezzoProdottoDiListino",
                c => new
                    {
                        IDListino = c.Guid(nullable: false),
                        IDProdottoDiListino = c.Guid(nullable: false),
                        Prezzo = c.Decimal(precision: 20, scale: 4),
                        Maggiorazione = c.Decimal(precision: 20, scale: 4),
                        isPredefinito = c.Boolean(nullable: false),
                        CreatorIDentifier = c.Guid(),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => new { t.IDListino, t.IDProdottoDiListino })
                .ForeignKey("dbo.tabListini", t => t.IDListino)
                .ForeignKey("dbo.tabProdottiDiListino", t => t.IDProdottoDiListino)
                .Index(t => t.IDListino)
                .Index(t => t.IDProdottoDiListino);
            
            CreateTable(
                "dbo.tabListini",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 50),
                        DataInizioValidita = c.DateTime(),
                        DataFineValidita = c.DateTime(),
                        TipoListino = c.Int(nullable: false),
                        CreatorIDentifier = c.Guid(),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.tabSetAttributiProdottoListino",
                c => new
                    {
                        IDProdottoListino = c.Guid(nullable: false),
                        IDGruppoAttributi = c.Guid(nullable: false),
                        Valore = c.String(),
                        CreatorIDentifier = c.Guid(),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => new { t.IDProdottoListino, t.IDGruppoAttributi })
                .ForeignKey("dbo.tabProdottiDiListino", t => t.IDProdottoListino)
                .ForeignKey("dbo.tabGruppoAttributi", t => t.IDGruppoAttributi)
                .Index(t => t.IDProdottoListino)
                .Index(t => t.IDGruppoAttributi);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tabSetAttributiProdottoListino", "IDGruppoAttributi", "dbo.tabGruppoAttributi");
            DropForeignKey("dbo.tabProdottiToGruppoAttributi", "IDGruppoAttributi", "dbo.tabGruppoAttributi");
            DropForeignKey("dbo.tabProdottiToGruppoAttributi", "IDProdotto", "dbo.tabProdotti");
            DropForeignKey("dbo.tabProdottiDiListino", "IDProdotto", "dbo.tabProdotti");
            DropForeignKey("dbo.tabSetAttributiProdottoListino", "IDProdottoListino", "dbo.tabProdottiDiListino");
            DropForeignKey("dbo.tabPrezzoProdottoDiListino", "IDProdottoDiListino", "dbo.tabProdottiDiListino");
            DropForeignKey("dbo.tabPrezzoProdottoDiListino", "IDListino", "dbo.tabListini");
            DropIndex("dbo.tabSetAttributiProdottoListino", new[] { "IDGruppoAttributi" });
            DropIndex("dbo.tabSetAttributiProdottoListino", new[] { "IDProdottoListino" });
            DropIndex("dbo.tabPrezzoProdottoDiListino", new[] { "IDProdottoDiListino" });
            DropIndex("dbo.tabPrezzoProdottoDiListino", new[] { "IDListino" });
            DropIndex("dbo.tabProdottiDiListino", new[] { "IDProdotto" });
            DropIndex("dbo.tabProdottiToGruppoAttributi", new[] { "IDGruppoAttributi" });
            DropIndex("dbo.tabProdottiToGruppoAttributi", new[] { "IDProdotto" });
            DropTable("dbo.tabSetAttributiProdottoListino");
            DropTable("dbo.tabListini");
            DropTable("dbo.tabPrezzoProdottoDiListino");
            DropTable("dbo.tabProdottiDiListino");
            DropTable("dbo.tabProdotti");
            DropTable("dbo.tabProdottiToGruppoAttributi");
            DropTable("dbo.tabGruppoAttributi");
        }
    }
}
