using System;
using System.ComponentModel.DataAnnotations;

namespace licensemanager.Model.DataBaseModel
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }

        public DateTime Date { get; set; }

    }
}
