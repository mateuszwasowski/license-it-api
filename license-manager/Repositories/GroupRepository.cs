using System.Collections.Generic;
using System.Linq;
using licensemanager.Model.DataBaseModel;
using licensemanager.Models.AppModel;
using licensemanager.Repositories.Interfaces;

namespace licensemanager.Repositories
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        private readonly DataBaseContext _context;

        public GroupRepository(DataBaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
