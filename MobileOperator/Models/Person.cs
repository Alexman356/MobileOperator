namespace MobileOperator
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Person
    {
        [Key]
        [Column("person_ID")]
        public int Id { get; set; }

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
                value += "01.01.1900";
                Birthdate = new DateTime(
                    int.Parse(value.Substring(6, 4)),
                    int.Parse(value.Substring(3, 2)),
                    int.Parse(value.Substring(0, 2)));
            }
        }
        public string Gender { get; set; }

        public string Series_passport { get; set; }

        public string Number_passport { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }
    }
}
