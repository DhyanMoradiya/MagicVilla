using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
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
            return View(villaList);
        }

        public IActionResult CreateVilla()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateVillaAsync(VillaCreateDTO villaCreateDTO)
		{
            if(ModelState.IsValid)
            {
				APIResponse response = await _villaService.CreateAsync<APIResponse>(villaCreateDTO);
                if(response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
			}
			return View(villaCreateDTO);
		}

        public async Task<IActionResult> UpdateVilla (int id)
        {
            APIResponse response = await _villaService.GetAsync<APIResponse>(id);
            if(response !=  null && response.IsSuccess)
            {
                VillaUpdateDTO updateDto =  JsonConvert.DeserializeObject<VillaUpdateDTO>(Convert.ToString(response.Result));
                return View(updateDto);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> UpdateVilla(VillaUpdateDTO updateDto)
		{
            if (ModelState.IsValid)
            {
                APIResponse response = await _villaService.UpdateAsync<APIResponse>(updateDto.Id, updateDto);
                if (response != null && response.IsSuccess)
                {

                    return RedirectToAction(nameof(Index));
                }
            }
			return View(updateDto);
		}

		public async Task<IActionResult> DeleteVilla(int id)
		{
            APIResponse response = await _villaService.GetAsync<APIResponse>(id);
            if(response != null && response.IsSuccess)
            {
                VillaDTO dto = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
                return View(dto);
            }
            return NotFound();
		}

        [HttpPost]
		public async Task<IActionResult> DeleteVilla(VillaDTO dto) { 
            
            APIResponse response = await _villaService.DeleteAsync<APIResponse>(dto.Id);
            if(response!=null && response.IsSuccess)
            {
				return RedirectToAction(nameof(Index));
			}
            return NotFound();
		}
	}
}
