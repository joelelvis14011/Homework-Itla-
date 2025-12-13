using Spectre.Console;
using RentCar.Services;
using RentCar.Models;

namespace RentCar.Screens
{
    public static class PaymentScreen
    {
        public static string Show()
        {
            Console.Clear();
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold purple]Payments Menu[/]")
                    .AddChoices("Add Payment", "List Payments", "Update Payment", "Delete Payment", "Back")
            );
        }

        public static void AddPayment(PaymentService service, RentalContractService contractService)
        {
            var contractId = AnsiConsole.Ask<int>("Enter [purple]Contract ID[/]:");
            var contract = contractService.GetById(contractId);
            if (contract == null)
            {
                AnsiConsole.MarkupLine("[red]Contract not found[/]");
                return;
            }

            var amount = AnsiConsole.Ask<decimal>("Enter [purple]Payment Amount[/]:");
            var method = AnsiConsole.Ask<string>("Enter [purple]Payment Method[/] (Cash, Card, Transfer):");

            var payment = new Payment
            {
                RentalContractId = contractId,
                Amount = amount,
                Method = method,
                PaymentDate = DateTime.Now
            };

            service.Create(payment);
            AnsiConsole.MarkupLine("[bold green]Payment registered successfully![/]");
        }

        public static void ListPayments(PaymentService service)
        {
            var payments = service.GetAll();
            var table = new Table()
                .AddColumn("ID")
                .AddColumn("Contract ID")
                .AddColumn("Customer")
                .AddColumn("Amount")
                .AddColumn("Method")
                .AddColumn("Date");

            foreach (var p in payments)
            {
                table.AddRow(
                    p.Id.ToString(),
                    p.RentalContractId.ToString(),
                    p.RentalContract.Customer.FullName,
                    p.Amount.ToString("C"),
                    p.Method,
                    p.PaymentDate.ToString("g")
                );
            }

            AnsiConsole.Write(table);
        }

        public static void UpdatePayment(PaymentService service)
        {
            var id = AnsiConsole.Ask<int>("Enter [purple]Payment ID[/] to update:");
            var payment = service.GetById(id);
            if (payment == null)
            {
                AnsiConsole.MarkupLine("[red]Payment not found[/]");
                return;
            }

            payment.Amount = AnsiConsole.Ask<decimal>("Enter new [purple]Amount[/]:", payment.Amount);
            payment.Method = AnsiConsole.Ask<string>("Enter new [purple]Method[/]:", payment.Method);
            payment.PaymentDate = AnsiConsole.Ask<DateTime>("Enter new [purple]Payment Date[/]:", payment.PaymentDate);

            service.Update(payment);
            AnsiConsole.MarkupLine("[bold green]Payment updated successfully![/]");
        }

        public static void DeletePayment(PaymentService service)
        {
            var id = AnsiConsole.Ask<int>("Enter [purple]Payment ID[/] to delete:");
            service.Delete(id);
            AnsiConsole.MarkupLine("[bold red]Payment deleted successfully![/]");
        }
    }
}
