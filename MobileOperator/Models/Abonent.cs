namespace MobileOperator
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Abonent
    {
        public Abonent()
        {
            this.Contracts = new HashSet<Contract>();
        }

        [Key]
        public int abonent_ID { get; set; }

        /*[Required]
        [ForeignKey("person_ID")]*/
        [Column("person_ID")]
        public int PersonId { get; set; }

        /*[Required]
        [ForeignKey("user_login")]*/
        [Column("user_login")]
        public string UserId { get; set; }
    
        public virtual Person Person { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
