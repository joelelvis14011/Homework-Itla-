using System;
using System.Collections.Generic;
using System.Linq;
//Joel Ant. Sanchez Rojas--- 2025-1311

{
    Console.Title = "Contact Management";

    ShowWelcome();

    bool running = true;

    List<int> ids = new List<int>();
    Dictionary<int, string> names = new Dictionary<int, string>();
    Dictionary<int, string> lastnames = new Dictionary<int, string>();
    Dictionary<int, string> addresses = new Dictionary<int, string>();
    Dictionary<int, string> phones = new Dictionary<int, string>();
    Dictionary<int, string> emails = new Dictionary<int, string>();
    Dictionary<int, int> ages = new Dictionary<int, int>();
    Dictionary<int, bool> bestFriends = new Dictionary<int, bool>();

    while (running)
    {
        Console.Clear();
        Console.WriteLine();
        ShowMenu(ids.Count);

        Console.WriteLine();
        if (!int.TryParse(Console.ReadLine(), out int option))
        {
            ShowError("You must enter a valid number.");
            Pause();
            continue;
        }

        Console.WriteLine();
        switch (option)
        {
            case 1:
                Console.Clear();
                AddContact(ids, names, lastnames, addresses, phones, emails, ages, bestFriends);
                Pause();
                break;

            case 2:
                Console.Clear();
                ViewContacts(ids, names, lastnames, addresses, phones, emails, ages, bestFriends);
                Pause();
                break;

            case 3:
                Console.Clear();
                SearchContact(ids, names, lastnames, addresses, phones, emails, ages, bestFriends);
                Pause();
                break;

            case 4:
                Console.Clear();
                ModifyContact(ids, names, lastnames, addresses, phones, emails, ages, bestFriends);
                Pause();
                break;

            case 5:
                Console.Clear();
                DeleteContact(ids, names, lastnames, addresses, phones, emails, ages, bestFriends);
                Pause();
                break;

            case 6:
                running = false;
                break;

            default:
                ShowError("Invalid option. Try again.");
                Pause();
                break;
        }
    }
}

// Interface
static void ShowWelcome()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Welcome to the Contact List\n");
    Console.WriteLine($"Current date and time: {DateTime.Now}\n");
    Console.ResetColor();
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
    Console.Clear();
}

static void ShowMenu(int totalContacts)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("                 MAIN MENU                   \n");
    Console.WriteLine($"\nTotal contacts: {totalContacts}\n");
    Console.WriteLine("1. Add Contact");
    Console.WriteLine("2. View Contacts");
    Console.WriteLine("3. Search Contact");
    Console.WriteLine("4. Modify Contact");
    Console.WriteLine("5. Delete Contact");
    Console.WriteLine("6. Exit\n");
    Console.ResetColor();
    Console.Write("Enter the number of the desired option: ");
}

static void ShowError(string message)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"\nError: {message}\n");
    Console.ResetColor();
}

static void Pause()
{
    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
    Console.Clear();
}

// Funcionality
//Joel Ant. Sanchez Rojas--- 2025-1311

static void AddContact(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames,
    Dictionary<int, string> addresses, Dictionary<int, string> phones, Dictionary<int, string> emails,
    Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
{
    string name;
    while (true)
    {
        Console.Write("Enter first name: ");
        name = Console.ReadLine();

        if (!string.IsNullOrEmpty(name))
            break;

        Console.WriteLine("The name cannot be empty. Please try again.\n");
    }

    string lastname;
    while (true)
    {
        Console.Write("\nEnter last name: ");
        lastname = Console.ReadLine();

        if (!string.IsNullOrEmpty(lastname))
            break;

        Console.WriteLine("The last name cannot be empty. Please try again.\n");
    }

    string address;
    while (true)
    {
        Console.Write("\nEnter address: ");
        address = Console.ReadLine();

        if (!string.IsNullOrEmpty(address))
            break;

        Console.WriteLine("The address cannot be empty. Please try again.\n");
    }

    string phone;
    while (true)
    {
        Console.Write("\nEnter phone number:  ");
        phone = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(phone) && phone.All(char.IsDigit)) break;
        ShowError("Phone number must contain only digits.");
    }

    string email;
    while (true)
    {
        Console.Write("\nEnter email:  ");
        email = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(email) && email.Contains("@") && email.Contains(".")) break;
        ShowError("Enter a valid email.");
    }

    int age;
    while (true)
    {
        Console.Write("\nEnter age: ");
        if (int.TryParse(Console.ReadLine(), out age) && age > 0) break;
        ShowError("Invalid age.");
    }

    Console.WriteLine("\nIs this person your best friend? 1. Yes, 2. No  ");
    bool isBestFriend = Console.ReadLine() == "1";

    int id = ids.Count + 1;
    ids.Add(id);
    names[id] = name;
    lastnames[id] = lastname;
    addresses[id] = address;
    phones[id] = phone;
    emails[id] = email;
    ages[id] = age;
    bestFriends[id] = isBestFriend;

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("\nContact added successfully.\n");
    Console.ResetColor();
}

static void ViewContacts(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames,
    Dictionary<int, string> addresses, Dictionary<int, string> phones, Dictionary<int, string> emails,
    Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
{
    Console.Clear();
    Console.WriteLine("\n--- Contact List ---\n");

    if (ids.Count == 0)
    {
        ShowError("No contacts registered.");
        return;
    }

    foreach (var id in ids)
    {
        Console.WriteLine($"======= ID: {id} =======");
        Console.WriteLine($"Name: {names[id]} {lastnames[id]}");
        Console.WriteLine($"Address: {addresses[id]}");
        Console.WriteLine($"Phone: {phones[id]}");
        Console.WriteLine($"Email: {emails[id]}");
        Console.WriteLine($"Age: {ages[id]}");
        Console.WriteLine($"Best Friend: {(bestFriends[id] ? "Yes" : "No")}\n");
    }
}
//Joel Ant. Sanchez Rojas--- 2025-1311

static void SearchContact(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames,
    Dictionary<int, string> addresses, Dictionary<int, string> phones, Dictionary<int, string> emails,
    Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
{
    Console.Clear();
    Console.WriteLine("\nEnter the name or last name to search:");
    string query = Console.ReadLine().ToLower();

    var results = ids.Where(id =>
        names[id].ToLower().Contains(query) ||
        lastnames[id].ToLower().Contains(query)).ToList();

    if (results.Count == 0)
    {
        ShowError("No contacts found.");
        return;
    }

    Console.WriteLine("\n--- Search Results ---\n");
    foreach (var id in results)
    {
        Console.WriteLine($"======= ID: {id} =======");
        Console.WriteLine($"Name: {names[id]} {lastnames[id]}");
        Console.WriteLine($"Address: {addresses[id]}");
        Console.WriteLine($"Phone: {phones[id]}");
        Console.WriteLine($"Email: {emails[id]}");
        Console.WriteLine($"Age: {ages[id]}");
        Console.WriteLine($"Best Friend: {(bestFriends[id] ? "Yes" : "No")}\n");
    }
}

static void ModifyContact(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames,
    Dictionary<int, string> addresses, Dictionary<int, string> phones, Dictionary<int, string> emails,
    Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
{
    Console.Clear();
    Console.WriteLine("\nEnter the ID of the contact to modify:");
    if (!int.TryParse(Console.ReadLine(), out int id) || !ids.Contains(id))
    {
        ShowError("Invalid ID.");
        return;
    }

    Console.WriteLine($"Current name: {names[id]}. New name (Press Enter to keep):");
    string newName = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(newName)) names[id] = newName;

    Console.WriteLine($"Current last name: {lastnames[id]}. New last name (Press Enter to keep):");
    string newLastName = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(newLastName)) lastnames[id] = newLastName;

    Console.WriteLine($"Current address: {addresses[id]}. New address (Press Enter to keep):");
    string newAddress = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(newAddress)) addresses[id] = newAddress;

    Console.WriteLine($"Current phone: {phones[id]}. New phone (Press Enter to keep):");
    string newPhone = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(newPhone) && newPhone.All(char.IsDigit)) phones[id] = newPhone;

    Console.WriteLine($"Current email: {emails[id]}. New email (Press Enter to keep):");
    string newEmail = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(newEmail) && newEmail.Contains("@") && newEmail.Contains(".")) emails[id] = newEmail;

    Console.WriteLine($"Current age: {ages[id]}. New age (Press Enter to keep):");
    string newAge = Console.ReadLine();
    if (int.TryParse(newAge, out int age) && age > 0) ages[id] = age;

    Console.WriteLine($"Is best friend? Current: {(bestFriends[id] ? "Yes" : "No")}. New value (1. Yes, 2. No, Enter to keep):");
    string newFriend = Console.ReadLine();
    if (newFriend == "1") bestFriends[id] = true;
    else if (newFriend == "2") bestFriends[id] = false;

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("\nContact modified successfully.\n");
    Console.ResetColor();
}
//Joel Ant. Sanchez Rojas--- 2025-1311

static void DeleteContact(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames,
    Dictionary<int, string> addresses, Dictionary<int, string> phones, Dictionary<int, string> emails,
    Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
{
    Console.Clear();
    Console.WriteLine("\nEnter the ID to delete:");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        ShowError("Invalid ID.");
        return;
    }

    if (names.ContainsKey(id))
    {
        names.Remove(id);
        lastnames.Remove(id);
        addresses.Remove(id);
        phones.Remove(id);
        emails.Remove(id);
        ages.Remove(id);
        bestFriends.Remove(id);
        ids.Remove(id);

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\nContact deleted successfully.\n");
        Console.ResetColor();
    }
    else
    {
        ShowError("No contact found with that ID.");
    }
}

