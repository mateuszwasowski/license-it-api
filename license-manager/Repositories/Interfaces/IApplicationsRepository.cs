using System.Collections.Generic;
using licensemanager.Models.AppModel;
using licensemanager.Models.DataBaseModel;

namespace licensemanager.Repositories.Interfaces
{
    public interface IApplicationsRepository: IRepository<Application>
    {
        IEnumerable<ApplicationModel> GetApplicationModel();
        ApplicationModel GetApplicationModelById(int id);
        IEnumerable<ApplicationModel> GetApplicationModel(int idGroup);
        bool CheckExitsForGroup(Application model);
    }
}