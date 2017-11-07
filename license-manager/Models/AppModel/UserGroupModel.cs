using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace licensemanager.Models.AppModel
{
    [DataContract]
    public class UserGroupModel
    {
        [Key]
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "idUser")]
        public int IdUser { get; set; }

        [DataMember(Name = "idGroup")]
        public int IdGroup { get; set; }
        
        [DataMember(Name = "groupName")]
        public string GroupName { get; set; }
        
        [DataMember(Name = "groupLogoUrl")]
        public string GroupLogoUrl { get; set; }
    }
}