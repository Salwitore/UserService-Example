using Data.EntityClasses;
using DataAccess.Repositories.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class TicketBusiness
    {

        public IEnumerable<Ticket> GetTickets()
        {
            using (UnitOfWork uow = new UnitOfWork(new Data.ContextClasses.MasterContext()))
            {
                return uow.TicketRepository.GetAll();
            }

        }


        public void AddTicket(Ticket ticket)
        {
            using (UnitOfWork uow = new UnitOfWork(new Data.ContextClasses.MasterContext()))
            {
                uow.TicketRepository.Add(ticket);
                uow.SaveChanges();
            }
        }


        public Ticket GetTicket(int ticketId)
        {
            using (UnitOfWork uow = new UnitOfWork(new Data.ContextClasses.MasterContext()))
            {
                return uow.TicketRepository.GetById(ticketId);
            }
        }


        public Ticket DeleteTicket(int ticketId)
        {
            using (UnitOfWork uow = new UnitOfWork(new Data.ContextClasses.MasterContext()))
            {
                var ticket = uow.TicketRepository.GetById(ticketId);
                uow.TicketRepository.Delete(ticket);
                uow.SaveChanges();
                return ticket;
            }

        }

        public Ticket UpdateTicket(Ticket updateTicket, int ticketId)
        {
            using (UnitOfWork uow = new UnitOfWork(new Data.ContextClasses.MasterContext()))
            {

                var ticket = uow.TicketRepository.UpdateTicket(updateTicket, ticketId);
                uow.SaveChanges();
                return ticket;
            }

        }

        public Ticket TicketComplete(int ticketId)
        {
            using (UnitOfWork uow = new UnitOfWork(new Data.ContextClasses.MasterContext()))
            {
                var ticket = uow.TicketRepository.GetById(ticketId);
                if (ticket == null)
                {
                    return null;
                }
                ticket.TicketState = true;
                uow.SaveChanges();
                return ticket;
            }

        }
    }
}
