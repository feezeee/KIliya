using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("tickets")]
    public class Ticket
    {
        [Column("ticket_id")]
        public int Id { get; set; }

        [Column("client_id")]
        public int ClientId { get; set; }
        public Client? Client { get; set; }


        [Column("worker_id")]
        public int WorkerId { get; set; }
        public Worker? Worker { get; set; }

        [Column("all_price")]
        [Range(0, int.MaxValue, ErrorMessage = "Цена не может быть отрицательной")]
        public int AllPrice { get; set; }

        public List<TrainVanSit> Van_SitPlaces { get; set; } = new List<TrainVanSit>();

    }
}
