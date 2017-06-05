using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TokenMaster.Models
{
    public class UserEvent
    {
        public string EventGuid { get; set; }
        public int TokenCount { get; set; }

        public UserEvent(string eventGuid, int tokens)
        {
            EventGuid = eventGuid;
            TokenCount = tokens;
        }
    }
}