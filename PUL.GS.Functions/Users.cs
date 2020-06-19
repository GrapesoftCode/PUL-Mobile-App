using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using PUL.GS.Models;
using System.Collections.Generic;

namespace PUL.GS.Functions
{
    public static class Users
    {
        [FunctionName("Users")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Users/{room}")] HttpRequest req,
            [Table("Users", Connection = "StorageConnection")] CloudTable userTable,
            string room,
            ILogger log)
        {
            TableQuery<UserEntity> rangeQuery =
                new TableQuery<UserEntity>().Where(
                    TableQuery.GenerateFilterCondition("PartitionKey",
                    QueryComparisons.Equal,
                    room));
            var users = new List<UserEntity>();

            foreach (var entity in await userTable.ExecuteQuerySegmentedAsync(rangeQuery, null))
            {
                users.Add(entity);
            }
            return new OkObjectResult(users);
        }
    }
}
