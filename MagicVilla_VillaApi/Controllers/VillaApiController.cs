using AutoMapper;
using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Model.Dto;
using MagicVilla_VillaApi.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaApi.Controllers
{
    [Route("api/VillaApi")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;
        public VillaApiController(IVillaRepository  dbVilla, IMapper mapper) { 
            _dbVilla = dbVilla;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IEnumerable<VillaDTO>> GetVillas()
        {
            IEnumerable<Villa> vilaList = await _dbVilla.GetAllAsync();
            return _mapper.Map<List<VillaDTO>>(vilaList) ;
        }

        [HttpGet("id:int", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDTO>> GetVilla(int id) {
            if(id == 0)
            {
                return BadRequest();
            }
            var villa = await _dbVilla.GetAsync(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<VillaDTO>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody] VillaCreateDTO createDTO)
        {
            if(createDTO == null)
            {
                return BadRequest(createDTO);
            }
            if(await _dbVilla.GetAsync(u => u.Name == createDTO.Name) !=  null)
            {
                ModelState.AddModelError("CustomError", "Villa Name alredy Exists");
                return BadRequest(ModelState);
            }
            Villa model = _mapper.Map<Villa>(createDTO);
            await _dbVilla.CreateAsync(model);
            return CreatedAtRoute("GetVilla", model.Id, createDTO);
        }


        [HttpDelete("id:int")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var villa = await _dbVilla.GetAsync(v => v.Id == id);
            if(villa == null)
            {
                return NotFound();
            }
            await _dbVilla.RemoveAsync(villa);
     
            return NoContent();
            
        }

        [HttpPut("id:int")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateVilla(int id,[FromBody] VillaUpdateDTO updateDTO) {
            if(updateDTO == null || updateDTO.Id != id) { 
                return BadRequest();
            } 
            var villa = await _dbVilla.GetAsync(u => u.Id == updateDTO.Id, Trecked : false);
            if(villa == null)
            {
                return NotFound();
            }
            Villa modal = _mapper.Map<Villa>(updateDTO);
            await _dbVilla.UpdateAsync(modal);

            return NoContent();
        }

        [HttpPatch("id:int")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> updateDTOPatch)
        {
            if (updateDTOPatch == null | id == 0)
            {
                return BadRequest();
            }
            var villa = await _dbVilla.GetAsync(u => u.Id == id, Trecked : false);
            if (villa == null)
            {
                return NotFound();
            }

            var updateDTO = _mapper.Map<VillaUpdateDTO>(villa);
            updateDTOPatch.ApplyTo(updateDTO, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = _mapper.Map<Villa>(updateDTO);
            await _dbVilla.UpdateAsync(model);
            
            return NoContent();
        }
    }
}
