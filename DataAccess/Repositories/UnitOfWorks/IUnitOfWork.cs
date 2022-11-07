using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.UnitOfWorks
{
    public interface IUnitOfWork :IDisposable
    {
        IUserRepository UserRepository { get; }
        ITicketRepository TicketRepository { get; }
        IUserTicketRepository UserTicketRepository { get; }

        int SaveChanges();
    }
}
