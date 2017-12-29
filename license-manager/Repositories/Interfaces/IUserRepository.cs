using System.Collections.Generic;
using licensemanager.Models.AppModel;
using licensemanager.Models.DataBaseModel;

namespace licensemanager.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetUsers();

        UserModel GetActiveUserByEmailWithPass(string username);

        bool ExistUserByEmail(string email);
        UserModel GetUserByEmail(string email);
    }
}