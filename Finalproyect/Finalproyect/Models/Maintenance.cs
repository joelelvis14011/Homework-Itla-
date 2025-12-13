using System.ComponentModel.DataAnnotations;

namespace RentCar.Models
{
    public class Maintenance
    {
        public int Id { get; set; }

        [Required]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public decimal Cost { get; set; }
    }
}
