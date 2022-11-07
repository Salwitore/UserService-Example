using Data.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IUserTicketRepository : IRepository<UserTicket>
    {
        UserTicket MatchUserTicket(UserTicket user);
    }
}
