using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientNotification
{
    public enum NotificationType : int
    {
        Delete = 1,
        UpdateOrInsert = 2
    }
    public class DTONotification
    {
        public  string CreatorIdentifier { get; set; }
        public string NotificationDTOType { get; set; }
        public string Key { get; set; }
        
        public  NotificationType NotificationType { get; set; }
        public  DateTime CreationDateTime { get; set; }
        public  PendingNotificationStatus Status { get; set; } = PendingNotificationStatus.Queued;

    }


}
