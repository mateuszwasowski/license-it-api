using System.Collections.Generic;
using System.Linq;
using licensemanager.Model.DataBaseModel;
using licensemanager.Models.AppModel;
using licensemanager.Repositories;

namespace licensemanager.Classes
{
    public class ApplicationClass
    {
        public static bool CheckExist(int id)
        {
            IApplicationsRepository appRepo = new ApplicationsRepository(new DataBaseContext());

            return appRepo.GetById(id) != null;
        }

        public static IEnumerable<Application> ConvertPermission(IEnumerable<ApplicationModel> data)
        {
            return data?.Select(x => new Application
            {
                IsActive = x.IsActive,
                Description = x.Description,
                Creation = x.Creation,
                Id = x.Id,
                Name = x.Name,
                Version = x.Version
            });
        }

        public static IEnumerable<ApplicationModel> ConvertPermission(IEnumerable<Application> data)
        {
            return data?.Select(x => new ApplicationModel
            {
                IsActive = x.IsActive,
                Description = x.Description,
                Creation = x.Creation,
                Id = x.Id,
                Name = x.Name,
                Version = x.Version
            });
        }

        public static ApplicationModel ConvertPermission(Application data)
        {
            if (data == null)
                return null;

            return   new ApplicationModel
            {
                IsActive = data.IsActive,
                Description = data.Description,
                Creation = data.Creation,
                Id = data.Id,
                Name = data.Name,
                Version = data.Version
            };
        }
    }
}
