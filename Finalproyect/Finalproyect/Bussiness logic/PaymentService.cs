using RentCar.Data;
using RentCar.Models;
using Microsoft.EntityFrameworkCore;

namespace RentCar.Services
{
    public class PaymentService
    {
        private readonly DbContextApp _context;
        public PaymentService(DbContextApp context)
        {
            _context = context;
        }

        public void Create(Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
        }

        public List<Payment> GetAll()
        {
            return _context.Payments
                .Include(p => p.RentalContract)
                .ToList();
        }

        public Payment? GetById(int id)
        {
            return _context.Payments
                .Include(p => p.RentalContract)
                .FirstOrDefault(p => p.Id == id);
        }

        public void Update(Payment payment)
        {
            _context.Payments.Update(payment);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var p = GetById(id);
            if (p != null)
            {
                _context.Payments.Remove(p);
                _context.SaveChanges();
            }
        }
    }
}
