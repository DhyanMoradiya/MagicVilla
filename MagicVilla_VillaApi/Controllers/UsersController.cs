
using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Model.Dto;
using MagicVilla_VillaApi.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.RegularExpressions;

namespace MagicVilla_VillaApi.Controllers
{
    [Route("api/v{version:ApiVersion}/UsersAuth")]
    [ApiController]
    [ApiVersionNeutral]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private APIResponse _response;

        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
            _response = new APIResponse();  
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
        {
            if(loginRequestDTO == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Data not provided");
                return BadRequest(_response);
            }
            LoginResponseDTO loginResponseDTO = await _userRepo.Login(loginRequestDTO);
            if(loginResponseDTO.User == null || String.IsNullOrEmpty(loginResponseDTO.Token)) {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username or Password is invalid");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = loginResponseDTO;
            return Ok(_response);
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(RegisterationRequestDTO registerationRequestDTO)
        {
            bool isUniqueUsername = await _userRepo.IsUniqueUser(registerationRequestDTO.UserName);
            if(!isUniqueUsername)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username already Exists");
                return BadRequest(_response);
            }
           LocalUser user =  await _userRepo.Register(registerationRequestDTO);
            if(user == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Error While registoring");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
    }
}
