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
        bool ExistByName(int idGroup, string name);
        IEnumerable<ClientsModel> GetClientsModel(int idGroup);
    }
}