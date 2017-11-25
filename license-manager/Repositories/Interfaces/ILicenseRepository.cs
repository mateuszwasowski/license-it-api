using System.Collections.Generic;
using licensemanager.Models.AppModel;
using licensemanager.Models.DataBaseModel;

namespace licensemanager.Repositories.Interfaces
{
    public interface ILicenseRepository : IRepository<Licenses>
    {
        IEnumerable<LicenseModel> GetLicenseModel();
        bool CheckExistNumber(string stringVal);
        IEnumerable<LicenseModel> GetLicensesModelByApplication(int idApp);
        LicenseModel GetById(int id);
        bool EditLicense(int id, LicenseModel licenseModel);
    }
}