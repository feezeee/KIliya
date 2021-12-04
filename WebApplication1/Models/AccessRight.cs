using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("access_rights")]
    public class AccessRight
    {
        [Column("access_right_id")]
        public int Id { get; set; }

        [Column("access_right_name")]
        [Required(ErrorMessage = "Не указаны права доступа")]
        [MaxLength(45)]
        [StringLength(45, ErrorMessage = "Длина строки должна быть до 45 символов")]
        [Remote(action: "CheckName", controller: "AccessRight", AdditionalFields = "Id", ErrorMessage = "Такие права доступа уже существуют", HttpMethod = "POST")]
        public string Name { get; set; }

        public List<Worker> Workers { get; set; } = new List<Worker>();

    }
}
