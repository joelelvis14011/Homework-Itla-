using RentCar.Data;
using RentCar.Models;
using Microsoft.EntityFrameworkCore;

namespace RentCar.Services
{
    public class MaintenanceService
    {
        private readonly DbContextApp _context;
        public MaintenanceService(DbContextApp context)
        {
            _context = context;
        }

        public void Add(Maintenance maintenance)
        {
            _context.Maintenances.Add(maintenance);
            _context.SaveChanges();
        }

        public List<Maintenance> GetAll()
        {
            return _context.Maintenances
                .Include(m => m.Vehicle)
                .ToList();
        }

        public Maintenance? GetById(int id)
        {
            return _context.Maintenances
                .Include(m => m.Vehicle)
                .FirstOrDefault(m => m.Id == id);
        }

        public void Update(Maintenance maintenance)
        {
            _context.Maintenances.Update(maintenance);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var m = GetById(id);
            if (m != null)
            {
                _context.Maintenances.Remove(m);
                _context.SaveChanges();
            }
        }
    }
}
