using System;
using System.Linq;
using System.Text;
using licensemanager.Model.DataBaseModel;
using licensemanager.Models.AppModel;
using licensemanager.Repositories;

namespace licensemanager.Classes
{
    public static class LicenseClass
    {

        public static string GetNewLicenseString()
        {
            var stringVal = RandomString(24);

            while (CheckExistLicenseByNumber(stringVal))
            {
                stringVal = RandomString(24);
            }

            return stringVal;
        }


        private static string RandomString(int length)
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

        public static bool InsertPermissions(Licenses license)
        {
            IPermissionsRepository permRepo = new PermissionsRepository(new DataBaseContext());
            foreach (var row in license.Permissions)
            {
                row.IdLicense = license.Id;
                permRepo.Insert(row);
            }

            return true;
        }

        public static Exception ValidateLicenseAdd(LicenseModel dataToAdd)
        {
            if(dataToAdd.IdClient <=0)
                return new Exception("Id Client is required");

            if (dataToAdd.IdApplication <= 0)
                return new Exception("Id Application is required");

            return null;
        }

        private static bool CheckExistLicenseByNumber(string number)
        {
            ILicenseRepository repo = new LicenseRepository(new DataBaseContext());

            return repo.CheckExistNumber(number);
        }
    }
}
