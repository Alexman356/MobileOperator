using System;
using System.Collections.Generic;

namespace MobileOperator
{
    public class Person : IEntity
    {
        public Person()
        {
            Abonents = new HashSet<Abonent>();
            Employees = new HashSet<Employee>();
        }

        public int person_ID { get; set; }

        public string Last_name { get; set; }

        public string First_name { get; set; }

        public string Middle_name { get; set; }

        public DateTime Birthdate { get; set; }

        public string Birthdate_s
        {
            get
            {
                return Birthdate.ToShortDateString();
            }

            set
            {
                if (DateTime.TryParse(value, out DateTime dDate))
                {
                    Birthdate = dDate;
                }
            }
        }

        public string Gender { get; set; }

        public string Series_passport { get; set; }

        public string Number_passport { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Abonent> Abonents { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
