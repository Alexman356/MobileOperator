using System.Collections.Generic;

namespace MobileOperator
{
    public class Employee : IEntity
    {
        public Employee()
        {
            Contracts = new HashSet<Contract>();
        }

        public int? employee_ID { get; set; }

        public int person_ID { get; set; }

        public string user_login { get; set; }

        public int Salary { get; set; }

        public string Post { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }

        public virtual Person Person { get; set; }

        public virtual User User { get; set; }
    }
}
