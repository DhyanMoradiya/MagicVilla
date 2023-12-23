using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using static MagicVilla_Utility.SD;

namespace MagicVilla_Web.Services
{
    public class UserService : BaseService, IUserService
    {

        private string VillaAPi;
        public UserService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            VillaAPi = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public async Task<T> LoginAsync<T>(LoginRequestDTO loginRequestDTO)
        {
            return await SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.POST,
                Url = VillaAPi + "/api/v1/UsersAuth/login",
                Data = loginRequestDTO
            });
        }

        public async Task<T> RegisterAsync<T>(RegisterationRequestDTO registerationRequestDTO)
        {
            return await SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.POST,
                Url = VillaAPi + "/api/v1/UsersAuth/register",
                Data = registerationRequestDTO
            });
        }
    }
}
