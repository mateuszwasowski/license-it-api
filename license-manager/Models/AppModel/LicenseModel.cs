using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace licensemanager.Models.AppModel
{
    public class LicenseModel
    {
        [Key]
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "idApplication")]
        public int IdApplication { get; set; }

        [DataMember(Name = "idClient")]
        public int IdClient { get; set; }

        [DataMember(Name = "clientName")]
        public string ClientName { get; set; }

        [DataMember(Name = "number")]
        public string Number { get; set; }

        [DataMember(Name = "assignedVersion")]
        public decimal AssignedVersion { get; set; }

        [DataMember(Name = "isActive")]
        public bool IsActive { get; set; }

        [DataMember(Name = "isActivated")]
        public bool IsActivated { get; set; }

        [DataMember(Name = "identityNumber")]
        public string IdentityNumber { get; set; }

        [DataMember(Name = "creation")]
        public System.DateTime Creation { get; set; }

        [DataMember(Name = "inclusion")]
        public DateTime? Inclusion { get; set; }

        [DataMember(Name = "expiration")]
        public DateTime? Expiration { get; set; }

        [DataMember(Name = "permissionsModel")]
        public virtual IEnumerable<PermissionsModel> PermissionsModel { get; set; }

        [DataMember(Name = "applicationModel")]
        public virtual ApplicationModel ApplicationModel { get; set; }
    }

}
