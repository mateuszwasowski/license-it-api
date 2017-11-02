using System;
using System.Linq;
using licensemanager.Classes;
using licensemanager.Model.DataBaseModel;

namespace licensemanager.Settings
{
    public static class DbInitializer
    {
        public static void Initialize(DataBaseContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            UsersRecords(context);
            UserGroupsRecords(context);
            ApplicationsRecords(context);
            ClientsRecords(context);
            LicensesRecords(context);
            PermissionsRecords(context);

        }
        
        private static void UsersRecords(DataBaseContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            var users = new User
            {
                Id = 1,
                Email = "test@licenseit.pro",
                FirstName = "test",
                LastName = "test",
                Password = CryptoClass.CreateHash("test"),
                Date = DateTime.Now,
                IsActive = true,
                IsDelete = false,
            };

            context.Users.Add(users);

            context.SaveChanges();
        }

         private static void UserGroupsRecords(DataBaseContext context)
        {
            if (context.UserGroup.Any())
            {
                return;
            }

            var userGroup = new UserGroup
            {
                Id = 1,
                IdUser = 1,
                IdGroup = 1
            };

            context.UserGroup.Add(userGroup);

            context.SaveChanges();
        }

         private static void GroupsRecords(DataBaseContext context)
        {
            if (context.Group.Any())
            {
                return;
            }

            var group = new Group
            {
                Id = 1,
                Name = "Test",
                Description = "Test description",
                Date = DateTime.Now,
                IsActive = true,
                IsDelete = false,
            };

            context.Group.Add(group);

            context.SaveChanges();
        }

        private static void LicensesRecords(DataBaseContext context)
        {
            if (context.Licenses.Any())
            {
                return;
            }

            var license = new Licenses
            {
                Id = 1,
                IsActive = true,
                AssignedVersion = 1,
                IdClients = 1,
                Creation = DateTime.Now,
                Expiration = DateTime.MaxValue,
                Inclusion = DateTime.Now,
                IsActivated = false,
                Number = "1111-1111-1111-1111",
                IdentityNumber = null,
                IdApplication = 1,
                
            };

            context.Licenses.Add(license);

            context.SaveChanges();
        }
        private static void ApplicationsRecords(DataBaseContext context)
        {
            if (context.Application.Any())
            {
                return;
            }

            var app = new Application
            {
                IsActive = true,
                Creation = DateTime.Now,
                Description = "opis",
                Name = "testowa apka",
                Version = (decimal)1.0

            };

            context.Application.Add(app);

            context.SaveChanges();
        }

        private static void PermissionsRecords(DataBaseContext context)
        {
            if (context.Permissions.Any())
            {
                return;
            }

            var permission = new Permissions
            {
               IdLicense = 1,
               Name = "DEMO",
               IsActive = true
            };

            context.Permissions.Add(permission);

            context.SaveChanges();
        }

        private static void ClientsRecords(DataBaseContext context)
        {
            if (context.Clients.Any())
            {
                return;
            }

            var clients = new Clients
            {
               Creation = DateTime.Now,
               IsActive = true,
               Updated = DateTime.Now,
               Name = "UAM"
            };

            context.Clients.Add(clients);

            context.SaveChanges();
        }
    }
}