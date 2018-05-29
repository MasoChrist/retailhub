using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientNotification
{

    /// <summary>
    /// A.K.A Orchestrator
    /// </summary>
    public class ClientNotificatorService:IOrchestrator
    {


        public void AppendNotification(DTONotification notification)
        {
            notification.Status = PendingNotificationStatus.Queued;
            notification.CreationDateTime = DateTime.UtcNow;
            var clients = getClientsForCurrentNotification(notification);
            if( clients == null || !clients.Any()) return;

            //-->appendo la modifica
            var tabNotifica = new NotificationQueue
            {
                CreationDateTime = DateTime.UtcNow,
                CreatorID = notification.CreatorIdentifier,
                NotificationDTOType = notification.NotificationDTOType,
                NotificationDTOkey = notification.Key,
                OperationType = notification.NotificationType
            };



            using (var ctx = new OrmPendingNotifications())
            {
                ctx.NotificationQueue.Attach(tabNotifica);
                foreach (var cl in clients)
                {
                    var tabClient = new NotificationToClient();
                    tabClient.ClientID = cl.ID;
                    tabClient.Status = PendingNotificationStatus.Queued;
                    tabClient.NotificationID = tabNotifica.ID;
                    tabClient.NotificationQueue = tabNotifica;
                    ctx.NotificationToClient.Add(tabClient);
                    //ctx.Entry(tabClient).State = EntityState.Added;
                }
                ctx.SaveChanges();
            }
           

        }

        /// <summary>
        /// Gets the list of client who should receive my notification
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        private List<Clients> getClientsForCurrentNotification(DTONotification notification)
        {
            //->per ora è molto semplice: c'è una tabella NotificationRules dove vado per tipoEntità e creator a prendere tutti i client che la devono ricevere
            //(escluso me stesso)
            //torno la lista
            using (var ctx = new OrmPendingNotifications())
            {
                return
                    ctx.NotificationRules.Where(x => x.ClientCreatorID == notification.CreatorIdentifier && notification.NotificationDTOType.Equals(x.NotificationDTOType)
                    //escludo il loopback: il mio client non ci deve essere
                    && x.ClientReceiverID!= notification.CreatorIdentifier
                    )
                        .Select(x => x.ReceiverClient)
                        .ToList();
            }
        }

      

        /// <summary>
        /// returns the next pending notifications for a client
        /// the idea is this: if i track all the status change of an entity, i only need to notify the client of the last  status change for each entitytype and entitykey.
        /// then the client will ask for the current value of the entity and update accordingly
        /// </summary>
        /// <param name="clientName"></param>
        /// <returns></returns>
        public DTOPendingNotification GetNextPendingNotification(Guid clientIdentifier)
        {
            var lstNotificheToReturn = new List<DTOPendingNotification>();
           // ParseNotifications();
            using (var ctx = new OrmPendingNotifications())
            {
                var notifica =
                ctx.NotificationToClient.Where(x => x.Status == PendingNotificationStatus.Queued && x.ClientID == clientIdentifier).Select(x=> x.NotificationQueue)
                    .GroupBy(x => new
                    {
                        x.NotificationDTOkey,
                        x.NotificationDTOType
                    }).Select(x => new
                    {
                        Client = clientIdentifier,
                        x.Key.NotificationDTOType,
                        x.Key,
                        CreationDateTime = x.Max(y => y.CreationDateTime),
                        CreatorIdentifier = x.Min(y => y.CreatorID),
                        NotificationType = x.Min(y => y.OperationType)
                    }).FirstOrDefault();


                return
                new DTOPendingNotification
                {
                    Client = notifica.Client,
                    CreationDateTime = notifica.CreationDateTime,
                    Notification = new DTONotification
                    {
                        Key = notifica.Key.NotificationDTOkey,
                        CreationDateTime = notifica.CreationDateTime,
                        NotificationDTOType = notifica.NotificationDTOType
                        ,
                        CreatorIdentifier = notifica.CreatorIdentifier,
                        NotificationType = notifica.NotificationType
                    }
                };
                
            }
          
        }
        /// <summary>
        /// since i know the datetime in wich the notification has been created,
        /// and i asked for the last status, i can flag as handled all the statusChange for the entity prior to the processed notification
        /// </summary>
        /// <param name="clientIdentifier"></param>
        /// <param name="Notification"></param>
        public void SetStatus(Guid clientIdentifier, DTONotification Notification)
        {
            using (var ctx = new OrmPendingNotifications())
            {
                var notificationsToFlag = ctx.NotificationToClient.Where(
                    //-->Notifications not handled
                    x => x.Status != PendingNotificationStatus.Handled
                   //-->created before the date of the notification i want to flag
                   && x.NotificationQueue.CreationDateTime <= Notification.CreationDateTime
                   //For my client
                   && x.ClientID == clientIdentifier
                   //ofcourse for the same entity
                   && x.NotificationQueue.NotificationDTOkey.Equals(Notification.Key) &&
                   x.NotificationQueue.NotificationDTOType.Equals(Notification.NotificationDTOType));
                foreach (var t in notificationsToFlag)
                {
                    t.Status =Notification.Status;
                    ctx.Entry(t).State = EntityState.Modified;
                }
                ctx.SaveChanges();
            }
        }
    }
}
