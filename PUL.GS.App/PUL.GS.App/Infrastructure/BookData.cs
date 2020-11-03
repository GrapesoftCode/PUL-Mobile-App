using PUL.GS.App.Utilities;
using PUL.GS.Models;
using PUL.GS.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUL.GS.App.Infrastructure
{
    public class BookData
    {
        private readonly AppSettings settings;

        public BookData(AppSettings configuration)
        {
            settings = configuration;
        }


        public async Task<Response<Book>> AddBook(Book book)
        {
            var response = new Response<Book>() { Success = true };
            try
            {
                var client = new HttpClientWrapper<Book, Book>();
                var serviceResponse = await client.Consume(new Uri(settings.baseUrl),
                    ServiceURIs.Book.AddBook, HttpVerb.Post, book);
                response.objectResult = serviceResponse;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = new Error() { Message = ex.Message };
            }
            return response;
        }


        public async Task<Response<Book>> GetBookByUserId(string userId)
        {
            var response = new Response<Book>() { Success = true };
            try
            {
                var client = new HttpClientWrapper<Book, Book>();
                var serviceResponse = await client.Consume(new Uri(settings.baseUrl),
                    $"{ServiceURIs.Book.GetBookByUserId}/{userId}", HttpVerb.Get);
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
