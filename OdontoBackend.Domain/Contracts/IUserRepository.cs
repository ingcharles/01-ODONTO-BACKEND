using OdontoBackend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Domain.Contracts
{
    public interface IUserRepository
    {
        Task<IQueryable<User>> GetUserByCiPas(User request);
   
    }
}
