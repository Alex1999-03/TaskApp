using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Data
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {
        public UserRepository(TaskAppContext context) : base(context)
        {
        }

        public async Task<User?> Authenticate(string username, string password)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
        }
    }
}
