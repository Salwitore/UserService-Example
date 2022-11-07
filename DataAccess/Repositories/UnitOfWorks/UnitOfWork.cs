using Data.ContextClasses;
using DataAccess.Repositories.Interfaces;
using DataAccess.Repositories.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private MasterContext _masterContext;

        public UnitOfWork(MasterContext masterContext)
        {
            _masterContext = masterContext;
        }

        private IUserRepository userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(_masterContext);
                }
                return userRepository;
            }
        }
        private ITicketRepository ticketRepository;
        public ITicketRepository TicketRepository
        {
            get
            {
                if (ticketRepository == null)
                {
                    ticketRepository = new TicketRepository(_masterContext);
                }
                return ticketRepository;
            }
        }
        private IUserTicketRepository userTicketRepository;
        public IUserTicketRepository UserTicketRepository
        {
            get
            {
                if (userTicketRepository == null)
                {
                    userTicketRepository = new UserTicketRepository(_masterContext);
                }
                return userTicketRepository;
            }
        }


        public void Dispose()
        {
            _masterContext.Dispose();
        }

        public int SaveChanges()
        {
            return _masterContext.SaveChanges();
        }
    }
}
