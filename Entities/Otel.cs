using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Otel : BaseEntity
    {
        [StringLength(50)]
        public string OtelName { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        public virtual IEnumerable<Garage>? Garages { get; set; }
    }
}
