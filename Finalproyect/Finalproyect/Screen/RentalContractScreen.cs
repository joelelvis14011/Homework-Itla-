using Spectre.Console;
using RentCar.Services;
using RentCar.Models;

namespace RentCar.Screens
{
    public static class RentalContractScreen
    {
        public static string Show()
        {
            Console.Clear();
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Contracts Menu[/]")
                    .AddChoices("Create Contract", "List Contracts", "Update Contract", "Cancel Contract", "Back")
            );
        }

        public static void CreateContract(
            RentalContractService service,
            CustomerService customerService,
            VehicleService vehicleService)
        {
            var customerId = AnsiConsole.Ask<int>("Enter [yellow]Customer ID[/]:");
            var vehicleId = AnsiConsole.Ask<int>("Enter [yellow]Vehicle ID[/]:");
            var start = AnsiConsole.Ask<DateTime>("Enter [yellow]Start Date[/]:");
            var end = AnsiConsole.Ask<DateTime>("Enter [yellow]End Date[/]:");

            var vehicle = vehicleService.GetById(vehicleId);
            if (vehicle == null || !vehicle.IsAvailable)
            {
                AnsiConsole.MarkupLine("[red]Vehicle not available[/]");
                return;
            }

            var days = (end - start).Days;
            var total = days * vehicle.PricePerDay;

            var contract = new RentalContract
            {
                CustomerId = customerId,
                VehicleId = vehicleId,
                StartDate = start,
                EndDate = end,
                TotalCost = total,
                IsActive = true
            };

            service.Create(contract);

            vehicle.IsAvailable = false;
            vehicleService.Update(vehicle);

            AnsiConsole.MarkupLine($"[bold green]Contract created successfully! Total: {total:C}[/]");
        }

        public static void ListContracts(RentalContractService service)
        {
            var contracts = service.GetAll();

            var table = new Table()
                .AddColumn("ID")
                .AddColumn("Customer")
                .AddColumn("Vehicle")
                .AddColumn("Start")
                .AddColumn("End")
                .AddColumn("Total")
                .AddColumn("Active");

            foreach (var c in contracts)
            {
                table.AddRow(
                    c.Id.ToString(),
                    c.Customer.FullName,
                    c.Vehicle.Model,
                    c.StartDate.ToShortDateString(),
                    c.EndDate.ToShortDateString(),
                    c.TotalCost.ToString("C"),
                    c.IsActive ? "Yes" : "No"
                );
            }

            AnsiConsole.Write(table);
        }

        public static void UpdateContract(
    RentalContractService service,
    VehicleService vehicleService)
        {
            var id = AnsiConsole.Ask<int>("Enter [yellow]Contract ID[/] to update:");
            var contract = service.GetById(id);

            if (contract == null)
            {
                AnsiConsole.MarkupLine("[red]Contract not found[/]");
                return;
            }

            if (!contract.IsActive)
            {
                AnsiConsole.MarkupLine("[red]Cannot update an inactive contract[/]");
                return;
            }

            var newEnd = AnsiConsole.Ask<DateTime>(
                "Enter new [yellow]End Date[/]:",
                contract.EndDate);

            if (newEnd <= contract.StartDate)
            {
                AnsiConsole.MarkupLine("[red]End date must be after start date[/]");
                return;
            }

            contract.EndDate = newEnd;

            // 🔑 Asegurar que el vehículo esté cargado
            if (contract.Vehicle == null)
            {
                contract.Vehicle = vehicleService.GetById(contract.VehicleId);
                if (contract.Vehicle == null)
                {
                    AnsiConsole.MarkupLine("[red]Associated vehicle not found[/]");
                    return;
                }
            }

            var days = (contract.EndDate - contract.StartDate).Days;
            contract.TotalCost = days * contract.Vehicle.PricePerDay;

            service.Update(contract);

            AnsiConsole.MarkupLine("[bold green]Contract updated successfully![/]");
        }


        public static void CancelContract(
            RentalContractService service,
            VehicleService vehicleService)
        {
            var id = AnsiConsole.Ask<int>("Enter [yellow]Contract ID[/] to cancel:");
            var contract = service.GetById(id);

            if (contract == null)
            {
                AnsiConsole.MarkupLine("[red]Contract not found[/]");
                return;
            }

            contract.IsActive = false;
            service.Update(contract);

            var vehicle = vehicleService.GetById(contract.VehicleId);
            if (vehicle != null)
            {
                vehicle.IsAvailable = true;
                vehicleService.Update(vehicle);
            }

            AnsiConsole.MarkupLine("[bold red]Contract cancelled successfully![/]");
        }
    }
}
