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
using Microsoft.WindowsAzure.Storage.Table;
using PUL.GS.Models.Messages;
using PUL.GS.Models;

namespace PUL.GS.Functions
{
    public static class RemoveFromGroup
    {
        [FunctionName("RemoveFromGroup")]
        public static async Task Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [SignalR(HubName = "appulchat")] IAsyncCollector<SignalRGroupAction> signalRGroupAction,
            [Table("Users", Connection = "StorageConnection")] CloudTable userTable,
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
                Action = GroupAction.Remove
            });

            TableQuery<UserEntity> rangeQuery = new TableQuery<UserEntity>()
                .Where(TableQuery.GenerateFilterCondition("RowKey",
                QueryComparisons.Equal,
                message.Sender));

            foreach (var entity in await userTable
                .ExecuteQuerySegmentedAsync(rangeQuery, null))
            {
                TableOperation deleteOperation = TableOperation.Delete(entity);
                TableResult result = await userTable.ExecuteAsync(deleteOperation);
            }
        }
    }
}
