using Business.Abstract;
using Business.JWT;
using DTO.Person;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private IPersonService _personService; 
        private readonly JwtSettings _jwtSettings;

        public PersonsController(IPersonService personService, IOptions<JwtSettings> jwtSettings)
        {
            _personService = personService;
            _jwtSettings = jwtSettings.Value; //JwtSettings türünü, IPersonService olarak aldığımız için .value kullanıyoruz
        }
        
        [HttpPost("Upsert")]
        public UserRegisterDto ProccesPerson([FromBody] UserRegisterDto userRegisterDto)
        {
            return _personService.UpsertPerson(userRegisterDto);
        }
        [HttpGet("{id}")]
        public UserLoginDto getOnePerson(long id)
        {
            return _personService.GetOnePerson(id);
        }

        [HttpPost("[action]")]
        public void DeletePerson(long id)
        {
            _personService.DeletePerson(id);
        }

        [HttpGet("[action]")]
        public List<UserResponseDto> GetAllPerson()
        {
            return _personService.GetAllPerson();
        }

    }
}
