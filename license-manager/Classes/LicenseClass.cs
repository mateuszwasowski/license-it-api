using System;
using System.Linq;
using System.Text;
using licensemanager.Models.AppModel;
using licensemanager.Models.DataBaseModel;
using licensemanager.Repositories;
using licensemanager.Repositories.Interfaces;

namespace licensemanager.Classes
{
    public class LicenseClass
    {

        public ILicenseRepository LicenseRepository = new LicenseRepository(new DataBaseContext());
        public IPermissionsRepository PermissionsRepository = new PermissionsRepository(new DataBaseContext());

        public string GetNewLicenseString()
        {
            var stringVal = RandomString(24);

            while (CheckExistLicenseByNumber(stringVal))
            {
                stringVal = RandomString(24);
            }

            return stringVal;
        }

        private string RandomString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ123456789";
            var stringVal = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            var builder = new StringBuilder();
            int count = 0;
            foreach (var currentChar in stringVal)
            {
                builder.Append(currentChar);
                if ((++count % 4) == 0)
                {
                    builder.Append('-');
                }
            }
            var number = builder.ToString().Substring(0, builder.Length - 1);

            return number;
        }

        public static string RandomStringNoDash(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijklmnpqrstuwxyz123456789!@#$%^&*()_+?><|}{-";
            var stringVal = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            var builder = new StringBuilder();
            foreach (var currentChar in stringVal)
            {
                builder.Append(currentChar);
            }
            var number = builder.ToString().Substring(0, builder.Length - 1);

            return number;
        }

        public bool InsertPermissions(Licenses license)
        {
            foreach (var row in license.Permissions)
            {
                row.IdLicense = license.Id;
                PermissionsRepository.Insert(row);
            }

            return true;
        }

        public Exception ValidateLicenseAdd(LicenseModel dataToAdd)
        {
            if(dataToAdd.IdClient <=0)
                return new Exception("Id Client is required");

            if (dataToAdd.IdApplication <= 0)
                return new Exception("Id Application is required");

            return null;
        }

        private bool CheckExistLicenseByNumber(string number)
        {
            return LicenseRepository.CheckExistNumber(number);
        }
    }
}
