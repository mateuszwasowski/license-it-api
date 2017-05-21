using System.Collections.Generic;
using System.Linq;
using licensemanager.Model.DataBaseModel;
using licensemanager.Models.AppModel;

namespace licensemanager.Repositories
{
    public class ApplicationsRepository : Repository<Application>, IApplicationsRepository
    {
        private readonly DataBaseContext _context;

        public ApplicationsRepository(DataBaseContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationModel> GetApplicationModel()
        {
            return _context.Application.Select(x => new ApplicationModel()
            {
                Name = x.Name,
                IsActive = x.IsActive,
                Id = x.Id,
                Version = x.Version,
                Creation = x.Creation,
                Description = x.Description
            });
        }

        public ApplicationModel GetApplicationModelById(int id)
        {
            var app = _context.Application.FirstOrDefault(x => x.Id == id);

            if (app == null)
                return null;

            return new ApplicationModel
            {
                Name = app.Name,
                IsActive = app.IsActive,
                Id = app.Id,
                Version = app.Version,
                Creation = app.Creation,
                Description = app.Description
            };
        }

    }
}
