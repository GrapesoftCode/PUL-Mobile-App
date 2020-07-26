using PUL.GS.App.Utilities;
using PUL.GS.Models;
using PUL.GS.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.App.Infrastructure
{
    public class AccountData
    {
        private readonly AppSettings settings;

        public AccountData(AppSettings configuration)
        {
            settings = configuration;
        }

        public Response<string> GetToken(User user)
        {
            var response = new Response<string>() { Success = true };
            try
            {
                var client = new HttpClientWrapper<User, string>();
                var serviceResponse = client.Consume(new Uri(settings.baseUrl),
                    $"{ServiceURIs.Account.GetToken}/", HttpVerb.Post, user).Result;
                response.objectResult = serviceResponse;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = new Error() { Message = ex.Message };
            }
            return response;
        }

        public Response<User> GetUserByCredentials(string user, string password, string token)
        {
            var response = new Response<User>() { Success = true };
            try
            {
                var client = new HttpClientWrapper<User, User>();
                var serviceResponse = client.Consume(new Uri(settings.baseUrl),
                    $"{ServiceURIs.Account.GetUserByCredentials}/{user}/{password}", HttpVerb.Get, null, token).Result;
                response.objectResult = serviceResponse;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = new Error() { Message = ex.Message };
            }
            return response;
        }

        public Response<User> AddUser(User user)
        {
            var response = new Response<User>() { Success = true };
            try
            {
                var client = new HttpClientWrapper<User, User>();
                var serviceResponse = client.Consume(new Uri(settings.baseUrl),
                    ServiceURIs.Account.AddUser, HttpVerb.Post, user).Result;
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
