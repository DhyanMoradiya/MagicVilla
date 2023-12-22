using AutoMapper;
using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Model.Dto;
using MagicVilla_VillaApi.Repository.IRepository;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using System.Net;


namespace MagicVilla_VillaApi.Controllers
{
    [Route("api/VillaApi")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {
        private readonly IVillaRepository _dbVilla;
        private APIResponse _response;
        private readonly IMapper _mapper;
        public VillaApiController(IVillaRepository  dbVilla, IMapper mapper) { 
            _dbVilla = dbVilla;
            _mapper = mapper;
            _response = new APIResponse();
        }


        [HttpGet]
        public async Task<APIResponse> GetVillas()
        {
            try
            {
                IEnumerable<Villa> vilaList = await _dbVilla.GetAllAsync();
                _response.Result = vilaList;
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                _response.ErrorMessages.Add(e.Message);
                _response.IsSuccess = false;
               
            }
            return _response;

        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVilla(int id) {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                var villa = await _dbVilla.GetAsync(u => u.Id == id);
                if (villa == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<VillaDTO>(villa);
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                _response.ErrorMessages.Add(e.Message);
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaCreateDTO createDTO)
        {
            try
            {
                if (createDTO == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);

                }
                if (await _dbVilla.GetAsync(u => u.Name == createDTO.Name) != null)
                {
                    ModelState.AddModelError("CustomError", "Villa Name alredy Exists");
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }   
                Villa villa = _mapper.Map<Villa>(createDTO);
                await _dbVilla.CreateAsync(villa);
                _response.StatusCode = HttpStatusCode.OK;
                return CreatedAtRoute("GetVilla",new { id = villa.Id }, _response);
            }
            catch (Exception e)
            {
                _response.ErrorMessages.Add(e.Message);
                _response.IsSuccess = false;
                return _response;
            }
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                var villa = await _dbVilla.GetAsync(v => v.Id == id);
                if (villa == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }
                await _dbVilla.RemoveAsync(villa);

                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                _response.ErrorMessages.Add(e.Message);
                _response.IsSuccess = false;
            }
            return _response;
            
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id,[FromBody] VillaUpdateDTO updateDTO) {
            try
            {
                if (updateDTO == null || updateDTO.Id != id)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                var villa = await _dbVilla.GetAsync(u => u.Id == updateDTO.Id, Trecked: false);
                if (villa == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }
                Villa modal = _mapper.Map<Villa>(updateDTO);
                await _dbVilla.UpdateAsync(modal);

                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                _response.ErrorMessages.Add(e.Message);
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> updateDTOPatch)
        {
            try
            {
                if (updateDTOPatch == null | id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                var villa = await _dbVilla.GetAsync(u => u.Id == id, Trecked: false);

                if (villa == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }

                var updateDTO = _mapper.Map<VillaUpdateDTO>(villa);
                updateDTOPatch.ApplyTo(updateDTO, ModelState);
                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                var model = _mapper.Map<Villa>(updateDTO);
                await _dbVilla.UpdateAsync(model);

                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                _response.ErrorMessages.Add(e.Message);
                _response.IsSuccess = false;
            }
            return _response;
        }
    }
}
