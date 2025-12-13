using System.ComponentModel.DataAnnotations;

namespace RentCar.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public int RentalContractId { get; set; }
        public RentalContract RentalContract { get; set; } = null!;

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        public decimal Amount { get; set; }

        public string Method { get; set; } = string.Empty;
    }
}