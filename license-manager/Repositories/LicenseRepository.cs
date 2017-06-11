using System.Collections.Generic;
using System.Linq;
using licensemanager.Classes;
using licensemanager.Model.DataBaseModel;
using licensemanager.Models.AppModel;

namespace licensemanager.Repositories
{
   
    public class LicenseRepository : Repository<Licenses>, ILicenseRepository
    {
        private readonly DataBaseContext _context;

        public LicenseRepository(DataBaseContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<LicenseModel> GetLicenseModel()
        {

            return _context.Licenses.Select(x => new LicenseModel()
            {
               Id = x.Id,
               IdClient = x.IdClients,
               ClientName = ClientClass.GetClientName(x.IdClients),
               IsActive = x.IsActive,
               Creation = x.Creation,
               IdApplication = x.IdApplication,
               PermissionsModel = PermissionClass.ConvertPermission(x.Permissions),
               Number = x.Number,
               AssignedVersion = x.AssignedVersion,
               Inclusion = x.Inclusion,
               IsActivated = x.IsActivated,
               IdentityNumber = x.IdentityNumber,
               Expiration = x.Expiration,
               ApplicationModel = ApplicationClass.ConvertPermission(x.Application)
                
            });
        }

        public bool CheckExistNumber(string number)
        {
            return _context.Licenses.FirstOrDefault(x => x.Number == number) != null;
        }

        public IEnumerable<LicenseModel> GetLicensesModelByApplication(int idApp)
        {
            return _context.Licenses.Where(x=>x.IdApplication== idApp).Select(x => new LicenseModel()
            {
                Id = x.Id,
                IdClient = x.IdClients,
                ClientName = ClientClass.GetClientName(x.IdClients),
                IsActive = x.IsActive,
                Creation = x.Creation,
                IdApplication = x.IdApplication,
                PermissionsModel = PermissionClass.ConvertPermission(x.Permissions),
                Number = x.Number,
                AssignedVersion = x.AssignedVersion,
                Inclusion = x.Inclusion,
                IsActivated = x.IsActivated,
                IdentityNumber = x.IdentityNumber,
                Expiration = x.Expiration,
                ApplicationModel = ApplicationClass.ConvertPermission(x.Application)

            });
        }

        public LicenseModel GetById(int id)
        {
            return _context.Licenses.Where(x => x.Id == id).Select(x => new LicenseModel
            {
                Id = x.Id,
                IdClient = x.IdClients,
                ClientName = ClientClass.GetClientName(x.IdClients),
                IsActive = x.IsActive,
                Creation = x.Creation,
                IdApplication = x.IdApplication,
                PermissionsModel = PermissionClass.ConvertPermission(x.Permissions),
                Number = x.Number,
                AssignedVersion = x.AssignedVersion,
                Inclusion = x.Inclusion,
                IsActivated = x.IsActivated,
                IdentityNumber = x.IdentityNumber,
                Expiration = x.Expiration,
                ApplicationModel = ApplicationClass.ConvertPermission(x.Application)

            }).FirstOrDefault();
        }

        public bool EditLicense(int id, LicenseModel licenseModel)
        {

            var license = _context.Licenses.FirstOrDefault(x => x.Id == id);
            if (license == null)
                return false;

            if(licenseModel.AssignedVersion>0)
                license.AssignedVersion = licenseModel.AssignedVersion;

            if (licenseModel.Expiration != null)
                license.Expiration = licenseModel.Expiration;

            if (licenseModel.IdApplication > 0)
                license.IdApplication = licenseModel.IdApplication;

            if (licenseModel.IdClient > 0)
                license.IdClients = licenseModel.IdClient;

            if (licenseModel.Inclusion != null)
                license.Inclusion = licenseModel.Inclusion;

            if (!string.IsNullOrEmpty(licenseModel.IdentityNumber))
                license.IdentityNumber = licenseModel.IdentityNumber;

            license.IsActivated = licenseModel.IsActivated;
            license.IsActive = licenseModel.IsActive;

            Update(license);

            return true;
        }
    }
}
