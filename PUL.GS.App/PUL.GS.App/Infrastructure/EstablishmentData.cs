﻿using PUL.GS.App.Utilities;
using PUL.GS.Models;
using PUL.GS.Models.Common;
using PUL.GS.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUL.GS.App.Infrastructure
{
    public class EstablishmentData
    {
        private readonly AppSettings settings;

        public EstablishmentData(AppSettings configuration)
        {
            settings = configuration;
        }

        public async Task<Response<Pager<Establishment>>> GetRecordsEstablishment(int actualPage = 1, int recordsPerPage = 3)
        {
            var response = new Response<Pager<Establishment>>() { Success = true };
            try
            {
                var client = new HttpClientWrapper<User, Pager<Establishment>>();
                var serviceResponse = await client.Consume(new Uri(settings.baseUrl),
                    $"{ServiceURIs.Establishment.GetRecordsEstablishment}?actualPage={actualPage}&recorsPerPage={recordsPerPage}", HttpVerb.Get);
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
