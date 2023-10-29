using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetUserById(string userId);
    }
}
