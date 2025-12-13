using Spectre.Console;

namespace RentCar.Screens
{
    public static class MainMenu
    {
        public static void Show()
        {
            while (true)
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[bold dodgerblue2]\n   RENTCAR SYSTEM – MAIN MENU[/]\n");

                var option = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]Select an option:[/]")
                        .AddChoices(new[]
                        {
                            "Vehicles",
                            "Customers",
                            "Contracts",
                            "Payments",
                            "Exit"
                        }));

                switch (option)
                {
                    case "Vehicles":
                        VehicleScreen.Show();
                        break;

                    case "Customers":
                        CustomerScreen.Show();
                        break;

                    case "Contracts":
                        RentalContractScreen.Show();
                        break;

                    case "Payments":
                        PaymentScreen.Show();
                        break;

                    case "Exit":
                        return;
                }
            }
        }
    }
}
