using System;
using System.Linq;
using licensemanager.Classes;
using licensemanager.Models.DataBaseModel;

namespace licensemanager.Settings
{
    public static class DbInitializer
    {
        public static void Initialize(DataBaseContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
         
            UsersRecords(context);
            
        }

        private static void UsersRecords(DataBaseContext context)
        {
            // Look for any students.
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

       
    }
}