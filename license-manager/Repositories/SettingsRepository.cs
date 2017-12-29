using System.Collections.Generic;
using System.Linq;
using licensemanager.Models.AppModel;
using licensemanager.Models.DataBaseModel;
using licensemanager.Repositories.Interfaces;

namespace licensemanager.Repositories
{
    public class SettingsRepository : Repository<SettingsDb>, ISettingsRepository
    {
        private readonly DataBaseContext _context;

        public SettingsRepository(DataBaseContext context) : base(context)
        {
            _context = context;
        }

    }
}
