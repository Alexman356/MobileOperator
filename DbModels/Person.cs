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
    
    public partial class Person
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Person()
        {
            this.Abonents = new HashSet<Abonent>();
            this.Employees = new HashSet<Employee>();
        }
    
        public int person_ID { get; set; }
        public string Last_name { get; set; }
        public string First_name { get; set; }
        public string Middle_name { get; set; }
        public System.DateTime Birthdate { get; set; }
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
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Abonent> Abonents { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}