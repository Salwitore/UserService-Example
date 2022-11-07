using Data.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface ITicketRepository:IRepository<Ticket>
    {
        Ticket UpdateTicket(Ticket updateTicket, int ticketId);

    }
}
