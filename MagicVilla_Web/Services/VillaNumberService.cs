using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using static MagicVilla_Utility.SD;

namespace MagicVilla_Web.Services
{
    public class VillaNumberService : BaseService, IVillaNumberService
    {

        private string VillaAPi;
        public VillaNumberService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            VillaAPi = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public async Task<T> CreateAsync<T>(VillaNumberCreateDTO dto, string Token)
        {
            return await SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.POST,
                Url = VillaAPi + "/api/v1/VillaNumberAPI",
                Data = dto,
                Token = Token
            });
        }

        public async Task<T> DeleteAsync<T>(int id, string Token)
        {
            return await SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.DELETE,
                Url = VillaAPi + "/api/v1/VillaNumberAPI/" + id,
                Token = Token
            });
        }

        public async Task<T> GetAllAsync<T>(string Token)
        {
            return await SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.GET,
                Url = VillaAPi + "/api/v1/VillaNumberAPI",
                Token = Token
            });
        }

        public async Task<T> GetAsync<T>(int id, string Token)
        {
            return await SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.GET,
                Url = VillaAPi + "/api/v1/VillaNumberAPI/" + id,
                Token = Token
            });
        }

        public async Task<T> UpdateAsync<T>(int id, VillaNumberUpdateDTO dto, string Token)
        {
            return await SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.PUT,
                Url = VillaAPi + "/api/v1/VillaNumberAPI/" + id,
                Data = dto,
                Token = Token

            });
        }
    }
}
