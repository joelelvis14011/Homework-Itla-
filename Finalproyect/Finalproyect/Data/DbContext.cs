using Microsoft.EntityFrameworkCore;
using RentCar.Models;
using System.Collections.Generic;

namespace RentCar.Data
{
    public class DbContextApp : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<RentalContract> RentalContracts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(
                "Server=JC;Database=RentCarDB;Trusted_Connection=True;TrustServerCertificate=True"
            );
        }
    }
}
