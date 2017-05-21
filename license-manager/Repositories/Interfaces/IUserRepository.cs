using System.Collections.Generic;
using licensemanager.Model.DataBaseModel;
using licensemanager.Models.AppModel;

namespace licensemanager.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetUsers();

        UserModel GetActiveUserByEmailWithPass(string username);

        bool ExistUserByEmail(string email);
    }
}