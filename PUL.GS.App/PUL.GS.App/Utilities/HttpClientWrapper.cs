using PUL.GS.Models.Common;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PUL.GS.App.Utilities
{
    public class HttpClientWrapper<TRequest, TResponse> where TRequest : class where TResponse : class
    {
        private static HttpClient client;

        /// <summary>
        /// Realiza una llamada tipo POST (verbo HTTP) asíncrona, enviandole una solicitud en formato Json
        /// </summary>
        /// <param name="baseAddress">Uri base del servicio</param>
        /// <param name="methodAddress">Uri del método del servicio que se va a consumir</param>
        /// <param name="request">Objeto con los datos de la solicitud que se va a enviar al servicio</param>
        /// <returns>Respuesta (objeto Response) que devuelve el servicio</returns>
        public async Task<TResponse> Consume(Uri baseAddress, string methodAddress, HttpVerb method, TRequest request = null, string token = null)
        {
            try
            {
                if (client is null)
                {
                    client = new HttpClient()
                    {
                        BaseAddress = baseAddress
                    };
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }

                if (!string.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                StringContent requestContent = null;
                HttpResponseMessage responseMessage = null;
                var serializedRequest = SerializeHelper.SerializeObject(request);

                switch (method)
                {
                    case HttpVerb.Post:
                        requestContent = new StringContent(serializedRequest, Encoding.Unicode, "application/json");
                        responseMessage = await client.PostAsync(methodAddress, requestContent).ConfigureAwait(false);
                        break;
                    case HttpVerb.Put:
                        requestContent = new StringContent(serializedRequest, Encoding.Unicode, "application/json");
                        responseMessage = await client.PutAsync(methodAddress, requestContent).ConfigureAwait(false);
                        break;
                    case HttpVerb.Get:
                        responseMessage = await client.GetAsync(methodAddress).ConfigureAwait(false);
                        break;
                    case HttpVerb.Delete:
                        responseMessage = await client.DeleteAsync(methodAddress).ConfigureAwait(false);
                        break;
                }

                return ValidateResponseMessage(responseMessage);
            }
            catch (Exception ex)
            {
                //Logger.WriteLog(ex, MCEventSource.UserInterface);
                throw ex;
            }
        }
        /// <summary>
        /// Valida el response para deserializar o enviar excepción
        /// </summary>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        private TResponse ValidateResponseMessage(HttpResponseMessage responseMessage)
        {
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            var statusCode = responseMessage.StatusCode;

            if (statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.Created)
                return SerializeHelper.DeserializeObject<TResponse>(content);

            var error = SerializeHelper.DeserializeObject<Error>(content);
            throw new Exception(error.Message);
        }
    }
}
