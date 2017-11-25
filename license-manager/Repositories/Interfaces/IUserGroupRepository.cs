using System.Collections.Generic;
using licensemanager.Models.AppModel;
using licensemanager.Models.DataBaseModel;

namespace licensemanager.Repositories.Interfaces
{
    public interface IUserGroupRepository : IRepository<UserGroup>
    {
        IEnumerable<UserGroupModel> GetUserGroupModelByIdUser(int id);
        IEnumerable<UserGroupModel> GetUserGroupModelByIdGroup(int id);
        bool Exist(int idUser, int idGroup);
        UserGroup GetByIdUserIdGroup(int idUser, int idGroup);
    }
}