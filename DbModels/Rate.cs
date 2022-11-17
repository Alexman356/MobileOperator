using System;
using System.Collections.Generic;

namespace MobileOperator
{
    public class Rate
    {
        public Rate()
        {
            Numbers = new HashSet<Number>();
        }

        public short Rate_ID { get; set; }

        public string Name_rate { get; set; }

        public short Cost { get; set; }

        public short Internet { get; set; }

        public short Minutes { get; set; }

        public short SMS { get; set; }

        public virtual ICollection<Number> Numbers { get; set; }
    }
}
