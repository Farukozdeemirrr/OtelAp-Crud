using DataAccess.Abstract;
using DTO.Person;
using FluentValidation;
using FluentValidation.Results;

namespace Business.Validation.Person
{
    public class UserLoginValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginValidator()
        {

            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email boş olamaz.")
               .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz.");


        }
    }
}