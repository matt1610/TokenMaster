using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenMaster.Models
{
    public class EventStand
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string EventId { get; set; }

        public string Name { get; set; }
        public virtual EventModel EventModel { get; set; }

        //[ForeignKey("EventId")]
        public virtual ICollection<EventDevice> EventDevices { get; set; } 
    }
}
