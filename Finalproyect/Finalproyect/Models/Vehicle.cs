using System.ComponentModel.DataAnnotations;

namespace RentCar.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required]
        public string Brand { get; set; } = string.Empty;

        [Required]
        public string Model { get; set; } = string.Empty;

        public int Year { get; set; }

        [Required]
        public string LicensePlate { get; set; } = string.Empty;

        public bool IsAvailable { get; set; } = true;

        public decimal PricePerDay { get; set; }

        public List<RentalContract> Contracts { get; set; } = new();

        public List<Maintenance> Maintenances { get; set; } = new();

    }
}
