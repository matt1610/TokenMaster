using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TokenMaster.Models
{
    public class EventModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string EventOwner { get; set; }
        public string OwnerId { get; set; }
        public int TotalTokens { get; set; }
        public int MaxGuestTokens { get; set; }
        public string PurchaseItemsJSON { get; set; }
    }
}