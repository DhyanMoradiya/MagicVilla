using AutoMapper;
using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Model.Dto;
using MagicVilla_VillaApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MagicVilla_VillaApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        string secret; 
        public UserRepository(ApplicationDbContext db, IMapper mapper, IConfiguration configuration)
        {

            _db = db;
            _mapper = mapper;
            secret = configuration.GetValue<string>("ApiSettings:Secret");
        }

        public async Task<bool> IsUniqueUser(string username)
        {
           LocalUser user = await _db.localUsers.FirstOrDefaultAsync(u => u.UserName == username); 
            if(user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            LocalUser user = await _db.localUsers.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower() && u.Password == loginRequestDTO.Password);
            if (user == null)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null
                };
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Role, user.Role)
                    }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescripter);

            LoginResponseDTO loginResponseDTO = new()
            {
                Token = tokenHandler.WriteToken(token),
                User = user,
            };

            return loginResponseDTO;

        }

        public async Task<LocalUser> Register(RegisterationRequestDTO registerationRequestDTO)
        {
           LocalUser user  = _mapper.Map<LocalUser>(registerationRequestDTO);
            await _db.localUsers.AddAsync(user);
            await _db.SaveChangesAsync();
            user.Password = "";
            return user;
        }
    }
}
