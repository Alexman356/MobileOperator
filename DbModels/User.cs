using System;
using System.Collections.Generic;

namespace MobileOperator
{
    public class User : IEntity
    {
        public User()
        {
            Abonents = new HashSet<Abonent>();
            Employees = new HashSet<Employee>();
        }

        public string Login { get; set; }

        public string Role { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public virtual ICollection<Abonent> Abonents { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        public string Password
        {
            set
            {
                var encryptor = new Encryptor();
                PasswordSalt = encryptor.GetHashSalt();
                PasswordHash = encryptor.PasswordEncrypt(value, PasswordSalt);
            }
        }
    }
}
