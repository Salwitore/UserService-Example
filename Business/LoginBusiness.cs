using Data.EntityClasses;
using DataAccess.Repositories.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class LoginBusiness
    {
        public User Login(string Email, string userPassword)
        {
            using (UnitOfWork uow = new UnitOfWork(new Data.ContextClasses.MasterContext()))
            {
                return uow.UserRepository.GetAll(u => u.Email == Email && u.UserPassword == userPassword).FirstOrDefault();
            }
        }
    }
}
