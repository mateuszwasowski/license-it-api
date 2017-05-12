using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace licensemanager.Models.AppModel
{
    [DataContract]
    public class UserModel
    {
        [Key]
        [DataMember(Name = "id")]
        public int Id { get; set; }
        
        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }
        
        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        [DataMember(Name = "isActive")]
        public bool IsActive { get; set; }

        [DataMember(Name = "isDelete")]
        public bool IsDelete { get; set; }
    }
}
