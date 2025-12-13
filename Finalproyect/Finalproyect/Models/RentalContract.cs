using System.ComponentModel.DataAnnotations;

namespace RentCar.Models
{
    public class RentalContract
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = null!;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public decimal TotalCost { get; set; }

        public bool IsActive { get; set; } = true;

        public List<Payment> Payments { get; set; } = new();
    }
}
