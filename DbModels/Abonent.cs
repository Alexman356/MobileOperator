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
    
    public partial class Abonent
    {
        public Abonent()
        {
            this.Contracts = new HashSet<Contract>();
        }
    
        public int abonent_ID { get; set; }
        public int person_ID { get; set; }
        public string user_login { get; set; }
    
        public virtual Person Person { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}