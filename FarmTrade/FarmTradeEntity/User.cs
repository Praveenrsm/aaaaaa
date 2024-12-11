using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmTradeEntity
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string UserName { get; set; }
        public string Email { get; set; }
        public string password { get; set; }
        public string phonenumber { get; set; }
        public string role { get; set; }
        // New fields for password reset
        public string ResetToken { get; set; }
        public DateTime? ResetTokenExpiry { get; set; }

    }
}
