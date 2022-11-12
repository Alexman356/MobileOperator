namespace MobileOperator
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Employee
    {
        public Employee()
        {
            Contracts = new HashSet<Contract>();
        }

        [Key]
        public int employee_ID { get; set; }

        public int person_ID { get; set; }

        public string user_login { get; set; }

        public string Number_telephone { get; set; }

        public string Post { get; set; }
    
        public virtual ICollection<Contract> Contracts { get; set; }

        public virtual Person Person { get; set; }

        public virtual User User { get; set; }
    }
}
