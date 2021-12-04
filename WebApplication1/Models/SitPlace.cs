using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("sit_places")]
    public class SitPlace
    {
        [Column("sit_place_id")]
        public int Id { get; set; }

        [Column("place_name")]
        [Required(ErrorMessage = "Не указано наименование поезда")]
        [MaxLength(45)]
        [StringLength(45, ErrorMessage = "Длина строки должна быть до 45 символов")]
        [Remote(action: "CheckName", controller: "SitPlace", AdditionalFields = "Id", ErrorMessage = "Такое место уже существует!", HttpMethod = "POST")]
        public string Name { get; set; }

        [Range(0,int.MaxValue, ErrorMessage = "Цена не может быть отрицательной")]
        [Column("place_price")]
        public int Price { get; set; }


        public List<TrainVanSit> TrainVanSits { get; set; } = new List<TrainVanSit>();

    }
}
