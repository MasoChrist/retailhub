namespace EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inheritance1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tabGruppoAttributi", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.tabProdottiToGruppoAttributi", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.tabProdotti", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.tabProdottiDiListino", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.tabPrezzoProdottoDiListino", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.tabListini", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.tabSetAttributiProdottoListino", "isDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tabSetAttributiProdottoListino", "isDeleted");
            DropColumn("dbo.tabListini", "isDeleted");
            DropColumn("dbo.tabPrezzoProdottoDiListino", "isDeleted");
            DropColumn("dbo.tabProdottiDiListino", "isDeleted");
            DropColumn("dbo.tabProdotti", "isDeleted");
            DropColumn("dbo.tabProdottiToGruppoAttributi", "isDeleted");
            DropColumn("dbo.tabGruppoAttributi", "isDeleted");
        }
    }
}
