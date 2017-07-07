using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenMaster.Models
{
    public class TransactionRequest
    {
        public string EventId { get; set; }
        public string StandId { get; set; }
        public string DeviceId { get; set; }
        public int TokenAmount { get; set; }
    }
}
