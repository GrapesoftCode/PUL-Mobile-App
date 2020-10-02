using PUL.GS.App.Utilities;
using PUL.GS.Models;
using PUL.GS.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.App.Infrastructure
{
    public class ContactData
    {
        private readonly AppSettings settings;

        public ContactData(AppSettings configuration)
        {
            settings = configuration;
        }

        public Response<IEnumerable<Contact>> GetAll(string username)
        {
            var response = new Response<IEnumerable<Contact>>() { Success = true };
            try
            {
                var client = new HttpClientWrapper<User, IEnumerable<Contact>>();
                var serviceResponse = client.Consume(new Uri(settings.baseUrl),
                    $"{ServiceURIs.Contact.GetListContacts}/{username}", HttpVerb.Get).Result;
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
