using System;
using System.Collections.Generic;

namespace MobileOperator
{
    public class Contract
    {
        public int contract_ID { get; set; }

        public int abonent_ID { get; set; }

        public int? employee_ID { get; set; }

        public string Number_telephone { get; set; }

        public DateTime Date { get; set; }

        public string Date_s => Date.ToShortDateString();

        public bool Status { get; set; }

        public virtual Abonent Abonent { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Number Number { get; set; }
    }
}
