using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using static MagicVilla_Utility.SD;

namespace MagicVilla_Web.Services
{
    public class VillaService : BaseService, IVillaService
    {

        private string VillaAPi;
        public VillaService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            VillaAPi = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public async Task<T> CreateAsync<T>(VillaCreateDTO dto)
        {
            return await SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.POST,
                Url = VillaAPi + "/api/VillaApi",
                Data = dto
            });
        }

        public async Task<T> DeleteAsync<T>(int id)
        {
            return await SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.DELETE,
                Url = VillaAPi + "/api/VillaApi/" + id
            });
        }

        public async Task<T> GetAllAsync<T>()
        {
            return await SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.GET,
                Url = VillaAPi + "/api/VillaApi",

            });
        }

        public async Task<T> GetAsync<T>(int id)
        {
            return await SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.GET,
                Url = VillaAPi + "/api/VillaApi/" + id,

            });
        }

        public async Task<T> UpdateAsync<T>(int id, VillaUpdateDTO dto)
        {
            return await SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.GET,
                Url = VillaAPi + "/api/VillaApi/" + id,
                Data = dto

            });
        }
    }
}
