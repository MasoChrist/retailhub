using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientNotification
{

    /// <summary>
    /// A.K.A Orchestrator
    /// </summary>
    public class ClientNotificatorService
    {

        private  static ConcurrentBag<DTONotificationToClient> ClientToNotification = new ConcurrentBag<DTONotificationToClient>();
        /// <summary>
        /// List of item from client ( clients wants to notify these commands )
        /// </summary>
        private static ConcurrentBag<DTONotification> NotificationsIN = new ConcurrentBag<DTONotification>();
        /// <summary>
        /// pending operations for the clients
        /// </summary>
        private  static  ConcurrentBag<DTOPendingNotification> NotificationOut = new ConcurrentBag<DTOPendingNotification>();
        /// <summary>
        /// adds a notification to the NotificationsIN Queue
        /// </summary>
        /// <param name="notification"></param>
        public void AppendNotification(DTONotification notification)
        {
            notification.Status = PendingNotificationStatus.Queued;
            notification.CreationDateTime = DateTime.UtcNow;
            NotificationsIN.Add(notification);

        }

        public void ParseNotifications()
        {
            foreach (var Dto in NotificationsIN.Where(x=> x.Status == PendingNotificationStatus.Queued))
            {
                var lstClientsForThisDTO =
                    ClientToNotification.FirstOrDefault(
                        x =>
                            x.CreatorIdentifier.Equals(Dto.CreatorIdentifier) &&
                            x.NotificationDTOType.Equals(Dto.NotificationDTOType))?.Receivers;
                if(lstClientsForThisDTO==null) return;
                foreach (var client in lstClientsForThisDTO)
                {
                    var dtoPending = new DTOPendingNotification();
                    dtoPending.Notification = Dto;
                    dtoPending.Client = client;
                    //Creation datetime MUST be the Dto
                    dtoPending.CreationDateTime = Dto.CreationDateTime;
                    dtoPending.Status = PendingNotificationStatus.Queued;
                    NotificationOut.Add(dtoPending);

                }
                Dto.Status = PendingNotificationStatus.Handled;

            }
        }

        /// <summary>
        /// returns all the pending notifications for a client
        /// the idea is this: if i track all the status change of an entity, i only need to notify the client of the last  status change for each entitytype and entitykey.
        /// then the client will ask for the current value of the entity and update accordingly
        /// </summary>
        /// <param name="clientName"></param>
        /// <returns></returns>
        public List<DTOPendingNotification> GetPendingNotifications(string clientName)
        {
            var lstNotificheToReturn = new List<DTOPendingNotification>();
            ParseNotifications();
            var notifiche =
                NotificationOut.Where(x => x.Status == PendingNotificationStatus.Queued && x.Client.Equals(clientName))
                    .GroupBy(x => new
                    {
                        x.Notification.Key,
                        x.Notification.NotificationDTOType
                    }).Select(x => new 
                    {
                        Client = clientName,
                        x.Key.NotificationDTOType,
                        x.Key.Key,
                        CreationDateTime = x.Max(y => y.CreationDateTime),
                        CreatorIdentifier = x.Min(y=> y.Client),
                        NotificationType = x.Min(y=> y.Notification.NotificationType)
                    });

            
            foreach (var notifica in notifiche)
            {
                lstNotificheToReturn.Add(
                new DTOPendingNotification
                {
                    Client = notifica.Client,
                    CreationDateTime = notifica.CreationDateTime,
                    Notification = new DTONotification
                    {
                        Key = notifica.Key,
                        CreationDateTime = notifica.CreationDateTime,NotificationDTOType = notifica.NotificationDTOType
                        ,CreatorIdentifier = notifica.CreatorIdentifier,NotificationType = notifica.NotificationType
                    }
                }
                );
            }
            return lstNotificheToReturn;
            
        }

        /// <summary>
        /// since i know the datetime in wich the notification has been created,
        /// and i asked for the last status, i can flag as handled all the statusChange for the entity prior to the processed notification
        /// </summary>
        /// <param name="clientIdentifier"></param>
        /// <param name="Notification"></param>
        public void SetOkStatus(string clientIdentifier, DTONotification Notification)
        {
            var NotificationsToFlag = NotificationOut.Where(
                //-->Notifications not handled
                x => x.Status != PendingNotificationStatus.Handled
                     //-->created before the date of the notification i want to flag
                     && x.Notification.CreationDateTime <= Notification.CreationDateTime
                     //For my client
                     && x.Client.Equals(clientIdentifier)
                     //ofcourse for the same entity
                     && x.Notification.Key.Equals(Notification.Key) &&
                     x.Notification.NotificationDTOType.Equals(Notification.NotificationDTOType));
            foreach (var t in NotificationsToFlag)
            {
               t.Status = PendingNotificationStatus.Queued;
            }
         
        }



        

    }
}
