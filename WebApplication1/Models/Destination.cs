using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("destinations")]
    public class Destination
    {

        [Column("destination_id")]
        public int Id { get; set; }

        [Column("destination_name")]
        [Required(ErrorMessage = "Не указана станция")]
        [MaxLength(45)]
        [StringLength(45, ErrorMessage = "Длина строки должна быть до 45 символов")]
        [Remote(action: "CheckName", controller: "Destination", AdditionalFields = "Id", ErrorMessage = "Такой пункт назначения уже существует!", HttpMethod = "POST")]
        public string Name { get; set; }


        public List<TrainDestination> TrainDestinations { get; set; } = new List<TrainDestination>();        

    }
}
