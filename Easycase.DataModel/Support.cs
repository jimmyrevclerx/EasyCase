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
    
    public partial class Support
    {
        public long ID { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<long> ImageId { get; set; }
        public string Email { get; set; }
        public string Greeting { get; set; }
        public string PhoneNo { get; set; }
        public string FacebookUrl { get; set; }
        public string Linkdin { get; set; }
        public string WebsiteUrl { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        public virtual Image Image { get; set; }
    }
}
