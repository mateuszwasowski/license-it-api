using System.Collections.Generic;
using licensemanager.Models.AppModel;
using licensemanager.Models.DataBaseModel;

namespace licensemanager.Repositories.Interfaces
{
    public interface IClientsRepository : IRepository<Clients>
    {
        IEnumerable<ClientsModel> GetClientsModel();
        ClientsModel GetClientsModelById(int id);
        bool ExistByName(int idGroup, string name);
        IEnumerable<ClientsModel> GetClientsModel(int idGroup);
    }
}