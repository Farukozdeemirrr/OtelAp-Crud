
using Business.Abstract;
using Business.Concrete;
using Business.JWT;
using Business.Mapping;
using Business.Validation;

using DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Token oluþturuyoruz
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt => 
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty))
                    };
                });

            //Oluþturduðumuz tokený bütün controllerslara enjekte ediyoruz.
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

            // Add services to the container.

            //builder.Services.AddDbContext<OtelDbContext>(options =>
            //options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IOtelService, OtelManager>();
            builder.Services.AddScoped<IOtelRepository, OtelRepository>();

            builder.Services.AddScoped<IPersonService, PersonManager>(); // BU SATIR GEREKLÝ
            builder.Services.AddScoped<IPersonRepository, PersonRepository>();

            builder.Services.AddScoped<IGarageService, GarageManager>();
            builder.Services.AddScoped<IGarageRepository, GarageRepository>();

            builder.Services.AddAutoMapper(typeof(MappingProfiles));

            // ? FluentValidation ekleme
            builder.Services.AddValidatorsFromAssemblyContaining<OtelValidator>(); // OtelValidator otomatik bulunur
            builder.Services.AddFluentValidationAutoValidation(); // Otomatik validasyon middleware ekler
            //builder.Services.AddScoped<GarageValidator>();
            //builder.Services.AddFluentValidationAutoValidation();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
