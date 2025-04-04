using DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DTO.Otel;
using DTO.Person;
using Entities;
using FluentValidation;
using FluentValidation.Results;

public class UserRegisterValidator : AbstractValidator<UserRegisterDto>
{
    private IPersonRepository _personRepository;
    public UserRegisterValidator(IPersonRepository personRepository)
    {
        _personRepository = personRepository;

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Ad boş olamaz.")
            .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Soyad boş olamaz.")
            .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş olamaz.")
            .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre boş olamaz.")
            .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalı.");

        RuleFor(x => x.Email).Custom((x, context) =>
        {
            if (EmailExists(x))
                context.AddFailure(new ValidationFailure
                {
                    ErrorMessage = "Bu e-posta adresi zaten kayıtlı."
                });
        });
    }

    private bool EmailExists(string email)
    {

        using (var context = new OtelDbContext())
        {
            return _personRepository.GetAll(context).Any(x => x.Email == email);
        }
        
    }
}
