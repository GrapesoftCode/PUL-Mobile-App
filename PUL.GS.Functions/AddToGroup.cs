using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PUL.GS.Models;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using PUL.GS.Models.Messages;

namespace PUL.GS.Functions
{
    public static class AddToGroup
    {
        [FunctionName("AddToGroup")]
        [return: Table("Users", Connection = "StorageConnection")]
        public static async Task<UserEntity> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [SignalR(HubName = "appulchat")] IAsyncCollector<SignalRGroupAction> signalRGroupAction,
            ILogger log)
        {
            var message = new JsonSerializer()
                 .Deserialize<UserConnectedMessage>(
                new JsonTextReader(new StreamReader(req.Body)));

            await signalRGroupAction.AddAsync(new SignalRGroupAction()
            {
                ConnectionId = message.Token,
                UserId = message.Sender,
                GroupName = message.GroupName,
                Action = GroupAction.Add
            });

            Random r = new Random();
            var red = r.Next(0, 255).ToString("X2");
            var green = r.Next(0, 255).ToString("X2");
            var blue = r.Next(0, 255).ToString("X2");

            var item = new UserEntity
            {
                UserId = message.Sender,
                Room = message.GroupName,
                Color = $"#{red}{green}{blue}",
                Avatar = $"image_{r.Next(1, 51)}.png",
                PartitionKey = message.GroupName,
                RowKey = message.Sender
            };
            return item;
        }
    }
}
