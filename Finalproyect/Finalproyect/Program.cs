using Spectre.Console;
using RentCar.Data;
using RentCar.Services;
using RentCar.Screens;

class Program
{
    static void Main()
    {
        var context = new DbContextApp();

        var customerService = new CustomerService(context);
        var vehicleService = new VehicleService(context);
        var contractService = new RentalContractService(context);
        var paymentService = new PaymentService(context);
        var maintenanceService = new MaintenanceService(context);

        while (true)
        {
            Console.Clear();

            // 🔰 Logo PastorRentCar
            AnsiConsole.MarkupLine("[bold yellow]-----Pastor_RentCar-----[/]\n");
            Console.WriteLine(@"
                ______
               /|_||_\`.__
              (   _    _ _\
             =`-(_)--(_)-'
            ");

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Main Menu PastorRentCar[/]")
                    .AddChoices("Customers", "Vehicles", "Contracts", "Payments", "Maintenance", "Exit")
            );

            switch (option)
            {
                case "Customers":
                    HandleCustomerMenu(customerService);
                    break;
                case "Vehicles":
                    HandleVehicleMenu(vehicleService);
                    break;
                case "Contracts":
                    HandleContractMenu(contractService, customerService, vehicleService);
                    break;
                case "Payments":
                    HandlePaymentMenu(paymentService, contractService);
                    break;
                case "Maintenance":
                    HandleMaintenanceMenu(maintenanceService, vehicleService);
                    break;
                case "Exit":
                    return;
            }

            Console.ReadKey();
        }
    }

    // 🔹 Submenú de Customers
    static void HandleCustomerMenu(CustomerService customerService)
    {
        while (true)
        {
            Console.Clear();
            var custOption = CustomerScreen.Show();

            switch (custOption)
            {
                case "Add Customer":
                    CustomerScreen.AddCustomer(customerService);
                    break;
                case "List Customers":
                    CustomerScreen.ListCustomers(customerService);
                    break;
                case "Update Customer":
                    CustomerScreen.UpdateCustomer(customerService);
                    break;
                case "Delete Customer":
                    CustomerScreen.DeleteCustomer(customerService);
                    break;
                case "Back":
                    return;
            }

            AnsiConsole.MarkupLine("\n[grey]Press any key to return to Customers Menu...[/]");
            Console.ReadKey();
        }
    }

    // 🔹 Submenú de Vehicles
    static void HandleVehicleMenu(VehicleService vehicleService)
    {
        while (true)
        {
            Console.Clear();
            var vehOption = VehicleScreen.Show();

            switch (vehOption)
            {
                case "Add Vehicle":
                    VehicleScreen.AddVehicle(vehicleService);
                    break;
                case "List Vehicles":
                    VehicleScreen.ListVehicles(vehicleService);
                    break;
                case "Update Vehicle":
                    VehicleScreen.UpdateVehicle(vehicleService);
                    break;
                case "Delete Vehicle":
                    VehicleScreen.DeleteVehicle(vehicleService);
                    break;
                case "Back":
                    return;
            }

            AnsiConsole.MarkupLine("\n[grey]Press any key to return to Vehicles Menu...[/]");
            Console.ReadKey();
        }
    }

    // 🔹 Submenú de Contracts
    static void HandleContractMenu(RentalContractService contractService, CustomerService customerService, VehicleService vehicleService)
    {
        while (true)
        {
            Console.Clear();
            var contractOption = RentalContractScreen.Show();

            switch (contractOption)
            {
                case "Create Contract":
                    RentalContractScreen.CreateContract(contractService, customerService, vehicleService);
                    break;
                case "List Contracts":
                    RentalContractScreen.ListContracts(contractService);
                    break;
                case "Update Contract":
                    RentalContractScreen.UpdateContract(contractService, vehicleService);
                    break;
                case "Cancel Contract":
                    RentalContractScreen.CancelContract(contractService, vehicleService);
                    break;
                case "Back":
                    return;
            }

            AnsiConsole.MarkupLine("\n[grey]Press any key to return to Contracts Menu...[/]");
            Console.ReadKey();
        }
    }

    // 🔹 Submenú de Payments
    static void HandlePaymentMenu(PaymentService paymentService, RentalContractService contractService)
    {
        while (true)
        {
            Console.Clear();
            var payOption = PaymentScreen.Show();

            switch (payOption)
            {
                case "Add Payment":
                    PaymentScreen.AddPayment(paymentService, contractService);
                    break;
                case "List Payments":
                    PaymentScreen.ListPayments(paymentService);
                    break;
                case "Update Payment":
                    PaymentScreen.UpdatePayment(paymentService);
                    break;
                case "Delete Payment":
                    PaymentScreen.DeletePayment(paymentService);
                    break;
                case "Back":
                    return;
            }

            AnsiConsole.MarkupLine("\n[grey]Press any key to return to Payments Menu...[/]");
            Console.ReadKey();
        }
    }

    // 🔹 Submenú de Maintenance
    static void HandleMaintenanceMenu(MaintenanceService maintenanceService, VehicleService vehicleService)
    {
        while (true)
        {
            Console.Clear();
            var option = MaintenanceScreen.Show();

            switch (option)
            {
                case "Add Maintenance":
                    MaintenanceScreen.AddMaintenance(maintenanceService, vehicleService);
                    break;
                case "List Maintenances":
                    MaintenanceScreen.ListMaintenances(maintenanceService);
                    break;
                case "Update Maintenance":
                    MaintenanceScreen.UpdateMaintenance(maintenanceService, vehicleService);
                    break;
                case "Delete Maintenance":
                    MaintenanceScreen.DeleteMaintenance(maintenanceService);
                    break;
                case "Back":
                    return;
            }

            AnsiConsole.MarkupLine("\n[grey]Press any key to return to Maintenance Menu...[/]");
            Console.ReadKey();
        }
    }
}
