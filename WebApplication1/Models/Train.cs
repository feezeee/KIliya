using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("trains")]
    public class Train
    {
        [Column("train_id")]
        public int Id { get; set; }

        [Column("train_name")]
        [Required(ErrorMessage = "Не указано наименование поезда")]
        [MaxLength(45)]
        [StringLength(45, ErrorMessage = "Длина строки должна быть до 45 символов")]
        //[Remote(action: "CheckName", controller: "Train", AdditionalFields = "Id", ErrorMessage = "Такой поезд уже существует!", HttpMethod = "POST")]
        public string Name { get; set; }


        public List<TrainDestination> TrainDestinations { get; set; } = new List<TrainDestination>();
        public List<TrainVanSit> TrainVanSits { get; set; } = new List<TrainVanSit>();
    }
}
