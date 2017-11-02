using System.Collections.Generic;
using System.Linq;
using licensemanager.Model.DataBaseModel;
using licensemanager.Models.AppModel;
using licensemanager.Repositories.Interfaces;

namespace licensemanager.Repositories
{
    public class UserGroupRepository : Repository<UserGroup>, IUserGroupRepository
    {
        private readonly DataBaseContext _context;

        public UserGroupRepository(DataBaseContext context) : base(context)
        {
            _context = context;
        }

    }
}
