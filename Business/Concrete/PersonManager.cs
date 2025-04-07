using AutoMapper;
using Business.Abstract;
using Business.Validation.Person;
using DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DTO.Otel;
using DTO.Person;
using Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PersonManager : IPersonService
    {
        private IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly UserLoginValidator _loginValidator;
        private readonly UserRegisterValidator _registerValidator;
        private readonly PasswordService _passwordService;


        public PersonManager(IPersonRepository personRepository,
            IMapper mapper,
            UserLoginValidator loginValidator,
            UserRegisterValidator registerValidator,
            PasswordService passwordService)
        {
            _mapper = mapper;
            _personRepository = personRepository;
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
            _passwordService = passwordService;
        }

        public UserRegisterDto UpsertPerson(UserRegisterDto userRegisterDto)
        {
            if (userRegisterDto.Id == 0)
                return CreatePerson(userRegisterDto);
            else
                return UpdatePerson(userRegisterDto);
        }

        public UserRegisterDto CreatePerson(UserRegisterDto userRegister)
        {
            var validation = _registerValidator.Validate(userRegister);

            if (!validation.IsValid)
                throw new ValidationException(validation.Errors);

            using (var context = new OtelDbContext())
            {
                var entityPerson = _mapper.Map<Person>(userRegister);

                // 🔒 Şifre hashleniyor 
                entityPerson.Password = _passwordService.HashPassword(userRegister.Password);

                var createPerson = _personRepository.Add(context, entityPerson);
                context.SaveChanges();

                return _mapper.Map<UserRegisterDto>(createPerson);
            }
        }


        public UserRegisterDto UpdatePerson(UserRegisterDto userRegister)
        {
            var validation = _registerValidator.Validate(userRegister);

            if (!validation.IsValid)
                throw new ValidationException(validation.Errors);

            using (var context = new OtelDbContext())
            {
                var entityPerson = _mapper.Map<Person>(userRegister);

                // 🔒 Güncellenmiş şifre tekrar hashleniyor
                entityPerson.Password = _passwordService.HashPassword(userRegister.Password);

                var updatePerson = _personRepository.Update(context, entityPerson);
                context.SaveChanges();

                return _mapper.Map<UserRegisterDto>(updatePerson);
            }
        }


        public void DeletePerson(long id)
        {
            

            using (var context = new OtelDbContext())
            {
                var deletePerson = new UserRegisterDto { Id = id };
                _personRepository.Delete(context, id);

                context.SaveChanges();
            }
        }

        public List<UserResponseDto> GetAllPerson()
        {
            using (var context = new OtelDbContext())
            {
                var personQuery = _personRepository.GetAll(context);
                var personList = _mapper.ProjectTo<UserResponseDto>(personQuery);
                return personList.ToList();
            }
        }

        public UserResponseDto GetPersonById(long id)
        {
            

            using (var context = new OtelDbContext())
            {
                var personGet = _personRepository.GetById(context, id);
                return _mapper.Map<UserResponseDto>(personGet);
            }
        }

        public UserLoginDto GetOnePerson(long id)
        {

            using (var context = new OtelDbContext())
            {
                var personGet = _personRepository.GetById(context, id);
                return _mapper.Map<UserLoginDto>(personGet);
            }
        }

        public bool EmailExists(string email)
        {
            using (var context = new OtelDbContext())
            {
                return _personRepository.GetAll(context).Any(x => x.Email == email);
            }
        }
     


    }
}
