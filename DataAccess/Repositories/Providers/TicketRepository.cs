using Data.ContextClasses;
using Data.EntityClasses;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Providers
{
    public class TicketRepository :Repository<Ticket>,ITicketRepository
    {
        public TicketRepository(MasterContext context):base(context)
        {
            
        }

        public MasterContext masterContext { get { return DbContext as MasterContext; }  }

        public Ticket UpdateTicket(Ticket updateTicket, int ticketId)
        {
            var ticket = masterContext.Tickets.Where(x => x.TicketId == ticketId).FirstOrDefault();

            if (ticket == null)
            {
                return null;
            }

            ticket.TicketTitle = updateTicket.TicketTitle;
            ticket.TicketState = updateTicket.TicketState;
            ticket.Description = updateTicket.Description;

            return ticket;
        }

    }
}
