using PUL.GS.Models.Common;
using PUL.GS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using PUL.GS.App.Utilities;

namespace PUL.GS.App.Infrastructure
{
    public class NotificationData
    {
        private readonly OneSignal oneSignal;

        public NotificationData(OneSignal configuration)
        {
            oneSignal = configuration;
        }

        public Response<NotificationData> CreateNotification(Notification notification)
        {
            var response = new Response<NotificationData>() { Success = true };
            try
            {
                var service = new HttpClientWrapper<Notification, NotificationData>();
                var serviceResponse = service.Consume(new Uri(oneSignal.baseUrl),
                    $"{ServiceURIs.Notification.CreateNotification}", HttpVerb.Post, notification, null, true).Result;
                response.objectResult = serviceResponse;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = new Error() { Message = ex.Message };
            }
            return response;
        }
    }
}
