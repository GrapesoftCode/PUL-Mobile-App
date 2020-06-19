using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using PUL.GS.Models.Messages;
using PUL.GS.Functions.Helpers;

namespace PUL.GS.Functions
{
    public static class Messages
    {
        [FunctionName("Messages")]
        public static async Task Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
            [SignalR(HubName = "appulchat")] IAsyncCollector<SignalRMessage> signalRMessages,
            ILogger log)
        {
            var serializeObject = new JsonSerializer()
               .Deserialize(new JsonTextReader(new StreamReader(req.Body)));

            var message =
                JsonConvert
                .DeserializeObject<ChatMessage>(serializeObject.ToString());



            if (message.TypeInfo.Name == nameof(UserConnectedMessage))
            {
                message = JsonConvert
                   .DeserializeObject<UserConnectedMessage>(serializeObject.ToString());
                await signalRMessages.AddAsync(
                    new SignalRMessage
                    {
                        GroupName = message.GroupName,
                        Target = "ReceiveMessage",
                        Arguments = new[] { message }
                    });
            }
            else if (message.TypeInfo.Name == nameof(SimpleTextMessage))
            {
                message = JsonConvert
                    .DeserializeObject<SimpleTextMessage>(serializeObject.ToString());
                var signalRMessage = new SignalRMessage()
                {
                    Target = "ReceiveMessage",
                    Arguments = new[] { message }
                };

                if (message.GroupName != null)
                {
                    signalRMessage.GroupName = message.GroupName;
                }
                else if (message.Recipient != null)
                {
                    signalRMessage.UserId = message.Recipient;
                }
                await signalRMessages.AddAsync(signalRMessage);
            }
            else if (message.TypeInfo.Name == nameof(PhotoMessage))
            {
                var photoMessage =
                    JsonConvert.DeserializeObject<PhotoMessage>(serializeObject.ToString());
                var bytes =
                    Convert.FromBase64String(photoMessage.Base64Photo);
                var url = await StorageHelper.Upload(bytes, photoMessage.FileEnding);

                message = new PhotoUrlMessage(photoMessage.Sender)
                {
                    Id = photoMessage.Id,
                    Timestamp = photoMessage.Timestamp,
                    Url = url,
                    GroupName = photoMessage.GroupName
                };

                await signalRMessages.AddAsync(
                    new SignalRMessage
                    {
                        GroupName = message.GroupName,
                        Target = "ReceiveMessage",
                        Arguments = new[] { message }
                    });
            }
        }
    }
}
