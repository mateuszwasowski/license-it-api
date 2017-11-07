using System.Collections.Generic;
using System.Linq;
using licensemanager.Model.DataBaseModel;
using licensemanager.Models.AppModel;

namespace licensemanager.Repositories
{
    public class ClientsRepository : Repository<Clients>, IClientsRepository
    {
        private readonly DataBaseContext _context;

        public ClientsRepository(DataBaseContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<ClientsModel> GetClientsModel()
        {
            return _context.Clients.Select(x => new ClientsModel()
            {
                Name = x.Name,
                IsActive = x.IsActive,
                Id = x.Id,
                Creation = x.Creation,
                Updated = x.Updated,
                IdGroup = x.IdGroup
            });
        }

        public ClientsModel GetClientsModelById(int id)
        {
            var app = _context.Clients.FirstOrDefault(x => x.Id == id);

            if (app == null)
                return null;

            return new ClientsModel
            {
                Name = app.Name,
                IsActive = app.IsActive,
                Id = app.Id,
                Creation = app.Creation,
                Updated = app.Updated,
                IdGroup = app.IdGroup
            };
        }

        public bool ExistByName(int idGroup, string name)
        {
            return _context.Clients.Where(x=>x.IdGroup == idGroup).FirstOrDefault(x => x.Name.Equals(name)) != null;
        }

        public IEnumerable<ClientsModel> GetClientsModel(int idGroup)
        {
              return _context.Clients.Where(x=>x.IdGroup==idGroup).Select(x => new ClientsModel()
            {
                Name = x.Name,
                IsActive = x.IsActive,
                Id = x.Id,
                Creation = x.Creation,
                Updated = x.Updated,
                IdGroup = x.IdGroup
            });
        }
    }
}
