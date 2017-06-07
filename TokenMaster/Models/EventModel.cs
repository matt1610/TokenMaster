using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TokenMaster.Models
{
    public class EventModel
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id = Guid.NewGuid();
        [Required]
        public string EventName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Location { get; set; }
        public string EventOwner { get; set; }
        public string OwnerId { get; set; }
        [Required]
        public int MaxTokens { get; set; }
        public int TokensUsed { get; set; }
        [Required]
        public float TokenPrice { get; set; }
        [Required]
        public string Currency { get; set; }
        public string PurchaseItemsJSON { get; set; }
        [Required]
        public string AddressVenue { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [ForeignKey("Id")]
        public ICollection<EventStand> EventStands { get; set; }
    }
}