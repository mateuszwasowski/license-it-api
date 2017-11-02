using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace licensemanager.Models.AppModel
{
    [DataContract]
    public class GroupModel
    {
        [Key]
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "isActive")]
        public bool IsActive { get; set; }
        [DataMember(Name = "isDelete")]
        public bool IsDelete { get; set; }
        [DataMember(Name = "date")]
        public DateTime Date { get; set; }
    }
}
