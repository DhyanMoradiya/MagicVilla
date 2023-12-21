using MagicVilla_Web.Model;
using MagicVilla_Web.Model.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;
        public VillaController(IVillaService villaService)
        {
            _villaService = villaService;
        }
        public async Task<IActionResult> IndexAsync()
        {
            List<VillaDTO> villaList = new();

            APIResponse response = await _villaService.GetAllAsync<APIResponse>();
            if(response != null && response.IsSuccess) { 
                   villaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));            
            }
            return View();
        }
    }
}
