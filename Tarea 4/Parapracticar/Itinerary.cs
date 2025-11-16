using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Parapracticar
{
    public class Itinerary
    {
        private List<Contact> contacts = new List<Contact>();
        private int nextId = 1;

        public Itinerary()
        {
            SeedContacts();
        }

        private void SeedContacts()
        {
            var seed = new[]
            {
                new { Name = "Ethan", Lastname = "Clark", Address = "Kingston, DR", Phone = "8092345678", Email = "ethan.clark@example.com", Age = 30, IsBestFriend = true },
                new { Name = "Sophia", Lastname = "Lopez", Address = "Puerto Plata, DR", Phone = "8098765432", Email = "sophia.lopez@example.com", Age = 27, IsBestFriend = false }
            };

            foreach (var c in seed)
            {
                contacts.Add(new Contact(GetNextId(), c.Name, c.Lastname, c.Phone, c.Email, c.Address, c.Age, c.IsBestFriend));
            }
        }

        private int GetNextId() => nextId++;

        public void AddContact()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== Add New Contact ===");
            Console.ResetColor();

            // Name
            string name;
            do
            {
                Console.Write("Name: ");
                name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Name cannot be empty.");
                    Console.ResetColor();
                }
            } while (string.IsNullOrEmpty(name));

            // Lastname
            Console.Write("Last name: ");
            string lastname = Console.ReadLine();

            // Phone
            string phone;
            while (true)
            {
                Console.Write("Phone: ");
                phone = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(phone) && phone.All(char.IsDigit)) break;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Phone must contain only digits.");
                Console.ResetColor();
            }

            if (contacts.Any(c => c.Phone == phone))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: A contact with this phone already exists.");
                Console.ResetColor();
                return;
            }

            // Email
            string email;
            while (true)
            {
                Console.Write("Email: ");
                email = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(email))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Email cannot be empty.");
                    Console.ResetColor();
                    continue;
                }

                // Validación con regex
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                bool formatOk = Regex.IsMatch(email, pattern);
                bool hasDoubleDot = email.Contains("..");

                if (formatOk && !hasDoubleDot)
                {
                    if (contacts.Any(c => c.Email == email))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error: A contact with this email already exists.");
                        Console.ResetColor();
                    }
                    else
                    {
                        break; // ✅ Email válido y no duplicado
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Invalid email format. Must contain '@' and '.' in correct positions, and no '..'.");
                    Console.ResetColor();
                }
            }

            // Address
            Console.Write("Address: ");
            string address = Console.ReadLine();

            // Age
            int age;
            Console.Write("Age: ");
            while (!int.TryParse(Console.ReadLine(), out age) || age <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Age must be a positive number.");
                Console.ResetColor();
                Console.Write("Age: ");
            }

            // Best Friend
            bool isBestFriend;
            while (true)
            {
                Console.Write("Is best friend? (y/n): ");
                string input = Console.ReadLine().Trim().ToLower();

                if (input == "y")
                {
                    isBestFriend = true;
                    break;
                }
                else if (input == "n")
                {
                    isBestFriend = false;
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Please enter 'y' = yes or 'n' = no.");
                    Console.ResetColor();
                }
            }

            // Guardar contacto
            contacts.Add(new Contact(GetNextId(), name, lastname, phone, email, address, age, isBestFriend));

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Contact added successfully!");
            Console.ResetColor();
        }

        public void ViewContacts()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== Contact List ===");
            Console.ResetColor();

            if (!contacts.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No contacts available.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("{0,-5} | {1,-15} | {2,-15} | {3,-25} | {4,-20} | {5,-5} | {6,-12}",
                "ID", "Name", "Phone", "Email", "Address", "Age", "Best Friend");
            Console.WriteLine(new string('-', 120));

            foreach (var contact in contacts)
            {
                Console.WriteLine("{0,-5} | {1,-15} | {2,-15} | {3,-25} | {4,-20} | {5,-5} | {6,-12}",
                    contact.Id,
                    $"{contact.Name} {contact.Lastname}",
                    contact.Phone,
                    contact.Email,
                    contact.Address,
                    contact.Age,
                    contact.IsBestFriend ? "Yes" : "No");
            }
        }

        public Contact SearchContact(int id)
        {
            var contact = contacts.FirstOrDefault(c => c.Id == id);
            if (contact == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No contact found with that ID.");
                Console.ResetColor();
            }
            return contact;
        }

        public void SearchByName(string name)
        {
            var results = contacts.Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!results.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No contacts found with that name.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Contacts found:");
            foreach (var contact in results)
            {
                Console.WriteLine(contact);
            }
            Console.ResetColor();
        }

        public void EditContact(int id)
        {
            var contact = SearchContact(id);
            if (contact == null) return;

            Console.Write($"New phone ({contact.Phone}): ");
            string newPhone = Console.ReadLine();
            if (!string.IsNullOrEmpty(newPhone) && contacts.Any(c => c.Phone == newPhone && c.Id != id))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Another contact already has this phone.");
                Console.ResetColor();
                return;
            }
            if (!string.IsNullOrEmpty(newPhone)) contact.Phone = newPhone;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Contact updated successfully!");
            Console.ResetColor();
        }

        public void DeleteContact(int id)
        {
            var contact = SearchContact(id);
            if (contact == null) return;

            contacts.Remove(contact);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Contact with ID {id} deleted successfully!");
            Console.ResetColor();
        }
    }
}


