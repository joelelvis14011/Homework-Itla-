using Spectre.Console;
using RentCar.Services;
using RentCar.Models;

namespace RentCar.Screens
{
    public static class VehicleScreen
    {
        public static string Show()
        {
            Console.Clear();
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold blue]Vehicles Menu[/]")
                    .AddChoices("Add Vehicle", "List Vehicles", "Update Vehicle", "Delete Vehicle", "Back")
            );
        }

        public static void AddVehicle(VehicleService service)
        {
            var brand = AnsiConsole.Ask<string>("Enter [blue]Brand[/]:");
            var model = AnsiConsole.Ask<string>("Enter [blue]Model[/]:");
            var year = AnsiConsole.Ask<int>("Enter [blue]Year[/]:");
            var plate = AnsiConsole.Ask<string>("Enter [blue]License Plate[/]:");
            var price = AnsiConsole.Ask<decimal>("Enter [blue]Price per Day[/]:");

            var vehicle = new Vehicle
            {
                Brand = brand,
                Model = model,
                Year = year,
                LicensePlate = plate,
                PricePerDay = price
            };

            service.Create(vehicle);
            AnsiConsole.MarkupLine("[bold green]Vehicle added successfully![/]");
        }

        public static void ListVehicles(VehicleService service)
        {
            var vehicles = service.GetAll();
            var table = new Table().AddColumn("ID").AddColumn("Brand").AddColumn("Model").AddColumn("Year").AddColumn("Plate").AddColumn("Price/Day").AddColumn("Available");

            foreach (var v in vehicles)
                table.AddRow(v.Id.ToString(), v.Brand, v.Model, v.Year.ToString(), v.LicensePlate, v.PricePerDay.ToString("C"), v.IsAvailable ? "Yes" : "No");

            AnsiConsole.Write(table);
        }

        public static void UpdateVehicle(VehicleService service)
        {
            var id = AnsiConsole.Ask<int>("Enter [blue]Vehicle ID[/] to update:");
            var vehicle = service.GetById(id);
            if (vehicle == null)
            {
                AnsiConsole.MarkupLine("[red]Vehicle not found[/]");
                return;
            }

            vehicle.Brand = AnsiConsole.Ask<string>("Enter new [blue]Brand[/]:", vehicle.Brand);
            vehicle.Model = AnsiConsole.Ask<string>("Enter new [blue]Model[/]:", vehicle.Model);
            vehicle.Year = AnsiConsole.Ask<int>("Enter new [blue]Year[/]:", vehicle.Year);
            vehicle.LicensePlate = AnsiConsole.Ask<string>("Enter new [blue]License Plate[/]:", vehicle.LicensePlate);
            vehicle.PricePerDay = AnsiConsole.Ask<decimal>("Enter new [blue]Price per Day[/]:", vehicle.PricePerDay);

            service.Update(vehicle);
            AnsiConsole.MarkupLine("[bold green]Vehicle updated successfully![/]");
        }

        public static void DeleteVehicle(VehicleService service)
        {
            var id = AnsiConsole.Ask<int>("Enter [blue]Vehicle ID[/] to delete:");
            service.Delete(id);
            AnsiConsole.MarkupLine("[bold red]Vehicle deleted successfully![/]");
        }
    }
}
