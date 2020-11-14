using PUL.GS.App.Utilities;
using PUL.GS.Models;
using PUL.GS.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUL.GS.App.Infrastructure
{
    public class ActivityData
    {
        private readonly AppSettings settings;

        public ActivityData(AppSettings configuration)
        {
            settings = configuration;
        }

        public async Task<Response<IEnumerable<Activity>>> GetAll(string username)
        {
            var response = new Response<IEnumerable<Activity>>() { Success = true };
            try
            {
                var client = new HttpClientWrapper<Activity, IEnumerable<Activity>>();
                var serviceResponse = await client.Consume(new Uri(settings.baseUrl),
                    $"{ServiceURIs.Activity.GetAll}/{username}", HttpVerb.Get);
                response.objectResult = serviceResponse;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = new Error() { Message = ex.Message };
            }
            return response;
        }

        public async Task<Response<Activity>> AddActivity(Activity activity)
        {
            var response = new Response<Activity>() { Success = true };
            try
            {
                var client = new HttpClientWrapper<Activity, Activity>();
                var serviceResponse = await client.Consume(new Uri(settings.baseUrl),
                    ServiceURIs.Activity.AddActivity, HttpVerb.Post, activity);
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
