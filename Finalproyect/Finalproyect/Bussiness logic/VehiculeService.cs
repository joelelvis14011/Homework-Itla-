using RentCar.Data;
using RentCar.Models;

namespace RentCar.Services
{
    public class VehicleService
    {
        private readonly DbContextApp _context;
        public VehicleService(DbContextApp context)
        {
            _context = context;
        }


        public void Create(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();
        }

        public List<Vehicle> GetAll()
        {
            return _context.Vehicles.ToList();
        }

        public Vehicle? GetById(int id)
        {
            return _context.Vehicles.FirstOrDefault(v => v.Id == id);
        }

        public void Update(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var v = GetById(id);
            if (v != null)
            {
                _context.Vehicles.Remove(v);
                _context.SaveChanges();
            }
        }
    }
}
