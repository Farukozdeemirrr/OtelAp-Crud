
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Garage
{
    public class GarageDto
    {
        public long? Id { get; set; }
        public long? OtelId { get; set; }
        public string? OtelName { get; set; }  // Otelin adını da ekledik!
        public int Capacity { get; set; }
        public int CarCount { get; set; }

        //OtelId yerine ilgili otelin adını(Name) de döndürüyoruz.
        //Böylece istemci, garajın hangi otelde olduğunu direkt görebilir.
        //Eğer Garage sadece bir OtelId döndürseydi, istemci OtelId’yi alıp başka bir API çağrısı yapmak zorunda kalırdı.

        //DB → DTO → Veritabanından gelen verileri istemciye göndermek için kullanılır. (GET işlemleri) OKUMA
        //DTO → DB → İstemciden gelen verileri veritabanına kaydetmek veya güncellemek için kullanılır. (POST, PUT, PATCH işlemleri) DEĞİŞİKLİK YAPMA
    }
}
