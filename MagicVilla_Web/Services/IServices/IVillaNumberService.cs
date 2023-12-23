using MagicVilla_Web.Models.Dto;

namespace MagicVilla_Web.Services.IServices
{
    public interface IVillaNumberService
    {
        Task<T> GetAllAsync<T>(string Token);
        Task<T> GetAsync<T>(int id, string Token);
        Task<T> CreateAsync<T>(VillaNumberCreateDTO dto, string Token);
        Task<T> UpdateAsync<T>(int id, VillaNumberUpdateDTO dto, string Token);
        Task<T> DeleteAsync<T>(int id, string Token);
    }
}
