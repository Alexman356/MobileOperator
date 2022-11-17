using System.Collections.Generic;

namespace MobileOperator
{
    public class Number : IEntity
    {
        public Number()
        {
            Contracts = new HashSet<Contract>();
        }

        public string Number_telephone { get; set; }

        public short? rate_ID { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }

        public virtual Rate Rate { get; set; }
    }
}
