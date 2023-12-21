using MagicVilla_Utility;
using MagicVilla_Web.Model;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using Newtonsoft.Json;
using System.Text;


namespace MagicVilla_Web.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse apiRespose;
        private IHttpClientFactory httpClient;
        public BaseService(IHttpClientFactory httpClient) { 
            this.apiRespose = new APIResponse();
            this.httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                HttpClient client = httpClient.CreateClient("MagicAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);

                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                }

                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;

                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;

                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;

                    case SD.ApiType.PATCH:
                        message.Method = HttpMethod.Patch;
                        break;

                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                HttpResponseMessage apiResponse = null;

                apiResponse = await client.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var APIresponse = JsonConvert.DeserializeObject<T>(apiContent);

                return APIresponse;
            }catch(Exception e)
            {
                var dto = new APIResponse
                {
                    ErrorMessages = new List<string>
                    {
                        Convert.ToString(e)
                    },
                    IsSuccess = false
                };

                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);

                return APIResponse;

            }
        }
    }
}
