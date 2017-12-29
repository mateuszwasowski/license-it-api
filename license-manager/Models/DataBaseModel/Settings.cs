using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace licensemanager.Models.DataBaseModel
{
    public class SettingsDb
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string EmailHost { get; set; }
        public int EmailPort { get; set; }
        public string EmailUsername { get; set; }
        public string EmailPassword { get; set; }
        public string Email { get; set; }
        public string EmailFromName { get; set; }
        public string EmailSubjectGroupInvitation { get; set; }
        public string EmailBodyGroupInvitation { get; set; }
        public DateTime Date { get; set; }

    }
}
