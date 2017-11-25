using System.Collections.Generic;
using licensemanager.Models.AppModel;
using licensemanager.Models.DataBaseModel;

namespace licensemanager.Repositories.Interfaces
{
    public interface IGroupRepository : IRepository<Group>
    {
        IEnumerable<GroupModel> GetGroupsModel();
        GroupModel GetGroupModelById(int id);
        bool ExistByName(string name);
    }
}