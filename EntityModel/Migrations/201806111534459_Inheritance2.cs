namespace EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inheritance2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.tabPrezzoProdottoDiListino");
            DropPrimaryKey("dbo.tabSetAttributiProdottoListino");
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.baseEntityTables", t => t.IdCategoriaPadre)
                .Index(t => t.IdCategoriaPadre);
            
            CreateTable(
                "dbo.baseEntityTabletabProperties",
                c => new
                    {
                        baseEntityTable_ID = c.Guid(nullable: false),
                        tabProperties_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.baseEntityTable_ID, t.tabProperties_ID })
                .ForeignKey("dbo.baseEntityTables", t => t.baseEntityTable_ID)
                .ForeignKey("dbo.tabProperties", t => t.tabProperties_ID)
                .Index(t => t.baseEntityTable_ID)
                .Index(t => t.tabProperties_ID);
            
            CreateTable(
                "dbo.tabProperties",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropertyName = c.String(),
                        PropertyValue = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.baseEntityTables", t => t.ID)
                .Index(t => t.ID);
            
            AddColumn("dbo.tabProdotti", "CodiceArticolo", c => c.String(nullable: false));
            AddColumn("dbo.tabPrezzoProdottoDiListino", "ID", c => c.Guid(nullable: false));
            AddColumn("dbo.tabSetAttributiProdottoListino", "ID", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.tabPrezzoProdottoDiListino", "ID");
            AddPrimaryKey("dbo.tabSetAttributiProdottoListino", "ID");
            CreateIndex("dbo.tabGruppoAttributi", "ID");
            CreateIndex("dbo.tabListini", "ID");
            CreateIndex("dbo.tabPrezzoProdottoDiListino", "ID");
            CreateIndex("dbo.tabProdotti", "ID");
            CreateIndex("dbo.tabProdottiDiListino", "ID");
            CreateIndex("dbo.tabProdottiToGruppoAttributi", "ID");
            CreateIndex("dbo.tabSetAttributiProdottoListino", "ID");
            AddForeignKey("dbo.tabGruppoAttributi", "ID", "dbo.baseEntityTables", "ID");
            AddForeignKey("dbo.tabListini", "ID", "dbo.baseEntityTables", "ID");
            AddForeignKey("dbo.tabPrezzoProdottoDiListino", "ID", "dbo.baseEntityTables", "ID");
            AddForeignKey("dbo.tabProdotti", "ID", "dbo.baseEntityTables", "ID");
            AddForeignKey("dbo.tabProdottiDiListino", "ID", "dbo.baseEntityTables", "ID");
            AddForeignKey("dbo.tabProdottiToGruppoAttributi", "ID", "dbo.baseEntityTables", "ID");
            AddForeignKey("dbo.tabSetAttributiProdottoListino", "ID", "dbo.baseEntityTables", "ID");
            DropColumn("dbo.tabGruppoAttributi", "CreatorIDentifier");
            DropColumn("dbo.tabGruppoAttributi", "CreationDate");
            DropColumn("dbo.tabGruppoAttributi", "LastModifiedDate");
            DropColumn("dbo.tabGruppoAttributi", "LastModifiedBy");
            DropColumn("dbo.tabGruppoAttributi", "isDeleted");
            DropColumn("dbo.tabProdottiToGruppoAttributi", "CreatorIDentifier");
            DropColumn("dbo.tabProdottiToGruppoAttributi", "CreationDate");
            DropColumn("dbo.tabProdottiToGruppoAttributi", "LastModifiedDate");
            DropColumn("dbo.tabProdottiToGruppoAttributi", "LastModifiedBy");
            DropColumn("dbo.tabProdottiToGruppoAttributi", "isDeleted");
            DropColumn("dbo.tabProdotti", "CreatorIDentifier");
            DropColumn("dbo.tabProdotti", "CreationDate");
            DropColumn("dbo.tabProdotti", "LastModifiedDate");
            DropColumn("dbo.tabProdotti", "LastModifiedBy");
            DropColumn("dbo.tabProdotti", "isDeleted");
            DropColumn("dbo.tabProdottiDiListino", "CreatorIDentifier");
            DropColumn("dbo.tabProdottiDiListino", "CreationDate");
            DropColumn("dbo.tabProdottiDiListino", "LastModifiedDate");
            DropColumn("dbo.tabProdottiDiListino", "LastModifiedBy");
            DropColumn("dbo.tabProdottiDiListino", "isDeleted");
            DropColumn("dbo.tabPrezzoProdottoDiListino", "CreatorIDentifier");
            DropColumn("dbo.tabPrezzoProdottoDiListino", "CreationDate");
            DropColumn("dbo.tabPrezzoProdottoDiListino", "LastModifiedDate");
            DropColumn("dbo.tabPrezzoProdottoDiListino", "LastModifiedBy");
            DropColumn("dbo.tabPrezzoProdottoDiListino", "isDeleted");
            DropColumn("dbo.tabListini", "CreatorIDentifier");
            DropColumn("dbo.tabListini", "CreationDate");
            DropColumn("dbo.tabListini", "LastModifiedDate");
            DropColumn("dbo.tabListini", "LastModifiedBy");
            DropColumn("dbo.tabListini", "isDeleted");
            DropColumn("dbo.tabSetAttributiProdottoListino", "CreatorIDentifier");
            DropColumn("dbo.tabSetAttributiProdottoListino", "CreationDate");
            DropColumn("dbo.tabSetAttributiProdottoListino", "LastModifiedDate");
            DropColumn("dbo.tabSetAttributiProdottoListino", "LastModifiedBy");
            DropColumn("dbo.tabSetAttributiProdottoListino", "isDeleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tabSetAttributiProdottoListino", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.tabSetAttributiProdottoListino", "LastModifiedBy", c => c.String(maxLength: 50));
            AddColumn("dbo.tabSetAttributiProdottoListino", "LastModifiedDate", c => c.DateTime());
            AddColumn("dbo.tabSetAttributiProdottoListino", "CreationDate", c => c.DateTime());
            AddColumn("dbo.tabSetAttributiProdottoListino", "CreatorIDentifier", c => c.Guid());
            AddColumn("dbo.tabListini", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.tabListini", "LastModifiedBy", c => c.String(maxLength: 50));
            AddColumn("dbo.tabListini", "LastModifiedDate", c => c.DateTime());
            AddColumn("dbo.tabListini", "CreationDate", c => c.DateTime());
            AddColumn("dbo.tabListini", "CreatorIDentifier", c => c.Guid());
            AddColumn("dbo.tabPrezzoProdottoDiListino", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.tabPrezzoProdottoDiListino", "LastModifiedBy", c => c.String(maxLength: 50));
            AddColumn("dbo.tabPrezzoProdottoDiListino", "LastModifiedDate", c => c.DateTime());
            AddColumn("dbo.tabPrezzoProdottoDiListino", "CreationDate", c => c.DateTime());
            AddColumn("dbo.tabPrezzoProdottoDiListino", "CreatorIDentifier", c => c.Guid());
            AddColumn("dbo.tabProdottiDiListino", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.tabProdottiDiListino", "LastModifiedBy", c => c.String(maxLength: 50));
            AddColumn("dbo.tabProdottiDiListino", "LastModifiedDate", c => c.DateTime());
            AddColumn("dbo.tabProdottiDiListino", "CreationDate", c => c.DateTime());
            AddColumn("dbo.tabProdottiDiListino", "CreatorIDentifier", c => c.Guid());
            AddColumn("dbo.tabProdotti", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.tabProdotti", "LastModifiedBy", c => c.String(maxLength: 50));
            AddColumn("dbo.tabProdotti", "LastModifiedDate", c => c.DateTime());
            AddColumn("dbo.tabProdotti", "CreationDate", c => c.DateTime());
            AddColumn("dbo.tabProdotti", "CreatorIDentifier", c => c.Guid());
            AddColumn("dbo.tabProdottiToGruppoAttributi", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.tabProdottiToGruppoAttributi", "LastModifiedBy", c => c.String(maxLength: 50));
            AddColumn("dbo.tabProdottiToGruppoAttributi", "LastModifiedDate", c => c.DateTime());
            AddColumn("dbo.tabProdottiToGruppoAttributi", "CreationDate", c => c.DateTime());
            AddColumn("dbo.tabProdottiToGruppoAttributi", "CreatorIDentifier", c => c.Guid());
            AddColumn("dbo.tabGruppoAttributi", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.tabGruppoAttributi", "LastModifiedBy", c => c.String(maxLength: 50));
            AddColumn("dbo.tabGruppoAttributi", "LastModifiedDate", c => c.DateTime());
            AddColumn("dbo.tabGruppoAttributi", "CreationDate", c => c.DateTime());
            AddColumn("dbo.tabGruppoAttributi", "CreatorIDentifier", c => c.Guid());
            DropForeignKey("dbo.tabSetAttributiProdottoListino", "ID", "dbo.baseEntityTables");
            DropForeignKey("dbo.tabProperties", "ID", "dbo.baseEntityTables");
            DropForeignKey("dbo.tabProdottiToGruppoAttributi", "ID", "dbo.baseEntityTables");
            DropForeignKey("dbo.tabProdottiDiListino", "ID", "dbo.baseEntityTables");
            DropForeignKey("dbo.tabProdotti", "ID", "dbo.baseEntityTables");
            DropForeignKey("dbo.tabPrezzoProdottoDiListino", "ID", "dbo.baseEntityTables");
            DropForeignKey("dbo.tabListini", "ID", "dbo.baseEntityTables");
            DropForeignKey("dbo.tabGruppoAttributi", "ID", "dbo.baseEntityTables");
            DropForeignKey("dbo.baseEntityTabletabProperties", "tabProperties_ID", "dbo.tabProperties");
            DropForeignKey("dbo.baseEntityTabletabProperties", "baseEntityTable_ID", "dbo.baseEntityTables");
            DropForeignKey("dbo.baseEntityTables", "IdCategoriaPadre", "dbo.baseEntityTables");
            DropIndex("dbo.tabSetAttributiProdottoListino", new[] { "ID" });
            DropIndex("dbo.tabProperties", new[] { "ID" });
            DropIndex("dbo.tabProdottiToGruppoAttributi", new[] { "ID" });
            DropIndex("dbo.tabProdottiDiListino", new[] { "ID" });
            DropIndex("dbo.tabProdotti", new[] { "ID" });
            DropIndex("dbo.tabPrezzoProdottoDiListino", new[] { "ID" });
            DropIndex("dbo.tabListini", new[] { "ID" });
            DropIndex("dbo.tabGruppoAttributi", new[] { "ID" });
            DropIndex("dbo.baseEntityTabletabProperties", new[] { "tabProperties_ID" });
            DropIndex("dbo.baseEntityTabletabProperties", new[] { "baseEntityTable_ID" });
            DropIndex("dbo.baseEntityTables", new[] { "IdCategoriaPadre" });
            DropPrimaryKey("dbo.tabSetAttributiProdottoListino");
            DropPrimaryKey("dbo.tabPrezzoProdottoDiListino");
            DropColumn("dbo.tabSetAttributiProdottoListino", "ID");
            DropColumn("dbo.tabPrezzoProdottoDiListino", "ID");
            DropColumn("dbo.tabProdotti", "CodiceArticolo");
            DropTable("dbo.tabProperties");
            DropTable("dbo.baseEntityTabletabProperties");
            DropTable("dbo.baseEntityTables");
            AddPrimaryKey("dbo.tabSetAttributiProdottoListino", new[] { "IDProdottoListino", "IDGruppoAttributi" });
            AddPrimaryKey("dbo.tabPrezzoProdottoDiListino", new[] { "IDListino", "IDProdottoDiListino" });
        }
    }
}
