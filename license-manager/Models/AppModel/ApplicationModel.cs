using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace licensemanager.Models.AppModel
{
    public class ApplicationModel
    {
        [Key]
        [DataMember(Name = "id")]
        public int Id { get; set; }
        
        [DataMember(Name = "idGroup")]
        public int IdGroup { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "version")]
        public decimal Version { get; set; }

        [DataMember(Name = "isActive")]
        public bool IsActive { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "creation")]
        public DateTime Creation { get; set; }

    }
}
