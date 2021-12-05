using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("trains_has_vans_has_sits")]
    public class TrainVanSit
    {
        [Column("train_van_sit_id")]
        public int TrainVanSitId { get; set; }

        [Required]
        [Column("train_id")]
        public int TrainId { get; set; }
        public Train? Train { get; set; }

        [Required]
        [Column("van_id")]
        public int VanId { get; set; }
        public Van? Van { get; set; }



        [Required]
        [Column("sit_place_id")]
        public int SitPlaceId { get; set; }
        public SitPlace? SitPlace { get; set; }



        public List<Ticket> Ticket { get; set; } = new List<Ticket>();

    }
}
