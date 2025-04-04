using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Garage : BaseEntity
    {
        public long OtelId { get; set; }

        [ForeignKey("OtelId")]
        public virtual Otel? otel { get; set; }
        public int Capacity { get; set; }
        public int CarCount { get; set; }
    }
}
