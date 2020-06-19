using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace PUL.GS.Functions
{
    public static class NogotiateFunction
    {
        [FunctionName("negotiate")]
        public static SignalRConnectionInfo Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "negotiate/{userId}")] HttpRequest req,
            [SignalRConnectionInfo(HubName = "appulchat", UserId = "{userId}")] SignalRConnectionInfo connectionInfo,
            ILogger log)
        {
            return connectionInfo;
        }
    }
}
