//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Easycase.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            this.Assigneds = new HashSet<Assigned>();
            this.Cases = new HashSet<Case>();
        }
    
        public long ID { get; set; }
        public string CreatedBy { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public Nullable<int> Prefix { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string Country { get; set; }
        public Nullable<long> PurposeId { get; set; }
        public string Citizenship { get; set; }
        public Nullable<long> ImageId { get; set; }
        public string Notes { get; set; }
        public Nullable<long> MartialStatusId { get; set; }
        public Nullable<bool> Deleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Assigned> Assigneds { get; set; }
        public virtual Image Image { get; set; }
        public virtual MartialStatu MartialStatu { get; set; }
        public virtual Purpose Purpose { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Case> Cases { get; set; }
    }
}
