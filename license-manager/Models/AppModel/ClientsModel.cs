using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace licensemanager.Models.AppModel
{
    public class ClientsModel
    {
        [Key]
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
        
        [DataMember(Name = "isActive")]
        public bool IsActive { get; set; }
        
        [DataMember(Name = "creation")]
        public DateTime? Creation { get; set; }

        [DataMember(Name = "updated")]
        public DateTime? Updated { get; set; }
        
    }
}
