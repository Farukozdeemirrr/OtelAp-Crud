using DTO.Garage;
using FluentValidation;

namespace Business.Validation
{
    public class GarageValidator : AbstractValidator<GarageDto>
    {
        public GarageValidator()
        {
            // Oluşturma (Create) İşlemi
            RuleSet("CreateRules", () =>
            {
                // Yeni garaj oluşturulurken Id boş veya 0 olmalıdır.
                RuleFor(x => x.Id)
                    .Must(id => id == null || id == 0)
                    .WithMessage("Yeni garaj oluşturulurken Id değeri boş veya 0 olmalıdır.");

                RuleFor(x => x.OtelId)
                    .NotNull().WithMessage("Otel Id'si gereklidir.")
                    .GreaterThan(0).WithMessage("Geçerli bir Otel Id giriniz.");

                RuleFor(x => x.Capacity)
                    .GreaterThan(0).WithMessage("Oluşturma için kapasite sıfırdan büyük olmalıdır.")
                    .LessThanOrEqualTo(1000).WithMessage("Kapasite 1000'den fazla olamaz.");

                RuleFor(x => x.CarCount)
                    .GreaterThanOrEqualTo(0).WithMessage("Araç sayısı negatif olamaz.")
                    .LessThanOrEqualTo(x => x.Capacity)
                    .WithMessage("Araç sayısı kapasiteyi aşamaz.");
            });

            // Güncelleme (Update) İşlemi
            RuleSet("UpdateRules", () =>
            {
                RuleFor(x => x.Id)
                    .NotNull().WithMessage("Güncelleme için geçerli bir Id gereklidir.")
                    .GreaterThan(0).WithMessage("Güncelleme için Id 0'dan büyük olmalıdır.");

                RuleFor(x => x.OtelId)
                    .NotNull().WithMessage("Güncelleme için Otel Id gereklidir.")
                    .GreaterThan(0).WithMessage("Güncelleme için geçerli bir Otel Id giriniz.");

                RuleFor(x => x.Capacity)
                    .GreaterThan(0).WithMessage("Güncelleme için kapasite sıfırdan büyük olmalıdır.")
                    .LessThanOrEqualTo(1000).WithMessage("Kapasite 1000'den fazla olamaz.");

                RuleFor(x => x.CarCount)
                    .GreaterThanOrEqualTo(0).WithMessage("Araç sayısı negatif olamaz.")
                    .LessThanOrEqualTo(x => x.Capacity)
                    .WithMessage("Araç sayısı kapasiteyi aşamaz.");
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
