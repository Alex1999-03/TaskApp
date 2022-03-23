using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User?> Authenticate(string username, string password);
    }
}
