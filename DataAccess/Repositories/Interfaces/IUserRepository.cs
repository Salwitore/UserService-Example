using Data.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IUserRepository :IRepository<User>
    {
        User UpdateUser(User updateUser, int userId);

        User LoadingUser(int userId);

        
    }
}
