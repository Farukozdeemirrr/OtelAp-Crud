using AutoMapper;
using Business.Abstract;
using DTO.Garage;
using Microsoft.AspNetCore.Mvc;

namespace OtelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GaragesController : ControllerBase
    {
        private IGarageService _garageService;

        public GaragesController(IGarageService garageService)
        {
            _garageService = garageService;
        }

        [HttpPost("Upsert")]
        public GarageDto ProcessGarage([FromBody] GarageDto garage)
        {
            return _garageService.UpsertGarage(garage);
        }

        [HttpGet("[action]")]
        public List<GarageDto> GetAllGarages()
        {
            return _garageService.GetAllGarages();
        }

        //----------- DTO İFADELERİ ----------------
        [HttpGet("{id}")]
        public GarageDto GetOneGarage(long id)
        {
            return _garageService.GetGarageById(id);
        }

        [HttpPost("[action]")]
        public void DeleteGarage(int id)
        {
            _garageService.DeleteGarage(id);
        }

    }
}