using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.EntityClasses
{
    public class Ticket
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key,Column(Order = 0)]
        public int TicketId { get; private set; }
        public bool TicketState { get; set; }
        public string TicketTitle { get; set; }
        public string Description { get; set; }

        
        public ICollection<UserTicket>? UserTickets { get; private set; }
    }
}
