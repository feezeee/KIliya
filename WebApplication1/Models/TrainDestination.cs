using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("trains_destinations")]
    public class TrainDestination
    {
        [Column("train_destination_id")]
        public int Id { get; set; }

        [Column("train_id")]
        [Required]
        public int TrainId { get; set; }
        public Train? Train { get; set; }


        [Column("destination_id")]
        [Required]
        public int DestinationId { get; set; }
        public Destination? Destination { get; set; }


        [Column("departure_time")]
        public DateTime? DepartureTime { get; set; }

        [Column("arrival_time")]
        public DateTime? ArrivalTime { get; set; }
    }
}
