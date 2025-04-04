using DTO.Otel;
using FluentValidation;

namespace Business.Validation
{
    public class OtelValidator : AbstractValidator<OtelDto>
    {
        public OtelValidator()
        {
            // Oluşturma (Create) İşlemi
            RuleSet("CreateRules", () =>
            {
                RuleFor(x => x.Id)
                    .Must(id => id == null || id == 0)
                    .WithMessage("Yeni otel oluşturulurken Id değeri boş veya 0 olmalıdır.");

                RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Otel adı zorunludur.")
                    .MaximumLength(50).WithMessage("Otel adı en fazla 50 karakter olabilir.");

                RuleFor(x => x.City)
                    .NotEmpty().WithMessage("Şehir bilgisi zorunludur.")
                    .MaximumLength(50).WithMessage("Şehir adı en fazla 50 karakter olabilir.");
            });

            // Güncelleme (Update) İşlemi
            RuleSet("UpdateRules", () =>
            {
                RuleFor(x => x.Id)
                    .NotNull().WithMessage("Güncelleme için geçerli bir Id gereklidir.")
                    .GreaterThan(0).WithMessage("Güncelleme için Id 0'dan büyük olmalıdır.");

                RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Otel adı zorunludur.")
                    .MaximumLength(50).WithMessage("Otel adı en fazla 50 karakter olabilir.");

                RuleFor(x => x.City)
                    .NotEmpty().WithMessage("Şehir bilgisi zorunludur.")
                    .MaximumLength(50).WithMessage("Şehir adı en fazla 50 karakter olabilir.");
            });

            // Silme (Delete) İşlemi
            RuleSet("DeleteRules", () =>
            {
                RuleFor(x => x.Id)
                    .NotNull().WithMessage("Silme işlemi için geçerli bir Id gereklidir.")
                    .GreaterThan(0).WithMessage("Silme işlemi için Id 0'dan büyük olmalıdır.");
            });
        }
    }
}
