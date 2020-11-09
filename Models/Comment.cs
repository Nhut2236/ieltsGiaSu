using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingServices.Models
{
    public class Comment
    {
        public string Id { get; set; }
        public string ServiceType { get; set; }
        public string Content { get; set; }
        public int Rate { get; set; }
        public string CreatedAt { get; set; }

    }
}
