using Spectre.Console;
using RentCar.Services;
using RentCar.Models;

namespace RentCar.Screens
{
    public static class CustomerScreen
    {
        public static string Show()
        {
            Console.Clear();
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold green]Customers Menu[/]")
                    .AddChoices("Add Customer", "List Customers", "Update Customer", "Delete Customer", "Back")
            );
        }

        public static void AddCustomer(CustomerService service)
        {
            var name = AnsiConsole.Ask<string>("Enter [green]Full Name[/] ([grey]0 = Back[/]):");
            if (name == "0") return;
            var doc = AnsiConsole.Ask<string>("Enter [green]Document Number[/]:");
            var phone = AnsiConsole.Ask<string>("Enter [green]Phone[/]:");
            var email = AnsiConsole.Ask<string>("Enter [green]Email[/]:");

            var customer = new Customer
            {
                FullName = name,
                DocumentNumber = doc,
                Phone = phone,
                Email = email
            };

            service.Create(customer);
            AnsiConsole.MarkupLine("[bold green]Customer added successfully![/]");
        }

        public static void ListCustomers(CustomerService service)
        {
            var customers = service.GetAll();
            var table = new Table().AddColumn("ID").AddColumn("Name").AddColumn("Document").AddColumn("Phone").AddColumn("Email");

            foreach (var c in customers)
                table.AddRow(c.Id.ToString(), c.FullName, c.DocumentNumber, c.Phone, c.Email);

            AnsiConsole.Write(table);
        }

        public static void UpdateCustomer(CustomerService service)
        {
            var id = AnsiConsole.Ask<int>("Enter [green]Customer ID[/] to update:");
            var customer = service.GetById(id);
            if (customer == null)
            {
                AnsiConsole.MarkupLine("[red]Customer not found[/]");
                return;
            }

            customer.FullName = AnsiConsole.Ask<string>("Enter new [green]Full Name[/]:", customer.FullName);
            customer.DocumentNumber = AnsiConsole.Ask<string>("Enter new [green]Document Number[/]:", customer.DocumentNumber);
            customer.Phone = AnsiConsole.Ask<string>("Enter new [green]Phone[/]:", customer.Phone);
            customer.Email = AnsiConsole.Ask<string>("Enter new [green]Email[/]:", customer.Email);

            service.Update(customer);
            AnsiConsole.MarkupLine("[bold green]Customer updated successfully![/]");
        }

        public static void DeleteCustomer(CustomerService service)
        {
            var id = AnsiConsole.Ask<int>("Enter [green]Customer ID[/] to delete:");
            service.Delete(id);
            AnsiConsole.MarkupLine("[bold red]Customer deleted successfully![/]");
        }
    }
}
