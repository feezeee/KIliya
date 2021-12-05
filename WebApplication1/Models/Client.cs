using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("clients")]
    public class Client
    {
        [Column("client_id")]
        public int Id { get; set; }


        [Column("first_name")]
        [Required(ErrorMessage = "Не указано имя клиента")]
        [MaxLength(45)]
        [StringLength(45, ErrorMessage = "Длина строки должна быть до 45 символов")]
        public string FirstName { get; set; }
        

        [Column("last_name")]
        [Required(ErrorMessage = "Не указана фамилия клиента")]
        [MaxLength(45)]
        [StringLength(45, ErrorMessage = "Длина строки должна быть до 45 символов")]
        public string LastName { get; set; }


        [Column("pass_number")]
        [Required(ErrorMessage = "Не указан паспорт")]
        [MaxLength(45)]
        [StringLength(45, ErrorMessage = "Длина строки должна быть до 45 символов")]
        [Remote(action: "CheckPass", controller: "Client", AdditionalFields = "Id", ErrorMessage = "Такой клиент уже существует!", HttpMethod = "POST")]
        public string PassNumber { get; set; }


        [Column("phone_number")]
        [MaxLength(45)]
        [StringLength(45, ErrorMessage = "Длина строки должна быть до 45 символов")]
        public string? PhoneNumber { get; set; }


        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
        
    }
}
