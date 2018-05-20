using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientNotification
{

    /// <summary>
    /// Must have a list of receiver for each Dto type we want to broadcast
    /// </summary>
    public class DTONotificationToClient
    {
        public string CreatorIdentifier { get; set; }
        public  string NotificationDTOType { get; set; }
        public  List<string> Receivers { get; set; }

    }
}
