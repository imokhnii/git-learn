using DownHillParkAPI.Data;
using DownHillParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.Repositories
{
    public interface IUserRepository
    {
        

    }
    public class UserRepository : IUserRepository
    {
        private readonly DownHillParkAPIContext db;
        public UserRepository(DownHillParkAPIContext db)
        {
            this.db = db;
        }
        
    }
}
