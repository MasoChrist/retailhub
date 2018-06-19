namespace EntityModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SqlServerEntities : DbContext
    {
        public SqlServerEntities()
            : base("name=SqlServerEntities")
        {
        }

        public virtual DbSet<tabGruppoAttributi> tabGruppoAttributi { get; set; }
        public virtual DbSet<tabListini> tabListini { get; set; }
        
        public virtual DbSet<tabPrezzoProdottoDiListino> tabPrezzoProdottoDiListino { get; set; }
        public virtual DbSet<tabProdotti> tabProdotti { get; set; }
        public virtual DbSet<tabProdottiDiListino> tabProdottiDiListino { get; set; }
        public virtual DbSet<tabProdottiToGruppoAttributi> tabProdottiToGruppoAttributi { get; set; }
        public virtual DbSet<tabSetAttributiProdottoListino> tabSetAttributiProdottoListino { get; set; }
        public virtual  DbSet<tabProperties> tabProperties { get; set; }

        public  virtual  DbSet<tabCategorie> tabCategorie { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            modelBuilder.Entity<baseEntityTable>()
                .Map<baseEntityTable>(m => m.Requires("Type").HasValue("baseEntityTable"))
                .Map<tabCategorie>(m => m.Requires("Type").HasValue("tabCategorie"))
                .Map<tabProdotti>(m => m.Requires("Type").HasValue("tabProdotti"))
                .Map<tabGruppoAttributi>(m => m.Requires("Type").HasValue("tabGruppoAttributi"))
                .Map<tabSetAttributiProdottoListino>(m => m.Requires("Type").HasValue("tabSetAttributiProdottoListino"))
                .Map<tabProdottiDiListino>(m => m.Requires("Type").HasValue("tabProdottiDiListino"))
                .Map<tabPrezzoProdottoDiListino>(m => m.Requires("Type").HasValue("tabPrezzoProdottoDiListino"))
                .Map<tabListini>(m => m.Requires("Type").HasValue("tabListini"))
                .Map<tabProdottiToGruppoAttributi>(m => m.Requires("Type").HasValue("tabProdottiToGruppoAttributi"));
               
            modelBuilder.Entity<tabListini>()
                .HasMany(e => e.tabPrezzoProdottoDiListino)
                .WithRequired(e => e.tabListini)
                .HasForeignKey(e => e.IDListino)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tabPrezzoProdottoDiListino>()
                .Property(e => e.Prezzo)
                .HasPrecision(20, 4);

            modelBuilder.Entity<tabPrezzoProdottoDiListino>()
                .Property(e => e.Maggiorazione)
                .HasPrecision(20, 4);

            modelBuilder.Entity<tabProdotti>()
                .HasMany(e => e.tabProdottiDiListino)
                .WithRequired(e => e.tabProdotti)
                .HasForeignKey(e => e.IDProdotto)
                .WillCascadeOnDelete(false);
            

            modelBuilder.Entity<tabProdottiDiListino>()
                .HasMany(e => e.tabPrezzoProdottoDiListino)
                .WithRequired(e => e.tabProdottiDiListino)
                .HasForeignKey(e => e.IDProdottoDiListino)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tabProdottiDiListino>()
                .HasMany(e => e.tabSetAttributiProdottoListino)
                .WithRequired(e => e.tabProdottiDiListino)
                .HasForeignKey(e => e.IDProdottoListino)
                .WillCascadeOnDelete(false);



        }
    }
}
