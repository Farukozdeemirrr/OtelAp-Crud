using DTO.Garage;
using DTO.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPersonService
    {
        List<UserResponseDto> GetAllPerson();

        UserResponseDto GetPersonById(long id);
        UserLoginDto GetOnePerson(long id);

        UserRegisterDto CreatePerson(UserRegisterDto userRegisterDto);

        UserRegisterDto UpdatePerson(UserRegisterDto userRegisterDto);

        UserRegisterDto UpsertPerson(UserRegisterDto userRegisterDto);

        bool EmailExists(string email);

        void DeletePerson(long id);


    }
}
