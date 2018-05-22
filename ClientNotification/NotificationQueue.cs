namespace ClientNotification
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClientNotification.NotificationQueue")]
    public partial class NotificationQueue
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NotificationQueue()
        {
            NotificationToClient = new HashSet<NotificationToClient>();
        }

        public Guid ID { get; set; }

       

        [Required]
        [StringLength(255)]
        public string NotificationDTOType { get; set; }

        [Required]
        [StringLength(255)]
        public string NotificationDTOkey { get; set; }

        public DateTime CreationDateTime { get; set; }

        public NotificationType OperationType { get; set; }

        public Guid CreatorID { get; set; }

        public virtual Clients Clients { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NotificationToClient> NotificationToClient { get; set; }
    }
}
