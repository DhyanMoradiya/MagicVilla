using AutoMapper;
using MagicVilla_VillaApi.Model.Dto;
using MagicVilla_VillaApi.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MagicVilla_VillaApi.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;

namespace MagicVilla_VillaApi.Controllers.v2
{
    [Route("api/v{version:ApiVersion}/VillaNumberApi")]
    [ApiController]
    [ApiVersion("2.0")]
    public class VillaNumberController : ControllerBase
    {
        private readonly IVillaNumberRepository _dbVillaNumber;
        private APIResponse _response;
        private readonly IMapper _mapper;
        private readonly IVillaRepository _dbVilla;
        public VillaNumberController(IVillaNumberRepository dbVillaNumber,IVillaRepository dbVilla, IMapper mapper)
        {
            _dbVillaNumber = dbVillaNumber;
            _mapper = mapper;
            _response = new APIResponse();
            _dbVilla = dbVilla;
        }


        [HttpGet("GetString")]
        public IEnumerable<string> Get() {
            return new string[] { "hello", "it is Version 2" };
         }

    }
}
