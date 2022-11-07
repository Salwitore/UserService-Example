using Data.EntityClasses;
using DataAccess.Repositories.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class UserBusiness
    {

        //UserBusiness

        public void AddUser(User user)
        {
            using (UnitOfWork uow = new UnitOfWork(new Data.ContextClasses.MasterContext()))
            {
                uow.UserRepository.Add(user);
                uow.SaveChanges();
            }
        }


        public User DeleteUser(int userId)
        {
            using (UnitOfWork uow = new UnitOfWork(new Data.ContextClasses.MasterContext()))
            {
                var user = uow.UserRepository.GetById(userId);
                uow.UserRepository.Delete(user);
                uow.SaveChanges();
                return user;
            }
        }


        public User GetUser(int userId)
        {
            using (UnitOfWork uow = new UnitOfWork(new Data.ContextClasses.MasterContext()))
            {
                return uow.UserRepository.GetById(userId);
            }
        }


        public IEnumerable<User> GetUsers()
        {
            using (UnitOfWork uow = new UnitOfWork(new Data.ContextClasses.MasterContext()))
            {
                return uow.UserRepository.GetAll();
            }
        }




        public User UpdateUser(User updateUser, int userId)
        {
            using (UnitOfWork uow = new UnitOfWork(new Data.ContextClasses.MasterContext()))
            {
                var user = uow.UserRepository.UpdateUser(updateUser, userId);
                uow.SaveChanges();
                return user;
            }
        }

        public User LoadingUser(int userId)
        {
            using (UnitOfWork uow = new UnitOfWork(new Data.ContextClasses.MasterContext()))
            {
                var user = uow.UserRepository.LoadingUser(userId);

                return user;
            }
        }
    }
}
