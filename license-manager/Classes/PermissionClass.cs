using System.Collections.Generic;
using System.Linq;
using licensemanager.Models.AppModel;
using licensemanager.Models.DataBaseModel;

namespace licensemanager.Classes
{
    public class PermissionClass
    {
        public static IEnumerable<Permissions> ConvertPermission(IEnumerable<PermissionsModel> data)
        {
            return data?.Select(x=>new Permissions
            {
                IsActive = x.IsActive,
                Id = x.Id,
                Name = x.Name,
                IdLicense = x.IdLicense
            });
        }

        public static IEnumerable<PermissionsModel> ConvertPermission(IEnumerable<Permissions> data)
        {
            return data?.Select(x => new PermissionsModel
            {
                IsActive = x.IsActive,
                Id = x.Id,
                Name = x.Name,
                IdLicense = x.IdLicense
            });
        }
    }
}
