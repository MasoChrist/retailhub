using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientNotification
{

    public enum PendingNotificationStatus
    {
        Queued = 0,
        Processing = 1,
      
        Handled = 2,
        Error =3
    }
    public class DTOPendingNotification
    {
        public  DTONotification Notification { get; set; }
        public  string Client { get; set; }
        public  DateTime CreationDateTime { get; set; }
        public  PendingNotificationStatus Status { get; set; }
    }
}
