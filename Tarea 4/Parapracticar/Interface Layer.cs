using System;
using System.Collections.Generic;
using System.Linq;

namespace Parapracticar
{
    class Program
    {
        static void Main(string[] args)
        {
            Itinerary itinerary = new Itinerary();
            bool running = true;

            while (running)
            {
                Console.Clear();
                ShowMenu();
                int choice = ReadInt("Choose an option: ");

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        itinerary.AddContact();
                        break;

                    case 2:
                        Console.Clear();
                        itinerary.ViewContacts();
                        break;

                    case 3:
                        Console.Clear();
                        int idSearch = ReadInt("Enter ID to search: ");
                        var contact = itinerary.SearchContact(idSearch);
                        Console.WriteLine(contact != null ? contact.ToString() : "Contact not found.");
                        break;

                    case 4:
                        Console.Clear();
                        int idEdit = ReadInt("Enter ID to edit: ");
                        itinerary.EditContact(idEdit);
                        break;

                    case 5:
                        Console.Clear();
                        int idDelete = ReadInt("Enter ID to delete: ");
                        itinerary.DeleteContact(idDelete);
                        break;

                    case 6:
                        Console.Clear();
                        string searchName = ReadRequiredString("Enter name to search: ");
                        itinerary.SearchByName(searchName);
                        break;

                    case 7:
                        Console.Clear();
                        running = false;
                        Console.WriteLine("Exiting program...");
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }

                Console.WriteLine("\nPress ENTER to continue...");
                Console.ReadLine();
            }
        }

        static void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== My Contact Agenda ===");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1. Add Contact");
            Console.WriteLine("2. View Contacts");
            Console.WriteLine("3. Search Contact by ID");
            Console.WriteLine("4. Edit Contact");
            Console.WriteLine("5. Delete Contact");
            Console.WriteLine("6. Search by Name");
            Console.WriteLine("7. Exit");
            Console.WriteLine("-------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        // Helper: read integer safely
        static int ReadInt(string prompt)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (int.TryParse(input, out value))
                {
                    return value;
                }
                Console.WriteLine("Error: Please enter a valid number.");
            }
        }

        // Helper: read required string (not empty)
        static string ReadRequiredString(string prompt)
        {
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Error: This field cannot be empty.");
                }
            } while (string.IsNullOrEmpty(input));
            return input;
        }

       
    }
}
