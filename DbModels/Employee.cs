namespace MobileOperator
{
    using System.Collections.Generic;
    
    public partial class Employee
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
