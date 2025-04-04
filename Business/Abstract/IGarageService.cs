using DTO.Garage;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IGarageService
    {
        List<GarageDto> GetAllGarages();

        GarageDto GetGarageById(long id);

        GarageDto CreateGarage(GarageDto garage);

        GarageDto UpdateGarage(GarageDto garage);

        GarageDto UpsertGarage(GarageDto garage);

        void DeleteGarage(long id);
       
    }
}
