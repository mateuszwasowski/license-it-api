using System.Collections.Generic;
using licensemanager.Model.DataBaseModel;
using licensemanager.Models.AppModel;
using licensemanager.Repositories.Interfaces;

namespace licensemanager.Repositories
{
    public interface IClientsRepository : IRepository<Clients>
    {
        IEnumerable<ClientsModel> GetClientsModel();
        ClientsModel GetClientsModelById(int id);
    }
}