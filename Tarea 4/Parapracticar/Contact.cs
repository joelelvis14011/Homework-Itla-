using System;

namespace Parapracticar
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public bool IsBestFriend { get; set; }

        public Contact(int id, string name, string lastname, string phone, string email, string address, int age, bool isBestFriend)
        {
            Id = id;
            Name = name;
            Lastname = lastname;
            Phone = phone;
            Email = email;
            Address = address;
            Age = age;
            IsBestFriend = isBestFriend;
        }

        public override string ToString()
        {
            // Si es mejor amigo, resaltamos en verde
            if (IsBestFriend)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("★ BEST FRIEND ★");
                Console.ResetColor();
            }

            return $"ID: {Id}\n" +
                   $"Name: {Name} {Lastname}\n" +
                   $"Phone: {Phone}\n" +
                   $"Email: {Email}\n" +
                   $"Address: {Address}\n" +
                   $"Age: {Age}\n" +
                   $"Best Friend: {(IsBestFriend ? "Yes" : "No")}";
        }
    }
}
