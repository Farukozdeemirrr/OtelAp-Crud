using AutoMapper;
using Business.Abstract;
using Business.Validation;
using DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DTO.Garage;
using DTO.Otel;
using Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class GarageManager : IGarageService
    {
        private IGarageRepository _garageRepository;
        private IMapper _mapper;

        private readonly GarageValidator _garageValidator;

        // EKLENDİ: Validator dependency injection ile alındı
        public GarageManager(
            IGarageRepository garageRepository,
            IMapper mapper,
            GarageValidator garageValidator)
        {
            _garageRepository = garageRepository;
            _mapper = mapper;
            _garageValidator = garageValidator;
        }

        public GarageDto UpsertGarage(GarageDto garage)
        {
            if (garage.Id == 0)
                return CreateGarage(garage);
            else
                return UpdateGarage(garage);
        }

        public GarageDto CreateGarage(GarageDto garage)
        {
            //// EKLENDİ: Validasyon işlemi
            //var validationResult = _garageValidator.Validate(garage);

            //if (!validationResult.IsValid)
            //{
            //    throw new ValidationException(validationResult.Errors);
            //}

            _garageValidator.Validate(garage, opt =>
            {
                opt.IncludeRuleSets("CreateRules");
                opt.ThrowOnFailures();
            });

            var entityGarage = _mapper.Map<Garage>(garage);

            using (var context = new OtelDbContext())
            {
                var createGarage = _garageRepository.Add(context, entityGarage);

                context.SaveChanges();

                return _mapper.Map<GarageDto>(createGarage);
            }
        }

        //--------------------------------------------------------------
        public GarageDto UpdateGarage(GarageDto garage)
        {
            //if (garage == null)
            //{
            //    throw new ArgumentNullException("Güncellenecek öğe bulunamadı");
            //}

            //// EKLENDİ: Validasyon işlemi
            //var validationResult = _garageValidator.Validate(garage);

            //if (!validationResult.IsValid)
            //{
            //    throw new ValidationException(validationResult.Errors);
            //}
            var validationResult = _garageValidator.Validate(garage, opt =>
            {
                opt.IncludeRuleSets("UpdateRules");

            });

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);

            }

            var entityGarage = _mapper.Map<Garage>(garage);

            using (var context = new OtelDbContext())
            {
                var result = _garageRepository.Update(context, entityGarage);
                context.SaveChanges();

                return _mapper.Map<GarageDto>(result);
            }

        }

        //----------------------------------------------------------
        //RULESETİ BURADA NASIL KULLANACAĞIZ????
        public void DeleteGarage(long id)
        {
            // Silme işlemi için geçerli bir DTO oluşturuyoruz.
            var deleteGarageDto = new GarageDto { Id = id };

            // "Delete" ruleseti üzerinden doğrulama yapıyoruz.
            var validationResult = _garageValidator.Validate(deleteGarageDto, options =>
            {
                options.IncludeRuleSets("DeleteRules");
                options.ThrowOnFailures();
            });

            using (var context = new OtelDbContext())
            {
                _garageRepository.Delete(context, id);
                context.SaveChanges();
            }
        }

        public List<GarageDto> GetAllGarages()
        {
            using (var context = new OtelDbContext())
            {
                var garageList = _garageRepository.GetAll(context);

                return _mapper.Map<List<GarageDto>>(garageList);
            }
        }

        public GarageDto GetGarageById(long id)
        {
            using (var context = new OtelDbContext())
            {
                var garageGet = _garageRepository.GetById(context, id);
                return _mapper.Map<GarageDto>(garageGet);
            }
        }

    }
}
