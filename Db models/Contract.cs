//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MobileOperator
{
    using System;
    using System.Collections.Generic;
    
    public partial class Contract
    {
        public int contract_ID { get; set; }
        public int abonent_ID { get; set; }
        public int employee_ID { get; set; }
        public string Number_telephone { get; set; }
        public System.DateTime Date { get; set; }
        public string Date_s
        {
            get
            {
                return Date.ToShortDateString();
            }
            set { }
        }
        public bool Status { get; set; }
    
        public virtual Abonent Abonent { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Number Number { get; set; }
    }
}
