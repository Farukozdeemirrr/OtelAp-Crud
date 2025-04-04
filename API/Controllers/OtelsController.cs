using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using AutoMapper;
using DTO.Otel;
using DTO.General;

namespace OtelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtelsController : ControllerBase
    {
        private IOtelService _otelService;

        public OtelsController(IOtelService otelService)
        {
            _otelService = otelService;
        }

        /// <summary>
        /// Yeni otel kaydeder.
        /// </summary>
        /// <param name="otel"></param>
        /// <returns></returns>
        [HttpPost("Upsert")]
        public OtelDto ProcessOtel([FromBody] OtelDto otel)
        {
            return _otelService.UpsertOtel(otel);
        }

        [HttpPost("[action]")]
        public void DeleteOtel(long id)
        {
            _otelService.DeleteOtel(id);
        }

        [HttpPost("[action]")]
        public List<OtelDto> GetAllOtels([FromBody] PagingInput<string> pagingInput)
        {
            return _otelService.GetAllOtels(pagingInput);
        }

        [HttpGet("{id}")]
        public OtelDto GetOtelById(long id)
        {
            return _otelService.GetOtelById(id);
        }

    }
}


