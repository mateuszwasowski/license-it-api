using licensemanager.Model.DataBaseModel;

namespace licensemanager.Repositories
{
    public class PermissionsRepository : Repository<Permissions>, IPermissionsRepository
    {
        private readonly DataBaseContext _context;

        public PermissionsRepository(DataBaseContext context) : base(context)
        {
            _context = context;
        }

    }
}
