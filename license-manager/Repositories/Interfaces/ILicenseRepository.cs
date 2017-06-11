using System.Collections.Generic;
using licensemanager.Model.DataBaseModel;
using licensemanager.Models.AppModel;
using licensemanager.Repositories.Interfaces;

namespace licensemanager.Repositories
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