using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmTradeEntity
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string pinCode { get; set; }
        public string country { get; set; }
        public string Phone { get; set; }
        [ForeignKey("User")]
        public Guid userId { get; set; }
        public User user { get; set; }  
    }

}
