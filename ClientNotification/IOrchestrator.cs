using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientNotification
{
    public interface IOrchestrator
    {
        DTOPendingNotification GetNextPendingNotification(Guid clientIdentifier);
        void SetStatus(Guid clientIdentifier, DTONotification Notification);
    }
}
