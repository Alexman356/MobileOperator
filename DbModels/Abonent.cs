using System;
using System.Collections.Generic;

namespace MobileOperator
{
    public class Abonent
    {
        public Abonent()
        {
            Contracts = new HashSet<Contract>();
        }

        public int abonent_ID { get; set; }

        public int person_ID { get; set; }

        public string user_login { get; set; }

        public virtual Person Person { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
