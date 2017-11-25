using System.Collections.Generic;
using System.Linq;
using licensemanager.Models.AppModel;
using licensemanager.Models.DataBaseModel;
using licensemanager.Repositories.Interfaces;

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
                Description = x.Description,
                IdGroup = x.IdGroup,
                Hash = x.Hash
            });
        }

        public IEnumerable<ApplicationModel> GetApplicationModel(int idGroup)
        {
            return _context.Application.Where(x=>x.IdGroup==idGroup).Select(x => new ApplicationModel()
            {
                Name = x.Name,
                IsActive = x.IsActive,
                Id = x.Id,
                Version = x.Version,
                Creation = x.Creation,
                Description = x.Description,
                IdGroup = x.IdGroup,
                Hash = x.Hash
            });
        }

        public bool CheckExitsForGroup(Application model)
        {
            if (model == null)
                return false;

            return _context.Application.FirstOrDefault(x => x.Name.Equals(model.Name) && x.IdGroup.Equals(model.IdGroup)) != null;
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
                Description = app.Description,
                IdGroup = app.IdGroup,
                Hash = app.Hash
            };
        }

    }
}
