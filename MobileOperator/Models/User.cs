using System.ComponentModel.DataAnnotations;

namespace MobileOperator
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        [Key]
        [Column("Login")]
        public string Id { get; set; }

        public string Role { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }
    }
}
