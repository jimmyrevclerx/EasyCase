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
    
    public partial class Contact
    {
        public long ID { get; set; }
        public Nullable<long> ContactTypeId { get; set; }
        public string MobileNo { get; set; }
        public string WorkNo { get; set; }
        public string Email { get; set; }
        public string Prefix { get; set; }
        public string OtherNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FaxNo { get; set; }
        public string MailingAddress { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<bool> Deleted { get; set; }
    
        public virtual ContactType ContactType { get; set; }
    }
}
