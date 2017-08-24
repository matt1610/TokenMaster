using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenMaster.Models
{
    public class TransactionRequestModel
    {
        public string DeviceId { get; set; }
        public int TokenAmount { get; set; }
    }
}
