using System.Collections.Generic;
using System.Linq;
using licensemanager.Models.AppModel;
using licensemanager.Models.DataBaseModel;
using licensemanager.Repositories.Interfaces;

namespace licensemanager.Repositories
{
    public class GroupInvitationsRepository : Repository<GroupInvitations>, IGroupInvitationsRepository
    {
        private readonly DataBaseContext _context;

        public GroupInvitationsRepository(DataBaseContext context) : base(context)
        {
            _context = context;
        }

        public bool Exist(GroupInvitationsModel dataToAdd)
        {
             return _context.GroupInvitations.FirstOrDefault(x=>x.GroupId == dataToAdd.GroupId && x.Email == dataToAdd.Email) != null;
        }

        public GroupInvitations GetByToken(string token)
        {
           return _context.GroupInvitations.FirstOrDefault(x=>x.Token.Equals(token));
        }
    }
}
