using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TokenMaster.Models
{
    public class Transaction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public Guid EventId { get; set; }
        public string UserId { get; set; }
        [Required]
        public int TokenAmount { get; set; }

        public string StandId { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}