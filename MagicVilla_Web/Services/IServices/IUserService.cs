using MagicVilla_Web.Model.Dto;

namespace MagicVilla_Web.Services.IServices
{
    public interface IUserService
    {

        Task<T> LoginAsync<T>(LoginRequestDTO loginRequestDTO);
        Task<T> RegisterAsync<T>(RegisterationRequestDTO registerationRequestDTO)
    }
}
