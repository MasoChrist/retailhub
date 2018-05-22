namespace ClientNotification
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClientNotification.NotificationToClient")]
    public partial class NotificationToClient
    {
        public Guid ID { get; set; }

        public Guid ClientID { get; set; }

        public Guid NotificationID { get; set; }

        public PendingNotificationStatus Status { get; set; }

        public virtual Clients Clients { get; set; }

        public virtual NotificationQueue NotificationQueue { get; set; }
    }
}
