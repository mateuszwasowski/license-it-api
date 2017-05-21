using System.ComponentModel.DataAnnotations;

namespace licensemanager.Model.DataBaseModel
{
    public class Application
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Version { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public System.DateTime Creation { get; set; }
    }
}