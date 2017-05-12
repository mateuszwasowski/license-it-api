using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using licensemanager.Models.AppModel;
using licensemanager.Models.DataBaseModel;
using licensemanager.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace licensemanager.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DataBaseContext _context;

        public UserRepository(DataBaseContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users;
        }

        public UserModel GetActiveUserByEmailWithPass(string email)
        {
            var user = _context.Users.Where(x => x.IsActive).FirstOrDefault(x => x.Email.Equals(email));

            if (user == null)
                return null;

            return new UserModel
            {
                Id = user.Id,
                LastName = user.LastName,
                Password = user.Password,
                Email = user.Email,
                FirstName = user.FirstName,
                IsActive = user.IsActive,
                IsDelete = user.IsDelete
            };
        }

        public bool ExistUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(x => x.Email.Equals(email)) != null;
        }
    }
}
