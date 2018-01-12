using System.Collections.Generic;
using System.Linq;
using licensemanager.Models.AppModel;
using licensemanager.Models.DataBaseModel;
using licensemanager.Repositories.Interfaces;

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

        public UserModel GetUserByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email.Equals(email));

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

        public IEnumerable<UserModel> GetUsersByIdGroup(int id)
        {
            return from userGroup in _context.UserGroup
                   join groupObj in _context.Group on userGroup.IdGroup equals groupObj.Id
                   join userObj in _context.Users on userGroup.IdUser equals userObj.Id
                   where (userGroup.IdGroup == id && groupObj.IsActive && !groupObj.IsDelete && userObj.IsActive && !userObj.IsDelete && userGroup.IdUser == userObj.Id)
                   select new UserModel
                   {
                       Id = userObj.Id,
                       FirstName = userObj.FirstName,
                       LastName = userObj.LastName,
                       Email = userObj.Email,
                       IsActive = userObj.IsActive,
                       IsDelete = userObj.IsDelete
                   };
        }
    }
}
