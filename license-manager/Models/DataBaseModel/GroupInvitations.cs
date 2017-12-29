using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace licensemanager.Models.DataBaseModel
{
    public class GroupInvitations
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int IdUserInviting { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public DateTime Date { get; set; }
    }
}
