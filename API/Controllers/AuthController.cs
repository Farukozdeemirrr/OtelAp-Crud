using Business.Abstract;
using Business.JWT;
using DTO.General;
using DTO.Otel;
using DTO.Person;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IPersonService _personService;
        private readonly JwtSettings _jwtSettings;
        private IGarageService _garageService;
        private IOtelService _otelService;

        public AuthController(IPersonService personService, IOptions<JwtSettings> jwtSettings, IGarageService garageService, IOtelService otelService)
        {
            _personService = personService;
            _jwtSettings = jwtSettings.Value; //JwtSettings türünü, IPersonService olarak aldığımız için .value kullanıyoruz
            _otelService = otelService;
            _garageService = garageService;
        }

        // Token üretimi
        private string makeToken(UserLoginDto userLogin)
        {
            if (_jwtSettings.Key == null)
            {
                throw new Exception("Key null olamaz");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claim = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userLogin.Id.ToString()),
                new Claim(ClaimTypes.Email, userLogin.Email),
                new Claim(ClaimTypes.Role, userLogin.Role.ToString()),
                new Claim("CreatedDate", DateTime.Now.ToString())

            };

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claim,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Kullanıcı doğrulama işlemi
        private UserLoginDto useAuth(UserLoginDto userLogin)
        {
            // Burada IPersonService üzerinden veritabanı kontrolü yapılmalı
            var user = _personService.GetAllPerson()
                .FirstOrDefault(x => x.Email == userLogin.Email && x.Password == userLogin.Password); // Not: Şifre hash'li değilse
            //MAPLENECEK!!!!!!!!!!!!!!!
            if (user == null)
                return null;

            return new UserLoginDto
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                Role = user.role
            };
        }

        [HttpPost("login")]
        public IActionResult userLogin([FromBody] UserLoginDto userLogin)
        {
            var userLoginDto = useAuth(userLogin);
            if (userLoginDto == null)
            {
                return Unauthorized("Geçersiz kullanıcı bilgileri");
            }

            var token = makeToken(userLoginDto);
            return Ok(new { token }); // => JSON olarak {"token": "...."} döner
        }


        [HttpPost("register")]
        public UserRegisterDto Register([FromBody] UserRegisterDto registerDto)
        {
            // Kullanıcıyı ekle
            return _personService.UpsertPerson(registerDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost ("admin-otelleri")]
        public IActionResult GetHotels([FromBody] PagingInput<string> pagingInput)
        {
            var hotels = _otelService.GetAllOtels(pagingInput);     // Oteller
            var garages = _garageService.GetAllGarages(); // Garajlar

            return Ok(new
            {
                Hotels = hotels,
                Garages = garages
            });
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("garajlar")]
        public IActionResult GetGarages()
        {
            var garage = _garageService.GetAllGarages();

            return Ok(garage);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("change-role")]
        public IActionResult ChangeUserRole(UserRegisterDto userRegisterDto)
        {
            var updated = _personService.UpdatePerson(userRegisterDto);
            return Ok(updated);
        }
    }
}
