using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Model.Dto;

namespace MagicVilla_VillaApi.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<bool> IsUniqueUser(string  username);

        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);

        Task<LocalUser> Register(RegisterationRequestDTO registerationRequestDTO);
    }
}
