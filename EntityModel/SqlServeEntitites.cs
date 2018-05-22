namespace EntityModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SqlServeEntitites : DbContext
    {
        public SqlServeEntitites()
            : base("name=SqlServeEntitites")
        {
        }

        public virtual DbSet<Price> Price { get; set; }
        public virtual DbSet<PriceList> PriceList { get; set; }
        public virtual DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Price>()
                .Property(e => e.Amount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Price>()
                .Property(e => e.Discount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<PriceList>()
                .HasMany(e => e.Price)
                .WithRequired(e => e.PriceList)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PriceList>()
                .HasOptional(e => e.PriceList1)
                .WithRequired(e => e.PriceList2);

            modelBuilder.Entity<Products>()
                .HasMany(e => e.Price)
                .WithRequired(e => e.Products)
                .HasForeignKey(e => e.ProductID)
                .WillCascadeOnDelete(false);
        }
    }
}
