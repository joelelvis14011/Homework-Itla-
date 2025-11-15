using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Contacto  (Donde se crea la capa de datos)
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


        public Contact(int id, string name, string Lastname, string phone, string email, string address, int age, bool IsBestFriend)
        {
            Id = id;
            Name = name;
            this.Lastname = Lastname;
            Phone = phone;
            Email = email;
            Address = address;
            Age = age;
            this.IsBestFriend = IsBestFriend;
        }


        public override string ToString()
        {
            return $"{Id} | {Name} | {Phone} | {Email} | {Address}";
        }
    }
}
