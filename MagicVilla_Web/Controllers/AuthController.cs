
using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace MagicVilla_Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
           _userService = userService;
        }

        public IActionResult Login()
        {
            LoginRequestDTO loginRequestDTO = new LoginRequestDTO();
            return View(loginRequestDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
        {
            if (ModelState.IsValid)
            {
                APIResponse response = await _userService.LoginAsync<APIResponse>(loginRequestDTO);

                if(response != null && response.IsSuccess)
                {
                    LoginResponseDTO loginResponseDTO  = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(response.Result));

                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.Name, loginResponseDTO.User.Name));
                    identity.AddClaim(new Claim(ClaimTypes.Role, loginResponseDTO.User.Role));
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(principal);

                    HttpContext.Session.SetString(SD.SessionToken, loginResponseDTO.Token);
                    return RedirectToAction("Index", "Home");
                }
                if (response != null && response.ErrorMessages.Count > 0)
                {
                    ModelState.AddModelError("ErrorMessage", response.ErrorMessages.FirstOrDefault());
                    return View(loginRequestDTO);
                }
            }
            return View(loginRequestDTO);
        }

        public IActionResult Register()
        {
            RegisterationRequestDTO registerationRequestDTO = new RegisterationRequestDTO();
            return View(registerationRequestDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterationRequestDTO registerationRequestDTO)
        {
            if(ModelState.IsValid)
            {
                APIResponse response =  await _userService.RegisterAsync<APIResponse>(registerationRequestDTO);
                
                if(response != null && response.IsSuccess) { 
                    return RedirectToAction(nameof(Login));
                }
                if(response.ErrorMessages.Count > 0)
                {
                    ModelState.AddModelError("ErrorMessage", response.ErrorMessages.FirstOrDefault());
                    return View(registerationRequestDTO);
                }
            }
            return View(registerationRequestDTO);
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.Remove(SD.SessionToken);
            return RedirectToAction(nameof(Login));
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
