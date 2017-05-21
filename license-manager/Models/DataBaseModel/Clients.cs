using System;
using System.ComponentModel.DataAnnotations;

namespace licensemanager.Model.DataBaseModel
{
    public class Clients
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? Creation { get; set; }

        public bool IsActive { get; set; }

        public DateTime? Updated { get; set; }
    }
}