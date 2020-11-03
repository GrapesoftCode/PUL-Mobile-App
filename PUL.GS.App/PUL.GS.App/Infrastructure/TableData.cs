using PUL.GS.App.Utilities;
using PUL.GS.Models;
using PUL.GS.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUL.GS.App.Infrastructure
{
    public class TableData
    {
        private readonly AppSettings settings;
        public TableData(AppSettings configuration)
        {
            settings = configuration;
        }

        public async Task<Response<IEnumerable<Table>>> GetTables(string userId, string establishmentId)
        {
            var response = new Response<IEnumerable<Table>>() { Success = true };
            try
            {
                var client = new HttpClientWrapper<IEnumerable<Table>, IEnumerable<Table>>();
                var serviceResponse = await client.Consume(new Uri(settings.baseUrl),
                    $"{ServiceURIs.Table.GetAllTables}/{userId}/{establishmentId}", HttpVerb.Get);
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
