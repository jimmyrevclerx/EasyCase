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
    
    public partial class CasesNote
    {
        public long ID { get; set; }
        public Nullable<long> CorporateProfileId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Subject { get; set; }
        public string Notes { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    
        public virtual CorporateProfileInformation CorporateProfileInformation { get; set; }
    }
}
