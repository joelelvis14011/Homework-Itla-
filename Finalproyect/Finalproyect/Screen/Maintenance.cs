using Spectre.Console;
using RentCar.Services;
using RentCar.Models;

namespace RentCar.Screens
{
    public static class MaintenanceScreen
    {
        public static string Show()
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Maintenance Menu[/]")
                    .AddChoices("Add Maintenance", "List Maintenances", "Update Maintenance", "Delete Maintenance", "Back")
            );
        }

        public static void AddMaintenance(MaintenanceService maintenanceService, VehicleService vehicleService)
        {
            var vehicleId = AnsiConsole.Ask<int>("Enter Vehicle ID:");
            var vehicle = vehicleService.GetById(vehicleId);
            if (vehicle == null)
            {
                AnsiConsole.MarkupLine("[red]Vehicle not found[/]");
                return;
            }

            var description = AnsiConsole.Ask<string>("Enter description:");
            var cost = AnsiConsole.Ask<decimal>("Enter cost:");

            var maintenance = new Maintenance
            {
                VehicleId = vehicleId,
                Date = DateTime.Now,
                Description = description,
                Cost = cost
            };

            maintenanceService.Add(maintenance);
            AnsiConsole.MarkupLine("[green]Maintenance record added successfully[/]");
        }

        public static void ListMaintenances(MaintenanceService maintenanceService)
        {
            var maintenances = maintenanceService.GetAll();
            if (maintenances.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]No maintenance records found[/]");
                return;
            }

            var table = new Table()
                .AddColumn("ID")
                .AddColumn("Vehicle")
                .AddColumn("Date")
                .AddColumn("Description")
                .AddColumn("Cost");

            foreach (var m in maintenances)
            {
                table.AddRow(
                    m.Id.ToString(),
                    m.Vehicle?.Model ?? $"ID:{m.VehicleId}",
                    m.Date.ToShortDateString(),
                    m.Description,
                    m.Cost.ToString("C")
                );
            }

            AnsiConsole.Write(table);
        }

        public static void UpdateMaintenance(MaintenanceService maintenanceService, VehicleService vehicleService)
        {
            var id = AnsiConsole.Ask<int>("Enter Maintenance ID to update:");
            var m = maintenanceService.GetById(id);
            if (m == null)
            {
                AnsiConsole.MarkupLine("[red]Maintenance not found[/]");
                return;
            }

            AnsiConsole.MarkupLine($"[bold]Current:[/] VehicleId={m.VehicleId}, Date={m.Date:yyyy-MM-dd}, Desc={m.Description}, Cost={m.Cost:C}");

            if (AnsiConsole.Confirm("Change vehicle?"))
            {
                var newVehicleId = AnsiConsole.Ask<int>("Enter new Vehicle ID:");
                var v = vehicleService.GetById(newVehicleId);
                if (v == null)
                {
                    AnsiConsole.MarkupLine("[red]New vehicle not found[/]");
                    return;
                }
                m.VehicleId = newVehicleId;
            }

            if (AnsiConsole.Confirm("Change date?"))
                m.Date = AnsiConsole.Ask<DateTime>("Enter date (yyyy-MM-dd):");

            if (AnsiConsole.Confirm("Change description?"))
                m.Description = AnsiConsole.Ask<string>("Enter description:");

            if (AnsiConsole.Confirm("Change cost?"))
                m.Cost = AnsiConsole.Ask<decimal>("Enter cost:");

            maintenanceService.Update(m);
            AnsiConsole.MarkupLine("[green]Maintenance updated successfully[/]");
        }

        public static void DeleteMaintenance(MaintenanceService maintenanceService)
        {
            var id = AnsiConsole.Ask<int>("Enter Maintenance ID to delete:");
            var m = maintenanceService.GetById(id);
            if (m == null)
            {
                AnsiConsole.MarkupLine("[red]Maintenance not found[/]");
                return;
            }

            if (AnsiConsole.Confirm($"Are you sure you want to delete maintenance ID {id}?"))
            {
                maintenanceService.Delete(id);
                AnsiConsole.MarkupLine("[green]Maintenance deleted successfully[/]");
            }
        }
    }
}
