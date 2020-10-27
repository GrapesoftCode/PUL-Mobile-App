using PUL.GS.App.Utilities;
using PUL.GS.Models;
using PUL.GS.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.App.Infrastructure
{
    public class CategoryData
    {
        private readonly AppSettings settings;

        public CategoryData(AppSettings configuration)
        {
            settings = configuration;
        }

        public Response<IEnumerable<Category>> GetCategoriesHome()
        {
            var response = new Response<IEnumerable<Category>>() { Success = true };
            try
            {
                var client = new HttpClientWrapper<User, IEnumerable<Category>>();
                var serviceResponse = client.Consume(new Uri(settings.baseUrl),
                    $"{ServiceURIs.Category.GetCategoryHome}", HttpVerb.Get).Result;
                response.objectResult = serviceResponse;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = new Error() { Message = ex.Message };
            }
            return response;
        }

        public Response<IEnumerable<Menu>> GetListHomeByCategory(string category)
        {
            var response = new Response<IEnumerable<Menu>>() { Success = true };
            try
            {
                var client = new HttpClientWrapper<User, IEnumerable<Menu>>();
                var serviceResponse = client.Consume(new Uri(settings.baseUrl),
                    $"{ServiceURIs.Category.GetListHomeByCategory}/{category}", HttpVerb.Get).Result;
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
