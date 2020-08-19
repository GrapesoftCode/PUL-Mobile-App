using PUL.GS.App.Utilities;
using PUL.GS.Models;
using PUL.GS.Models.Common;
using PUL.GS.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.App.Infrastructure
{
    public class ComboData
    {
        private readonly AppSettings settings;

        public ComboData(AppSettings configuration)
        {
            settings = configuration;
        }

        public Response<Pager<Combo>> GetAll(int actualPage = 1, int recordsPerPage = 3)
        {
            var response = new Response<Pager<Combo>>() { Success = true };
            try
            {
                var client = new HttpClientWrapper<User, Pager<Combo>>();
                var serviceResponse = client.Consume(new Uri(settings.baseUrl),
                    $"{ServiceURIs.Combo.GetRecordsCombo}/", HttpVerb.Get).Result;
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
