namespace ClientNotification
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OrmPendingNotifications : DbContext
    {
        public OrmPendingNotifications()
            : base("name=OrmPendingNotifications")
        {
        }

        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<NotificationQueue> NotificationQueue { get; set; }
        public virtual DbSet<NotificationRules> NotificationRules { get; set; }
        public virtual DbSet<NotificationToClient> NotificationToClient { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clients>()
                .HasMany(e => e.NotificationQueue)
                .WithRequired(e => e.Clients)
                .HasForeignKey(e => e.CreatorID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Clients>()
                .HasMany(e => e.NotificationRules)
                .WithRequired(e => e.CreatorClient)
                .HasForeignKey(e => e.ClientCreatorID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Clients>()
                .HasMany(e => e.NotificationRules1)
                .WithRequired(e => e.ReceiverClient)
                .HasForeignKey(e => e.ClientReceiverID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Clients>()
                .HasMany(e => e.NotificationToClient)
                .WithRequired(e => e.Clients)
                .HasForeignKey(e => e.ClientID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NotificationQueue>()
                .HasMany(e => e.NotificationToClient)
                .WithRequired(e => e.NotificationQueue)
                .HasForeignKey(e => e.NotificationID)
                .WillCascadeOnDelete(false);
        }
    }
}
