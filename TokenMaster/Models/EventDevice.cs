﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenMaster.Models
{
    public class EventDevice
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public Guid StandId { get; set; }
        public string Name { get; set; }
<<<<<<< HEAD
        [ForeignKey("StandId")]
        public virtual EventStand EventStand { get; set; }

=======
        public virtual EventStand EventStand { get; set; }
>>>>>>> 5d3e2531bb6e182d41c5f26edfff90cbf64aaa38
    }
}