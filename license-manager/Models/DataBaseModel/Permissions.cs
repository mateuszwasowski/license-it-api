using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace licensemanager.Model.DataBaseModel
{
    public class Permissions
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("License")]
        public int IdLicense { get; set; } //Foreign key for Licenses

        public string Name { get; set; }
        public bool IsActive { get; set; }

        public virtual Licenses License { get; set; }
    }
}