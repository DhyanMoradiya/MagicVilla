using AutoMapper;
using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Models.ViewModels;
using MagicVilla_Web.Services;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
	public class VillaNumberController : Controller
	{
		private readonly IMapper _mapper;
		private readonly IVillaNumberService _villaNumberService;
        private readonly IVillaService _villaService;
        public VillaNumberController(IVillaNumberService villanumberService, IMapper mapper, IVillaService villaService)
        {
			_villaNumberService = villanumberService;
			_mapper = mapper;
            _villaService = villaService;
        }
        public async Task<IActionResult> Index()
		{
			List<VillaNumberDTO> villaNumberList = new List<VillaNumberDTO>();
			APIResponse response = await _villaNumberService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
			if(response != null || response.IsSuccess)
			{
				villaNumberList = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(Convert.ToString(response.Result));
			}
			return View(villaNumberList);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateVillaNumberAsync()
        {
           VillaNumberCreateVM createVillaNumberVM = new VillaNumberCreateVM();

            APIResponse response = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                createVillaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result)).Select(i=> new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                });
            }
            return View(createVillaNumberVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumberAsync(VillaNumberCreateVM createVillaNumberVM)
        {
            if (ModelState.IsValid)
            {
                APIResponse response = await _villaNumberService.CreateAsync<APIResponse>(createVillaNumberVM.VillaNumber, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa Number Created Successfully";
                    return RedirectToAction(nameof(Index));
                }
                if (response.ErrorMessages != null)
                {
                    ModelState.AddModelError("ErrorMessage", response.ErrorMessages.FirstOrDefault());
                }
            }
           
            APIResponse resp = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                createVillaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(resp.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                });
            }
            return View(createVillaNumberVM);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateVillaNumber(int id)
        {
            VillaNumberUpdateVM updateVillaNumberVM = new VillaNumberUpdateVM();
            APIResponse response = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                updateVillaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                });
            }

            APIResponse VillaNumberResponse = await _villaNumberService.GetAsync<APIResponse>(id, HttpContext.Session.GetString(SD.SessionToken));
            if (VillaNumberResponse != null && VillaNumberResponse.IsSuccess)
            {
                VillaNumberDTO villaNumberDto = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(VillaNumberResponse.Result));
                updateVillaNumberVM.VillaNumber = _mapper.Map<VillaNumberUpdateDTO>(villaNumberDto);

                return View(updateVillaNumberVM);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVillaNumber(VillaNumberUpdateVM updateVillaNumberVM)
        {
            if (ModelState.IsValid)
            {
                APIResponse response = await _villaNumberService.UpdateAsync<APIResponse>(updateVillaNumberVM.VillaNumber.VillaNo, updateVillaNumberVM.VillaNumber, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa Number Upadted Successfully";
                    return RedirectToAction(nameof(Index));
                }
                if(response.ErrorMessages != null)
                {
                    ModelState.AddModelError("ErrorMessage", response.ErrorMessages.FirstOrDefault());
                }
            }

            APIResponse resp = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                updateVillaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(resp.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                });
            }
            return View(updateVillaNumberVM);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteVillaNumber(int id)
        {
            VillaNumberDeleteVM deleteVillaNumbVM = new VillaNumberDeleteVM();
            APIResponse resp = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                deleteVillaNumbVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(resp.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                });
            }

            APIResponse response = await _villaNumberService.GetAsync<APIResponse>(id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                VillaNumberDTO dto = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
                deleteVillaNumbVM.VillaNumber = dto;
                return View(deleteVillaNumbVM);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteVillaNumber(VillaNumberDeleteVM deleteVillaNumbVM)
        {

            APIResponse response = await _villaNumberService.DeleteAsync<APIResponse>(deleteVillaNumbVM.VillaNumber.VillaNo, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Villa Number Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}
