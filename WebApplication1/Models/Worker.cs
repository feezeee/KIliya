using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("workers")]
    public class Worker
    {
        public Worker()
        {
            Password = "0000";
        }
        [Column("worker_id")]
        public int Id { get; set; }

        [Column("first_name")]
        [Required(ErrorMessage = "Не указано имя работника")]
        [MaxLength(45)]
        [StringLength(45, ErrorMessage = "Длина строки должна быть до 45 символов")]
        public string FirstName { get; set; }

        [Column("last_name")]
        [Required(ErrorMessage = "Не указана фамилия работника")]
        [MaxLength(45)]
        [StringLength(45, ErrorMessage = "Длина строки должна быть до 45 символов")]
        public string LastName { get; set; }


        [Column("phone_number")]
        [Required(ErrorMessage = "Не указан номер телефона")]
        [MaxLength(45)]
        [StringLength(45, ErrorMessage = "Длина строки должна быть до 45 символов")]
        public string PhoneNumber { get; set; }


        [Column("password")]
        [MaxLength(45)]
        [StringLength(45, ErrorMessage = "Длина строки должна быть до 45 символов")]
        public string Password { get; set; }


        [Column("access_right_id")]
        public int AccessRightId { get; set; }
        public AccessRight? AccessRight { get; set; }


        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
