using DTO.General;
using DTO.Otel;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOtelService
    {
        List<OtelDto> GetAllOtels(PagingInput<string> pagingInput);
        OtelDto GetOtelById(long id);
        OtelDto CreateOtel(OtelDto otel);
        OtelDto UpdateOtel(OtelDto otel);
        OtelDto UpsertOtel (OtelDto otel);
        void DeleteOtel(long id);
       
    }
}
