using System.Collections.Generic;
using licensemanager.Model.DataBaseModel;
using licensemanager.Models.AppModel;

namespace licensemanager.Repositories.Interfaces
{
    public interface IGroupRepository : IRepository<Group>
    {
        IEnumerable<GroupModel> GetGroupsModel();
        GroupModel GetGroupModelById(int id);
        bool ExistByName(string name);
    }
}