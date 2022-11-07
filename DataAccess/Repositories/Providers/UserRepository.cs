using Data.ContextClasses;
using Data.EntityClasses;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Providers
{
    public class UserRepository : Repository<User> , IUserRepository
    {
        public UserRepository(MasterContext context) : base(context)
        {

        }
        public MasterContext masterContext { get { return DbContext as MasterContext; } }

        public User LoadingUser(int userId)
        {
            var user = masterContext.Users.Include(u => u.UserTickets).ThenInclude(ut => ut.Ticket).FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                return null;
            }
            return user;

        }

        

        public User UpdateUser(User updateUser, int userId)
        {
            var user = masterContext.Users.Where(u => u.UserId == userId).FirstOrDefault();
            if (user == null)
            {
                return null;
            }

            user.UserName = updateUser.UserName;
            user.UserPassword = updateUser.UserPassword;
            user.UserSurname = updateUser.UserSurname;
            user.Email = updateUser.Email;
            return user;

        }
    }
}
