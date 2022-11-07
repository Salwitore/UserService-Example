using Data.EntityClasses;
using DataAccess.Repositories.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class UserTicketBusiness
    {

        public UserTicket MatchUserTicket(UserTicket userTicket)
        {
            using (UnitOfWork uow = new UnitOfWork(new Data.ContextClasses.MasterContext()))
            {
                var userticket = uow.UserTicketRepository.MatchUserTicket(userTicket);

                if (userticket == null)
                {
                    return null;
                }
                uow.UserTicketRepository.Add(userticket);
                uow.SaveChanges();
                return userticket;
            }
        }
    }
}
