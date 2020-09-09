using PUL.GS.App.Utilities;
using PUL.GS.Models;
using PUL.GS.Models.Common;
using PUL.GS.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.App.Infrastructure
{
    public class MenuData
    {
        private readonly AppSettings settings;

        public MenuData(AppSettings configuration)
        {
            settings = configuration;
        }

        public Response<IEnumerable<Category>> GetCategoriesFood(string establishmentId)
        {
            var response = new Response<IEnumerable<Category>>() { Success = true };
            try
            {
                var client = new HttpClientWrapper<User, IEnumerable<Category>>();
                var serviceResponse = client.Consume(new Uri(settings.baseUrl),
                    $"{ServiceURIs.Menu.GetCategoriesFood}/{establishmentId}", HttpVerb.Get).Result;
                response.objectResult = serviceResponse;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = new Error() { Message = ex.Message };
            }
            return response;
        }

        public Response<IEnumerable<Menu>> GetListFoodsByCategory(string establishmentId, string category)
        {
            var response = new Response<IEnumerable<Menu>>() { Success = true };
            try
            {
                var client = new HttpClientWrapper<User, IEnumerable<Menu>>();
                var serviceResponse = client.Consume(new Uri(settings.baseUrl),
                    $"{ServiceURIs.Menu.GetListFoodsByCategory}/{establishmentId}/{category}", HttpVerb.Get).Result;
                response.objectResult = serviceResponse;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = new Error() { Message = ex.Message };
            }
            return response;
        }

        public Response<IEnumerable<CustomerGroup>> GetListFoods(string establishmentId)
        {
            var response = new Response<IEnumerable<CustomerGroup>>() { Success = true };
            try
            {
                var client = new HttpClientWrapper<User, IEnumerable<CustomerGroup>>();
                var serviceResponse = client.Consume(new Uri(settings.baseUrl),
                    $"{ServiceURIs.Menu.GetListFoods}/{establishmentId}", HttpVerb.Get).Result;
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
