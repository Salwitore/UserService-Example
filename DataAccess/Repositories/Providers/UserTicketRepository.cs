using Data.ContextClasses;
using Data.EntityClasses;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Providers
{
    public class UserTicketRepository : Repository<UserTicket>, IUserTicketRepository
    {
        public UserTicketRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public MasterContext masterContext { get { return DbContext as MasterContext; } }

        public UserTicket MatchUserTicket(UserTicket userTicket) //userId ile ticketId verip UserTicket tablosunda bunları matchlemem lazım 
        {
            var user = masterContext.Users.Where(u => u.UserId == userTicket.UserId).FirstOrDefault();
            var ticket = masterContext.Tickets.Where(t => t.TicketId == userTicket.TicketId).FirstOrDefault();

            if (user == null || ticket == null)
            {
                return null;
            }
            return userTicket;
        }
    }
}
