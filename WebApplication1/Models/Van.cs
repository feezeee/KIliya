using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("vans")]
    public class Van
    {
        [Column("van_id")]
        public int Id { get; set; }

        [Column("van_name")]
        [Required(ErrorMessage = "Не указано наименование поезда")]
        [MaxLength(45)]
        [StringLength(45, ErrorMessage = "Длина строки должна быть до 45 символов")]
        [Remote(action: "CheckName", controller: "Van", AdditionalFields = "Id", ErrorMessage = "Такой вагон уже существует!", HttpMethod = "POST")]
        public string Name { get; set; }


        public List<TrainVanSit> TrainVanSits { get; set; } = new List<TrainVanSit>();


    }
}
