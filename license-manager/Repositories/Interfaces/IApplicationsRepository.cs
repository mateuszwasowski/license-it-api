using System.Collections.Generic;
using licensemanager.Model.DataBaseModel;
using licensemanager.Models.AppModel;
using licensemanager.Repositories.Interfaces;

namespace licensemanager.Repositories
{
    public interface IApplicationsRepository: IRepository<Application>
    {
        IEnumerable<ApplicationModel> GetApplicationModel();
        ApplicationModel GetApplicationModelById(int id);
        IEnumerable<ApplicationModel> GetApplicationModel(int idGroup);
    }
}