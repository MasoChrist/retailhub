namespace ClientNotification
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClientNotification.NotificationRules")]
    public partial class NotificationRules
    {
        public Guid ID { get; set; }

        [Required]
        [StringLength(255)]
        public string NotificationDTOType { get; set; }

        public Guid ClientCreatorID { get; set; }

        public Guid ClientReceiverID { get; set; }

        public virtual Clients CreatorClient { get; set; }

        public virtual Clients ReceiverClient { get; set; }
    }
}
