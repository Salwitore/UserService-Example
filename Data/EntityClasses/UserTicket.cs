using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.EntityClasses
{
    public class UserTicket
    {
        
        public int UserId { get; set; }
        public  User? User { get; private set; }
        public int TicketId { get; set; }
        public  Ticket? Ticket { get; private set; }
    }
}
