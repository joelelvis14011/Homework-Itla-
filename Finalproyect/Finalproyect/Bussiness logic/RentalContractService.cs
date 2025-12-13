using RentCar.Data;
using RentCar.Models;
using Microsoft.EntityFrameworkCore;

namespace RentCar.Services
{
    public class RentalContractService
    {
        private readonly DbContextApp _context;
        public RentalContractService(DbContextApp context)
        {
            _context = context;
        }

        public void Create(RentalContract contract)
        {
            _context.RentalContracts.Add(contract);
            _context.SaveChanges();
        }

        public List<RentalContract> GetAll()
        {
            return _context.RentalContracts
                .Include(c => c.Customer)
                .Include(c => c.Vehicle)
                .Include(c => c.Payments)
                .ToList();
        }

        public RentalContract? GetById(int id)
        {
            return _context.RentalContracts
                .Include(c => c.Customer)
                .Include(c => c.Vehicle)
                .Include(c => c.Payments)
                .FirstOrDefault(c => c.Id == id);
        }

        public void Update(RentalContract contract)
        {
            _context.RentalContracts.Update(contract);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var c = GetById(id);
            if (c != null)
            {
                _context.RentalContracts.Remove(c);
                _context.SaveChanges();
            }
        }
    }
}
