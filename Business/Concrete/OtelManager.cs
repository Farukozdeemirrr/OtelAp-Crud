using AutoMapper;

using Business.Abstract;
using Business.Validation;
using DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DTO.Garage;
using DTO.General;
using DTO.Otel;
using Entities;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OtelManager : IOtelService
    {
        private IOtelRepository _otelRepository;
        private readonly IMapper _mapper;
        private OtelValidator _otelValidator;

        public OtelManager(IOtelRepository otelRepository, IMapper mapper, OtelValidator otelValidator)
        {
            _otelRepository = otelRepository;
            _mapper = mapper;
            _otelValidator = otelValidator;
        }
        public OtelDto UpsertOtel(OtelDto otel)
        {
            if (otel.Id == 0)
            {
                return CreateOtel(otel);
            }
            else
            {
                return UpdateOtel(otel);
            }
        }

        public OtelDto CreateOtel(OtelDto otel)
        {

            //ValidationResult validationResult = _otelValidator.Validate(otel);

            //if (!validationResult.IsValid)
            //{
            //    throw new ValidationException(validationResult.Errors);
            //}

            _otelValidator.Validate(otel, opt =>
            {
                opt.IncludeRuleSets("CreateRules");
                opt.ThrowOnFailures();
            });

            using (var context = new OtelDbContext())
            {
                var entityOtel = _mapper.Map<Otel>(otel);
                var createOtel = _otelRepository.Add(context, entityOtel);
                context.SaveChanges();

                return _mapper.Map<OtelDto>(createOtel);
            }
        }

        public OtelDto UpdateOtel(OtelDto otel)
        {
            //ValidationResult validationResult = _otelValidator.Validate(otel);

            //if (!validationResult.IsValid)
            //{
            //    throw new ValidationException(validationResult.Errors);
            //}

            _otelValidator.Validate(otel, options =>
            {
                options.IncludeRuleSets("UpdateRules");
                options.ThrowOnFailures();
            });

            using (var context = new OtelDbContext())
            {
                var entityOtel = _mapper.Map<Otel>(otel);
                var updateOtel = _otelRepository.Update(context, entityOtel);
                context.SaveChanges();

                return _mapper.Map<OtelDto>(updateOtel);
            }
        }

        public void DeleteOtel(long id)
        {
            //BURAYI SOR BAŞKA YOLU VAR MI???
            // Silme işlemi için geçerli bir DTO oluşturuyoruz.
            var deleteOtelDto = new OtelDto { Id = id };

            // "Delete" ruleseti üzerinden doğrulama yapıyoruz.
            var validationResult = _otelValidator.Validate(deleteOtelDto, options => options.IncludeRuleSets("DeleteRules"));
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);


            }
            using (var context = new OtelDbContext())
            {
                _otelRepository.Delete(context, id);
                context.SaveChanges();
            }

        }

        public List<OtelDto> GetAllOtels(PagingInput<string> pagingInput)
        {

            using (var context = new OtelDbContext())
            {
                var otelQuery = _otelRepository.GetAll(context);
                var projectedQuery = _mapper.ProjectTo<OtelDto>(otelQuery);

                var result = new List<OtelDto>();
                
                if (!string.IsNullOrWhiteSpace(pagingInput.Data))
                {
                    projectedQuery= projectedQuery.Where(x => x.City == pagingInput.Data || x.Name == pagingInput.Data);

                }

                if (pagingInput.Enable)
                {
                    result = projectedQuery.Skip((pagingInput.PageNumber - 1) * pagingInput.PageSize).Take(pagingInput.PageSize).ToList();
                }
                else
                {
                    result = projectedQuery.ToList();
                }

                return result;
            }
        }

        public OtelDto GetOtelById(long id)
        {

            using (var context = new OtelDbContext())
            {
                var otelGet = _otelRepository.GetById(context, id);
                return _mapper.Map<OtelDto>(otelGet);
            }
        }
    }
}
