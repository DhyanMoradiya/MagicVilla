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

        public async Task<T> CreateAsync<T>(VillaNumberCreateDTO dto)
        {
            return await SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.POST,
                Url = VillaAPi + "/api/VillaNumberAPI",
                Data = dto
            });
        }

        public async Task<T> DeleteAsync<T>(int id)
        {
            return await SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.DELETE,
                Url = VillaAPi + "/api/VillaNumberAPI/" + id
            });
        }

        public async Task<T> GetAllAsync<T>()
        {
            return await SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.GET,
                Url = VillaAPi + "/api/VillaNumberAPI",

            });
        }

        public async Task<T> GetAsync<T>(int id)
        {
            return await SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.GET,
                Url = VillaAPi + "/api/VillaNumberAPI/" + id,

            });
        }

        public async Task<T> UpdateAsync<T>(int id, VillaNumberUpdateDTO dto)
        {
            return await SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.PUT,
                Url = VillaAPi + "/api/VillaNumberAPI/" + id,
                Data = dto

            });
        }
    }
}
