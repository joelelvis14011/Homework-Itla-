using RentCar.Data;
using RentCar.Models;

namespace RentCar.Services
{
    public class CustomerService
    {
        private readonly DbContextApp _context;

        public CustomerService(DbContextApp context)
        {
            _context = context;
        }


        public void Create(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public List<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }

        public Customer? GetById(int id)
        {
            return _context.Customers.FirstOrDefault(c => c.Id == id);
        }

        public void Update(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var c = GetById(id);
            if (c != null)
            {
                _context.Customers.Remove(c);
                _context.SaveChanges();
            }
        }
    }
}
