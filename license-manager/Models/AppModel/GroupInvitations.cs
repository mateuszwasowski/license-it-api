using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace licensemanager.Models.AppModel
{
    [DataContract]
    public class GroupInvitationsModel
    {
        [DataMember(Name = "idUserInviting")]
        public int IdUserInviting { get; set; }
        
        [DataMember(Name = "name")]
        public string Name { get; set; }
        
        [DataMember(Name = "email")]
        public string Email { get; set; }
        
        [DataMember(Name = "token")]
        public string Token { get; set; }
        
        [DataMember(Name = "groupId")]
        public int GroupId { get; set; }
        
        [DataMember(Name = "date")]
        public DateTime Date { get; set; }
    }
}
