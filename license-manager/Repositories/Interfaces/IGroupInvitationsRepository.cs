using System.Collections.Generic;
using licensemanager.Models.AppModel;
using licensemanager.Models.DataBaseModel;

namespace licensemanager.Repositories.Interfaces
{
    public interface IGroupInvitationsRepository : IRepository<GroupInvitations>
    {
        GroupInvitations GetByToken(string token);
        bool Exist(GroupInvitationsModel dataToAdd);
    }
}